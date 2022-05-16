using System;
using System.Collections.Generic;
using System.IO;

namespace Supermarket
{
    internal class Program
    {
        static Dictionary<string, string> AddGoods(string name, char delimiter)
        {
            Dictionary<string, string> goodPrice = new Dictionary<string, string>();

            using (StreamReader sr = new StreamReader(name))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] keyvalye = line.Split(delimiter);
                    if (keyvalye.Length == 2)
                    {
                        goodPrice.Add(keyvalye[0], keyvalye[1]);
                    }
                }
            }

            return goodPrice;
        }

        static void Main(string[] args)
        {
            Dictionary<string, string> readGoodsOfFile;
            string name = "Goods.txt";
            char delimiter = '=';

            Supermarket supermarket = new Supermarket();
            var isOpen = true;

            readGoodsOfFile = AddGoods(name, delimiter);
            foreach (var item in readGoodsOfFile)
            {
                var valuePrice = Convert.ToInt32(item.Value);
                supermarket.AddGood(item.Key, valuePrice);
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Для начала работы, необходимо создать очередь.");
            Console.ResetColor();
            Console.WriteLine();
            supermarket.CreateQueue();
            Console.Clear();


            while (isOpen)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Администрирование магазина");
                Console.ResetColor();
                Console.WriteLine();
                supermarket.ShowInfo();

                Console.CursorVisible = false;
                supermarket.Sell();

                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
}
