﻿
double eps = Math.Pow(10, -1);

// транспонирование
static double[,] Transpose(double[,] matrix) 
{
    double t;
    for (int i = 0; i < 4; ++i)
    {
        for (int j = i; j < 4; ++j)
        {
            t = matrix[i,j];
            matrix[i,j] = matrix[j,i];
            matrix[j,i] = t;
        }
    }

    return matrix;
}
// умножение вектора на матрицу
static double[] VectorMatrixMultiplication(double[,] matrix, double[] vector)
{
    double[] result = new double[4];
    for (int i = 0; i < 4; i++)
    {
        result[i] = 0;
        for (int j = 0; j < 4; j++)
        {
            result[i] += vector[j] * matrix[i,j];
        }
    }

    return result;
}
// скалярное произведение
static double ScalarProduct(double[] x, double[] y)
{

    double result = 0;

    for (int i = 0; i < 4; ++i)
        result += x[i] * y[i];

    return result;
}


static double FindLambda(double[,] matrix)
{
    List<double[]> x = new();
    List<double[]> y = new();
    List<double> lambda = new();

    x.Add(new double[] { 1, 1, 1, 1 });
    y.Add(new double[] { 1, 1, 1, 1 });

    x.Add(VectorMatrixMultiplication(matrix, x[0]));
    y.Add(VectorMatrixMultiplication(Transpose(matrix), y[0]));

    lambda.Add(ScalarProduct(x[1], y[0]) / ScalarProduct(x[0], y[0]));

    x.Add(VectorMatrixMultiplication(matrix, x[1]));
    y.Add(VectorMatrixMultiplication(Transpose(matrix), y[1]));

    lambda.Add(ScalarProduct(x[2], y[1]) / ScalarProduct(x[1], y[1]));

    int iLambda = 1;
    int iVectors = 2;

    while (Math.Abs(lambda[iLambda] - lambda[iLambda - 1]) > Math.Pow(10, -2))
    {
        iLambda++;
        iVectors++;
        x.Add(VectorMatrixMultiplication(matrix, x[iVectors - 1]));
        y.Add(VectorMatrixMultiplication(Transpose(matrix), y[iVectors - 1]));

        lambda.Add(ScalarProduct(x[iVectors], y[iVectors - 1]) / ScalarProduct(x[iVectors - 1], y[iVectors - 1]));
    }

    return lambda.Last();
}

int k = 23;

var matrix = new double[,]
{
    {127-2*k, 1, 1, -1},
    {k-10, -167-10*k, -1, -3},
    {k, 12, 34*k, -k},
    {k-1, 2, 3, -12*k}
};

for (int i = 0; i < 4; ++i)
{
    for (int j = 0; j < 4; ++j)
        Console.Write($"{matrix[i, j]} ");
    Console.WriteLine();
}

Console.WriteLine();

Console.WriteLine(FindLambda(matrix));