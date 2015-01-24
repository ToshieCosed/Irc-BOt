using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    [Serializable]
    class NPC
    {
        List<parts> mylist = new List<parts>();

        public void initialize()
        {

            parts mypart = new parts();
            mypart.initialize(true, true, true);

        }

    
    }


}
