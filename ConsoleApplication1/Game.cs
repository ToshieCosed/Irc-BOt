using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;



namespace ConsoleApplication1
{
    [Serializable]
    public class Game
    {
        public List<Player> playerlist = new List<Player>();
        public Map_class my_map = new Map_class();
        public Dice gamedice = new Dice();
        public Monster monsterlist = new Monster();
        //Form1 beepform2 = new Form1();


        public Game()
        {

            monsterlist.initialize();
        }

        public string register_player(string playername, string pgender)
        {
            string event_string = "";
            bool registered = false;
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {
                    event_string = "You're already registered " + playername + "!";
                    registered = true;
                    //beepform2.send_beeps("24, 32, 38, 100");


                }

                // if (registered == false)
                //  {
                //  Player tempplayer = new Player() ;
                //  tempplayer.set_name(playername);
                // tempplayer.initialize_stats();
                // playerlist.Add(tempplayer);
                // event_string = "Congragulations " + playername + " you've started your journey at level 1!";
                // }
            }

            //reduntant code
            if (registered == false)
            {
                Player tempplayer = new Player();
                tempplayer.set_name(playername);
                tempplayer.initialize_stats(pgender);
                tempplayer.add_gold(2);
                playerlist.Add(tempplayer);
                event_string = "Congragulations " + playername + " you've started your journey at level 1!";
            }


            return event_string;


        }

