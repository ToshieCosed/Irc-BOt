using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Debug_Info
        
    {
       public int message_count;


       public void iterate_message_count(){
           message_count++; 

            
        }

       public int get_message_count()
       {
           return message_count;

       }

    }
}
