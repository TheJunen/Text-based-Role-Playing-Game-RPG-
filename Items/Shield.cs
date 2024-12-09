using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextBasedRolePlayGame.Items
{
    internal class Shield : Item
    {

        public Shield(string name, int defensePower, int price, string description)
        {
            Name = name;
            DefensePower = defensePower;
            Price = price;
            Description = description;
        }

        internal override void WriteOutInfo()
        {
            Console.WriteLine($"Name: {Name}, Defense Power: {DefensePower}, Price: {Price} coins, Description: {Description}");
        }


    }
}
