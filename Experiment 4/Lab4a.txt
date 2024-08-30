using System;

class Matrix
{
    private int rows;
    private int columns;
    private int[,] data;

    public Matrix(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
        this.data = new int[rows, columns];
    }

    public Matrix(int rows, int columns, int[,] data)
    {
        this.rows = rows;
        this.columns = columns;
        this.data = data;
    }

    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result += data[i, j] + " ";
            }
            result += "\n";
        }
        return result;
    }

    public Matrix Add(Matrix other)
    {
        if (this.rows != other.rows || this.columns != other.columns)
        {
            throw new ArgumentException("Matrices must have the same dimensions");
        }

        Matrix result = new Matrix(rows, columns);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result.data[i, j] = this.data[i, j] + other.data[i, j];
            }
        }
        return result;
    }

    public Matrix Subtract(Matrix other)
    {
        if (this.rows != other.rows || this.columns != other.columns)
        {
            throw new ArgumentException("Matrices must have the same dimensions");
        }

        Matrix result = new Matrix(rows, columns);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result.data[i, j] = this.data[i, j] - other.data[i, j];
            }
        }
        return result;
    }

    public void Display()
    {
        Console.WriteLine(this.ToString());
    }

    public Matrix AddScalar(int scalar)
    {
        Matrix result = new Matrix(rows, columns);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result.data[i, j] = this.data[i, j] + scalar;
            }
        }
        return result;
    }

    public static Matrix Add(Matrix a, Matrix b)
    {
        return a.Add(b);
    }

    public static Matrix Subtract(Matrix a, Matrix b)
    {
        return a.Subtract(b);
    }
}

class MatrixTest
{
    public delegate Matrix MatrixOperation(Matrix a, Matrix b);
    public delegate void DisplayMatrix(Matrix m);
    public static event DisplayMatrix DisplayEvent;

    static void Main(string[] args)
    {
        Matrix m1 = new Matrix(2, 2, new int[,] { { 1, 2 }, { 3, 4 } });
        Matrix m2 = new Matrix(2, 2, new int[,] { { 5, 6 }, { 7, 8 } });

        while (true)
        {
            Console.WriteLine("\nMatrix Operations Menu:");
            Console.WriteLine("1. Add two matrices using single delegate");
            Console.WriteLine("2. Subtract two matrices using single delegate");
            Console.WriteLine("3. Array of Delegates");
            Console.WriteLine("4. Multicast Delegates");
            Console.WriteLine("5. Event handling for display function");
            Console.WriteLine("6. Lambda Expression to Add 5 to a Matrix");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    MatrixOperation addOp = new MatrixOperation(Matrix.Add);
                    Console.WriteLine("Result of addition:");
                    Matrix addResult = addOp(m1, m2);
                    addResult.Display();
                    break;

                case 2:
                    MatrixOperation subOp = new MatrixOperation(Matrix.Subtract);
                    Console.WriteLine("Result of subtraction:");
                    Matrix subResult = subOp(m1, m2);
                    subResult.Display();
                    break;

                case 3:
                    MatrixOperation[] operations = new MatrixOperation[]
                    {
                        new MatrixOperation(Matrix.Add),
                        new MatrixOperation(Matrix.Subtract)
                    };
                    Console.WriteLine("Results using array of delegates:");
                    foreach (var op in operations)
                    {
                        op(m1, m2).Display();
                    }
                    break;

                case 4:
                    MatrixOperation multiOp = Matrix.Add;
                    multiOp += Matrix.Subtract;
                    Console.WriteLine("Results using multicast delegate:");
                    multiOp(m1, m2).Display();
                    break;

                case 5:
                    DisplayEvent += m => m.Display();
                    Console.WriteLine("Displaying matrices using event:");
                    DisplayEvent?.Invoke(m1);
                    DisplayEvent?.Invoke(m2);
                    break;

                case 6:
                    Func<Matrix, Matrix> addFive = (m) => m.AddScalar(5);
                    Console.WriteLine("Result after adding 5 to matrix:");
                    Matrix lambdaResult = addFive(m1);
                    lambdaResult.Display();
                    break;

                case 7:
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}