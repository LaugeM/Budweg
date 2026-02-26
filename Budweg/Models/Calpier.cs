using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Models
{
    public class Calpier
    {
        public int StampNumber { get; set; }
        public string Manufacturer { get; set; }
        public bool Approval { get; set; }
        public string ModelNumber { get; set; }
        public DateOnly? RegistrationDate { get; set; }
    }
}
