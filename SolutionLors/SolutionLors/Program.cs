using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ИЗ4
{
    class Polinom : Metod_Guyssa
    {

        public Polinom() // конструктор класса
        {
            Console.WriteLine("Введите степень рекурентного соотношения: ");
            int Stepen = Convert.ToInt32(Console.ReadLine()); //Считываем степень нашего РС

            Kol_korni = 0; //Подсчет корней
            this.Stepen = Stepen;
            this.kol = Stepen + 1; //Количество наших коэффициентов



            polinom = new double[kol]; //Массив коэффициентов
            Start_Value = new double[Stepen]; //Начальное заполнение
            korni = new double[Stepen]; //Корни характеристического уравнения

            Console.WriteLine("Введите соответствующие коэффициенты: ");
            for (int i = 0; i < kol; i++)
            {
                polinom[i] = Convert.ToDouble(Console.ReadLine());//Считываем наши коэффициенты из РС
            }

            Calculation_Korni(); //Находим корни характеристического уравнения

            if (proverka_na_razlichnosti) //Проверка на то, чтобы все корни были различны
            {
                Print_korni(); //Вывод корней хар ур
                Print_LORS();//Вывод общего решения
                Calculation_Const();//Вычисление постоянных из решения
                Printend_LORS();//Решения по начальному заполнению
            }
            else Console.WriteLine("Ошибка!!!"); //Если корни кратные, сообщаем об ошибки



        }

        public void Calculation_Korni()
        {
            int g = 0;
            while (polinom[Stepen - g] == 0)
            {
                korni[Kol_korni] = 0; //нашли корень 0
                if (g == 0) Kol_korni++;
                g++;
            }
            for (int i = 1; i <= Math.Abs(polinom[Stepen - g]); i++)//Проходим по всем числам свободного члена
            {
                if (Math.Abs(polinom[Stepen - g]) % i == 0)//Находим делитель свободного члена
                {
                    double sum = 0; //Сумма для положительного
                    double _sum = 0; //Сумма для отрицательного
                    for (int j = 0; j < kol - g; j++)//Проходим по всем коэффициентам
                    {
                        sum += polinom[j] * Math.Pow(i, kol - 1 - j - g); //Считаем для положительного делителя
                        _sum += polinom[j] * Math.Pow(-i, kol - 1 - j - g); //Считаем для отрицательного делителя
                    }
                    if (sum == 0) //Нашли корень положительного
                    {
                        korni[Kol_korni] = i; //нашли корень
                        Kol_korni++;
                    }
                    if (_sum == 0) //Нашли корень отрицательного
                    {
                        korni[Kol_korni] = -i; //нашли корень
                        Kol_korni++;
                    }

                }
            }

            if (Stepen == Kol_korni) //Если количество корней совпадает со степенью
            {
                proverka_na_razlichnosti = true;
                Console.WriteLine("Все корни различны");
            }
            else
            {
                proverka_na_razlichnosti = false;
                Console.WriteLine("Корни не различны");
            }
        }

        public void Calculation_Const()
        {
            int n = Stepen; //n - количество уравнений
            a = new double[n][]; //Матрица 
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                a[i] = new double[n];
                for (int j = 0; j < n; j++)
                    a[i][j] = Math.Pow(korni[j], i); //Возводим в соответсвующие степень
            }

            Console.WriteLine("Введите {0} начальных члена последовательности: ", Stepen);
            for (int i = 0; i < Stepen; i++)
            {
                Start_Value[i] = Convert.ToDouble(Console.ReadLine()); //Считываем начальные значения
            }

            Printed_System_of_equations(a, Start_Value, n);//Вывод уравнений

            C = G(a, Start_Value, n); //метод Гаусса

            for (int i = 0; i < n; i++)
                Console.Write("\nC" + i + "=" + C[i]);//C[i] - наши константы
        }


        //Вывод общего решения
        public void Print_LORS()
        {
            Console.WriteLine("Общее однородное: ");
            Console.Write("An = ");
            for (int i = 0; i < Kol_korni; i++)
            {
                Console.Write("+C{0}*{1}^n", i + 1, korni[i]);
            }
        }

        //Решения по начальному заполнению
        public void Printend_LORS()
        {
            Console.WriteLine("\nРешение ЛОРС: ");
            Console.Write("An = ");
            for (int i = 0; i < Kol_korni; i++)
            {
                Console.Write($"+{C[i]}*{korni[i]}^n"); ;
            }
        }

        //Вывод корней хар ур
        public void Print_korni()
        {
            Console.WriteLine("Корни характеристического уравнения: ");
            for (int i = 0; i < Kol_korni; i++)
            {
                Console.Write(korni[i] + " ");
            }
            Console.WriteLine();
        }


        bool proverka_na_razlichnosti;
        int kol; //Количесво элементов в полиноме
        int Kol_korni; // Количество корней
        int Stepen; //Степень полинома
        double[] polinom; //Массив коэффициентов
        double[] Start_Value; //начальное заполнение
        double[] korni; //Корни характеристического уравнения
        double[][] a;//матрица составленная по начальным значениям
        double[] C;//Постоянные в общем однородном

    }

    public class Metod_Guyssa
    {
        protected void Printed_System_of_equations(double[][] a, double[] y, int n)// вывод уравнений, y -  массив начальных значений, а - матрица нашего общего решения 
        {
            Console.WriteLine("Система уравнений: ");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(a[i][j] + "*C" + j);
                    if (j < n - 1)
                        Console.Write(" + ");
                }
                Console.WriteLine(" = " + y[i]);
            }
            return;
        }
        protected double[] G(double[][] a, double[] y, int n)//Метод Гаусса
        {
            double max;
            double[] C;
            int k, index;
            C = new double[n];//динамический массив констант

            for (k = 0; k < n; k++)
            {

                max = Math.Abs(a[k][k]);//максимальный элемент по диагонали
                index = k; //index - номер элемента

                // Поиск строки с максимальным a[i][k]
                for (int i = k + 1; i < n; i++)
                {
                    if (Math.Abs(a[i][k]) > max)
                    {
                        max = Math.Abs(a[i][k]);
                        index = i;
                    }
                }


                if (max == 0)
                {
                    Console.WriteLine("Нулевой столбец " + index);// нет ненулевых диагональных элементов
                    return C;
                }

                // Меняем местами k строку и строку с максимальным элементом
                for (int j = 0; j < n; j++)
                {
                    double bufer = a[k][j];
                    a[k][j] = a[index][j];
                    a[index][j] = bufer;
                }

                // Нормализация уравнений
                double t = y[k];
                y[k] = y[index];
                y[index] = t;



                for (int i = k; i < n; i++)
                {
                    double bufer = a[i][k];
                    if (t != 0)  // для нулевого коэффициента пропустить
                    {
                        for (int j = 0; j < n; j++)
                            a[i][j] = a[i][j] / bufer;
                        y[i] = y[i] / bufer; //Выносим коэффициент t=a[i][k] из матрицы, чтобы получить 1
                        if (i != k)   // уравнение не вычитать само из себя
                        {
                            for (int j = 0; j < n; j++)
                                a[i][j] = a[i][j] - a[k][j];
                            y[i] = y[i] - y[k]; //Вычитаем из i строки k строку
                        }
                    }
                }

            }

            // обратная подстановка
            for (k = n - 1; k >= 0; k--)
            {
                C[k] = y[k];
                for (int i = 0; i < k; i++)
                    y[i] = y[i] - a[i][k] * C[k];
            }
            return C; // возваращает найденные значения C
        }

    }
    class Program
    {
        //sample
        //1 −6 −52 150 675 
        //1 -8 12

        static void Main()
        {

            Polinom LORS = new Polinom();

            Console.ReadKey();


        }
    }
}
