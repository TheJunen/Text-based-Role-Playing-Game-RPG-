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
        public int AttackPower { get; private set; }
        public int DefensePower { get; private set; }
        public int CoinReward { get; private set; }
        public int XPReward { get; private set; }
        public string Description { get; private set; }

        public Boss(string name, int hp, int attackPower, int defensePower, int coinReward, int xPReward, string description)
        {
            Name = name;
            HP = hp;
            AttackPower = attackPower;
            DefensePower = defensePower;
            CoinReward = coinReward;
            XPReward = xPReward;
            Description = description;
        }
    }
}
