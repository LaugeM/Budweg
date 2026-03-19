using Budweg.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Budweg.ViewModels
{
    public class CaliperListViewModel : BaseListViewModel<Caliper>
    {
        public CaliperListViewModel() : base(new CaliperRepository())
        {
            InitializeEntityOC();
        }

        protected override void InitializeEntityOC()
        {
            try
            {
                foreach (Caliper c in entityRepo.GetAll())
                {
                    CaliperViewModel cvm = new(c);
                    EntityVMOC.Add(cvm);
                }
            }
            catch (IOException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
