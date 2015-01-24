using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    [Serializable]
    public class Item
    {
        private string item_name;
        private int weight;
        private int basic_cost;
        private int resell_value;
        private int heals_health;
        private int use_amount;
        private bool consumable;
        private bool soul_bound;
        private bool quest_only;
        private bool sellable;
        private bool tradable;
        private int rarity;
        private int private_value;
        private int minimum_level;
        private int hp_restore_amount;
        private int counter;


        public string get_name()
        {
            return item_name;
        }

        public void set_name(string tempname){

            item_name = tempname;
        }

        public void inc_counter(int amount)
        {
            counter = counter + amount;

        }

        public int get_counter()
        {
            return counter;

        }
    }
}
