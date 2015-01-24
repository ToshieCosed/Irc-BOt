using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

    class talker
    {
        private Dictionary<string, string> library = new Dictionary<string, string>();



        public string look_up(string this_phrase)
        {
            string result_string = "";

            if (!library.ContainsKey(this_phrase))
            {

                string[] split_string = this_phrase.Split(new Char[] { ' ' });
                bool found = false;
                foreach (string tempstring in split_string){
                    int match_count = 0;
                    foreach (var entry in library)
                    {
                        if (entry.Key.Contains( tempstring)){
                            match_count = match_count + 1;

                        }

                        if (match_count == split_string.Count())
                        {
                            result_string = entry.Value;
                            found = true;
                            break;

                        }

                    }
                    if (found == true)
                    {
                        break;
                    }

                }

               

            }

            if (library.ContainsKey(this_phrase)){

                result_string =library[this_phrase] ;
            }


            return result_string;
        }

        public void add_keys(string key, string value){

            if (!library.ContainsKey(key)) {

                library.Add(key, value);

            }

        }

    }
}
