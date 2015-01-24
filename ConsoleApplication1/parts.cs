using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    [Serializable]
    class parts
    {
        public List<Inventory> inventories = new List<Inventory>();
        public List<Entity_stats> stats = new List<Entity_stats>();
        private bool has_stats;
        private bool has_inventory;
        private bool contains_place;


        public void initialize(bool param1, bool param2, bool param3)
        {

            has_stats = param1;
            has_inventory = param2;
            contains_place = param3;

            if (has_stats == true) { Entity_stats these_stats = new Entity_stats(); these_stats.health=100 ; stats.Add(these_stats); }



        }
    }
}
