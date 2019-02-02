using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Parser
{
    public class Program
    {
        //Функция для преобразования "первого" аргумента в адрес файла.
        public static string Give_me_address(string argument)
        {
            //Копируем часть аргумента, содержащую адрес и возвращаем ее.
            return argument.Substring(argument.IndexOf("id") + 3,
                argument.Length - (argument.IndexOf("id") + 3));
        }

        //Функция для преобразования "второго" аргумента номер столбца для сортировки.
        public static int Give_me_columns_number(string second_argument)
        {
            //Копируем часть аргумента, содержащую номер и возвращаем его.
            int column_number = Convert.ToInt16(second_argument.Substring(second_argument.
                IndexOf("sort")+ 5, second_argument.Length - second_argument.
                IndexOf("sort") - 5));
            
            return column_number;
        }

        //Фунция для заполнения массива из файла.
        static string[,] Give_me_array(string file,
            int number_of_lines, int number_of_columns)
        {
            //Измерения массива.
            int i_line = 0, j_column = 0;

            //"Кармашки".
            int digital_pocket = 0;
            string string_pocket = "";

            //Объявляем о создании массива.
            string[,] array_from_file = new string[number_of_lines,
                number_of_columns];

            //Выполним разделение на строки и столбцы.
            foreach (char chr in file)
            {
                if (chr == '"')
                    digital_pocket++;

                else if (chr == ',' && (digital_pocket > 1 ||
                   digital_pocket == 0))
                {
                    digital_pocket = 0;
                    array_from_file[i_line, j_column] =
                       string_pocket;
                    string_pocket = String.Empty;
                    j_column++;
                }
                else if (chr == '\n')
                {
                    array_from_file[i_line, j_column] =
                        string_pocket;
                    digital_pocket = 0;
                    j_column = 0;
                    i_line++;
                    string_pocket = "";
                }
                else
                    string_pocket += chr;
            }

            //Возвращаемый массив.
            return array_from_file;
        }

        //Функция для создания списка максимальных длин колонн массива.
        public static List<int> Give_me_maximum_length(string[,] array_from_file)
        {
            //Создаем список для хранения длин максимальных длин колонн.
            List<int> maximum_in_columns = new List<int>() { };

            //Переменная для хранения наибольщей длины колонны массива.
            int max = 0;

            /*Универсализируем вывод массива, путем предоставления
             * универсального кол-ва символов для каждого элемента.*/
            for (int j = 0; j < array_from_file.GetLength(1); j++)
            {
                for (int i = 0; i < array_from_file.GetLength(0); i++)
                    if (array_from_file[i, j].Length > max)
                        max = array_from_file[i, j].Length;
                maximum_in_columns.Add(max);
                max = 0;
            }

            return maximum_in_columns;
        }

        //Функция для нахождения кол-ва строк и столбцов.
        static int Size(string lines_or_columns, string file)
        {
            /*Объявляем и инициализируем переменные для кол-ва
             * строк и столбцов в будущем массиве.*/

            int number_of_lines = 0, number_of_columns = 0;

            //"Кармашек".
            int digital_pocket = 0;

            //"Стоп-кран".
            bool we_have_all_columns = false;

            //Введем подсчет строк и столбцов для будущего массива.
            foreach (char chr in file)
            {
                if (!we_have_all_columns)
                {
                    if (chr == '"')
                        digital_pocket++;

                    if ((chr == ',' || chr == '\n' || chr ==';')
                        && (digital_pocket > 1 ||
                        digital_pocket == 0))
                    {
                        digital_pocket = 0;
                        number_of_columns++;
                    }
                }
                if (chr == '\n')
                {
                    we_have_all_columns = true;
                    number_of_lines++;
                }
            }

            /*В зависимости от запроса, возвращаем кол-во
             * столбцов, либо строк.*/

            switch(lines_or_columns)
            {
                case ("columns"):
                    return (number_of_columns);

                case ("lines"):
                    return (number_of_lines);

                //на случай ошибки в тексте программы.
                default:
                    while (true)
                    {
                        Console.WriteLine(" ERROR! Please, write" +
                            "what number you need(columns/lines)");
                        return Size(Console.ReadLine(), file);
                    }
            }
        }

        //Функция для "красивого" вывода содержимого файла на экран.
        static void Print_to_me(string [,] array_from_file)
        {

            //Создаем список для хранения длин максимальных длин колонн.
            List<int> maximum_in_columns = Give_me_maximum_length(array_from_file);
            
            //Непосредственно вывод.
            for (int i = 0; i < array_from_file.GetLength(0); i++)
            {
                for (int j = 0; j < array_from_file.GetLength(1); j++)
                    Console.Write(" | {0,-" + maximum_in_columns[j] + "}",
                        array_from_file[i, j]);

                Console.WriteLine();
            }
        }

        //Функция для "красивого" вывода содержимого файла на экран.
        static void Print_to_me(string[,] array_from_file, int column_number)
        {
            //Создаем список для хранения длин максимальных длин колонн.
            List<int> maximum_in_columns = Give_me_maximum_length(array_from_file);

            //Непосредственно вывод.
            for (int i = 0; i < array_from_file.GetLength(0); i++)
            {
                for (int j = 0; j < array_from_file.GetLength(1); j++)
                {
                    //Окрасим колонну по которой выполнена сортировка.
                    if (j == column_number)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" | {0,-" + maximum_in_columns[j] + "}",
                            array_from_file[i, j]);
                        Console.ResetColor();
                    }
                    else
                        Console.Write(" | {0,-" + maximum_in_columns[j] + "}",
                            array_from_file[i, j]);
                }
                Console.WriteLine();
            }
        }

        //Функция для сортировки.
        static string[,] Sort_me(string[,] array_from_file
            ,List <string> list_from_column, int column_number)
        {
            //Кармашек.
            string pocket = "";

            //Сортируем сам массив.
            for (int number = 0; number < list_from_column.Count; number++)
            {
                for (int i = 0; i < array_from_file.GetLength(0); i++)
                    if (array_from_file[i, column_number] == list_from_column[number])
                    {
                        for (int j = 0; j < array_from_file.GetLength(1); j++)
                        {
                            pocket = array_from_file[number, j];
                            array_from_file[number, j] = array_from_file[i, j];
                            array_from_file[i, j] = pocket;
                        }
                    }
            }
            return array_from_file;
        }

        //Функция для чтения файла.
        public static string Read_me(string file_address)
        {
            /*Открываем, а после закрываем поток, читаем файл по полученному 
            * адресу и заносим в файл.*/

            StreamReader sr1 = new StreamReader(file_address);
            string file = sr1.ReadToEnd();
            sr1.Close();

            return file;
        }


        //Функция для чтения файла, адрес которого указан в "первом" аргументе.
        static string[,] I_am_parser(string argument)
        {
            //Работаем с поступившим аргументом.

            //Вызовем функцию для преобразования аргумента в полноценный адрес.
            string file_address = Give_me_address(argument);

            //Читаем файл.
            string file = Read_me(file_address);

            //Превращаем прочитанный файл в массив.

            //Получаем кол-во строк и столцов в файле.
            int number_of_lines = Size("lines", file);
            int number_of_columns = Size("columns", file);

            return Give_me_array(file, number_of_lines,
                number_of_columns);
        }

        //Доп. функция.
        static string[,] Additional_function_for_sort(string argument, int column_number)
        {
            //получим переработанный массив.
            string[,] array_from_file = I_am_parser(argument);

            //Список для хранения элементов выбранной колонны.

            List<string> list_from_column = new List<string>() { };

            //Добавим в список все элементы выбранной колонны.
            for(int i=0; i< array_from_file.GetLength(0); i++)
            {
                list_from_column.Add(array_from_file[i, column_number]);
            }

            //Сортируем список.
            list_from_column.Sort();

            //Возвращаем обработанный массив.
            return Sort_me(array_from_file, list_from_column,
                column_number);
        }

        //Главная функция.
        static void Main(string[] args)
        { 
            //Проверяем наличие "второго" аргумента.
            try
            {
                //Превращаем первый аргумент в строку.
                string argument = args[0];

                //Превращаем второй аргумент в строку.
                string second_argument = args[1];

                /*Объявляем и инициализируем переменную,
                которая будет содержать номер колонны для сортировки,
                путем вызова функции для его получения из "второго" аргумента*/

                int number_of_column = Give_me_columns_number(second_argument);

                //Отступим одну строчку.
                Console.WriteLine();

                //Сортируем массив по угодной нам колонке и выводим на экран.
                Print_to_me(Additional_function_for_sort(argument,
                    number_of_column - 1), number_of_column-1);
            }
            catch (IndexOutOfRangeException)
            { 
                //Проверяем наличие "первого" аргумента.
                try
                {
                    //Превращаем переданный аргумент в строку.
                    string argument = args[0];

                    //Отступим одну строчку.
                    Console.WriteLine();

                    /*Выведем содержимое файла на экран
                     * с помощью нашего парсера.*/

                    Print_to_me(I_am_parser(argument));
                }

                /*Если мы не предоставили программе файл, то
                 * она выведет сообщение об ошибке.*/
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("\n I'm sorry, pal." +
                        " Wrong address or broken file!");
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine("\n   What do you mean, baddy? Check your command" +
                      " and try again :)");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("\n   Where did you get this file? It's broken!");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("\n Help me! I'm blind. " +
                        "I don't see your file!");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n   What do you mean, baddy? Check your command" +
                  " and try again :)");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("\n   Where did you get this file? It's broken!");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("\n Help me! I'm blind. " +
                    "I don't see your file!");
            }
        }     
    }
}
