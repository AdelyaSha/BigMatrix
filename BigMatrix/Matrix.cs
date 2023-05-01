using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigMatrix
{
    class Matrix
    {
        private double[,] matrix;

        private int rows, cols;

        public static int threadCount;

        

        Random r = new Random();

        public int ThreadCount => threadCount;
        


        public int Rows
        {
            get { return rows; }
            set => rows = value;
        }

        public int Cols
        {
            get { return cols; }
            set => cols = value;
        }

        protected Matrix()
        {

        }

        public Matrix(int Rows, int Cols)
        {
            rows = Rows; 
            cols = Cols;

            double[,] matrix = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = r.Next(1, 1);
                }

            }
            this.matrix = matrix;
        }

        public Matrix(Matrix matrix)
        {
            this.matrix = matrix.matrix;

            rows = matrix.rows;

            cols = matrix.cols;
        }

        public static Matrix operator +(Matrix matrix, Matrix matrix1)
        {

            if (matrix.rows == matrix1.rows && matrix.cols == matrix1.cols)
            {
                Matrix result_matrix = new Matrix();

                double[,] result = new double[matrix.rows, matrix.cols];

                for (int i = 0; i < matrix.rows; i++)
                {
                    for (int j = 0; j < matrix.cols; j++)
                    {
                        result[i, j] = matrix[i, j] + matrix1[i, j];
                    }
                }
                result_matrix.matrix = result;

                result_matrix.rows = matrix.rows;

                result_matrix.cols = matrix.cols;

                return result_matrix;
            }

            else
            {
                throw null;
            }
        }

        public virtual double this[int i, int j]
        {
            get { return matrix[i, j]; }

            set { matrix[i, j] = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(matrix[i, j] + " ");
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public static Matrix operator -(Matrix matrix, Matrix matrix1)
        {
            if (matrix.rows == matrix1.rows && matrix.cols == matrix1.cols)
            {
                Matrix result_matrix = new Matrix();

                double[,] result = new double[matrix.rows, matrix.cols];

                for (int i = 0; i < matrix.rows; i++)
                {
                    for (int j = 0; j < matrix.cols; j++)
                    {
                        result[i, j] = matrix[i, j] - matrix1[i, j];
                    }
                }
                result_matrix.rows = matrix.rows;

                result_matrix.cols = matrix.cols;

                result_matrix.matrix = result;

                return result_matrix;
            }

            else
            {
                throw null;
            }

        }
        public static Matrix operator *(Matrix matrix, int num)
        {
            Matrix result_matrix = new Matrix();

            double[,] result = new double[matrix.rows, matrix.cols];

            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.cols; j++)
                {
                    result[i, j] = matrix[i, j] * num;
                }
            }
            result_matrix.matrix = result;

            result_matrix.rows = matrix.rows;

            result_matrix.cols = matrix.cols;

            return result_matrix;
        }

        public static Matrix operator *(int num, Matrix matrix)
        {
            Matrix result_matrix = new Matrix();

            double[,] result = new double[matrix.rows, matrix.cols];

            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.cols; j++)
                {
                    result[i, j] = num * matrix[i, j];
                }
            }
            result_matrix.matrix = result;

            result_matrix.rows = matrix.rows;

            result_matrix.cols = matrix.cols;

            return result_matrix;
        }

        public static Matrix operator /(Matrix matrix, int num)
        {
            Matrix result_matrix = new Matrix();

            double[,] result = new double[matrix.rows, matrix.cols];

            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.cols; j++)
                {
                    result[i, j] = matrix[i, j] / num;
                }
            }
            result_matrix.matrix = result;

            result_matrix.rows = matrix.rows;

            result_matrix.cols = matrix.cols;

            return result_matrix;
        }

        public static Matrix operator *(Matrix matrix, Matrix matrix1)
        {
            if (matrix.cols == matrix1.rows)
            {
                Matrix result_matrix = new Matrix();

                double[,] result = new double[matrix.rows, matrix1.cols];

                
                for (int i = 0; i < matrix.matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix1.matrix.GetLength(1); j++)
                    {
                        for (int k = 0; k < matrix1.matrix.GetLength(0); k++)
                        {
                            result[i, j] += matrix[i, k] * matrix1[k, j];
                        }
                    }
                }


                result_matrix.matrix = result;

                result_matrix.cols = matrix1.cols;

                result_matrix.rows = matrix.rows;

                return result_matrix;
            }
            else
            {
                throw null;
            }
        }

        public Matrix Transpose()
        {
            Matrix result_matrix = new Matrix();

            double[,] transposed_matrix = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    transposed_matrix[i, j] = matrix[j, i];
                }
            }
            result_matrix.matrix = transposed_matrix;

            result_matrix.rows = transposed_matrix.GetLength(0);

            result_matrix.cols = transposed_matrix.GetLength(1);

            return result_matrix;
        }

        public override int GetHashCode()
        {
            return matrix.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Matrix matrix)
            {
                return this.matrix == matrix.matrix;
            }
            else
            {
                return false;
            }
        }

        public static bool operator ==(Matrix matrix, Matrix matrix1)
        {
            int counter = matrix.matrix.Length;

            if (matrix.rows == matrix1.rows && matrix.cols == matrix1.cols)
            {
                for (int i = 0; i < matrix.rows; i++)
                {
                    for (int j = 0; j < matrix.cols; j++)
                    {
                        if (matrix[i, j] == matrix1[i, j])
                        {
                            counter--;
                        }
                        else
                        {
                            return false;

                        }
                    }
                }
                if (counter == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Matrix matrix, Matrix matrix1)
        {
            int counter = matrix.matrix.Length;

            if (matrix.rows == matrix1.rows && matrix.cols == matrix1.cols)
            {
                for (int i = 0; i < matrix.rows; i++)
                {
                    for (int j = 0; j < matrix.cols; j++)
                    {
                        if (matrix[i, j] == matrix1[i, j])
                        {
                            counter--;
                        }
                        else
                        {
                            return true;

                        }
                    }
                }
                if (counter == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
