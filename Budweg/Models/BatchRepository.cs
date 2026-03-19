using System;
using System.Collections.Generic;
using System.Text;

namespace Budweg.Models
{
    public class BatchRepository : BaseRepo<Batch>
    {
        public BatchRepository() : base()
        {
        }

        public override void Add(Batch batch)
        {
        }

        public override List<Batch> GetAll()
        {
            List<Batch> batches = new();
            return batches;
        }
    }
}
