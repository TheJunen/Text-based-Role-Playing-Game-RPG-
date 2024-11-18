using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRolePlayGame.Items
{
    internal class Sword : Item
    {
        public Sword(string name, int attackPower, int price, string description)
        {
            Name = name;
            AttackPower = attackPower;
            Price = price;
            Description = description;
        }

        internal override void WriteOutInfo()
        {
            Console.WriteLine($"Name: {Name}, Attack Power: {AttackPower}, Price: {Price}, Description: {Description}");
        }


    }
}
