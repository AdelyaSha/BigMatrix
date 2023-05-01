using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigMatrix
{
    class QuadrateMatrix : Matrix
    {
        private double[,] quadrate_m;

        private int rows, cols;

        Random r = new Random();

        public static int threadCount;


        public new int Rows
        {
            get { return rows; }
            set => rows = value;
        }

        public new int Cols
        {
            get { return cols; }
            set => cols = value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(quadrate_m[i, j] + " ");
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public QuadrateMatrix(int Rows, int Cols) : base(Rows, Cols)
        {
            if (Rows != Cols)
            {
                throw new Exception("Такой массив нельзя конвертировать в квадратную матрицу!");
            }
            double[,] quad_matrix = new double[Rows, Cols];

            int size_of_array = 0;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    quad_matrix[i, j] = r.Next(1, 1);

                    size_of_array++;
                }

            }
            quadrate_m = quad_matrix;
            rows = Rows;
            cols = Cols;
        }

        public QuadrateMatrix(QuadrateMatrix quadrateMatrix) : base(quadrateMatrix)
        {
            quadrate_m = quadrateMatrix.quadrate_m;

            rows = quadrateMatrix.rows;

            cols = quadrateMatrix.cols;
        }

        public QuadrateMatrix()
        {

        }

        public QuadrateMatrix FindMinor(QuadrateMatrix quadrateMatrix, int colomn)
        {
            QuadrateMatrix result = new QuadrateMatrix();

            double[,] result_matrix = new double[quadrateMatrix.rows - 1, quadrateMatrix.cols - 1];

            for (int i = 1; i < quadrateMatrix.rows; i++)
            {
                for (int j = 0, col = 0; j < quadrateMatrix.cols; j++)
                {
                    if (j == colomn)
                    {
                        continue;
                    }

                    result_matrix[i - 1, col] = quadrateMatrix[i, j];

                    col++;
                }
            }
            result.quadrate_m = result_matrix;

            result.rows = result_matrix.GetLength(0);

            result.cols = result_matrix.GetLength(1);

            return result;
        }

        public override double this[int i, int j]
        {
            get { return quadrate_m[i, j]; }
            set => quadrate_m[i, j] = value;
        }

        public double FindTheDeterminant(QuadrateMatrix quadrateMatrix)
        {
            if (quadrateMatrix.cols == 2 && quadrateMatrix.rows == 2)
            {
                return (quadrateMatrix[0, 0] * quadrateMatrix[1, 1]) - (quadrateMatrix[0, 1] * quadrateMatrix[1, 0]);
            }

            else
            {
                double result = 0;

                for (int i = 0; i < quadrateMatrix.cols; i++)
                {
                    QuadrateMatrix MinorContainer = FindMinor(quadrateMatrix, i);

                    result += Math.Pow(-1, (0 + i)) * quadrateMatrix[0, i] * FindTheDeterminant(MinorContainer);
                }
                return result;
            }

        }

        public static QuadrateMatrix operator *(QuadrateMatrix quadrateMatrix, double num)
        {
            for (int i = 0; i < quadrateMatrix.rows; i++)
            {
                for (int j = 0; j < quadrateMatrix.cols; j++)
                {
                    quadrateMatrix[i, j] *= num;
                }
            }
            return new QuadrateMatrix(quadrateMatrix);
        }


        public QuadrateMatrix ReversedQMatrix(QuadrateMatrix quadrateMatrix)
        {
            double CheckTheDeterminant = FindTheDeterminant(quadrateMatrix);

            if (CheckTheDeterminant == 0)
            {
                throw new Exception("Нельзя найти обратную матрицу к той, у которой определитель равен 0!");
            }
            else
            {
                double[,] result_matrix = new double[quadrateMatrix.rows, quadrateMatrix.cols];

                QuadrateMatrix final_result = new QuadrateMatrix();

                for (int i = 0; i < quadrateMatrix.rows; i++)
                {
                    for (int j = 0; j < quadrateMatrix.cols; j++)
                    {
                        result_matrix[i, j] = Math.Round(Math.Pow(CheckTheDeterminant, -1), 5) * Math.Pow(-1, i + j) * quadrateMatrix[i, j];
                    }
                }
                final_result.quadrate_m = result_matrix;

                final_result.rows = result_matrix.GetLength(0);

                final_result.cols = result_matrix.GetLength(1);

                return final_result;
            }
        }
        public static QuadrateMatrix operator +(QuadrateMatrix quadrateMatrix, QuadrateMatrix quadrateMatrix1)
        {
            QuadrateMatrix result = new QuadrateMatrix();

            double[,] result_matrix = new double[quadrateMatrix.rows, quadrateMatrix.cols];

            if (quadrateMatrix.rows == quadrateMatrix1.rows && quadrateMatrix.cols == quadrateMatrix1.cols)
            {
                for (int i = 0; i < quadrateMatrix.rows; i++)
                {
                    for (int j = 0; j < quadrateMatrix.cols; j++)
                    {
                        result_matrix[i, j] = quadrateMatrix[i, j] + quadrateMatrix1[i, j];
                    }
                }

                result.quadrate_m = result_matrix;

                result.rows = quadrateMatrix.rows;

                result.cols = quadrateMatrix.cols;

                return result;
            }

            else
            {
                throw new Exception("Нельзя складывать квадратные матрицы разных размерностей!");
            }
        }
        public static QuadrateMatrix operator -(QuadrateMatrix quadrateMatrix, QuadrateMatrix quadrateMatrix1)
        {
            QuadrateMatrix result = new QuadrateMatrix();

            double[,] result_matrix = new double[quadrateMatrix.rows, quadrateMatrix.cols];

            if (quadrateMatrix.rows == quadrateMatrix1.rows && quadrateMatrix.cols == quadrateMatrix1.cols)
            {
                for (int i = 0; i < quadrateMatrix.rows; i++)
                {
                    for (int j = 0; j < quadrateMatrix.cols; j++)
                    {
                        result_matrix[i, j] = quadrateMatrix[i, j] - quadrateMatrix1[i, j];
                    }
                }
                result.quadrate_m = result_matrix;

                result.rows = quadrateMatrix.rows;

                result.cols = quadrateMatrix.cols;

                return result;
            }

            else
            {
                throw new Exception("Нельзя вычитать квадратные матрицы разных размерностей!");
            }
        }
        public static QuadrateMatrix operator *(int num, QuadrateMatrix quadrateMatrix)
        {
            QuadrateMatrix result = new QuadrateMatrix();

            double[,] result_matrix = new double[quadrateMatrix.rows, quadrateMatrix.cols];

            Parallel.For(1, threadCount + 1, lambda =>
            {
                for (int i = 0; i < quadrateMatrix.rows; i++)
                {
                    for (int j = 0; j < quadrateMatrix.cols; j++)
                    {
                        result_matrix[i, j] = num * quadrateMatrix[i, j];
                    }
                }
            });

            
            result.quadrate_m = result_matrix;

            result.rows = quadrateMatrix.rows;

            result.cols = quadrateMatrix.cols;

            return result;
        }
        public static QuadrateMatrix operator /(QuadrateMatrix quadrateMatrix, int num)
        {
            QuadrateMatrix result = new QuadrateMatrix();

            double[,] result_matrix = new double[quadrateMatrix.rows, quadrateMatrix.cols];

            for (int i = 0; i < quadrateMatrix.rows; i++)
            {
                for (int j = 0; j < quadrateMatrix.cols; j++)
                {
                    result[i, j] = quadrateMatrix[i, j] / num;
                }
            }
            result.quadrate_m = result_matrix;

            result.rows = quadrateMatrix.rows;

            result.cols = quadrateMatrix.cols;

            return result;
        }
        public static QuadrateMatrix operator *(QuadrateMatrix quadrateMatrix, QuadrateMatrix quadrateMatrix1)
        {
            if (quadrateMatrix.cols == quadrateMatrix1.rows)
            {
                QuadrateMatrix result_matrix = new QuadrateMatrix();

                ParallelOptions parallel = new ParallelOptions();

                parallel.MaxDegreeOfParallelism = threadCount;

                double[,] result = new double[quadrateMatrix.Rows, quadrateMatrix1.Cols];

                Parallel.For(0, result.GetLength(0), parallel, (i) =>
                {
                    for (int j = 0; j < quadrateMatrix.quadrate_m.GetLength(0); j++)
                    {
                        for (int k = 0; k < quadrateMatrix1.quadrate_m.GetLength(1); k++)
                        {
                            result[i, j] += quadrateMatrix[i, k] * quadrateMatrix1[k, j];
                        }
                    }
                });

                result_matrix.quadrate_m = result;

                result_matrix.cols = quadrateMatrix1.cols;

                result_matrix.rows = quadrateMatrix.rows;

                return result_matrix;
            }
            else
            {
                throw new Exception("Нельзя умножать матрицы, если число столбцов левой не равно числу строк правой!");
            }
        }
    }
}
