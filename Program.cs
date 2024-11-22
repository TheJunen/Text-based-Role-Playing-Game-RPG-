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
using System.Runtime.CompilerServices;
using TextBasedRolePlayGame.Items;
namespace TextBasedRolePlayGame
{
    class Program
    {
        static void Main(string[] args)
        {
            MainCharacter mainCharacter = new MainCharacter(100, 1, 10, 10);
            Boss bossLvl1 = new Boss("Boss lvl 1", 200, 1, 2, 1, "Lvl 1 boss");
            Boss bossLvl2 = new Boss("Boss lvl 2", 300, 2, 4, 2, "Lvl 2 boss");
            Boss bossLvl3 = new Boss("Boss lvl 3", 400, 3, 6, 3, "Lvl 3 boss");

            Shop shop = new Shop();
            GameLogic gameLogic = new GameLogic();

            List<Boss> defeatedBossesList = new List<Boss>();

            bool runningGame = true;

            while (runningGame)
            {
                Console.WriteLine("Hello and welcome to Text-based Role Playing Game. This consists of 3 bosses ranging from lvl 1-3. " +
                    "Each time you defeat a boss for the first time it will add to your story progress bar. You can can continue to fight previous defeated bosses to gain rewards.");
                Console.WriteLine("You can buy items from the shop and the items will be added to your inventory. while you are not fighting a boss, you can visit the shop and customise your characters equioment");
                Console.WriteLine();
                Console.WriteLine("Let's get started with your characters name:");
                mainCharacter.Name = Console.ReadLine();
                Console.WriteLine($"Nice to meet you, {mainCharacter.Name}");

                bool runningMenu = true;

                while (runningMenu)
                {
                    Console.WriteLine("Welcome to the menus. Write 1 to visit the shop, 2 to see inventory and choose to customise your characters equipment or 3 to fight bosses.");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            shop.VisitShop(mainCharacter);
                            break;
                        case "2":
                            Console.WriteLine("Write 1 to equip items from inventory or 2 to remove equipped items");

                            switch (Console.ReadLine())
                            {
                                case "1":
                                    mainCharacter.EquipItemsFromInventory(mainCharacter);
                                    break;
                                case "2":
                                    mainCharacter.RemoveEquippedItems(mainCharacter);
                                    break;
                                default:
                                    Console.WriteLine("Error. Write 1 or 2.");
                                    break;
                            }
                            break;
                        case "3":
                            bool runningBossMenu = true;

                            while (runningBossMenu)
                            {
                                Console.WriteLine("Write 1 to check the possibility to fight previously defeated bosses, 2 to fight new bosses or 3 to go back to the menu");

                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        if (defeatedBossesList.Count > 0)
                                        {
                                            Console.WriteLine("You have the possibility to fight these previously defeated bosses:");

                                            foreach (var boss in defeatedBossesList)
                                            {
                                                Console.WriteLine(boss.Name);
                                            }
                                            Console.WriteLine("Write 1 to continue or 2 to exit back to boss menu.");

                                            switch (Console.ReadLine())
                                            {
                                                case "1":
                                                    Console.WriteLine("You have chosen to fight. Write 1 to fight nr 1 from list, 2 for nr2 etc.");
                                                    int choice = int.Parse(Console.ReadLine()) - 1;
                                                    gameLogic.BossFightSimulator(mainCharacter, defeatedBossesList[choice]);
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
                                            Console.WriteLine($"You have not defeated {bossLvl1.Name}. Fight starts..");
                                            gameLogic.BossFightSimulator(mainCharacter, bossLvl1);
                                        }
                                        else if (bossLvl2.Defeated == false)
                                        {
                                            Console.WriteLine($"You have not defeated {bossLvl2.Name}. Fight starts..");
                                            gameLogic.BossFightSimulator(mainCharacter, bossLvl2);
                                        }
                                        else
                                        {
                                            Console.WriteLine($"You have not defeated {bossLvl3.Name}. Fight starts..");
                                            gameLogic.BossFightSimulator(mainCharacter, bossLvl3);
                                        }
                                        break;
                                    case "3":
                                        runningBossMenu = false;
                                        break;
                                    default:
                                        Console.WriteLine("Error. Write 1,2 or 3.");
                                        break;
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
    }
}