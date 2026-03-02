using System;
using System.Collections.Generic;
using System.Text;
using Budweg.Models;

namespace Budweg.ViewModels
{
    public class RegCaliperViewModel : BaseRegisterViewModel<Caliper>
    {
        public RegCaliperViewModel() : base(new CaliperRepository())
        {
        }

        public override bool CheckEntity()
        {
            return true;
        }

    }
}
