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
            CurrentEntity.Approval = false;
            CurrentEntity.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);
        }



        public override bool CheckEntity()
        {
            bool result = true;

            if (string.IsNullOrWhiteSpace(CurrentEntity.Manufacturer))
            {
                result = false;
            }

            if (string.IsNullOrWhiteSpace(CurrentEntity.ModelNumber))
            {
                result = false;
            }

            return result;
        }

    }
}
