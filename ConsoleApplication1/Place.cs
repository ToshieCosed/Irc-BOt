using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    [Serializable]
    class Place
    {
        public string name;
        private List<NPC> npcs = new List<NPC>();

        private void add_npc(NPC temp_npc){

            npcs.Add(temp_npc);

        }
    }

   

}

