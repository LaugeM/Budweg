using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Models
{
    public class Station
    {
        public int StationNumber { get; set; }
        public string StationName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Station()
        {
        }
    }
}
