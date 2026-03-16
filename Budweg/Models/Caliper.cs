using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Models
{
    public class Caliper
    {
        public string StampNumber { get; set; }
        public int? BatchNumber { get; set; }
        public string Manufacturer { get; set; }
        public bool Approval { get; set; } = false;
        public string ModelNumber { get; set; }
        public DateOnly? RegistrationDate { get; set; }
        public int? TimesRenovated { get; set; }

        public Caliper()
        {
        }
    }
}
