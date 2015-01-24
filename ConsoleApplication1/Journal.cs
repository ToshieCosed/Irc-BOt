using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    [Serializable]
    public class Journal
    {
        string this_entry;


        public string return_entry(){

            return this_entry;

        }

        public void set_entry(string entry)
        {

            this_entry = entry;

        }


    }




}

