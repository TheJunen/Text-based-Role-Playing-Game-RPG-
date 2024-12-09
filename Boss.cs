using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRolePlayGame
{
    internal class Boss
    {
        public string Name { get; private set; }
        public int HP { get; set; }
        public int Level { get; set; }
        public int AttackPower { get; private set; }
        public int DefensePower { get; private set; }
        public int CoinReward { get; private set; }
        public int XPReward { get; private set; }
        public bool Defeated { get; set; }
        public string Description { get; private set; }
        Random rand { get; } = new Random();

        public Boss(string name, int hp, int level, int coinReward, int xPReward, string description)
        {
            Name = name;
            HP = hp;
            Level = level;
            CoinReward = coinReward;
            XPReward = xPReward;
            Description = description;
        }

        internal void RandomStatsForBossLvl1(Boss boss) //gives random attack and defense stats between 3-6 for each boss fight 
        {
            boss.AttackPower = rand.Next(3, 7);
            boss.DefensePower = rand.Next(3, 7);
        }

        internal void RandomStatsForBossLvl2(Boss boss)
        {
            boss.AttackPower = rand.Next(60, 81);
            boss.DefensePower = rand.Next(60, 81);
        }

        internal void RandomStatsForBossLvl3(Boss boss)
        {
            boss.AttackPower = rand.Next(81, 101);
            boss.DefensePower = rand.Next(81, 101);
        }
    }
}
