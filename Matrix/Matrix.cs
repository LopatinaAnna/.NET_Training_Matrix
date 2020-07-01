using System;
using System.Runtime.Serialization;

namespace MatrixLibrary
{
    [Serializable]
    public class MatrixException : Exception
    {
        public MatrixException() : base()
        {
        }

        public MatrixException(string message) : base(message)
        {
        }

        protected MatrixException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class Matrix : ICloneable
    {
        private readonly int rows;
        private readonly int columns;
        private readonly double[,] array;

        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows
        {
            get => rows;
        }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Columns
        {
            get => columns;
        }

        /// <summary>
        /// Gets an array of floating-point values that represents the elements of this Matrix.
        /// </summary>
        public double[,] Array
        {
            get => array;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Matrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            this.array = new double[this.rows, this.columns];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class with the specified elements.
        /// </summary>
        /// <param name="array">An array of floating-point values that represents the elements of this Matrix.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Matrix(double[,] array)
        {
            this.rows = array.GetLength(0);
            this.columns = array.GetLength(1);
            this.array = array;
        }

        /// <summary>
        /// Allows instances of a Matrix to be indexed just like arrays.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <exception cref="ArgumentException"></exception>
        public double this[int row, int column]
        {
            get => Array[row, column];
            set => Array[row, column] = value;
        }

        /// <summary>
        /// Creates a deep copy of this Matrix.
        /// </summary>
        /// <returns>A deep copy of the current object.</returns>
        public object Clone()
        {
            return new Matrix(this.Array);
        }

        /// <summary>
        /// Adds two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is sum of two matrices.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Add(matrix2);
        }

        /// <summary>
        /// Subtracts two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is subtraction of two matrices</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Subtract(matrix2);
        }

        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is multiplication of two matrices.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Multiply(matrix2);
        }

        /// <summary>
        /// Adds <see cref="Matrix"/> to the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for adding.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Add(Matrix matrix)
        {
            Matrix result = this;
            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] += matrix[i, j];
                }
            return result;
        }

        /// <summary>
        /// Subtracts <see cref="Matrix"/> from the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for subtracting.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Subtract(Matrix matrix)
        {
            Matrix result = this;
            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] -= matrix[i, j];
                }
            return result;
        }

        /// <summary>
        /// Multiplies <see cref="Matrix"/> on the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for multiplying.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Multiply(Matrix matrix)
        {
            Matrix result = new Matrix(this.Rows, matrix.Columns);
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = 0;

                    for (int k = 0; k < this.Columns; k++)
                    {
                        result[i, j] += this[i, k] * matrix[k, j];
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Tests if <see cref="Matrix"/> is identical to this Matrix.
        /// </summary>
        /// <param name="obj">Object to compare with. (Can be null)</param>
        /// <returns>True if matrices are equal, false if are not equal.</returns>
        /// <exception cref="InvalidCastException">Thrown when object has wrong type.</exception>
        /// <exception cref="MatrixException">Thrown when matrices are incomparable.</exception>
        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode() => Array.GetHashCode();
    }
}