        public string get_start_time(string playername)
        {
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {
                    return playername + "started their journey at " + count.get_time();

                }

            }
            return "I don't know when you started playing. maybe you aren't registered. Sorry";
        }

        public string level_up(string playername)
        {

            string event_string = "";
            bool registered = false;
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {
                    event_string = count.level_up();
                    registered = true;

                }

            }

            return event_string;



        }


        //This gets the player's gender
        public string get_gender(string playername)
        {

            string event_string = "";
            bool registered = false;
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {
                    event_string = count.get_gender();
                    registered = true;

                }

            }

            return event_string;



        }


        //This gets the player's gender
        public string set_journal(string playername, string entry)
        {

            string event_string = "";
            bool registered = false;
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {
                    count.myjournal.set_entry(entry);
                    registered = true;

                }

            }

            return playername + " set their journal entry to " + entry;



        }

        public string get_journal(string playername)
        {

            string event_string = "";
            bool registered = false;
            string entry = "";
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {
                    entry = count.myjournal.return_entry();
                    registered = true;

                }

            }

            return "journal entry of " + playername + " : " + entry;



        }


        //This gets the player's gender
        public int get_gold(string playername)
        {

            int gold_amount = 0;
            bool registered = false;
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {
                    gold_amount = count.get_gold();

                }

            }

            return gold_amount;



        }


        //This gets the player's gender
        public int remove_gold(string playername, int amount)
        {

            int gold_amount = 0;
            bool registered = false;
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {
                    count.remove_gold(amount);

                }

            }

            return gold_amount;



        }

        //This add's gold to the player's inventory
        public int add_gold(string playername, int amount)
        {

            int gold_amount = 0;
            bool registered = false;
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {
                    count.add_gold(amount);

                }

            }

            return gold_amount;



        }



        public string gamble(int goldamount, string playername)
        {
            string event_string = " ";
            int total_gold = get_gold(playername);
            int new_gold = 0;


            if (total_gold < goldamount)
            {
                event_string = playername + " rummaged through " + get_gender(playername) + " pockets and didn't have the gold to wager. The bookie stared down at " + playername + " looking disgruntled and said: 'get outta here!' ";

            }

            if (total_gold >= goldamount)
            {
                Random gambler = new Random();
                int gamble_event = gambler.Next(12) + 1;

                int this_player = get_player(playername);
                string pronoun = " ";
                if (get_gender(playername) == "female") { pronoun = "her"; }
                if (get_gender(playername) == "male") { pronoun = "his"; }

                switch (gamble_event)
                {
                    case 1:
                        event_string = playername + " was able to pull a fast one on the bookie and earned double " + pronoun + " gold invested! ";
                        new_gold = goldamount * 2;
                        playerlist[this_player].add_gold(new_gold);

                        break;

                    case 2:
                        playerlist[this_player].remove_gold(goldamount);
                        event_string = playername + " was able to earn a quarter back of " + pronoun + " gold invested! The bookie was sharp.";
                        new_gold = goldamount / 4;
                        playerlist[this_player].add_gold(new_gold);

                        break;

                    case 3:
                        playerlist[this_player].remove_gold(goldamount);
                        event_string = playername + " was able to earn about half of " + pronoun + " gold invested! That's better then nothing ?";
                        new_gold = goldamount / 2;
                        playerlist[this_player].add_gold(new_gold);

                        break;

                    case 4:
                        event_string = playername + " broke even on" + pronoun + " gold invested! Might have lost some. Better watch it!";
                        break;

                    case 5:
                        event_string = playername + " noticed a fairy land on " + pronoun + " head though she earned nothing. The fairy dropped her a bag of gold then flew off. The bag contained triple! Lucky.";
                        new_gold = goldamount * 3;
                        playerlist[this_player].add_gold(new_gold);
                        playerlist[this_player].add_inventory("Empty-bag-of-gold");
                        playerlist[this_player].add_inventory("Fairy's-dust");
                        break;

                    case 6:
                        playerlist[this_player].remove_gold(goldamount);
                        event_string = playername + " felt a spell and lost " + pronoun + " concentration. The bookie took advantage and cheated " + pronoun + " out of all investments";
                        new_gold = 0;
                        break;

                    case 7:

                        event_string = "The bookie rejected you. Try again another time";

                        break;

                    case 8:

                        event_string = playername + " was going to gamble but instead stumbled on a mushroom on the way, and picked it up.";
                        playerlist[this_player].add_inventory("Mushroom");
                        break;

                    case 9:
                        event_string = playername + " was going to gamble but instead found 500 gold on the ground.";
                        playerlist[this_player].add_gold(500);

                        break;

                    case 10:
                        event_string = playername + " was going to gamble but then took a nap and forgot about it. Dust collected in " + pronoun + " pocket while asleep.";
                        playerlist[this_player].add_inventory("dust");

                        break;

                    case 11:
                        event_string = playername + " thought it was a good idea to break the fourth wall and  " + pronoun + " shouted 'this is a game what's the point if the money aint real'";
                        playerlist[this_player].add_inventory("strange_wormhole");

                        break;

                    case 12:
                        event_string = playername + " found an interesting book and began reading. 'marla marsha nepoka adooshkrey' then " + pronoun + " just sold it at the shop for 100 gold";
                        playerlist[this_player].add_gold(100);


                        break;





                }


            }
            Console.WriteLine(new_gold);
            int that_player = get_player(playername);
            int real_gold = playerlist[that_player].get_gold();
            Console.WriteLine("fake gold added is " + goldamount + " and real gold is " + real_gold);
            return event_string;


        }



        //This gives the player experience
        public string give_experience(string playername)
        {

            string event_string = "";
            bool registered = false;
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {
                    event_string = count.train();
                    registered = true;

                }

            }

            return event_string;



        }

        //This gives the player experience
        public string give_exp(string playername, int amount)
        {

            string event_string = "";
            bool registered = false;
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {
                    event_string = count.add_exp(amount);
                    registered = true;

                }

            }

            return event_string;



        }



        public string add_item(string tempitem, string playername)
        {

            string event_string = "";
            bool registered = false;
            string pronoun = "its";
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {
                    count.add_inventory(tempitem);
                    registered = true;

                    if (get_gender(playername) == "female") { pronoun = "her"; }
                    if (get_gender(playername) == "male") { pronoun = "his"; }

                    event_string = playername + " has added " + tempitem + " to " + pronoun + " inventory";

                }

            }

            if (registered == false)
            {

                event_string = playername + " couldn't figure out how to add an item to their inventory";
            }


            return event_string;
        }



        public string use_item(string tempitem, string playername)
        {

            string event_string = "";
            bool registered = false;
            string pronoun = "its";
            bool removed = false;
            bool used = false;
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {
                    if (tempitem != "little-chic")
                        if (tempitem != "young-hen")
                            if (tempitem != "little-golden-chic")
                                if (tempitem != "young-golden-hen")
                                    if (tempitem != "skeleton")
                                    {
                                        {
                                            {
                                                {
                                                    {
                                                        removed = count.remove_inventory(tempitem);
                                                    }
                                                }
                                            }
                                        }
                                    }
                    registered = true;

                    if (get_gender(playername) == "female") { pronoun = "her"; }
                    if (get_gender(playername) == "male") { pronoun = "his"; }

                    if (removed == true)
                    {
                        event_string = playername + " used one of  their " + tempitem + " from " + pronoun + " inventory";

                        if (tempitem == "Egg")
                        {
                            event_string = event_string + "Then it hatched and " + playername + " got a little-chic";
                            count.add_inventory("little-chic");
                            return event_string;

                        }
                    }




                    if (tempitem == "little-chic")
                    {

                        //this count.inc_counter thing. Count is the player number or index
                        // inc_counter is the counter bound to the item as a member of item class
                        // see itemstack.cs and item.cs and player.cs to find the wrappers to access
                        //the single integer member of item.cs called counter and private scope
                        count.inc_counter("little-chic", 1);

                        if (count.get_counter("little-chic") >= 5)
                        {
                            count.remove_inventory("little-chic");
                            count.add_inventory("young-hen");
                            return playername + "'s " + tempitem + " has grown into a young-hen! It might lay eggs if you pet it!";


                        }

                        return playername + " petted " + pronoun + " " + tempitem + " softly. ";
                    }


                    if (tempitem == "little-golden-chic")
                    {

                        //this count.inc_counter thing. Count is the player number or index
                        // inc_counter is the counter bound to the item as a member of item class
                        // see itemstack.cs and item.cs and player.cs to find the wrappers to access
                        //the single integer member of item.cs called counter and private scope
                        count.inc_counter("little-golden-chic", 1);

                        if (count.get_counter("little-golden-chic") >= 5)
                        {
                            count.remove_inventory("little-golden-chic");
                            count.add_inventory("young-golden-hen");
                            return playername + "'s " + tempitem + " has grown into a young-golden-hen! It might lay GOLDEN eggs if you pet it!";


                        }

                        return playername + " petted " + pronoun + " " + tempitem + " softly. ";
                    }

                    if (tempitem == "young-golden-hen")
                    {
                        Random this_random = new Random();
                        int lays_state = this_random.Next(4) + 1;
                        used = true;


                        switch (lays_state)
                        {
                            case 1:
                                return playername + " played with " + pronoun + " young-golden-hen and it squacked too loud. OUCH. My ears!! ";

                                break;
                            case 2:
                                count.add_inventory("golden-feathers");
                                return playername + " studied" + pronoun + " young-golden-hen and found some feathers so " + playername + " put them " + pronoun + " bag";
                                break;
                            case 3:
                                return playername + " nuzzled " + pronoun + " young-golden-hen and it pecked " + playername;
                                break;
                            case 4:
                                int egg_type = this_random.Next(120);

                                if (egg_type < 100)
                                {
                                    count.add_inventory("golden-egg");
                                    return playername + " poked " + pronoun + " young-golden-hen and it layed a golden egg right in " + pronoun + " hand";
                                }

                                if (egg_type > 99)
                                {
                                    count.add_inventory("HUGE-golden-egg");
                                    return playername + " petted " + pronoun + " young-hen and it clucked loudly, then a HUGE-golden-egg fell in " + pronoun + " bag!";
                                }

                                break;



                        }

                    }



                    if (tempitem == "10_gold")
                    {
                        count.add_gold(10);
                        event_string = " you added the gold to your wallet!";
                        return event_string;
                    }


                    if (tempitem == "Chigarus")
                    {

                        Random this_random = new Random();
                        int Chigarus_event = this_random.Next(7) + 1;
                        used = true;

                        switch (Chigarus_event)
                        {
                            case 1:

                                event_string = playername + " used the " + tempitem + " and went on a magical journey earning about 3000 gold and 5 potions and 1000 experience";
                                count.add_inventory("potion");
                                count.add_inventory("potion");
                                count.add_inventory("potion");
                                count.add_inventory("potion");
                                count.add_inventory("potion");
                                count.add_gold(3000);
                                count.add_experience(1000);

                                return event_string;

                                break;

                            case 2:
                                event_string = playername + " angered the " + tempitem + " and it rebelled against " + pronoun + " and fled stealing 100 gold";
                                int amount_left = count.get_gold();
                                int gold_taken = 0;
                                if (amount_left >= 100)
                                {
                                    gold_taken = 100;
                                    count.remove_gold(gold_taken);

                                }
                                else
                                {
                                    gold_taken = amount_left;
                                    count.remove_gold(gold_taken);
                                }


                                break;

                            case 3:
                                string this_mon = monsterlist.random_monster();
                                event_string = playername + " petted the " + tempitem + " and it transformed into a " + this_mon;
                                count.add_inventory(this_mon);


                                break;


                            case 4:
                                event_string = playername + " petted the " + tempitem + " and one of it's scales comes loose then it falls in" + pronoun + " bag";
                                count.add_inventory("Chigarus-Scale");


                                break;

                            case 5:
                                event_string = playername + " looked at the " + tempitem + " and it smiled back and said to " + pronoun + " One day I'll leave you then it put a rock in " + pronoun + " bag.";
                                count.add_inventory("Chigarus-rock");


                                break;


                            case 6:
                                int tried = 0;
                            retry:
                                string stolen = count.select_random_item();

                                if (count.remove_inventory(stolen) == true)
                                {

                                    event_string = playername + " looked at the " + tempitem + " and it grinned mischeviously at " + pronoun + " and said I want to steal  " + stolen + " from you. Then it took it and flew away";

                                }
                                else
                                    tried++;

                                if (tried >= 4) { event_string = playername + " felt that the " + tempitem + "had something mischevious in mind and decided not to pet it "; break; }
                                goto retry;


                                break;


                            case 7:

                                stolen = count.select_random_item();
                                event_string = playername + " tried to pet the " + tempitem + ". Then it began saying to  " + pronoun + ": I could have taken your " + stolen + " from you. Then it flew away";
                                count.add_inventory("Chigarus-dung");

                                break;

                        }
                    }


                    if (tempitem == "golden-egg")
                    {

                        event_string = playername + " shook " + pronoun + tempitem + " and a little-golden-chic hatched out of it";
                        count.add_inventory("little-golden-chic");
                        count.add_inventory("gold-egg-shells");

                        return event_string;


                    }

                    if (tempitem == "HUGE-golden-egg")
                    {
                        event_string = playername + " watched in terror as the HUGE-golden-egg collapsed on itself into a singularity and sucked away all " + pronoun + " gold leaving behind only golden egg shells";
                        count.add_inventory("HUGE-golden-egg-shells");
                        count.remove_gold(count.get_gold());


                    }

                    //new item events go after this comment

                    if (tempitem == "skeleton")
                    {

                        if (count.get_counter(tempitem) >= 3)
                        {

                            event_string = playername + " played with the " + tempitem + " and gained 2 experience. That's what you get for playing with bones. Then the bones dissintegrated into dust";
                            count.add_experience(2);
                            count.add_inventory("dust");
                        }


                        count.inc_counter(tempitem, 1);
                        return playername + " toyed with some bones and nothing happened.";



                        return event_string;

                    }



                    //Do not put new item events past this comment
                    //Before this comment is okay

                }




            }


            if (removed == false)
            {

                event_string = playername + " didn't seem to have any " + tempitem + " in " + pronoun + " inventory";

            }

            if (registered == false)
            {

                event_string = playername + " couldn't figure out how to remove an item from their inventory";
            }



            return event_string;
        }

        public string list_items(string playername)
        {

            string event_string = "";
            bool registered = false;
            string pronoun = "its";
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();

                if (count.get_name() == playername)
                {
                    registered = true;
                    event_string = count.get_inventory();
                    break;

                }

            }

            if (registered == false)
            {

                event_string = playername + " hasn't registered and doesn't have an inventory";
            }


            return event_string;
        }

        private int get_player(string playername)
        {

            var index = playerlist.FindIndex(a => a.get_name() == playername);
            Console.WriteLine("this was called and the item in their inventory is " + playerlist[index].get_inventory());

            return index;

        }

        public string equip_item(string playername, string itemname)
        {
            string pronoun = "its";
            string event_string = "";
            bool registered = false;
            string item = itemname;
            event_string = playername + " was unable to equip the " + itemname + ". Might wanna check to see if you actually have that";
            foreach (Player count in playerlist)
            {
                string tempname = count.get_name();
                if (count.get_name() == playername)
                {

                    registered = true;
                    bool has_it = count.check_inventory(itemname);
                    if (has_it==true)
                    {

                        count.remove_inventory(item);
                        // hot swapping items so that you don't lose them by equipping them.
                        if (count.equipped_item != "") { count.add_inventory(count.equipped_item); }
                        count.equipped_item = item;


                    }

                    if (get_gender(playername) == "female") { pronoun = "her"; }
                    if (get_gender(playername) == "male") { pronoun = "his"; }

                    if (has_it == true) { event_string = playername + " has equipped " + item + " to " + pronoun + " person"; }


                }

            }

            if (registered == false)
            {

                event_string = playername + " couldn't figure out how to add an item to their inventory";
            }


            return event_string;
        }





        public string get_monster(string playername)
        {

            int this_player = get_player(playername);
            string mon_name = monsterlist.random_monster();
            playerlist[this_player].add_inventory(mon_name);
            string gen = playerlist[this_player].get_gender();
            string pronoun = "";
            if (gen == "female") { pronoun = "her"; }
            if (gen == "male") { pronoun = "his"; }
            else
            { pronoun = "it's "; }

            string event_string = playername + " summoned an " + mon_name + " and its now in " + pronoun + " inventory";
            return event_string;
        }



    }
}




