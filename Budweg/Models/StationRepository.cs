using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Models
{
    public class StationRepository : BaseRepo<Station>
    {
        public StationRepository() : base()
        {
        }

        public override void Add(Station station)
        {
        }

        public override List<Station> GetAll()
        {
            List<Station> stations = new();
            return stations;
        }
    }
}
