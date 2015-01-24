using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class fortunes{

        List<string> tells = new List<string>();
         int amount_of_fortunes=0;


        public void load()
        {

            try
            {
                var array = File.ReadAllLines("fortunes.txt");
                for (int i = 0; i < array.Length; i++)
                {
                    amount_of_fortunes++;
                    tells.Add(array[i].ToString());
                    Console.WriteLine( array[i]);

                }
            }



            catch
            {

                Console.WriteLine("Didn't load");
            }




        }


        public string tell()
        {
            string event_string = " ";

            Random fortune_number = new Random();
            int num = fortune_number.Next(amount_of_fortunes);
            string[] temparr = tells.ToArray();
            event_string = temparr[num];


            return event_string;

        }
    }
}
