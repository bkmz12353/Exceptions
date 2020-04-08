using System;
using System.IO;

namespace exceptions
{
    class Program
    {
        static Matrix math2;
        static Matrix math1;
        static void Main(string[] args)
        {

            double[,] matrix, matrix1, matrix2;
            int N;
            string inter = "1.txt";
            string ct = "1";
            input(out matrix, inter);
            matrix1 = matrix;
            math1 = new Matrix(matrix1);
            output(matrix, ct);
            inter = "2.txt";
            ct = "2";
            input(out matrix, inter);
            matrix2 = matrix;
            math2 = new Matrix(matrix2);
            output(matrix, ct);
            actionselect();
            Console.ReadKey();

        }
        static void actionselect()
        {
            Console.WriteLine("Какое действие с матрицами вы желаете произвести?");
            Console.WriteLine(" 1-сложить матрицы\n 2-вычесть матрицы\n 3-умножить матрицы\n 4-вернуть пустую матрицу\n");
            int i = Convert.ToInt32(Console.ReadLine());
            switch (i)
            {
                case 1:

                    Console.WriteLine("Вы выбрали сложение матриц!");
                        double[,] plus = math1 + math2;
                    if (plus == null) { actionselect(); }
                    ConsoleAndFileWrite(plus.GetLength(0),plus.GetLength(1), plus, "OutPlusMatrix.txt");
                   actionselect();
                   break;
                case 2:
                   Console.WriteLine("Вы выбрали вычитаение матриц!");
                    double[,] minus = math1 - math2;
                   if (minus == null) { actionselect(); }
                    ConsoleAndFileWrite(minus.GetLength(0), minus.GetLength(1), minus, "OutMinusMatrix.txt");
                    actionselect();
                   break;
                case 3:
                    Console.WriteLine("Вы выбрали умножение матриц!");
                    double[,] multipl = math1*math2;
                   if (multipl == null) { actionselect(); }
                    ConsoleAndFileWrite(multipl.GetLength(0), multipl.GetLength(1), multipl, "OutMultiplMatrix.txt");

                    actionselect();
                    break;
                case 4:
                    Console.WriteLine("Для того чтобы получить пустую матрицу введите её размер!");
                    GetEmpty();
                    actionselect();
                    break;
                default:
                    Console.WriteLine("Я вас не понял... Возможно вы ввели неверное число? Повторите ввод!!!");
                    actionselect();
                    break;
            }
        }
        static void docheck()
        {
            int choose = Convert.ToInt32(Console.ReadLine());
            if (choose == 1) { GetEmpty(); }
            else if (choose == 2) { actionselect(); }
            else
            {
                Console.WriteLine("Неизвестное действие.\n Чтобы повторить ввод введите 1 или введите 2 чтобы выбрать другое действие.");
                docheck();
            }
        }
        static void GetEmpty()
        {
            Console.WriteLine("Кол-во строк:");
            int row = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Кол-во столбцов:");
            int col = Convert.ToInt32(Console.ReadLine());
            if (row == 0||col==0)
            {
                Console.WriteLine("Увы, размерность матрицы не может быть равна 0. Чтобы повторить ввод введите 1 или введите 2 чтобы выбрать другое действие.");

            }
            double[,] matrix = new double[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            Console.WriteLine("Ваша нулевая матрица размерности {0}x{1}", row,col);
            ConsoleAndFileWrite(row,col, matrix, "OutZeroMatrix.txt");
        }
        static void ConsoleAndFileWrite(int row,int col, double[,] matrix, string fileout)
        {
            StreamWriter outfile = new StreamWriter(fileout);
            string outer;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    outer = Convert.ToString(matrix[i, j]);
                    outfile.Write(outer + " ");
                    Console.Write("{0} ", matrix[i, j]);
                }
                outfile.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine();
            outfile.Close();
        }
        static void input(out double[,] matrix,  string inter)
        {
            StreamReader input = new StreamReader(inter);


            string file = input.ReadToEnd();
            string[] num = file.Trim().Split('\n');
            string[] chis = num[0].Trim().Split(' ');
            int row = num.Length;
            int col = chis.Length;
            matrix = new double[num.Length, chis.Length];
            for (int c = 0; c < num.Length; c++)
            {
                 chis = num[c].Trim().Split(' ');
                if (chis.Length != col)
                {
                    Console.WriteLine("Указана неверная матрица! Количество строк {0}, столбцов {1}", chis.Length, col);
                    Console.Read();
                    Environment.Exit(0);
                    return;
                }
                for (int g = 0; g < chis.Length; g++)
                    matrix[c, g] = double.Parse(chis[g]);
            }
            input.Close();
        }
        static void output(double[,] matrix,  string ct)
        {

            Console.WriteLine("Матрица {0}:", ct);
            for (int i = 0; i <matrix.GetLength(0); i++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {

                    Console.Write("{0} ", matrix[i, c]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
    class Matrix
    {
        double[,]  matrix;
        public Matrix( double [,] matrix)
        {
            this.matrix = matrix;
           
        }

        public static double[,] operator +(Matrix math1, Matrix math2)
        {
            int N1 = math1.matrix.GetLength(0);
            int N2 = math1.matrix.GetLength(1);
            double[,] res = new double[N1, N2];
            try
            {
                if (math1.matrix.GetLength(0) != math2.matrix.GetLength(0) || math1.matrix.GetLength(1)!= math2.matrix.GetLength(1))
                {
                    throw new MyExeption("Нельзя складывать разные по величине матрицы. Вы ввели матрицы размерами "+ math1.matrix.GetLength(0) + "x"+ math1.matrix.GetLength(1) + " и "+ math2.matrix.GetLength(0) + "x"+math2.matrix.GetLength(1)+"");
                }
                for (int row = 0; row < N1; row++)
                {
                    for (int col = 0; col < N2; col++)
                    {
                        res[row, col] = math1.matrix[row, col] + math2.matrix[row, col];
                    }
                }
                return res;
            }

            catch (MyExeption ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return null;
            }
            
        }
        public static double[,] operator -(Matrix math1, Matrix math2)
        {
            int N1 = math1.matrix.GetLength(0);
            int N2 = math1.matrix.GetLength(1);
            double[,] res = new double[N1, N2];
            try
            {
                if (math1.matrix.GetLength(0) != math2.matrix.GetLength(0) || math1.matrix.GetLength(1) != math2.matrix.GetLength(1))
                {
                    throw new MyExeption("Нельзя вычитать разные по величине матрицы. Вы ввели матрицы размерами " + math1.matrix.GetLength(0) + "x" + math1.matrix.GetLength(1) + " и " + math2.matrix.GetLength(0) + "x" + math2.matrix.GetLength(1) + "");
                }
                for (int row = 0; row < N1; row++)
                {
                    for (int col = 0; col < N2; col++)
                    {
                        res[row, col] = math1.matrix[row, col] - math2.matrix[row, col];
                    }
                }
                return res;
            }

            catch (MyExeption ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return null;
            }

        }
        public static double[,] operator *(Matrix math1, Matrix math2)
        {
            int N1 = math1.matrix.GetLength(0);
            int N2 = math2.matrix.GetLength(1);
            int N3= math1.matrix.GetLength(1);
            double[,] res = new double[N1, N2];
            try
            {
                if (N1 != N2 )
                {
                    throw new MyExeption("Нельзя совершить операцию умножения матриц. Вы ввели матрицы размерами " + math1.matrix.GetLength(0) + "x" + math1.matrix.GetLength(1) + " и " + math2.matrix.GetLength(0) + "x" + math2.matrix.GetLength(1) + "");
                }
                for (var i = 0; i < N1; i++)
                {
                    for (var j = 0; j < N2; j++)
                    {
                        res[i, j] = 0;

                        for (var k = 0; k < N3; k++)
                        {
                            res[i, j] += math1.matrix[i, k] * math2.matrix[k, j];
                        }
                    }
                }
                return res;
            }

            catch (MyExeption ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return null;
            }

        }

        

    }
    class MyExeption : Exception
    {
        public MyExeption(string message)

            : base(message)
        { }


    }
}