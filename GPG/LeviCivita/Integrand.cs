using System;

namespace LeviCivita
{
    class Integrand
    {
        public static double Denom(double r, double Delta, double A, double B, double alpha, double sigma)
	    {
		    return Math.Sqrt((Delta * Delta * Math.Pow(r, 8 * sigma * sigma - 8 * sigma) / (alpha * alpha)) - (A * A * Math.Pow(r, 8 * sigma * sigma - 2)) - (B * B) - (Math.Pow(r, 8 * sigma * sigma - 4 * sigma) / alpha));
	    }
        
        public static double IntT(double r, double Delta, double A, double B, double alpha, double sigma)
        {
            return (1 / (alpha * alpha)) * Delta * Math.Pow(r, 8 * sigma * sigma - 8 * sigma) / Integrand.Denom(r, Delta, A, B, alpha, sigma);
        }

        public static double IntPhi(double r, double Delta, double A, double B, double alpha, double sigma)
        {
            return A * Math.Pow(r, 8 * sigma * sigma - 2) / Integrand.Denom(r, Delta, A, B, alpha, sigma);
        }

        public static double IntZ(double r, double Delta, double A, double B, double alpha, double sigma)
        {
            return B / Integrand.Denom(r, Delta, A, B, alpha, sigma);
        }
    }
}
