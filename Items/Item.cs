using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRolePlayGame.Items
{
    abstract class Item 
    {
        //properties bør nesten alltid brukes når du bruker fields utenfor klassen. Fields brukes bare når det lagres internt i klassen
        public int AttackPower { get; protected set; } //skal kunne brukes i andre klasser , men kun endres innenfor arveklasser
        public int HP { get; protected set; }
        public int DefensePower { get; protected set; }
        public int Price { get; protected set; }
        public string Name { get; protected set; } //skal kunne legges til i inventory
        public string Description { get; protected set; }

        internal void AddItemToInventory(List<Item> inventory, Item item) //Må lages her da item trenger å overføres til MainCharacter
        {
            inventory.Add(item);
        }

        internal abstract void WriteOutInfo();
        internal void EquipItem(MainCharacter mainCharacter, Item item)
        {
            if (item is Healer)
            {
                item.HP 
            }
        }
    }
}
