import numpy as np
from math import factorial as _fact

def factorial(x):
    if x in factorial._cache:
        return factorial._cache[x]

    f = _fact(x)
    factorial._cache[x] = f

    return f

factorial._cache = {}

def nCk(n, k):
    ''' Binomial Distribution '''
    if (n, k) in nCk._cache:
        # Return cached value instead of recomputing
        return nCk._cache[(n,k)]

    product = 1
    for i in range(n, n-k, -1):
        product *= i
    for i in range(k):
        product /= i+1

    nCk._cache[(n,k)] = product
    return product

nCk._cache = {}

def Jacobian(n, p, X):
    arr = np.ndarray(shape=[p, 3*n])

    for L in range(1, p + 1):
        for i in range(1, n + 1):
            col_1 = 0     # Column i
            col_2 = 0     # Column N + i
            col_3 = 0     # Column 2N + i

            sign = 1

            Ui = X[i-1, 0]
            Vi = X[n + (i-1), 0]
            Si = X[2*n + (i-1), 0]
            
            for k in range(0, L + 1):
                const = sign * nCk(2*(k+L+1), 2*L+1) * (2*k+1) / (factorial(k+L+1)*factorial(L-k))
                
                col_1 += const * pow(Vi, L) * pow(Si, 2*k)
                col_2 += const * L * Ui * pow(Vi, L-1) * pow(Si, 2*k)
                col_3 += const * Ui * pow(Vi, L) * (2*k) * pow(Si, 2*k-1)


                sign = -sign
                
            arr[L-1, i-1] = col_1
            arr[L-1, n + (i-1)] = col_2
            arr[L-1, 2*n + (i-1)] = col_3

    return np.matrix(arr, copy=False)

def F(n, p, X):
    arr = np.ndarray(shape=[p, 1])

    for L in range(1, p + 1):
        coeff = pow(factorial(L), -2)
        for i in range(1, n + 1):
            sign = 1
            Ui = X[i-1, 0]
            Vi = X[n + (i-1), 0]
            Si = X[2*n + (i-1), 0]
            
            for k in range(0, L + 1):
                const = sign * nCk(2*(k+L+1), 2*L+1) * (2*k+1) / (factorial(k+L+1)*factorial(L-k))
                coeff += const * Ui * pow(Vi, L) * pow(Si, 2*k)
                sign = -sign
                
        arr[L-1, 0] = coeff
        
    return np.matrix(arr, copy=False)




###############################
#   Newton-Raphson Iteration  #
###############################


'''
# Commented out test Jacobian and Function vector for the following non-linear system of equations

# x^2 - y^2 = 32
#   x + y   =  8

# x = 6, y = 2
# Newton Raphson method gives this value just fine.

def Jacobian(N, X):
    arr = np.ndarray(shape=[2,2])
    arr[0,0] = 2 * X[0]
    arr[0,1] = -2 * X[1]
    arr[1,0] = 1
    arr[1,1] = 1

    return np.matrix(arr, copy=False)

def F(n, X):
    arr = np.ndarray(shape=[2,1])
    arr[0,0] = X[0,0]**2 - X[1,0]**2 - 32
    arr[1,0] = X[0,0] + X[1,0] - 8

    return np.matrix(arr, copy=False)
'''

N = 5
P = 14
x = np.ndarray(shape=[3*N, 1])

# Initialize trial solution
for i in range(0, 3*N):
    x[i,0] = (2*i+1)


X = np.matrix(x)

iterations = 1000

print("N =",N)
print("1st N values of the solution corresponds to U variable")
print("2nd N values of the solution corresponds to V variable")
print("3rd N values of the solution corresponds to S variable")
print()
print("Trial solution")
print(X)

print("Initial value of the co-efficients")
print(F(N,P, X))

print()
print()

# This is the iteration loop
for i in range(iterations):
    try:
        X1 = X - Jacobian(N, P, X).I * F(N, P, X)    
        if np.isnan((X.T * X - X1.T * X1).sum()):
            break
    
        X = X1
    except:
        pass
else:
    i += 1

# Print the obtained solution
print("After", i, "iterations, final solution:")
print(X)
print()
print("Plugging the obtained solution, values of the co-efficients")
print(F(N, P, X))
