using System;
using System.Collections.Generic;

namespace Supermarket
{
    internal class Supermarket
    {
        private List<Goods> _market = new List<Goods>();
        private Queue<Customer> _customerQueue = new Queue<Customer>();
        private List<Goods> _cart = new List<Goods>();
        private int _money = 0;
        private bool _isAdd = true;
        private int _j = 0;
        private bool _success;
        private int _check = 0;


        public void AddGood(string name, int price)
        {
            _market.Add(new Goods(name, price));
        }

        public void ShowInfo()
        {
            Console.WriteLine("В кассе: " + _money + " деняг");
            Console.WriteLine("В очереди " + _customerQueue.Count + " покупателей");
            Console.WriteLine();
        }

        public void CreateQueue()
        {
            while (_isAdd)
            {
                Console.WriteLine();
                Console.WriteLine("1. Добавить покупателя.\n2. Начать администрирование кафе.");
                Console.Write("Введите номер команды: ");
                var putUser = Console.ReadLine();
                Console.WriteLine();

                switch (putUser)
                {
                    case "1":
                        Console.WriteLine("В супермаркете есть:");
                        foreach (var item in _market)
                        {
                            Console.WriteLine($"{item.GoodName}: {item.GoodPrice}");
                        }

                        Console.WriteLine();
                        Console.Write("Введите название товара, который хотите добавить или напишите \"стоп\": ");
                        var NameOfGood = Console.ReadLine();

                        while (NameOfGood.ToLower() != "стоп")
                        {
                            foreach (var item in _market)
                            {
                                if (NameOfGood.ToLower() == item.GoodName.ToLower())
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    _cart.Add(new Goods(NameOfGood, item.GoodPrice));
                                    Console.WriteLine("Товар добавлен с список покупок.");
                                    Console.ResetColor();
                                    _j = 1;
                                }
                            }
                            if (_j == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Такого товара не существует.");
                                Console.ResetColor();
                            }
                            Console.WriteLine();
                            Console.Write("Введите название товара, который хотите добавить: ");
                            NameOfGood = Console.ReadLine();
                            _j = 0;
                        }

                        while (_check == 0) {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("Введите количество денег у покупателя: ");
                            putUser = Console.ReadLine().Replace(" ", "");
                            if (!String.IsNullOrEmpty(putUser))
                            {
                                if (Int32.TryParse(putUser, out int customerMoney) && customerMoney >= 0)
                                {
                                    _customerQueue.Enqueue(new Customer(customerMoney, _cart));
                                    _cart.Clear();
                                    _check = 1;
                                }
                                else
                                {
                                    Console.ResetColor();
                                    Console.WriteLine("Вы ввели что-то не то.");
                                }
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ResetColor();
                                Console.WriteLine("Вы ничего не ввели.");
                            }
                            Console.ResetColor();
                            _j = 0;
                        }
                        _check = 0;
                        break;

                    case "2":
                        _isAdd = false;
                        break;

                    default:
                        Console.WriteLine("Вы ввели что-то не то.");
                        break;
                }
            }
        }

        public void Sell()
        {
            if (_customerQueue.Count != 0)
            {
                _customerQueue.Peek().ShowMoney();
                _customerQueue.Peek().ShowCart();
                _success = _customerQueue.Peek().CountMoney();

                while (!_success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Необходимо убрать один товар");
                    Console.ResetColor();
                    Console.WriteLine();

                    var b = _customerQueue.Peek().DeleteGood();
                    if (b)
                    {
                        _customerQueue.Peek().ShowCart();
                        _success = _customerQueue.Peek().CountMoney();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("У покупателя не хватает денег. Он будет пропущен");
                        Console.ResetColor();
                        _customerQueue.Dequeue();
                        _success = true;
                        _check = -1;
                    }
                }

                if (_check == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Товары успешно оплачены");
                    Console.ResetColor();
                    _money += _customerQueue.Peek().CCountMoney;
                    _customerQueue.Dequeue();
                    _success = true;
                }
                _check = 0;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("В очереди не осталось покупателей");
                Console.ResetColor();
            }
        }
    }
}
