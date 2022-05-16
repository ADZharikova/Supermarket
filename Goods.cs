using System;

namespace Supermarket
{
    internal class Goods
    {
        public string GoodName { get; private set; }
        public int GoodPrice { get; private set; }

        public Goods(string name, int prace)
        {
            GoodPrice = prace;
            GoodName = name;
        }
    }
}
