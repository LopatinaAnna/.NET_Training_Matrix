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
        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows { get; }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Columns { get; }

        /// <summary>
        /// Gets an array of floating-point values that represents the elements of this Matrix.
        /// </summary>
        public double[,] Array { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            try
            {
                Array = new double[Rows, Columns];
            }
            catch when (rows <= 0)
            {
                throw new ArgumentOutOfRangeException("rows", "The rows value is wrong");
            }
            catch when (columns <= 0)
            {
                throw new ArgumentOutOfRangeException("columns", "The columns value is wrong");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class with the specified elements.
        /// </summary>
        /// <param name="array">An array of floating-point values that represents the elements of this Matrix.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Matrix(double[,] array)
        {
            try
            {
                Array = array;
                Rows = array.GetLength(0);
                Columns = array.GetLength(1);
            }
            catch
            {
                throw new ArgumentNullException("array", "The array value is wrong");
            }
        }

        /// <summary>
        /// Allows instances of a Matrix to be indexed just like arrays.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <exception cref="ArgumentException"></exception>
        public double this[int row, int column]
        {
            get
            {
                try
                {
                    return Array[row, column];
                }
                catch when (row <= 0)
                {
                    throw new ArgumentException("The row value is wrong", "row");
                }
                catch when (column <= 0)
                {
                    throw new ArgumentException("The column value is wrong", "column");
                }
            }
            set
            {
                try
                {
                    Array[row, column] = value;
                }
                catch when (row <= 0)
                {
                    throw new ArgumentException("The rows value is wrong", "row");
                }
                catch when (column <= 0)
                {
                    throw new ArgumentException("The columns value is wrong", "column");
                }
            }
        }

        /// <summary>
        /// Creates a deep copy of this Matrix.
        /// </summary>
        /// <returns>A deep copy of the current object.</returns>
        public object Clone()
        {
            return new Matrix(Array);
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
            try
            {
                return matrix1.Add(matrix2);
            }
            catch when (matrix1 == null)
            {
                throw new ArgumentNullException("matrix1", "Matrix is null");
            }
            catch when (matrix2 == null)
            {
                throw new ArgumentNullException("matrix2", "Matrix is null");
            }
            catch
            {
                throw new MatrixException("Matrices have inappropriate dimensions");
            }
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
            try
            {
                return matrix1.Subtract(matrix2);
            }
            catch when (matrix1 == null)
            {
                throw new ArgumentNullException("matrix1", "Matrix is null");
            }
            catch when (matrix2 == null)
            {
                throw new ArgumentNullException("matrix2", "Matrix is null");
            }
            catch
            {
                throw new MatrixException("Matrices have inappropriate dimensions");
            }
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
            try
            {
                return matrix1.Multiply(matrix2);
            }
            catch when (matrix1 == null)
            {
                throw new ArgumentNullException("matrix1", "Matrix is null");
            }
            catch when (matrix2 == null)
            {
                throw new ArgumentNullException("matrix2", "Matrix is null");
            }
            catch
            {
                throw new MatrixException("Matrices have inappropriate dimensions");
            }
        }

        /// <summary>
        /// Adds <see cref="Matrix"/> to the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for adding.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Add(Matrix matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException("matrix", "Matrix is null");
            }
            if (Rows != matrix.Rows || Columns != matrix.Columns)
            {
                throw new MatrixException("Matrices have inappropriate dimensions");
            }

            Matrix result = new Matrix(this.Rows, this.Columns);
            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = this[i, j] + matrix[i, j];
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
            if (matrix == null)
            {
                throw new ArgumentNullException("matrix", "Matrix is null");
            }
            if (Rows != matrix.Rows || Columns != matrix.Columns)
            {
                throw new MatrixException("Matrices have inappropriate dimensions");
            }

            Matrix result = new Matrix(Rows, Columns);
            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = this[i, j] - matrix[i, j];
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
            if (matrix == null)
            {
                throw new ArgumentNullException("matrix", "Matrix is null");
            }
            if (Columns != matrix.Rows)
            {
                throw new MatrixException("Matrices have inappropriate dimensions");
            }

            Matrix result = new Matrix(Rows, matrix.Columns);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = 0;

                    for (int k = 0; k < Columns; k++)
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
        public override bool Equals(object obj)
        {
            if ((obj == null) || GetType() != obj.GetType())
                return false;

            Matrix matrix = obj as Matrix;

            if (Rows != matrix.Rows || Columns != matrix.Columns)
                return false;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (this[i, j] != matrix[i, j])
                        return false;
                }
            }

            return true;
        }

        public override int GetHashCode() => Array.GetHashCode();
    }
}