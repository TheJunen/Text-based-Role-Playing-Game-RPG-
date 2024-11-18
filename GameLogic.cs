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
        internal void BossFightSimulator(MainCharacter mainCharacter, Boss boss)
        {
            if (mainCharacter == null)
            {
                throw new ArgumentNullException(nameof(mainCharacter), "Main character cannot be null");
            }

            if (boss == null)
            {
                throw new ArgumentNullException(nameof(boss), "Boss cannot be null");
            }

            int effectiveAttackPowerOnBoss = mainCharacter.AttackPower - boss.DefensePower;
            int effectiveAttackPowerOnMainCharacter = boss.AttackPower - mainCharacter.DefensePower;
            bool fightIsOver = false;

            while (!fightIsOver)
            {
                Console.WriteLine("Fight started");
                Console.WriteLine($"{mainCharacter.Name} attacks:");

                TakeDamageBoss(boss, effectiveAttackPowerOnBoss);

                Console.WriteLine($"{mainCharacter.Name} dealth {effectiveAttackPowerOnBoss} and {boss.Name} hp is {boss.HP}");

                if (boss.HP == 0)
                {
                    Console.WriteLine($"{boss.Name} died and the fight ended");
                    break;
                }

                Console.WriteLine($"{boss.Name} attacks:");

                TakeDamageMainCharacter(mainCharacter, effectiveAttackPowerOnMainCharacter);

                Console.WriteLine($"{boss.Name} dealth {effectiveAttackPowerOnMainCharacter} and {mainCharacter.Name} hp is {mainCharacter.HP}");

                if (mainCharacter.HP == 0)
                {
                    Console.WriteLine($"{mainCharacter.Name} died and the fight ended. Write 1 to continue to fight, 2 to end.");
                    
                    if (Console.ReadLine() == "2")
                    {
                        fightIsOver = true;
                        Console.WriteLine("You choosed to end the fight.");
                    }
                }
            }
        }

        private void TakeDamageMainCharacter(MainCharacter mainCharacter, int damage)
        {
            mainCharacter.HP = Math.Max(0, mainCharacter.HP - damage);
        }

        private void TakeDamageBoss(Boss boss, int damage)
        {
            boss.HP = Math.Max(0, boss.HP - damage);
        }

        private void GetReward(MainCharacter mainCharacter, Boss boss)
        {
            mainCharacter.Coins += boss.CoinReward;

            Console.WriteLine($"{boss.CoinReward} coins received");

            mainCharacter.XP 
        }
    }
}
