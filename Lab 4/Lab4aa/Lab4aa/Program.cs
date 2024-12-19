using System;
public class Matrix{
    private int rows;
    private int columns;
    private int[,] data;

    public Matrix(){
        rows = 0;
        columns = 0;
        data = new int[0, 0];
    }

    public Matrix(int rows, int columns, int[,] data){
        this.rows = rows;
        this.columns = columns;
        this.data = data;
    }

    public override string ToString(){
        string result = "";
        for (int i = 0; i < rows; i++){
            for (int j = 0; j < columns; j++){
                result += data[i, j] + " ";
            }
            result += "\n";
        }
        return result;
    }

    public Matrix Add(Matrix other){
        int[,] result = new int[rows, columns];
        for (int i = 0; i < rows; i++){
            for (int j = 0; j < columns; j++){
                result[i, j] = this.data[i, j] + other.data[i, j];
            }
        }
        return new Matrix(rows, columns, result);
    }

    public Matrix Subtract(Matrix other){
        int[,] result = new int[rows, columns];
        for (int i = 0; i < rows; i++){
            for (int j = 0; j < columns; j++){
                result[i, j] = this.data[i, j] - other.data[i, j];
            }
        }
        return new Matrix(rows, columns, result);
    }

    public void Display(){
        Console.WriteLine(this.ToString());
    }

    public Matrix AddScalar(int scalar){
        int[,] result = new int[rows, columns];
        for (int i = 0; i < rows; i++){
            for (int j = 0; j < columns; j++){
                result[i, j] = this.data[i, j] + scalar;
            }
        }
        return new Matrix(rows, columns, result);
    }
}

public class MatrixTest{
    public delegate Matrix MatrixOperation(Matrix a, Matrix b);
    public delegate void DisplayMatrix(Matrix m);
    public static event DisplayMatrix MatrixDisplayed;
    static void Main(string[] args){
        Matrix m1 = new Matrix(2, 2, new int[,] { { 1, 2 }, { 3, 4 } });
        Matrix m2 = new Matrix(2, 2, new int[,] { { 5, 6 }, { 7, 8 } });

        while (true){
            Console.WriteLine("\nMatrix  Menu:");
            Console.WriteLine("1. Add two matrices using single delegate");
            Console.WriteLine("2. Subtract two matrices using single delegate");
            Console.WriteLine("3. Array of Delegates");
            Console.WriteLine("4. Multicast Delegates");
            Console.WriteLine("5. Event handling for display function");
            Console.WriteLine("6. Lambda Expression to Add 5 to a Matrix");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice)){
                switch (choice){
                    case 1:
                        AddMatrices(m1, m2);
                        break;
                    case 2:
                        SubtractMatrices(m1, m2);
                        break;
                    case 3:
                        ArrayOfDelegates(m1, m2);
                        break;
                    case 4:
                        MulticastDelegates(m1, m2);
                        break;
                    case 5:
                        EventHandling(m1);
                        break;
                    case 6:
                        LambdaExpression(m1);
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            else{
                Console.WriteLine("Invalid input");
            }
        }
    }

    static void AddMatrices(Matrix m1, Matrix m2){
        MatrixOperation add = (a, b) => a.Add(b);
        Matrix result = add(m1, m2);
        Console.WriteLine("Result of addition:");
        result.Display();
    }

    static void SubtractMatrices(Matrix m1, Matrix m2){
        MatrixOperation subtract = (a, b) => a.Subtract(b);
        Matrix result = subtract(m1, m2);
        Console.WriteLine("Result of subtraction:");
        result.Display();
    }

    static void ArrayOfDelegates(Matrix m1, Matrix m2){
        MatrixOperation[] operations = new MatrixOperation[]
        {
            (a, b) => a.Add(b),
            (a, b) => a.Subtract(b)
        };

        Console.WriteLine("Results using array of delegates:");
        foreach (var operation in operations){
            Matrix result = operation(m1, m2);
            result.Display();
        }
    }

    static void MulticastDelegates(Matrix m1, Matrix m2){
        MatrixOperation multiOp = (a, b) => a.Add(b);
        multiOp += (a, b) => a.Subtract(b);
        Console.WriteLine("Results using multicast delegate:");
        Delegate[] delegates = multiOp.GetInvocationList();
        foreach (MatrixOperation op in delegates){
            Matrix result = op(m1, m2);
            result.Display();
        }
    }

    static void EventHandling(Matrix m){
        MatrixDisplayed += OnMatrixDisplayed;
        Console.WriteLine("Displaying matrix using event:");
        MatrixDisplayed?.Invoke(m);
        MatrixDisplayed -= OnMatrixDisplayed;
    }

    static void OnMatrixDisplayed(Matrix m){
        m.Display();
    }

    static void LambdaExpression(Matrix m){
        Func<Matrix, Matrix> addFive = matrix => matrix.AddScalar(5);
        Matrix result = addFive(m);
        Console.WriteLine("Result after adding 5 to each element:");
        result.Display();
    }
}