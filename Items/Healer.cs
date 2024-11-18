using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRolePlayGame.Items
{
    internal class Healer : Item
    {
        public Healer(string name, int hp, int price, string description)
        {
            Name = name;
            HP = hp;
            Price = price;
            Description = description;
        }

        internal override void WriteOutInfo()
        {
            Console.WriteLine($"Name: {Name}, HP: {HP}, Price: {Price}, Description: {Description}");
        }
    }
}
