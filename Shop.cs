using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRolePlayGame.Items;

namespace TextBasedRolePlayGame
{
    internal class Shop
    {
        public List<Item> shopItems { get; private set; } = new List<Item>
        {
                new Sword("Sword lvl 1", 10, 10, "Level 1 sword"),
                new Shield("Shield lvl 1", 10, 10, "Level 1 shield"),
                new Healer("Healer lvl 1", 10, 10, "Level 1 healer")
        };

        internal void VisitShop(MainCharacter mainCharacter)
        {
            bool doneVisitShop = false;

            while (!doneVisitShop)
            {
                Console.WriteLine("Welcome to the shop.");
                Console.WriteLine("Items in the shop:");
                foreach (Item item in shopItems)
                {
                    item.WriteOutInfo();
                }

                Console.WriteLine($"Main character's money: {mainCharacter.Coins}");

                Console.WriteLine("Write 1 to buy nr 1 from list, 2 for nr 2 etc");

                int choice = int.Parse(Console.ReadLine()) - 1;

                if (shopItems[choice].Price <= mainCharacter.Coins)
                {
                    mainCharacter.Coins -= shopItems[choice].Price;
                    mainCharacter.AddItemToInventory(shopItems[choice]);
                    Console.WriteLine($"The buy of {shopItems[choice].Name} was successful and {shopItems[choice].Price} was deducted." +
                        $" Item {shopItems[choice].Name} was added to {mainCharacter.Name}'s inventory. The balance is {mainCharacter.Coins}.");
                }

                Console.WriteLine("Write 1 to continue shopping, 2 to quit");

                if (Console.ReadLine() == "2")
                {
                    doneVisitShop = true;
                    Console.WriteLine("Exited shop");
                }
            }
        }
    }
}
