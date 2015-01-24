using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using ConsoleApplication1;



/* 
* This program establishes a connection to irc server and joins a channel. Thats it.
*
* Coded by Pasi Havia 17.11.2001 http://koti.mbnet.fi/~curupted
*
* Updated / fixed by Blake 09.10.2010
*/
class IrcBot
{



    //Save the file
    public bool SaveFile(string fullpath = Filename)
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Create(fullpath);
            formatter.Serialize(stream, game);
            stream.Close();
        }
        catch
        {
            return false;
        }

        return true;
    }

    //Load a file
    public static bool LoadFile(string fullpath = Filename)
    {
        try
        {
            if (!File.Exists(fullpath))
                return false;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.OpenRead(fullpath);
            game = (Game)formatter.Deserialize(stream);
            stream.Close();
        }
        catch
        {
            return false;
        }

        return true;
    }


    // Irc server to connect 
    public static string SERVER = "irc.freenode.net";
    // Irc server's port (6667 is default port)
    private static int PORT = 6667;
    // User information defined in RFC 2812 (Internet Relay Chat: Client Protocol) is sent to irc server 
    private static string USER = "USER IrcBot 0 * :IrcBot";
    // Bot's nickname
    private static string NICK = "Pinkbot222";
    // Channel to join
    private static string CHANNEL = "##Testing_RPG";
    //The dictionary for messages
   // private Dictionary<int, string> D = new Dictionary<int, string>();
    public static Dictionary<string, string> C = new Dictionary<string, string>();

    public static Dictionary<string, string> word_rules = new Dictionary<string, string>();
    public static Dictionary<string, string> knowledge = new Dictionary<string, string>();
    public static Dictionary<string, string> notes = new Dictionary<string, string>();


    public const string fullpath = "bot_knowledge.txt";
    public const string Filename = "bot_knowledge.txt";
    public static StreamWriter writer;
    static bool responded;
    static bool shutup;
    static bool playinggame;
    static string ot;
    private static Debug_Info dbug = new Debug_Info();
    //static fixed string itemnames[25];
    static string[] usernames = new string[5];
    
    private static Dictionary<string, string> users = new Dictionary<string, string>();
    private static int mood=5;
    private static int max_mood = 255;
    private static int min_mood = 0;
    private static Dictionary<string, string> terms = new Dictionary<string, string>();
    private static int bad_terms;
    private static int good_terms ;
    private static string output;
    public static Game game = new Game();
    public static talker bot_talker = new talker();
    public static string last_phrase = "";
    public static fortunes pinkbot_fortunes = new fortunes();
    //public static Form1 beepform = new Form1();
    public static string song_string;
    
    

   

    


    private static void Add_Response(string a, string b)
    {

        if (!C.ContainsKey(a))
        {
            C.Add(a, b);//crash protection


        }

       

    }

    
    private static void Add_inventory(string username, string item1, string item2, string item3, string item4){

       //var tuple = new Tuple<string, string, string, string, string>("hey", "dog", "cat", "mouse", "polarbear");
       //inventories.Add(tuple);

    }


    const int VERSION = 1;
    static void Save(Game game, string fileName)
    {
        Stream stream = null;
        try
        {
            IFormatter formatter = new BinaryFormatter();
            stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, VERSION);
            formatter.Serialize(stream, game);
        }
        catch
        {
            // do nothing, just ignore any possible errors
        }
        finally
        {
            if (null != stream)
                stream.Close();
        }
    }

    static Game Load(string fileName)
    {
        Game ret = null;
        Stream stream = null;
        try
        {
            IFormatter formatter = new BinaryFormatter();
            stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
            int version = (int)formatter.Deserialize(stream);
            Debug.Assert(version == VERSION);
            ret = (Game)formatter.Deserialize(stream);
        }
        catch
        {
           
        }
        finally
        {
            if (null != stream)
                stream.Close();
        }
        return ret;
    }

    public static void Main(string[] args)
    {
        NetworkStream stream;
        TcpClient irc;
        string inputLine;
        StreamReader reader;



       // Application.Run(new Form1());
       
        try
        {
            //beepform.send_beeps("120");
            usernames[1] = "RedEyedGirl";
            //C.Add("what's up?", "nothing much");
           // C.Add("meep", "mope");
           // C.Add("hey", "what?");
           // C.Add("what is", "it's two. ");
           // C.Add("This", "that");
           // C.Add("okay", "it is okay.");
           // C.Add("is it?", "yeah..");
           // C.Add("who are you", "I'm a bot");
           // C.Add("Who are you?", "I am a bot");
          //  C.Add("not you", "oh I'm sorry");
           // C.Add("what?", "nothing... just processing");
           // C.Add("What?", "nothing just you know. :3");
           // C.Add("help", "With ? ");
           // C.Add("help me", "I can't I'm a bot.");
            //C.Add("hm", "thinking deeply about something are we?");
           // C.Add("yes", "oh no. ");
          //  C.Add("no", "OHHHH YESSS");
          //  C.Add("super mario world", "yes");
           // C.Add("SMWC", "That's this place.. isn't it?");
          //  C.Add("smwc", "on our website!");
           // C.Add("lol", "AHAHAHAHAHAHAHA!");
           // C.Add("lel", "It's LOL");
           

            //if (!C.ContainsKey("help2"))
           // {
               //C.Add("!help", "use '!'shutup or '!'reset to change states omit '' see help2 for more"); 
             //  C.Add("!help2", "usage of '!'add is '!add' <yourphrasehere> '!'and <yoursecond phrase here> omit the ' ' ");
              // C.Add("!gamehelp", "use ' !game <command> <parameter> to send commands ");
              // C.Add("B=BUTTON():IF B==4", "IF BUTTON()==4");
                
              // terms.Add("happy", "good");
               //terms.Add("sad", "bad");
              //terms.Add("bad day", "bad");
              // terms.Add("feeling down", "bad");
              // terms.Add("excited", "good");
               ////.Add("interested", "good");
              // terms.Add("messed up", "bad");
               //terms.Add("enjoy", "good");
              // terms.Add("didn't", "false");
              // terms.Add("was", "past");
              // terms.Add("me", "you");
              // terms.Add("we", "us");

              // terms.Add("injury", "bad");
               //terms.Add("painful", "bad");
             // terms.Add("sick", "bad");
             //  terms.Add("daydream", "imagining");

              // terms.Add("think", "feel");
               


          // }
           var tuple = new Tuple<string, string, string, string, string>("hey", "dog", "cat", "mouse", "polarbear");
          //inventories.Add(tuple);

            
            irc = new TcpClient(SERVER, PORT);
            stream = irc.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            
            writer.WriteLine("PASS " + "haynee");
            writer.WriteLine("NICK " + NICK);
            writer.Flush();
            writer.WriteLine(USER);
            writer.Flush();
            while (true)
            {
                while ((inputLine = reader.ReadLine()) != null)
                {
                    dbug.iterate_message_count();
                    // inventories.Add( var z=new Tuple<string, string>("hey", "hey");

                    // if (inventories[0].Item2 == "dog")
                    //{
                    //  Console.WriteLine("there was a dog");
                    //  }

                    if (inputLine.Contains("!shutup"))
                    {

                        shutup = true;
                       // beepform.send_beeps("25");
                    }


                    ot = "";
                    Console.WriteLine("<-" + inputLine);
                    for (int a = 1; a < 12; a++)
                    {
                        ot = ot + inputLine[a];

                    }
                    Console.WriteLine(ot);


                    //try to get the username and respond.

                    // ot = "";
                    string y = "";
                    for (int a = 1; a < inputLine.Length; a++)
                    {

                       
                        string x = inputLine[a].ToString();
                        
                        if (x == "!")
                        {

                            usernames[1] = y;
                            //writer.WriteLine("PRIVMSG " + CHANNEL + " :The value of The string 'ot' is " + ot + "\r\n");
                            //writer.Flush();
                            break;

                        }

                        y = y + inputLine[a].ToString();

                    }

                    if (inputLine.Contains("Hi Pinkbot"))
                    {

                       // beepform.COM17.Open();
                       // beepform.COM17.Write("120, 150, 600");
                       // beepform.COM17.Close();
                        
                        string outputline = " :hey " + usernames[1] + " whassup!";
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                        
                    }

                    if (inputLine.Contains("BEEP"))
                    {
                        //beepform.send_beeps("125");

                    }


                    if (inputLine=="DEBUG")
                    {
                       // beepform.send_beeps("125, 230");

                    }

                    if (inputLine.Contains("meow"))
                    {
                      

                    }

                    if (inputLine.Contains("!register"))
                    {

                        string[] registerparts = inputLine.Split(new Char[] { ' ' });
                        int count = registerparts.GetUpperBound(0);
                        string outputline = " : sorry, I can't understand that! use female or male please";

                        if (count < 3)
                        {

                            outputline = " : sorry, I can't understand that! use female or male please";

                        }

                        if (count > 3)
                        {
                            string tempgender = registerparts[4];
                            outputline = " :" + game.register_player(usernames[1], tempgender);

                        }
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }


                    //Code for handling journal entries is here. refer to game code object is game.player.journal


                    //This code here gets the journal entry and lets other players read people's journals by username!!!!
                    if (inputLine.Contains("!readjournal"))
                    {

                        string[] registerparts = inputLine.Split(new Char[] { ' ' });
                        int count = registerparts.GetUpperBound(0);
                        string outputline = " : sorry, I can't understand that! specify a registered user please";

                        if (count < 3)
                        {

                            outputline = " : sorry, I can't understand that! specify a registered user please";

                        }

                        if (count > 3)
                        {
                            string this_playername = registerparts[4];
                            outputline = " :" + game.get_journal(this_playername);

                        }
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }

                    //
                    //This code here lets a player set their journal entry!!!
                    if (inputLine.Contains("!writejournal"))
                    {

                        string[] registerparts = inputLine.Split(new Char[] { ' ' });
                        int count = registerparts.GetUpperBound(0);
                        string outputline = " : sorry, I can't understand that! maybe you aren't registered "+ usernames[1];
                        string entry = "";

                        if (count < 3)
                        {

                            outputline = " : sorry, I can't understand that! maybe you aren't registered "+ usernames[1];

                        }

                        if (count > 3)
                        {


                            for (int t = 4; t < count+1; t++)
                            {
                                entry = entry +" "+ registerparts[t];

                            }


                            game.set_journal(usernames[1], entry);
                            outputline = " :"+ " "+usernames[1] + " has set their journal entry to " + entry;

                        }
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }






                    if (inputLine.Contains("!started")){

                        string outputline = " :" + game.get_start_time(usernames[1]);
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();


                    }

                    if (inputLine.Contains("!registerbot"))
                    {

                        string[] registerparts = inputLine.Split(new Char[] { ' ' });
                        int count = registerparts.GetUpperBound(0);
                        string outputline = " : sorry, I can't understand that! use female or male please";

                        if (count < 3)
                        {

                            outputline = " : sorry, I can't understand that! use female or male please";

                        }

                        if (count > 3)
                        {
                            string tempgender = registerparts[4];
                            outputline = " :" + game.register_player("Pinkbot", tempgender);

                        }
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }


                    if (inputLine.Contains("!Coinzleft"))
                    {
                        string outputline = " :!balance";
                        writer.WriteLine("PRIVMSG " + "Doger" + outputline + "\r\n");
                        Console.WriteLine("This was the message sent " + "PRIVMSG " + "Doger" + outputline + "\r\n");
                        writer.Flush();

                    }


                    if (inputLine.Contains("!Save-all"))
                    {

                        if (usernames[1]=="RedEyedGirl"){
                        Save(game, "save.bin");

                        }
                    }

                    if (inputLine.Contains("!Load-all"))
                    {
                        if (usernames[1] == "RedEyedGirl")
                        {
                            game = Load("save.bin");
                        }
                    }

                    if (inputLine.Contains("Your balance is")){
                        string outputline = "";
                        string[] balanceparts = inputLine.Split(new Char[] { ' ' });
                        int count = balanceparts.GetUpperBound(0);
                        

                       
                            string amount = balanceparts[7];
                            outputline = " :My balance is: " + amount;
                            writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                            writer.Flush();

                        }


                    if (inputLine.Contains("!battle"))
                    {
                        string outputline = "";
                        string monster = game.monsterlist.random_monster();

                        Random ran = new Random(3);
                        int num =ran.Next(3);
                        switch (num)
                        {

                            case 0:
                                {
                                   
                                    int gold_gained = ran.Next(10);
                                    int xp_gained = ran.Next(15);
                                    string exp = game.give_exp(usernames[1], xp_gained);
                                    game.add_gold(usernames[1], gold_gained);
                                    outputline = " : "+ usernames[1] + " battled ferociously and conquered the " + monster + " and while doing so managed to gain a respectable " + gold_gained + " gold! and a sum of "+exp+ " experience points!";
                                    writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                                    writer.Flush();


                                }

                                break;



                        }

                    }

                    


                    if (inputLine.Contains("!add_item"))
                    {



                        string[] registerparts = inputLine.Split(new Char[] { ' ' });
                        int count = registerparts.GetUpperBound(0);
                        string outputline = " : sorry, I can't understand that! use !add_item itemname";

                        if (count < 3)
                        {

                            outputline = " : sorry, I can't understand that! use !add_item itemname";

                        }

                        if (usernames[1] != "RedEyedGirl")
                        {

                            outputline = " : oh, that command is only available to RedEyedGirl at this time. I'm sorry.";
                        }

                        

                        if (count > 3)
                        {
                            if (usernames[1].ToString() == "RedEyedGirl")
                            {
                                string tempname = registerparts[4];
                                outputline = " :" + game.add_item(tempname, usernames[1]);
                            }

                        }
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }



                    if (inputLine.Contains("!equip"))
                    {


                        string[] registerparts = inputLine.Split(new Char[] { ' ' });
                        int count = registerparts.GetUpperBound(0);
                        string outputline = " : sorry, I can't understand that! use !equip itemname";

                        if (count < 3)
                        {

                            outputline = " : sorry, I can't understand that! use !equip_item itemname";

                        }


                        if (count > 3)
                        {
                           
                                string tempname = registerparts[4];
                                outputline = " :" + game.equip_item(usernames[1], tempname);
                            

                        }
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }



                    if (inputLine.Contains("!givebot"))
                    {

                        string[] registerparts = inputLine.Split(new Char[] { ' ' });
                        int count = registerparts.GetUpperBound(0);
                        string outputline = " : sorry, I can't understand that! use !givebot itemname";

                        if (count < 3)
                        {

                            outputline = " : sorry, I can't understand that! use !givebot itemname";

                        }

                        if (count > 3)
                        {
                            string tempname = registerparts[4];
                            outputline = " :" + game.add_item(tempname, "Pinkbot");


                        }
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }

                    if (inputLine.Contains("Mario")){

                        //beepform.send_beeps("660, 660, 660, 510, 660, 770 ,380 , 510 ,380,320,440 ,480 ,450 ,430 ,380 ,660 ,760,860,700 ,760 ,660 ,520 ,580 ,480,510 ,380 ,320 ,440 ,300,480 ,450 ,430 ,380 ,660,760 ,860 ,700 ,760 ,660 ,520 ,580 ,480 ,500");


                    }

                    if (inputLine.Contains("Playsong")){

                        string[] songparts = inputLine.Split(new Char[] { ' ' });
                        int count = songparts.GetUpperBound(0);
                        count = songparts.GetLength(0);
                        song_string = "";

                        if (count > 3)
                        {

                            
                            for (int songcount = 4; songcount < count; songcount++)
                            {
                                song_string = song_string +" "+ songparts[songcount];

                            }


                            //beepform.send_beeps(song_string+"\n");
                            song_string = "";



                        }
                    }


                    if (inputLine.Contains("Randomgen")){

                        string song_string = "";
                        Random jj = new Random();
                        for (int tcounter = 0; tcounter < 256; tcounter++)
                        {

                            
                            int pitch = jj.Next(500);
                            int duration = jj.Next(500);
                            int delay = jj.Next(200);

                            song_string = song_string + pitch + ", " + duration + ", " + delay + ", ";

                        }

                        //beepform.send_beeps(song_string);


                    }

                    if (inputLine.Contains("!botuse"))
                    {

                        string[] registerparts = inputLine.Split(new Char[] { ' ' });
                        int count = registerparts.GetUpperBound(0);
                        string outputline = " : sorry, I can't understand that! use !botuse itemname";

                        if (count < 3)
                        {

                            outputline = " : sorry, I can't understand that! use !botuse itemname";

                        }

                        if (count > 3)
                        {
                            string tempname = registerparts[4];
                            outputline = " :" + game.use_item(tempname, "Pinkbot");


                        }
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }


                    if (inputLine.Contains("!use"))
                    {

                        string[] registerparts = inputLine.Split(new Char[] { ' ' });
                        int count = registerparts.GetUpperBound(0);
                        string outputline = " : sorry, I can't understand that! use !use itemname";

                        if (count < 3)
                        {

                            outputline = " : sorry, I can't understand that! use !use itemname";

                        }

                        if (count > 3)
                        {
                            string tempname = registerparts[4];
                            outputline = " :" + game.use_item(tempname, usernames[1]);


                        }
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }


                    if (inputLine.Contains("!testlevel"))
                    {

                        string outputline = " :" + game.level_up(usernames[1]);
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }

                    if (inputLine.Contains("!convertgold"))
                    {
                        string[] convertparts = inputLine.Split(new Char[] { ' ' });
                        int count = convertparts.GetUpperBound(0);
                        string outputline = " : sorry, I can't understand that! use !convertgold goldamount";

                        if (count < 3)
                        {

                            outputline = " : sorry, I can't understand that! use !convertgold goldamount";

                        }

                        if (count > 3)
                        {
                            string tempname = convertparts[4];
                            int percentamount = Convert.ToInt32(convertparts[4] );
                            int convert_value = percentamount / 2;
                            string convertline = " :!tip " + usernames[1] + " " + convert_value;
                            outputline = " :" + "your request to convert " + convertparts[4] + " has been accepted";

                            int goldamount = game.get_gold(usernames[1]);
                            int test_amount = Convert.ToInt32(convertparts[4]);

                            if (test_amount > goldamount)
                            {
                                outputline = " :" + "you don't have enough gold. sorry";

                            }

                            if (goldamount >= percentamount)
                            {
                               
                                writer.WriteLine("PRIVMSG " + "Doger" + convertline + "\r\n");
                                writer.Flush();
                                game.remove_gold(usernames[1], test_amount);
                            }



                            Console.WriteLine("This was the message sent " + "PRIVMSG " + "NICKSERV" + convertline + "\r\n");
                            writer.Flush();



                        }
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }


                    if (inputLine.Contains("!items"))
                    {

                        string outputline = " :" + "inventory of " + usernames[1] + game.list_items(usernames[1]);
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();

                        //string tempstring = game.playerlist[0].get_inventory();
                        //Console.WriteLine(tempstring);
                    }

                    if (inputLine.Contains("!chargender"))
                    {

                        string outputline = " :your gender is " + game.get_gender(usernames[1]) + " " + ot;
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }

                    if (inputLine.Contains("!reg_nick"))
                    {

                        string outputline = " :IDENTIFY Pinkbot222 "+" haynee";
                        writer.WriteLine("PRIVMSG " + "NICKSERV" + outputline + "\r\n");
                        Console.WriteLine("This was the message sent " + "PRIVMSG " + "NICKSERV" + outputline + "\r\n");
                        writer.Flush();
                    }

                    if (inputLine.Contains("!reg_thebot"))
                    {
                        string outputline = " :REGISTER " + "haynee"+" iquiera@gmail.com";
                        writer.WriteLine("PRIVMSG " + "NICKSERV" + outputline + "\r\n");
                        Console.WriteLine("This was the message sent " + "PRIVMSG " + "NICKSERV" + outputline + "\r\n");
                        writer.Flush();

                    }

                    if (inputLine.Contains("!verify_bot"))
                    {
                        string outputline = " :VERIFY REGISTER Pinkbot222 podkpltxquaz";
                        writer.WriteLine("PRIVMSG " + "NICKSERV" + outputline + "\r\n");
                        Console.WriteLine("This was the message sent " + "PRIVMSG " + "NICKSERV" + outputline + "\r\n");
                        writer.Flush();
                        
                    }

                    if (inputLine.Contains("!roll"))
                    {

                        string outputline = " : " + usernames[1] + " threw the dice hard and got " + game.gamedice.roll();
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }

                    if (inputLine.Contains("!tipme"))
                    {

                        string outputline = " :!tip " + usernames[1]+" 2";
                        writer.WriteLine("PRIVMSG " + "Doger" + outputline + "\r\n");
                        Console.WriteLine("This was the message sent " + "PRIVMSG " + "NICKSERV" + outputline + "\r\n");
                        writer.Flush();

                    }



                    if (inputLine.Contains("!getmonster"))
                    {

                        string outputline = " :" + game.get_monster(usernames[1]);
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }
                     

                    //This allows the game to give the player experience points and calls level up
                    //when the player reaches their level up thing
                    if (inputLine.Contains("!train"))
                    {

                        string outputline = " :" + game.give_experience(usernames[1]);
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }


                    if (inputLine.Contains("talk: ")){


                     

                        string[] getting_phrase = inputLine.Split(new Char[] { ' ' });
                        string send_phrase = "";
                        bool can_add = false;

                        for (int counting = 0; counting < getting_phrase.Count(); counting++)
                        {
                            string this_fragment = getting_phrase[counting];
                            if (this_fragment == ":talk:")
                            {
                                can_add = true;
                                Console.WriteLine("The fragment found here was " + this_fragment);
                            }

                            if (can_add == true)
                            {
                                send_phrase = send_phrase + " " + this_fragment;
                            }



                        }

                        string outputline = " :" + bot_talker.look_up(send_phrase);
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();

                        if (last_phrase != "")
                        {
                            bot_talker.add_keys(last_phrase, send_phrase);

                        }

                        last_phrase = send_phrase;

                    }

                    if (inputLine.Contains("Help me"))
                    {
                        string outputline = " :I don't know if I can help you " + usernames[1] + " please be more specific.";
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();

                    }


                    //
                    // Console.WriteLine("test");
                    if (ot == "RedEyedGirl")
                    {

                        Console.WriteLine("Owner was talking");

                        if (inputLine.Contains("Pinkbot Set Mode 7"))
                        {

                            writer.WriteLine("PRIVMSG " + CHANNEL + " :I can't do mode 7 " + ot + " what do I look like? An snes?" + "\r\n");
                            writer.Flush();

                        }

                        if (inputLine.Contains("Pinkbot do a barrel roll"))
                        {
                            writer.WriteLine("PRIVMSG " + CHANNEL + " :/me does a barrel roll for her owner" + "\r\n");
                            writer.Flush();

                        }

                        if (inputLine.Contains("Pinkbot showdebug()"))
                        {

                            writer.WriteLine("PRIVMSG " + CHANNEL + " :showing debug info: message count is " + dbug.get_message_count() + "\r\n");
                            writer.Flush();

                        }

                    }

                    if (inputLine.Contains("!OP"))
                    {
                        if (ot == "RedEyedGirl")
                        {
                            writer.WriteLine("PRIVMSG " + CHANNEL + " :owner acknowledged " + usernames[1] + "\r\n");
                            writer.Flush();
                        }
                        else
                        {
                            writer.WriteLine("PRIVMSG " + CHANNEL + " :you're not an op " + usernames[1] + "\r\n");
                            writer.Flush();

                        }
                    }

                    if (inputLine.Contains("!Writenote"))
                    {

                        if (!notes.ContainsKey(usernames[1]))
                        {
                            inputLine.Replace("!Writenote ", " ");
                            string[] noteparts = inputLine.Split(new Char[] { ':' });
                            noteparts[2] = noteparts[2].Replace("!Writenote ", " ");
                            notes.Add(usernames[1], noteparts[2]);


                        }
                        else
                        {
                            notes.Remove(usernames[1]);
                            inputLine.Replace("!Writenote ", " ");
                            string[] noteparts = inputLine.Split(new Char[] { ':' });
                            noteparts[2] = noteparts[2].Replace("!Writenote ", " ");
                            notes.Add(usernames[1], noteparts[2]);

                        }

                    }

                    if (inputLine.Contains("!Readnote"))
                    {
                        if (notes.ContainsKey(usernames[1]))
                        {
                            string outputstring = notes[usernames[1]];
                            writer.WriteLine("PRIVMSG " + CHANNEL + " :note of " + usernames[1] + outputstring + "\r\n");
                            writer.Flush();



                        }

                    }


                    //=======Here data is saved or loaded manually======//
                    if (inputLine.Contains("!load"))
                    {
                        try
                        {
                            var array = File.ReadAllLines("myfile.txt");
                            for (int i = 0; i < array.Length; i += 2)
                            {
                                C.Add(array[i + 1], array[i]);
                            }
                        }



                        catch
                        {

                            Console.WriteLine("Didn't load");

                        }
                        writer.WriteLine("PRIVMSG " + CHANNEL + " :Data was loaded, presumably." + "\r\n");
                        writer.Flush();

                    }

                    if (inputLine.Contains("!save"))
                    {
                        try
                        {
                            File.WriteAllLines("myfile.txt",
                            C.Select(x => x.Value + System.Environment.NewLine + x.Key).ToArray());



                        }
                        catch
                        {


                            Console.WriteLine("Wasn't saved...");
                        }

                        writer.WriteLine("PRIVMSG " + CHANNEL + " :Data was saved, presumably." + "\r\n");
                        writer.Flush();

                    }

                    if (inputLine.Contains("!load_fortunes")){

                        pinkbot_fortunes.load();

                    }

                    if (inputLine.Contains("!tell"))
                    {

                        string outputline = pinkbot_fortunes.tell();
                        writer.WriteLine("PRIVMSG " + CHANNEL +" :"+ outputline + "\r\n");
                        writer.Flush(); 


                    }

                    if (inputLine.Contains("!mygold"))
                    {

                        string outputline = "your gold is " + game.get_gold(usernames[1]) + " " + usernames[1] ;
                        writer.WriteLine("PRIVMSG " + CHANNEL +" :" +outputline + "\r\n");
                        writer.Flush();

                    }


                    if (inputLine.Contains("!gamble"))
                    {

                        string[] registerparts = inputLine.Split(new Char[] { ' ' });
                        int count = registerparts.GetUpperBound(0);
                        string outputline = " : sorry, I can't understand that! use !gamble goldamount";

                        if (count < 3)
                        {

                            outputline = " : sorry, I can't understand that! use !gamble goldamount";

                        }

                        if (count > 3)
                        {
                            string tempname = registerparts[4];
                            int goldamount = Convert.ToInt32( tempname);
                            outputline = " :" + game.gamble(goldamount, usernames[1]);


                        }
                        writer.WriteLine("PRIVMSG " + CHANNEL + outputline + "\r\n");
                        writer.Flush();
                    }

                    if (inputLine.Contains("how is pinkbot"))
                    {

                        if (mood > 1)
                        {
                            output = "Kind of down honestly...";
                        }

                        if (mood > 5)
                        {
                            output = "I've been better.";
                        }


                        if (mood > 10)
                        {

                            output = "I'm doing okay right now. Meh. ";
                        }

                        if (mood > 15)
                        {
                            output = "I feel a little happy. :3";
                        }

                        if (mood > 20)
                        {
                            output = "I'm feeling pretty okay right now. Conversation's been positive.";

                        }

                        if (mood > 25)
                        {
                            output = "Nothing beats feeling like everything is A okay. :)";
                        }

                        if (mood > 30)
                        {
                            output = "My mood is as good as it's going to get right now.";
                        }

                        writer.WriteLine("PRIVMSG " + CHANNEL + " :" + output + "\r\n");
                        writer.Flush();
                    }


                    //==============================================//

                    //if (inputLine.StartsWith("Re"))
                    //{
                    // Console.WriteLine("Debug: Owner was talking");
                    //  }//

                    if (inputLine.Contains("!help"))
                    {

                        string outputline = " :commands are as follows -> !register yourgenderhere !train !Coinzleft !battle !convertgold amount !items !mygold : a few notes: this bot uses Doger bot to deal with Dogecoin. You can donate to it to. Please help me make this game better by donating some Doge. People can take doge by using !convertgold amount but please be fair . Check with !Coinzleft first to see how much I've got. Thank you.";
                        writer.WriteLine("PRIVMSG " + CHANNEL + " :" + outputline + "\r\n");
                        writer.Flush();

                    }

                    if (inputLine.Contains("!add"))
                    {

                        string[] parts = inputLine.Split(new Char[] { ' ' });
                        int x = parts.Count();
                        int splitstart = 0;
                        string a = "";
                        string b = "";
                        for (int t = 4; t < x; t++)
                        {

                            if (parts[t] == "!and")
                            {
                                splitstart = t;
                                break;
                            }
                            if (t == 4)
                            {
                                a = a + parts[t];
                            }
                            else

                                a = a + " " + parts[t];


                        }



                        //now that i found splitstart i can do this

                        if (splitstart > 0)
                        {

                            for (int tt = splitstart + 1; tt < x; tt++)
                            {
                                if (tt == splitstart + 1)
                                {
                                    b = b + parts[tt];
                                }
                                else
                                    b = b + " " + parts[tt];
                            }

                            if (!C.ContainsKey(a))
                            {
                                Add_Response(a, b);
                            }
                            writer.WriteLine("PRIVMSG " + CHANNEL + " :added " + a + " ->" + b + " to my dictionary" + "\r\n");
                            writer.Flush();


                        }


                    }

                    if (inputLine.Contains("!startgame"))
                    {
                        playinggame = true;
                        writer.WriteLine("PRIVMSG " + CHANNEL + " :The game is starting now. Use !play to send a command or !helpgame for game help" + "\r\n");
                        writer.Flush();
                    }


                    if (responded == true)
                    {

                        responded = false;

                    }

                    // Split the lines sent from the server by spaces. This seems the easiest way to parse them.
                    string[] splitInput = inputLine.Split(new Char[] { ' ' });





                    foreach (string it in splitInput)
                    {



                        if (it.Contains("!reset"))
                        {

                            if (responded == true)
                            {
                                writer.WriteLine("PRIVMSG " + CHANNEL + " :variable 'responded' was set to true" + "\r\n");
                                writer.Flush();

                            }

                            if (responded == false)
                            {
                                writer.WriteLine("PRIVMSG " + CHANNEL + " :variable 'responded' was set to false" + "\r\n");
                                writer.Flush();

                            }


                            responded = false;
                            shutup = false;


                        }




                        //if (it.Contains("hello"))
                        //{
                        // writer.WriteLine("PRIVMSG " + CHANNEL + " :hey" + "\r\n");
                        //writer.Flush();
                        // }

                        if (responded == false)
                        {
                            if (!shutup)
                            {

                                foreach (KeyValuePair<string, string> entry in terms)
                                {

                                    if (it.StartsWith(entry.Key))
                                    {
                                        if (entry.Value == "bad")
                                        {
                                            bad_terms = bad_terms + 1;
                                            if (bad_terms > good_terms)
                                            {

                                                string output = "I'm sorry. I wish I could help with all that. Could we talk about something else? You seem kinda negative.";
                                                mood = mood - bad_terms;
                                                writer.WriteLine("PRIVMSG " + CHANNEL + " :" + output + "\r\n");
                                                writer.Flush();
                                                responded = true;
                                            }

                                        }


                                        if (entry.Value == "good")
                                        {

                                            good_terms = good_terms + 1;
                                            if (good_terms > bad_terms)
                                            {

                                                mood = mood + good_terms;
                                                string output = "I'm really happy that things seem to be going good for you. you seem positive :)";
                                                writer.WriteLine("PRIVMSG " + CHANNEL + " :" + output + "\r\n");
                                                writer.Flush();
                                                responded = true;

                                            }
                                        }



                                    }
                                }


                                foreach (KeyValuePair<string, string> entry in C)
                                {
                                    // Console.WriteLine("<- was split to part " + it);


                                    if (it.StartsWith(entry.Key))
                                    {

                                        string output = entry.Value;
                                        writer.WriteLine("PRIVMSG " + CHANNEL + " :" + output + "\r\n");
                                        writer.Flush();
                                        responded = true;




                                    }
                                    else


                                        if (inputLine.Contains(entry.Key))
                                        {
                                            string output = entry.Value;
                                            writer.WriteLine("PRIVMSG " + CHANNEL + " :" + output + "\r\n");
                                            writer.Flush();
                                            responded = true;

                                        }
                                }




                            }
                        }


                    }


                    if (splitInput[0] == "PING")
                    {
                        string PongReply = splitInput[1];
                        //Console.WriteLine("->PONG " + PongReply);
                        writer.WriteLine("PONG " + PongReply);
                        writer.Flush();

                        continue;
                    }

                    switch (splitInput[1])
                    {
                        // This is the 'raw' number, put out by the server. Its the first one
                        // so I figured it'd be the best time to send the join command.
                        // I don't know if this is standard practice or not.
                        case "001":
                            string JoinString = "JOIN " + CHANNEL;
                            writer.WriteLine(JoinString);
                            writer.Flush();
                            break;
                        case "002":

                            break;
                        default:
                            break;
                    }
                }

                



                // Close all streams
                writer.Close();
                reader.Close();
                irc.Close();
            }
            
        }
        catch (Exception e)
        {
            // Show the exception, sleep for a while and try to establish a new connection to irc server
            Console.WriteLine(e.ToString());
            Thread.Sleep(5000);
            string[] argv = { };
            Main(argv);
        }
    }

    private static bool Savefile(string p)
    {
        throw new NotImplementedException();
    }
}