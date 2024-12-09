/*Tekstbasert Rollespill (RPG)
Lag et enkelt RPG-spill der spilleren har en karakter som kan gå på oppdrag, 
samle erfaring og forbedre ferdigheter basert på valg og interaksjoner.
Min definisjon av spillet:
 * Tekstbasert Rollespill (RPG) 
 * console app spill med en hovedperson som går igjennom forskjellige bosskamper og tjener coins og kan kjøpe i butikken
 * og får progresjon på historie % etter å slått bossen.
 * Får xp lvl etter å ha slått bane. Hvis du taper bane så mister du 50% penger eller xp
 * kan kjøpe sverd
 * Enkel form og grunnleggende funksjoner, bruk avanserte konsepter der det gir mening (se yt og internett)
*/
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using TextBasedRolePlayGame.Items;
namespace TextBasedRolePlayGame
{
    class Program //lage mer utfordrende bosser
    {
        public static int counter { get; private set; } = 0;
        static void Main(string[] args)
        {
            //starts Gametime Counter in a new thread
            Thread counterThread = new Thread(CounterGametime);
            counterThread.IsBackground = true; //makes it a background thread so counter will stop when main program stops
            counterThread.Start();

            MainCharacter mainCharacter = new MainCharacter(100, 1, 10, 1);
            Boss bossLvl1 = new Boss("Boss lvl 1", 120, 1, 2, 1, "Lvl 1 boss");
            Boss bossLvl2 = new Boss("Boss lvl 2", 300, 2, 4, 2, "Lvl 2 boss");
            Boss bossLvl3 = new Boss("Boss lvl 3", 450, 3, 6, 3, "Lvl 3 boss");

            Shop shop = new Shop();
            GameLogic gameLogic = new GameLogic();

            bool runningGame = true;

            while (runningGame)
            {
                Console.WriteLine("Hello and welcome to Text-based Role Playing Game. This consists of 3 bosses ranging from lvl 1-3. " +
                    "Each time you defeat a boss for the first time it will add to your story progress bar. You can continue to fight previous defeated bosses to gain rewards.");
                Console.WriteLine("You can buy items from the shop and the items will be added to your inventory. While you are not fighting a boss, you can visit the shop and customise your characters equioment");
                Console.WriteLine();
                Console.WriteLine("Let's get started with your characters name:");
                mainCharacter.Name = Console.ReadLine();
                Console.WriteLine($"Nice to meet you, {mainCharacter.Name}");

                Console.WriteLine($"{mainCharacter.Name} starting stats:");

                mainCharacter.PrintOutStats();


                bool runningMenu = true;
                bool gameCompletionMessageShown = false;

                while (runningMenu)
                {
                    if (gameLogic.StoryPercentBar == 100 && gameCompletionMessageShown == false)
                    {
                        Console.WriteLine("Congratulations! You have completed the game!");
                        CounterGameTimeGameCompletion();
                        Console.WriteLine();
                        Console.WriteLine("Write 'finish' to exit the game or 'continue' to keep playing the game.");

                        gameCompletionMessageShown = true;
                        bool validChoice = false;
                        string choice = "";
                        while (!validChoice)
                        {
                            choice = Console.ReadLine().ToLower();

                            if (choice == "finish")
                            {
                                validChoice = true;
                                runningGame = false;
                                Console.WriteLine("You have chosen to end the game. Quitting game now..");
                                break;
                            }
                            else if (choice == "continue")
                            {
                                validChoice = true;
                                Console.WriteLine("You have chosen to continue to play. Going back to the menu now..");
                            }
                        }
                        if (choice == "finish")
                        {
                            break;
                        }
                    }

                    double storyPercentBar = gameLogic.StoryPercentBar;
                    double roundedValueStoryPercentBar = Math.Round(storyPercentBar, 2);

                    Console.WriteLine();
                    Console.WriteLine($"Story percent bar: {roundedValueStoryPercentBar} %");
                    Console.WriteLine($"{mainCharacter.Name} currently coins: {mainCharacter.Coins}");

                    Console.WriteLine($"Welcome to the menus. Write 1 to visit the shop, 2 to see inventory, 3 to choose to customise your characters equipment," +
                        $" 4 to see {mainCharacter.Name} stats, 5 to see added stats from statsupgrade, 6 to fight bosses or 7 to quit the game.");

                    switch (Console.ReadLine()) //menu with 7 options
                    {
                        case "1":
                            shop.VisitShop(mainCharacter); //visits the shop
                            break;
                        case "2":
                            mainCharacter.PrintOutInventory(); //prints out the inventory
                            break;
                        case "3": //customise main character's equipments
                            Console.WriteLine("Write 1 to equip items from inventory or 2 to remove equipped items");

                            switch (Console.ReadLine())
                            {
                                case "1":
                                    mainCharacter.EquipItemsFromInventory();
                                    break;
                                case "2":
                                    mainCharacter.RemoveEquippedItems();
                                    break;
                                default:
                                    Console.WriteLine("Error. Write 1 or 2.");
                                    break;
                            }
                            break;
                        case "4": //prints out main character's stats
                            mainCharacter.PrintOutStats();
                            break;
                        case "5": //checks and prints out main character's upgraded stats through shop's stats upgrade
                            shop.CheckerStatsUpgrade();
                            break;
                        case "6": //boss fight menu for fighting previous or new bosses
                            bool runningBossMenu = true;

                            while (runningBossMenu)
                            {
                                Console.WriteLine("Write 1 to check the possibility to fight previously defeated bosses, 2 to fight new bosses or 3 to go back to the menu");

                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        if (gameLogic.defeatedBossesList.Count > 0)
                                        {
                                            Console.WriteLine("You have the possibility to fight these previously defeated bosses:");

                                            foreach (var boss in gameLogic.defeatedBossesList)
                                            {
                                                Console.WriteLine(boss.Name);
                                            }
                                            Console.WriteLine("Write 1 to continue or 2 to exit back to boss menu.");

                                            switch (Console.ReadLine())
                                            {
                                                case "1":
                                                    Console.WriteLine("You have chosen to fight. Write 1 to fight nr 1 from list, 2 for nr2 etc.");

                                                    bool correctChoiceInput = false;
                                                    int choiceItem = 0;

                                                    while (!correctChoiceInput)
                                                    {
                                                        if (int.TryParse(Console.ReadLine(), out choiceItem) && choiceItem <= gameLogic.defeatedBossesList.Count)
                                                        {
                                                            choiceItem -= 1;
                                                            correctChoiceInput = true;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Error. Write a valid number that are in the list range.");
                                                        }
                                                    }

                                                    gameLogic.BossFightSimulator(mainCharacter, gameLogic.defeatedBossesList[choiceItem]);
                                                    break;
                                                default:
                                                    Console.WriteLine("Returning back to boss menu");
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("No bosses has been defeated yet. Try to fight a new boss.");
                                        }
                                        break;
                                    case "2":
                                        if (bossLvl1.Defeated == false)
                                        {
                                            Console.WriteLine($"You have not defeated {bossLvl1.Name}. Write enter to continue.");
                                            Console.ReadLine();
                                            gameLogic.BossFightSimulator(mainCharacter, bossLvl1);
                                        }
                                        else if (bossLvl2.Defeated == false)
                                        {
                                            Console.WriteLine($"You have not defeated {bossLvl2.Name}. Write enter to continue.");
                                            Console.ReadLine();
                                            gameLogic.BossFightSimulator(mainCharacter, bossLvl2);
                                        }
                                        else
                                        {
                                            Console.WriteLine($"You have not defeated {bossLvl3.Name}. Write enter to continue.");
                                            Console.ReadLine();
                                            gameLogic.BossFightSimulator(mainCharacter, bossLvl3);
                                        }
                                        break;
                                    case "3":
                                        Console.WriteLine("Going back to the menu");
                                        runningBossMenu = false;
                                        break;
                                    default:
                                        Console.WriteLine("Error. Write 1,2 or 3.");
                                        break;
                                }
                            }
                            break;
                        case "7": //quit game option and verification check that ask you one more time if you want to quit 
                            Console.WriteLine("Are you sure you want to quit the game? You will lose all progress. Write 'quit' to quit the game or 'continue' to continue the game.");

                            bool validChoice = false;

                            while (!validChoice)
                            {
                                string choiceItem = Console.ReadLine().ToLower();
                                
                                if (choiceItem == "quit")
                                {
                                    runningMenu = false;
                                    runningGame = false;
                                    validChoice = true;
                                    Console.WriteLine("You have chosen to quit the game. Quitting game now.");
                                    break;
                                }
                                else if (choiceItem == "continue")
                                {
                                    validChoice = true;
                                    Console.WriteLine("You have chosen to continue the game. Going back to the menu.");
                                }
                            }
                            break;
                        default:
                            Console.WriteLine("Error. Write 1, 2 or 3.");
                            break;
                    }
                }
            }
        }

        static void CounterGametime() //counts the game time each minute
        {
            int minuteCounter = 0;

            while (true)
            {
                if (counter % 60 == 0 && counter > 0)
                {
                    minuteCounter++;
                    Console.WriteLine($"Counter: {minuteCounter} minutes played");
                }
                Thread.Sleep(1000);
                counter++;
            }
        }

        static void CounterGameTimeGameCompletion()
        {
            int minuteCounter = counter / 60;

            Console.WriteLine($"Counter for completed game: {minuteCounter} minutes played");
        }
    }
}