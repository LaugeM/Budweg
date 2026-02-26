using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Models
{
    public class Batch
    {
        public int BatchNumber { get; set; }
        public DateOnly? PickDate { get; set; }
        public int Quantity { get; set; }
        public bool ProcessStatus { get; set; }
        private DateTime SaleDate { get; set; }
    }
}
