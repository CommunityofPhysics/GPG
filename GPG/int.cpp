#include<iostream>
#include<fstream>
#include<cmath>
using namespace std;

double f(double x)
{
	return pow(x, 3);
}

double integral(double a, double b, double c, int n)
{
	double u = a;
	double z = b;
	double h = 1.0 / n;

	ofstream fout;
	fout.open("int.txt");

	for (int k = 0; k <= n * c; k++)
	{
		cout << u << "\t" << z << endl;
		fout << u << "\t" << z << endl;
		z = z + (h / 6) * (f(u) + 4 * f(u + (h / 2)) + f(u + h));
		u = u + h;
	}

	fout.close();
	return 0;
}

int main()
{
	double a, b, c;
	int n;

	cout << "Lower limit: ";
	cin >> a;
	cout << endl;

	cout << "Integral at lower limit: ";
	cin >> b;
	cout << endl;

	cout << "Number of units to integrate: ";
	cin >> c;
	cout << endl;

	cout << "Number of intervals per unity: ";
	cin >> n;
	cout << endl;

	integral(a, b, c, n);
	return 0;
}
