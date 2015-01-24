using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConsoleApplication1
{
    [Serializable]
    public class Player
    {
        // private static Tuple<string, string, string, string, string> inv = new Tuple<string, string, string, string, string>();
        private int health;
        private int currentlevel;
        private int experience_needed;
        private int total_experience;
        private string[] items = new string[10];
        private string playername;
        private string gender;
        private Inventory inventory = new Inventory();
        public DateTime started;
        public Journal myjournal = new Journal();
        public string enemy_name;
        public int enemy_hp;
        public int enemy_attack_pow;
        public int enemy_defense;
        public string enemy_weapon;
        public int attack;
        public int defense;
        public string equipped_item;



        private string lookup_inventory(int number) { return items[number]; }
        //add the item to the player's inventory if an item slot is free
        private bool add_item(string item)
        {
            bool added = false;
            for (int var = 0; var < 10; var++) { if (items[var] == "") { items[var] = item; added = true; return added; } }

            //return added in either case
            return added;

        }

        public string level_up()
        {
            total_experience = total_experience + experience_needed;
            experience_needed = total_experience + currentlevel * 12;
            currentlevel = currentlevel + 1;
            //only for debug

            int needed = experience_needed - total_experience;
            string event_string = playername + " has levelled up to " + currentlevel + " and needs " + needed + " experience points to gain another level";
            return event_string;
        }

        public string train()
        {
            Random randomize = new Random();
            int experience_gained = randomize.Next(8) + 1 * ((currentlevel / 2) + 1);
            return add_experience(experience_gained);

        }

        public string get_journal()
        {
            string ret_string = this.myjournal.return_entry();
            return ret_string;

        }

        public void set_journal(string entry){

            this.myjournal.set_entry(entry);

        }

        public string add_experience(int added)
        {

            total_experience = total_experience + added;
            if (total_experience > experience_needed)
            {
                return level_up();

            }

            return "You trained hard and managed to gain " + added + " experience points";

        }

        public string add_exp(int added)
        {

            total_experience = total_experience + added;
            if (total_experience > experience_needed)
            {
                return level_up();

            }

            return added.ToString();

        }


        public void set_name(string name)
        {

            playername = name;

        }

        public string get_name()
        {

            return playername;

        }

        public void initialize_stats(string pgender)
        {

            currentlevel = 1;
            experience_needed = 110;
            total_experience = 100;
            health = 100;
            gender = "un known";
            started = DateTime.Now;

            if (pgender == "female")
            {
                gender = pgender;
            }
            if (pgender == "male")
            {
                gender = pgender;
            }
        }

        public string get_gender()
        {
            return gender;

        }

        public string get_inventory()
        {
            string event_string = "";//playername;
            List<string> templist = inventory.list_inventory();
            foreach (string count in templist)
            {
                event_string = event_string + " : " + count.ToString() + ", ";


            }

            return event_string;

        }

        public string select_random_item()
        {
            List<string> templist = inventory.list_inventory();
            int countable = templist.Count();
            Random item = new Random();
            int this_item = item.Next(countable);
            string itemname = templist[this_item];

            return itemname;


        }

       

        public void add_inventory(string itemname)
        {
            Item thisitem = new Item();
            thisitem.set_name(itemname);
            inventory.add_item(thisitem);


        }

        public bool remove_inventory(string itemname)
        {
            //Item thisitem = new Item();
            //thisitem.set_name(itemname;

            return inventory.remove_item(itemname);


        }


        public bool check_inventory(string itemname)
        {
            //Item thisitem = new Item();
            //thisitem.set_name(itemname;

            return inventory.check_inventory(itemname);


        }


        public int get_gold()
        {

            return inventory.get_gold();
        }

        public void add_gold(int this_amount)
        {

            inventory.add_gold(this_amount);

        }

        public void remove_gold(int this_amount)
        {

            inventory.remove_gold(this_amount);

        }

        public void inc_counter(string itemname, int amount){

            inventory.add_item_counter(itemname, amount);

        }

        public int get_counter(string itemname)
        {
            int itemcounter = inventory.get_item_counter(itemname);
            return itemcounter;
            

        }

        public string get_time()
        {
            return started.ToString() ;


        }
    }
}

