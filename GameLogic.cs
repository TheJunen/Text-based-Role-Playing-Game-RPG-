using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRolePlayGame
{
    internal class GameLogic
    {
        public double StoryPercentBar { get; set; }
        public int numberOfBosses { get; set; } = 3;

        public List<Boss> defeatedBossesList { get; private set; } = new List<Boss>();


        internal void BossFightSimulator(MainCharacter mainCharacter, Boss boss) //Method for fighting a boss
        {
            int fightNumber = 1;
            bool runningPrefightPreparing = true;

            while (runningPrefightPreparing)
            {
                if (mainCharacter == null)
                {
                    throw new ArgumentNullException(nameof(mainCharacter), "Main character cannot be null");
                }

                if (boss == null)
                {
                    throw new ArgumentNullException(nameof(boss), "Boss cannot be null");
                }

                var bossStartHP = boss.HP;

                if (boss.Level == 1)
                {
                    boss.RandomStatsForBossLvl1(boss);
                    Console.WriteLine($"{boss.Name} stats: Attack: {boss.AttackPower}, Defense: {boss.DefensePower} and HP: {boss.HP}");
                }
                else if (boss.Level == 2)
                {
                    boss.RandomStatsForBossLvl2(boss);
                    Console.WriteLine($"{boss.Name} stats: Attack: {boss.AttackPower}, Defense: {boss.DefensePower} and HP: {boss.HP}");
                }
                else
                {
                    boss.RandomStatsForBossLvl3(boss);
                    Console.WriteLine($"{boss.Name} stats: Attack: {boss.AttackPower}, Defense: {boss.DefensePower} and HP: { boss.HP}");
                }

                Console.WriteLine($"{mainCharacter.Name} stats: Attack: {mainCharacter.AttackPower}, Defense: {mainCharacter.DefensePower} and HP: {mainCharacter.HP}");

                int effectiveAttackPowerOnMainCharacter = Math.Max(0, boss.AttackPower - mainCharacter.DefensePower);
                int effectiveAttackPowerOnBoss = Math.Max(0, mainCharacter.AttackPower - boss.DefensePower);

                Console.WriteLine($"{boss.Name} effective attack stat: Attack: {effectiveAttackPowerOnMainCharacter}");
                Console.WriteLine($"{mainCharacter.Name} effective attack stat: {effectiveAttackPowerOnBoss}");

                if (effectiveAttackPowerOnMainCharacter == 0 && effectiveAttackPowerOnBoss == 0)
                {
                    Console.WriteLine("Both boss and {mainCharacter.Name} has 0 in effective attack and therefore no winner can be declared. " +
                        "Fight earlier bosses and upgrade stats to avoid this. Returning back to menu..");
                    return;
                }
                Console.WriteLine("Fight started..");

                bool fightIsOver = false;

                while (!fightIsOver)
                {
                    Console.WriteLine($"{mainCharacter.Name} attacks:");

                    TakeDamageBoss(boss, effectiveAttackPowerOnBoss);

                    Console.WriteLine($"{mainCharacter.Name} dealth {effectiveAttackPowerOnBoss} and {boss.Name} hp is {boss.HP}");

                    if (boss.HP == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Fight nr: {fightNumber}");
                        Console.WriteLine($"{boss.Name} died, {mainCharacter.Name} HP is {mainCharacter.HP} and the fight ended");

                        if (boss.Defeated == false)
                        {
                            boss.Defeated = true;
                            AddStoryPercentage();
                            GetReward(mainCharacter, boss);
                            mainCharacter.LevelUpChecker();
                            defeatedBossesList.Add(boss);

                            Console.WriteLine("Do you wish to restart the fight? Write 1, to quit write 2.");
                            if (Console.ReadLine() == "1")
                            {
                                fightNumber++;
                                boss.HP = bossStartHP;
                                mainCharacter.HP = mainCharacter.OriginalHP;
                                break;
                            }
                            else
                            {
                                fightNumber++;
                                boss.HP = bossStartHP;
                                mainCharacter.HP = mainCharacter.OriginalHP;
                                runningPrefightPreparing = false;
                                break;
                            }
                        }
                        else
                        {
                            GetReward(mainCharacter, boss);
                            mainCharacter.LevelUpChecker();

                            Console.WriteLine();
                            Console.WriteLine("You won. You have already defeated this boss. Story percentage remains the same.");
                            Console.WriteLine("Do you wish to restart the fight? Write 1, to quit write 2.");

                            if (Console.ReadLine() == "1")
                            {
                                fightNumber++;
                                boss.HP = bossStartHP;
                                mainCharacter.HP = mainCharacter.OriginalHP;
                                break;
                            }
                            else
                            {
                                boss.HP = bossStartHP;
                                mainCharacter.HP = mainCharacter.OriginalHP;
                                runningPrefightPreparing = false;
                                break;
                            }
                            
                        }
                    }

                    Console.WriteLine($"{boss.Name} attacks:");

                    TakeDamageMainCharacter(mainCharacter, effectiveAttackPowerOnMainCharacter);

                    Console.WriteLine($"{boss.Name} dealth {effectiveAttackPowerOnMainCharacter} and {mainCharacter.Name} hp is {mainCharacter.HP}");

                    if (mainCharacter.HP == 0)
                    {
                        Console.WriteLine($"Fight nr: {fightNumber}");
                        Console.WriteLine($"{mainCharacter.Name} died, {boss.Name} hp is {boss.HP} and the fight ended. Write 1 to restart the fight, 2 to end.");

                        if (Console.ReadLine() == "1")
                        {
                            fightNumber++;
                            boss.HP = bossStartHP;
                            mainCharacter.HP = mainCharacter.OriginalHP;
                            fightIsOver = true;
                        }
                        else
                        {
                            fightIsOver = true;
                            runningPrefightPreparing = false;
                            boss.HP = bossStartHP;
                            mainCharacter.HP = mainCharacter.OriginalHP;
                            Console.WriteLine("You choosed to end the fight.");
                        }
                    }
                }
            }
        }

        private void TakeDamageMainCharacter(MainCharacter mainCharacter, int damage) //Attacking Main Character and ensures that hp does not drop below 0
        {
            mainCharacter.HP = Math.Max(0, mainCharacter.HP - damage);
        }

        private void TakeDamageBoss(Boss boss, int damage)
        {
            boss.HP = Math.Max(0, boss.HP - damage);
        }

        private void GetReward(MainCharacter mainCharacter, Boss boss) //gives reward based on boss reward stats
        {
            mainCharacter.Coins += boss.CoinReward;

            Console.WriteLine($"{boss.CoinReward} coins received");

            mainCharacter.XP += boss.XPReward;

            Console.WriteLine($"{boss.XPReward} xp received");
        }

        private void AddStoryPercentage() //adds percentage on Story Percent Bar
        {
            double percentPerBoss = 100.0 / numberOfBosses;
            StoryPercentBar += percentPerBoss;
        }
    }
}
