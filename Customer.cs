using System;
using System.Collections.Generic;

namespace Supermarket
{
    internal class Customer
    {
        private List<Goods> _cart = new List<Goods>();
        private int _customerMoney;
        public int CCountMoney { get; private set; } = 0;

        public Customer(int money, List<Goods> cart)
        {
            _customerMoney = money;
            foreach (var item in cart)
            {
                _cart.Add(item);
            }
        }

        public void ShowMoney()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Денег у покупателя: {_customerMoney}");
            Console.ResetColor();
            Console.WriteLine();
        }

        public bool CountMoney()
        {
            CCountMoney = 0;
            foreach (var item in _cart)
            {
                CCountMoney += item.GoodPrice;
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Итог: " + CCountMoney);
            Console.ResetColor();
            if (CCountMoney <= _customerMoney)
            {
                return true;
            }

            else return false;
        }

        public bool DeleteGood()
        {
            if (_cart.Count >= 2)
            {
                _cart.RemoveAt(1);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ShowCart()
        {
            Console.WriteLine("У покупателя в тележке:");
            foreach (var item in _cart)
            {
                Console.WriteLine($"{item.GoodName}: {item.GoodPrice} денег");
            }
        }
    }
}
