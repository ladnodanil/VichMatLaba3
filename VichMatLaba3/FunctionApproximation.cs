using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VichMatLaba3
{
    public class FunctionApproximation
    {
        float[] xi;
        float[] fxi;

        public FunctionApproximation(List<float> x, List<float> fx)
        {
            if (x.Count != fx.Count)
            {
                throw new Exception("dasda");
            }
            xi = new float[x.Count];
            fxi = new float[fx.Count];

            for (int i = 0; i < x.Count; i++)
            {
                xi[i] = x[i];
                fxi[i] = fx[i];
            }
            

        }
        
        private float[] GaussMethod(float[][] A, float[] B)
        {
            int n = A.Length; 
                              // Прямой ход
            for (int k = 0; k < n - 1; k++) 
            {
                

                // Поиск максимального элемента 
                int maxRow = k;
                float maxVal = Math.Abs(A[k][k]);
                for (int i = k + 1; i < n; i++)
                {
                    if (Math.Abs(A[i][k]) > maxVal)
                    {
                        maxRow = i;
                        maxVal = Math.Abs(A[i][k]);
                    }
                }

                // Обмен текущей строки с строкой с максимальным элементом
                if (maxRow != k)
                {
                    float[] tempRow = A[k];
                    A[k] = A[maxRow];
                    A[maxRow] = tempRow;

                    float tempB = B[k];
                    B[k] = B[maxRow];
                    B[maxRow] = tempB;
                }

                for (int j = k + 1; j < n; j++) 
                {
                    
                    if (Math.Abs(A[k][k]) < float.Epsilon)
                    {
                        throw new ArithmeticException("ноль");
                    }

                    float m = A[j][k] / A[k][k]; 

                    
                    for (int i = k; i < n; i++)
                    {
                        A[j][i] -= m * A[k][i];
                    }

                    B[j] -= m * B[k]; 
                }
            }

            // Обратный ход
            float[] X = new float[n];
            for (int i = n - 1; i >= 0; i--)
            {
                float sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += A[i][j] * X[j];
                }
                X[i] = (B[i] - sum) / A[i][i];
                
            }
            return X;

            
        }

        
        public float Newton(float x)
        {
            float sum = fxi[0];
            for (int i = 1; i < xi.Length; ++i) //подставляем значения х в многочлен
            {
                float F = 0;
                for (int j = 0; j <= i; ++j)
                {

                    float den = 1;
                    for (int k = 0; k <= i; ++k)
                        if (k != j)
                            den *= (xi[j] - xi[k]);
                    F += fxi[j] / den;
                }

                for (int k = 0; k < i; ++k)
                    F *= (x - xi[k]);
                sum += F;
            }
            return sum;
        }
        


        public float Lagrange(float x)
        {
            float L = 0;
            for (int i = 0; i < xi.Length; i++)
            {
                float P = 1;
                for (int j = 0; j < fxi.Length; j++)
                {
                    if(i != j)
                    {
                        P *= (x - xi[j]) / (xi[i] - xi[j]);
                    }
                }
                    L += P * fxi[i];

            }
            return L;
        }

        public float[] LeastSquareMethod(int n)
        {
            int m = xi.Length; // количество точек
            int k = n + 1; // кол коэф
            float[] c = new float[2 * n + 1];
            float[] d = new float[k];

            

            for (int i = 0; i < 2 * n+1; i++)
            {
                
                for (int j = 0; j < m; j++)
                {
                    c[i] += (float)Math.Pow(xi[j], i);
                    if (i <= n)
                    {
                    d[i] += fxi[j] * (float)Math.Pow(xi[j], i);

                    }
                }
            }

            float[][] system = new float[k][];

            for (int i = 0; i < system.Length; i++)
            {
                system[i] = new float[k];
            }

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    system[i][j] = c[i + j];
                }
            }

            float[] ai = GaussMethod(system, d);


            return ai;
        }

    }
}
