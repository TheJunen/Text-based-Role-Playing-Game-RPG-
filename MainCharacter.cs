using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TextBasedRolePlayGame.Items;

namespace TextBasedRolePlayGame
{
    internal class MainCharacter
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int OriginalHP { get; set; }
        public int Level { get; private set; }
        public int AttackPower { get; set; }
        public int DefensePower { get; set; }
        public int Coins { get; set; }
        public int XP { get; set; }
        public List<Item> Inventory { get; private set; }
        public List<Item> ItemsEquipped { get; private set; }


        public MainCharacter(int hp, int level, int attackPower, int defensePower)
        {
            HP = hp;
            OriginalHP = HP;
            Level = level;
            AttackPower = attackPower;
            DefensePower = defensePower;
            Inventory = new List<Item>();
            ItemsEquipped = new List<Item>();
        }

        internal void EquipItemsFromInventory() //equip chosen inventory items. You can only equip 1 items at once, but can run it mulitplie times.
        {
            bool running = true;
            while (running)
            {
                PrintOutInventory();

                Console.WriteLine("Write 1 to equip nr 1 from list, 2 for nr 2 etc.");

                bool validAnswer = false;
                int answer = 0;

                while (!validAnswer)
                {
                    if (int.TryParse(Console.ReadLine(), out answer) && answer <= Inventory.Count)
                    {
                        answer -= 1;
                        validAnswer = true;
                    }
                    else
                    {
                        Console.WriteLine("Error. Must be a number and within the numbers of items in inventory");
                    }
                }
                var item = Inventory[answer];
                Inventory.Remove(item);
                ItemsEquipped.Add(item);

                if (item is Healer)
                {
                    HP += item.HP;
                }
                else if (item is Shield)
                {
                    DefensePower += item.DefensePower;
                }
                else if (item is Sword)
                {
                    AttackPower += item.AttackPower;
                }

                Console.WriteLine($"{item.Name} equipped. Write 1 to equip one more item, 2 to exit");

                string answerQuitMethod = Console.ReadLine();
                if (answerQuitMethod == "2")
                    running = false;
            }
        }

        internal void RemoveEquippedItems() //removes chosen equipped items. You can only remove 1 items at once, but can run it mulitplie times.
        {
            bool running = true;
            while (running)
            {
                PrintOutEquippedItems();

                Console.WriteLine("Write 1 to remove nr 1 from list, 2 for nr 2 etc.");

                bool validAnswer = false;
                int answer = 0;

                while (!validAnswer)
                {
                    if (int.TryParse(Console.ReadLine(), out answer) && answer <= ItemsEquipped.Count)
                    {
                        answer -= 1;
                        validAnswer = true;
                    }
                    else
                    {
                        Console.WriteLine("Error. Must be a number and within the number of items in inventory.");
                    }
                }
                var item = ItemsEquipped[answer];

                ItemsEquipped.Remove(item);
                Inventory.Add(item);

                if (item is Healer)
                {
                    HP -= item.HP;
                }
                else if (item is Shield)
                {
                    DefensePower -= item.DefensePower;
                }
                else if (item is Sword)
                {
                    AttackPower -= item.AttackPower;
                }

                Console.WriteLine($"{item.Name} removed. Write 1 to remove one more item, 2 to exit");

                string answerQuitMethod = Console.ReadLine();
                if (answerQuitMethod == "2")
                    running = false;
            }
        }

        internal void AddItemToInventory(Item item)
        {
            Inventory.Add(item);
        }

        internal void PrintOutInventory()
        {
            Console.WriteLine("Printing out inventory:");
            foreach (var item in Inventory)
            {
                item.WriteOutInfo();
            }
        }

        internal void LevelUpChecker() //checks the xp and levels up if meets the requirements
        {
            if (XP >= 10 && XP <= 19 && Level == 1)
            {
                Console.WriteLine();
                StatsAdder(5);
                RunLevelUp();
                Console.WriteLine($"Congrats. {Name} is leveled up to {Level}. +5 in every stats added. Press enter to continue");
                Console.ReadLine();
            }
            else if (XP >= 20 && XP <= 29 && Level == 2)
            {
                Console.WriteLine();
                StatsAdder(5);
                RunLevelUp();
                Console.WriteLine($"Congrats. {Name} is leveled up to {Level}. +5 in every stats added. Press enter to continue");
                Console.ReadLine();
            }
            else if (XP >= 30 && XP <= 39 && Level == 3)
            {
                Console.WriteLine();
                StatsAdder(10);
                RunLevelUp();
                Console.WriteLine($"Congrats. {Name} is leveled up to {Level}. +10 in every stats added. Press enter to continue");
                Console.ReadLine();
            }
            else if (XP >= 40 && XP <= 49 && Level == 4)
            {
                Console.WriteLine();
                StatsAdder(10);
                RunLevelUp();
                Console.WriteLine($"Congrats. {Name} is leveled up {Level}. +10 in every stats added. Press enter to continue");
                Console.ReadLine();
            }
            else if (XP >= 50 && XP <= 59 && Level == 5)
            {
                Console.WriteLine();
                StatsAdder(10);
                RunLevelUp();
                Console.WriteLine($"Congrats. {Name} is leveled up {Level}. +10 in every stats added. Press enter to continue");
                Console.ReadLine();
            }
            else if (XP >= 60 && XP <= 69 && Level == 6)
            {
                Console.WriteLine();
                StatsAdder(10);
                RunLevelUp();
                Console.WriteLine($"Congrats. {Name} is leveled up {Level}. +10 in every stats added. Press enter to continue");
                Console.ReadLine();
            }
        }

        private void StatsAdder(int stat) //increases hp, attack and defense by the given value
        {
            AttackPower += stat;
            OriginalHP += stat;
            DefensePower += stat;
        }

        private void RunLevelUp() //adds 1 to the level stat
        { 
            Level += 1;
        }

        internal void PrintOutEquippedItems()
        {
            Console.WriteLine("Items equipped:");
            foreach (var item in ItemsEquipped)
            {
                item.WriteOutInfo();
            }
        }

        internal void PrintOutStats()
        {
            Console.WriteLine($"{Name}'s stats: Attack power: {AttackPower}, Defense power: {DefensePower}, HP: {HP}, Level: {Level} and XP: {XP}");
        }
    }
}
