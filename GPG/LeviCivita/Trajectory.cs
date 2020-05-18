using System;
using System.IO;

namespace LeviCivita
{
    class Trajectory
    {
        static double Integral(double R, double Gamma, double A, double B, double TWall, double alpha, double sigma, int n)
        {
            double t = 0;
            double r = R;
            double phi = 0;
            double z = 0;

            double h = 1.0 / Convert.ToDouble(n);
            double Delta = Math.Sqrt((alpha * Math.Pow(R, 4 * sigma)) + (alpha * alpha * A * A * Math.Pow(R, 8 * sigma - 2)) + ((alpha * alpha) * Math.Pow(R, 8 * sigma - 8 * sigma * sigma) * (Gamma * Gamma + B * B)));

            string Path = "Integral_a=" + Convert.ToString(alpha) + "_sigma=" + Convert.ToString(sigma) + "_R=" + Convert.ToString(r) + "_Gamma=" + Convert.ToString(Gamma) + "_Alpha=" + Convert.ToString(A) + "_Beta=" + Convert.ToString(B) + ".txt";
            StreamWriter File = new StreamWriter(Path);

            for (int k = 0; t <= TWall && r > 10 * Math.Abs(h); k++)
            {
                if (double.IsNaN(Integrand.Denom(r + 1.001 * h, Delta, A, B, alpha, sigma)))
                {
                    h = -h;
                }
                else 
                {
                    Console.WriteLine("{0:000.0000}\t\t{1:000.0000}\t\t{2:000.0000}\t\t{3:000.0000}", t, r, phi, z);
                    File.WriteLine("{0:000.0000}\t\t\t{1:000.0000}\t\t\t{2:000.0000}\t\t\t{3:000.0000}", t, r, phi, z);
                    t = t + (Math.Abs(h) / 6) * (Integrand.IntT(r, Delta, A, B, alpha, sigma) + 4 * Integrand.IntT(r + (h / 2), Delta, A, B, alpha, sigma) + Integrand.IntT(r + h, Delta, A, B, alpha, sigma));
                    phi = phi + (Math.Abs(h) / 6) * (Integrand.IntPhi(r, Delta, A, B, alpha, sigma) + 4 * Integrand.IntPhi(r + (h / 2), Delta, A, B, alpha, sigma) + Integrand.IntPhi(r + h, Delta, A, B, alpha, sigma));
                    z = z + (Math.Abs(h) / 6) * (Integrand.IntZ(r, Delta, A, B, alpha, sigma) + 4 * Integrand.IntZ(r + (h / 2), Delta, A, B, alpha, sigma) + Integrand.IntZ(r + h, Delta, A, B, alpha, sigma));
                    r = r + h;
                }

            }

            File.Close();
            return 0;
        }

        static void Main(string[] args)
        {
            double R, Gamma, A, B, TWall, alpha, sigma;
            int n;

            alpha = 1.0;

            Console.WriteLine("Energy density per unit length: ");
            sigma = double.Parse(Console.ReadLine());

            Console.WriteLine("Initial radial coodinate: ");
            R = double.Parse(Console.ReadLine());

            Console.WriteLine("Initial radial momentum: ");
            Gamma = double.Parse(Console.ReadLine());

            Console.WriteLine("Initial azimuthal momentum: ");
            A = double.Parse(Console.ReadLine());

            Console.WriteLine("Initial axial momentum: ");
            B = double.Parse(Console.ReadLine());

            Console.WriteLine("Time wall to terminate Trajectory: ");
            TWall = double.Parse(Console.ReadLine());

            Console.WriteLine("Number of intervals per unit radial coordinate: ");
            n = int.Parse(Console.ReadLine());

            Trajectory.Integral(R, Gamma, A, B, TWall, alpha, sigma, n);
            _ = Console.ReadKey();
        
        }

    }
}
/* 

            Console.WriteLine("{0:0.0000}", Delta);
            Console.WriteLine("{0:0.0000}", Integrand.Denom(r, Delta, A, B, alpha, sigma));

*/
