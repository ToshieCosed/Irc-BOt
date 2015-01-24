using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    [Serializable]
    public class Inventory
    {
        private List<itemstack> stacks = new List<itemstack>();
        private int gold;

        public bool add_item(Item tempitem)
        {

            // this should be able to succeed now
            bool added = false;
            foreach (itemstack count in stacks)
            {
                string tempname = tempitem.get_name();
                if (count.get_name() == tempname)
                {

                    //here is where the item is actually added to the stack
                    count.add_item(tempitem);
                    added=true;
                    break;

                }
                else
                {
                    //obviously the item doesn't actually exist
                    //itemstack tempstack = new itemstack();
                   // tempstack.add_item(tempitem);
                    //tempstack.set_name(tempitem.get_name());
                   // stacks.Add(tempstack);
                   // added = true;
                   // break;

                }

            }

            if (added==false)
            {
                //obviously the item doesn't actually exist
                itemstack tempstack = new itemstack();
                tempstack.add_item(tempitem);
                tempstack.set_name(tempitem.get_name());
                stacks.Add(tempstack);
                added = true;

            }

            return added;

        }



        public bool remove_item(string itemname)
        {

            // this should be able to succeed now
            bool removed = false;
            foreach (itemstack count in stacks)
            {
                string tempname = count.get_name();
                if (count.get_name() == itemname)
                {

                    //here is where the item is actually added to the stack
                    removed = count.remove_item(itemname);
                    break;

                }
                else
                {
                    //obviously the item doesn't actually exist
                    //itemstack tempstack = new itemstack();
                    // tempstack.add_item(tempitem);
                    //tempstack.set_name(tempitem.get_name());
                    // stacks.Add(tempstack);
                    // added = true;
                    // break;

                }

            }

            if (removed == false)
            {
                //obviously the item doesn't actually exist
                //itemstack tempstack = new itemstack();
               // tempstack.add_item(tempitem);
                //tempstack.set_name(tempitem.get_name());
                //stacks.Add(tempstack);
                //added = true;

            }

            return removed;

        }

        public List<string> list_inventory()
        {
            List<string> item_list = new List<string>();
            string item_profile;
            int this_amount;
            string this_name;
            foreach (itemstack count in stacks)
            {
                this_amount = count.get_amount();
                this_name = count.get_name();
                item_profile = this_name + " " + this_amount;
                item_list.Add(item_profile);
                
            }

            return item_list;

        }


        public bool check_inventory(string itemname)
        {
            List<string> item_list = new List<string>();
            string item_profile;
            int this_amount;
            string this_name;
            bool has_item = false;
            foreach (itemstack count in stacks)
            {
                this_amount = count.get_amount();
                this_name = count.get_name();
                //item_profile = this_name + " " + this_amount;
                //item_list.Add(item_profile);

                if (this_name == itemname)
                {
                    if (this_amount > 0)
                    {
                        has_item = true;

                    }
                }


                }

            

            return has_item;
        }

        public void add_gold(int amount)
        {
            gold = gold + amount;
        }


        public void remove_gold(int amount)
        {

            gold = gold - amount;
        }

        public int get_gold()
        {
            return gold;
        }

        private int get_stack_by_name(string itemname)
        {

            var index = stacks.FindIndex(a => a.get_name() == itemname);
            return index;

        }

        public int get_item_counter(string itemname)
        {
            //This goes a few levels deep :)
                   //first it gets the index of a stack of items with the item name given
                    //It then uses this index to get the counter amount of items
                        //within that stack, of the same name (Redundant stringname but needed)
                        //locally within the function for varying reasons
            int counter = stacks[get_stack_by_name(itemname)].get_counter(itemname);

            return counter;
        }

        public void add_item_counter(string itemname, int amount){

            int index = get_stack_by_name(itemname);
            stacks[index].inc_counter(itemname, amount);

            
        }

       

    }
}
