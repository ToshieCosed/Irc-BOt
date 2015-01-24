using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    [Serializable]
    public class Map_class
    {

        private List<Place> places = new List<Place>();

        public void initialize_place(string placename)
        {
            Place temp_place = new Place();
            temp_place.name = placename;
            Add_place(temp_place);

        }

        private void Add_place(Place temp_place)
        {
            places.Add(temp_place);

        }


    }
}
