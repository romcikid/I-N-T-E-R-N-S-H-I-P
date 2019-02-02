using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Parser.Tests
{
    [TestClass()]
    public class ProgramTests
    {

        [TestMethod()]
        public void Give_me_addressTest()
        {
            //Создаем строку, имитирующую "второй" аргумент.
            string argument = "id=C:\file.csv";

            //Выделяем адрес из строки.
            string file_adress = Program.
                Give_me_address(argument);

            //Проверяем полученное и эталонное значения.
            Assert.AreEqual("C:\file.csv", file_adress);
        }

        [TestMethod()]
        public void Give_me_columns_numberTest()
        {
            //Создаем строку, имитирующую "второй" аргумент.
            string argument = "sort=3";

            //Вычисляем номер колонны.
            int number_of_column = Program.
                Give_me_columns_number(argument);

            //Проверяем полученное и эталонное значения.
            Assert.AreEqual(3, number_of_column);
        }

        [TestMethod()]
        public void Give_me_maximum_lengthTest()
        {
            //Создаем эталонный список.
            List<int> maximum_in_columns = new List<int>() { 9, 3, 7 };

            //Созданный массив, который выступит аргументом.
            string[,] array_for_example =
            {
                { "or", " ", "for" },
                { "what's up", "bro", "Oh, my!"}
            };

            //Получим список с помощью программы.
            List<int> maximum_from_program = Program.
                Give_me_maximum_length(array_for_example);

            //Проверим в цикле.
            for (int i = 0; i < maximum_in_columns.Count; i++)
            {
                Assert.AreEqual(maximum_in_columns[i],
                    maximum_from_program[i]);
            }
        }

        [TestMethod()]
        public void Read_meTest()
        {
            //Содержимое файла.
            string text_in_file = "Hello, Megan!";

            //Прочитаем файл.
            string text_from_file = Program.Read_me(@" Write your own address \test.txt");

            //Сравниваем значения.
            Assert.AreEqual(text_in_file, text_from_file);
        }
    }
}