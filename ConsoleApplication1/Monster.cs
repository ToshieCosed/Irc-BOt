using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    [Serializable]
    public class Monster
    {
        string locations = "swamp, river, mountain";
        public List<string> monster_names = new List<string>();

        public void initialize()
        {
            monster_names.Add("Chigarus");
            monster_names.Add("Pignomer");
            monster_names.Add("SwordMantarus");
            monster_names.Add("SwordmantaSru");
            monster_names.Add("GigaLantrus");
            monster_names.Add("Figgarus");
            monster_names.Add("Pythonagorus");
            monster_names.Add("Fishtaillambada");
            monster_names.Add("Skeletorinian-Cowfinch");
            monster_names.Add("Buggazuttus");
            monster_names.Add("Abeltaurian");
            monster_names.Add("Rock-gregarian");
            monster_names.Add("Famblata-BAM!");
            monster_names.Add("North-Swoo-egger");
            monster_names.Add("South-swoo-egger");
            monster_names.Add("East-swoo-egger");
            monster_names.Add("West-swoo-egger");
            monster_names.Add("Ikesthanaruut");
            monster_names.Add("Ikesthanthoroughs");
            monster_names.Add("Kaler-Whale");
            monster_names.Add("SeaFin-Blossom");
            monster_names.Add("Kinnot-fish");
            monster_names.Add("Iksanthanthoroot-Double-Meat-Monster");
            monster_names.Add("Buzza-zer-Baroo-UU-Min");
            monster_names.Add("Hiking-bariking-Tike-Crab");
            monster_names.Add("Pillow-sworn-noble-Justic-Fish");
            monster_names.Add("octo-begoto-robotto-Finch-Wurm");
            monster_names.Add("Venarious-Venemous-Bettle-Moth");
            monster_names.Add("Sailing-Lamp-Turn-Toad");
            monster_names.Add("skeleton");
         



        }

        public string random_monster()
        {
            string monstername = "";
            var anyindex = monster_names.Count();
            Random this_mon = new Random();
            int monin = this_mon.Next(anyindex);
            monstername = monster_names[monin];


            return monstername;

        }
    }
}