using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TextBasedRolePlayGame.Items;

namespace TextBasedRolePlayGame
{
    internal class Shop
    {
        public int NumberAttackStatUpgrade { get; private set; }
        public int NumberDefenseStatUpgrade { get; private set; }
        public int NumberHPStatUpgrade { get; private set; }

        public List<Item> shopItems { get; private set; } = new List<Item>
        {
                new Sword("Sword lvl 1", 10, 20, "Level 1 sword"),
                new Shield("Shield lvl 1", 10, 20, "Level 1 shield"),
                new Healer("Healer lvl 1", 10, 20, "Level 1 healer")
        };

        internal void VisitShop(MainCharacter mainCharacter)
        {
            bool doneVisitShop = false;

            while (!doneVisitShop)
            {
                Console.WriteLine("Welcome to the shop.");

                Console.WriteLine("Write 1 to shop items in shop, 2 to shop for stats upgrade or 3 to exit shop");

                var choiceMenu = Console.ReadLine();

                if (choiceMenu == "1") //main menu for shop items and stats upgrade
                {
                    Console.WriteLine("Items in the shop:");
                    foreach (Item item in shopItems)
                    {
                        item.WriteOutInfo();
                    }

                    Console.WriteLine($"{mainCharacter.Name}'s coins: {mainCharacter.Coins}");

                    Console.WriteLine("Write 1 to buy nr 1 from list, 2 for nr 2 etc");

                    bool correctChoiceInput = false;
                    int choiceItem = 0;

                    while (!correctChoiceInput)
                    {
                        if (int.TryParse(Console.ReadLine(), out choiceItem))
                        {
                            choiceItem -= 1;
                            correctChoiceInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Error. Write a valid number");
                        }

                    }


                    if (shopItems[choiceItem].Price <= mainCharacter.Coins)
                    {
                        mainCharacter.Coins -= shopItems[choiceItem].Price;
                        mainCharacter.AddItemToInventory(shopItems[choiceItem]);
                        Console.WriteLine($"The buy of {shopItems[choiceItem].Name} was successful and {shopItems[choiceItem].Price} was deducted." +
                            $" Item {shopItems[choiceItem].Name} was added to {mainCharacter.Name}'s inventory. The balance is {mainCharacter.Coins}.");
                    }
                    else
                    {
                        Console.WriteLine($"{mainCharacter.Name} don't have enough coins. Balance is {mainCharacter.Coins} coins. {shopItems[choiceItem].Name} cost {shopItems[choiceItem].Price} coins.");
                    }
                }
                else if (choiceMenu == "2") //stats upgrade
                {
                    bool runningStatsUpgradeMenu = true;
                    while (runningStatsUpgradeMenu)
                    {
                        Console.WriteLine("The attack, defense and HP upgrades each adds +1 stat to their respective categories. Each upgrade costs 2 coins.");
                        Console.WriteLine("Write 1 to buy Attack upgrade, 2 to buy Defense upgrade, 3 to buy HP upgrade or 4 to return to shop menu");

                        var choiceStatsUpgradeMenu = Console.ReadLine();

                        if (choiceStatsUpgradeMenu == "1")
                        {
                            Console.WriteLine("Write how many you want to buy of attack upgrade");
                            int amountAttackUpgrade = 0;
                            bool validAmountAnswer = false;

                            while (!validAmountAnswer)
                            {
                                string choice = Console.ReadLine();

                                if (int.TryParse(choice, out amountAttackUpgrade) && mainCharacter.Coins >= amountAttackUpgrade * 2)
                                {
                                    validAmountAnswer = true;
                                }
                                else
                                {
                                    Console.WriteLine("Error. Write a number and within your coins budget.");
                                }
                            }
                            if (mainCharacter.Coins >= 2 * amountAttackUpgrade)
                            {
                                mainCharacter.Coins -= 2 * amountAttackUpgrade;
                                AttackUpgrade(mainCharacter, amountAttackUpgrade);
                                Console.WriteLine($"Attack Upgrade purchased {amountAttackUpgrade} times");
                            }
                            else
                            {
                                Console.WriteLine($"{mainCharacter.Name} don't have enough coins");
                            }
                        }
                        else if (choiceStatsUpgradeMenu == "2")
                        {
                            Console.WriteLine("Write how many you want to buy of defense upgrade");
                            int amountDefenseUpgrade = 0;
                            bool validAmountAnswer = false;

                            while (!validAmountAnswer)
                            {
                                string choice = Console.ReadLine();

                                if (int.TryParse(choice, out amountDefenseUpgrade) && mainCharacter.Coins >= amountDefenseUpgrade * 2)
                                {
                                    validAmountAnswer = true;
                                }
                                else
                                {
                                    Console.WriteLine("Error. Write a number and within your coins budget.");
                                }
                            }
                            if (mainCharacter.Coins >= 2 * amountDefenseUpgrade)
                            {
                                mainCharacter.Coins -= 2 * amountDefenseUpgrade;
                                DefenseUpgrade(mainCharacter, amountDefenseUpgrade);
                                Console.WriteLine($"Defense Upgrade purchased {amountDefenseUpgrade} times");
                            }
                            else
                            {
                                Console.WriteLine($"{mainCharacter.Name} don't have enough coins");
                            }
                        }
                        else if (choiceStatsUpgradeMenu == "3")
                        {
                            Console.WriteLine("Write how many you want to buy of HP upgrade");
                            int amountHPUpgrade = 0;
                            bool validAmountAnswer = false;

                            while (!validAmountAnswer)
                            {
                                string choice = Console.ReadLine();

                                if (int.TryParse(choice, out amountHPUpgrade) && mainCharacter.Coins >= amountHPUpgrade * 2)
                                {
                                    validAmountAnswer = true;
                                }
                                else
                                {
                                    Console.WriteLine("Error. Write a number and within your coins budget.");
                                }
                            }
                            if (mainCharacter.Coins >= 2 * amountHPUpgrade)
                            {
                                mainCharacter.Coins -= 2 * amountHPUpgrade;
                                HPUpgrade(mainCharacter, amountHPUpgrade);
                                Console.WriteLine($"HP Upgrade purchased {amountHPUpgrade} times");
                            }
                            else
                            {
                                Console.WriteLine($"{mainCharacter.Name} don't have enough coins");
                            }
                        }
                        else if (choiceStatsUpgradeMenu == "4")
                        {
                            runningStatsUpgradeMenu = false;
                            Console.WriteLine("Returning back to shop menu");
                        }
                    }
                }
                else if (choiceMenu == "3")
                {
                    doneVisitShop = true;
                    break;
                }

                Console.WriteLine("Write 1 to continue shopping, 2 to quit");

                if (Console.ReadLine() == "2")
                {
                    doneVisitShop = true;
                    Console.WriteLine("Exited shop");
                }
            }
        }

        private void AttackUpgrade(MainCharacter mainCharacter, int amount) //adds specifed amount to ättack stats
        {
            mainCharacter.AttackPower += amount;
            NumberAttackStatUpgrade += amount;

            Console.WriteLine($"+{amount} attack was added to {mainCharacter.Name}");
        }
        private void DefenseUpgrade(MainCharacter mainCharacter, int amount) //adds specifed amount to defense stats
        {
            mainCharacter.DefensePower += amount;
            NumberDefenseStatUpgrade += amount;

            Console.WriteLine($"+{amount} defense was added to {mainCharacter.Name}");
        }

        private void HPUpgrade(MainCharacter mainCharacter, int amount) //adds specifed amount to HP stats
        {
            mainCharacter.OriginalHP += amount;
            NumberHPStatUpgrade += amount;

            Console.WriteLine($"+{amount} HP was added to {mainCharacter.Name}");
        }

        internal void CheckerStatsUpgrade() //Prints out added stats on all upgradeable categories
        {
            Console.WriteLine($"You have added +attack stat: {NumberAttackStatUpgrade}, +defense stat: {NumberDefenseStatUpgrade} and +HP stat: {NumberHPStatUpgrade}");
        }
    }
}
