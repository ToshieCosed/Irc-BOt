using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    [Serializable]
    public class itemstack
    {
        private List<Item> stack = new List<Item>();
        private string itemname;
        private int internal_id;
        private int amount_count;


        public void add_item(Item tempitem)
        {
            //This adds an item to the stack
            //increases count
            //This stack can hold an item of any type but should only hold items of the same type
            stack.Add(tempitem);
            itemname = tempitem.get_name();
            amount_count++;

            Console.WriteLine("Added " + itemname + "to inventory");

        }

        public bool remove_item(string itemname)
        {
            bool removed = false;

            foreach (Item count in stack)
            {
                Item tempitem = count;
                if (tempitem.get_name() == itemname)
                {
                    if (amount_count > 0)
                    {
                        amount_count--;
                        stack.Remove(count);
                        removed = true;
                        break;
                    }
                }

            }

            return removed;
        }

        //Getters and setters so that the inventory class can use it
        public string get_name()
        {
            return itemname;
        }

        public void set_name(string name){

            itemname = name;

        }

        public int get_amount()
        {
            return amount_count;

        }


        //These three functions together
                //allow the program to search for an item
            //within a given stack
                //by item name
                    //Required for certain things
                        //Please use game.player.function to access these interfaces
                            //if you find this un reliable please find a way around it
                               
        private int get_anyitem(string itemname)
        {

            var index = stack.FindIndex(a => a.get_name() == itemname);
           
            return index;

        }

        public void inc_counter(string itemname, int amount){

            int this_item = get_anyitem(itemname);
            if (stack.Count() > 0)
            {
                stack[this_item].inc_counter(amount);
            }
        }

        public int get_counter(string itemname)
        {
            int this_item = get_anyitem(itemname);
            return stack[this_item].get_counter();

        }
    }
}
