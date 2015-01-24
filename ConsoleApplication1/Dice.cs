using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    [Serializable]
    public class Dice
    {

        public int roll()
        {

            Random roll = new Random();
            return roll.Next(11)+1;

        }
    }
}
