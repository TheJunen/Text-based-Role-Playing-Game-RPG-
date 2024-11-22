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
        public int HP { get; set; } //må bruke properties og lære meg det. Les chatgpt historikk og yt videoer
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
            Level = level;
            AttackPower = attackPower;
            DefensePower = defensePower;
            Inventory = new List<Item>();
            ItemsEquipped = new List<Item>();
        }

        internal void EquipItemsFromInventory(MainCharacter mainCharacter) //siden listen ligger i MainCharacter, så lages metoden her
        {
            bool running = true;
            while (running)
            {
                PrintOutInventory();

                Console.WriteLine("Write 1 to equip nr 1 from list, 2 for nr 2 etc.");
                int answer = Convert.ToInt32(Console.ReadLine()) - 1;
                var item = Inventory[answer];
                Inventory.Remove(item);
                ItemsEquipped.Add(item);

                if (item is Healer)
                {
                    mainCharacter.HP += item.HP;
                }
                else if (item is Shield)
                {
                    mainCharacter.DefensePower += item.DefensePower;
                }
                else if (item is Sword)
                {
                    mainCharacter.AttackPower += item.AttackPower;
                }

                Console.WriteLine($"{Inventory[answer].Name} equipped. Write 1 to equip one more item, 2 to exit");

                string answerQuitMethod = Console.ReadLine();
                if (answerQuitMethod == "2")
                    running = false;
            }
        }

        internal void RemoveEquippedItems(MainCharacter mainCharacter) //removes chosen equipped items. You can only remove 1 items at once, but can run it mulitplie times.
        {
            bool running = true;
            while (running)
            {
                PrintOutEquippedItems();

                Console.WriteLine("Write 1 to remove nr 1 from list, 2 for nr 2 etc.");
                int answer = Convert.ToInt32(Console.ReadLine()) - 1;
                var item = ItemsEquipped[answer];
                ItemsEquipped.Remove(item);
                Inventory.Add(item);

                if (item is Healer)
                {
                    mainCharacter.HP += item.HP;
                }
                else if (item is Shield)
                {
                    mainCharacter.DefensePower += item.DefensePower;
                }
                else if (item is Sword)
                {
                    mainCharacter.AttackPower += item.AttackPower;
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
            foreach (var item in Inventory)
            {
                item.WriteOutInfo();
            }
        }

        internal void LevelUpChecker()
        {
            if (XP >= 10 && XP <= 19 && Level == 1)
            {
                StatsAdder(5);
                RunLevelUp();
                Console.WriteLine($"Congrats. {Name} is leveled up to {Level}. +5 in every stats added. ");
            }
            else if (XP >= 20 && XP <= 29 && Level == 2)
            {
                StatsAdder(10);
                RunLevelUp();
                Console.WriteLine($"Congrats. {Name} is leveled up to {Level}. +10 in every stats added");
            }
            else if (XP >= 30 && XP <= 39 && Level == 3)
            {
                StatsAdder(15);
                RunLevelUp();
                Console.WriteLine($"Congrats. {Name} is leveled up to {Level}. +15 in every stats added.");
            }
            else if (XP >= 40 && XP <= 49 && Level == 4)
            {
                StatsAdder(20);
                RunLevelUp();
                Console.WriteLine($"Congrats. {Name} is leveled up {Level}. +20 in every stats added.");
            }
        }

        private void StatsAdder(int stat)
        {
            HP += stat;
            AttackPower += stat;
            DefensePower += stat;
        }

        private void RunLevelUp()
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

    }
}
