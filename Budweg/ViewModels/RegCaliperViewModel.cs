using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Budweg.Commands;
using Budweg.Models;

namespace Budweg.ViewModels
{
    public class RegCaliperViewModel : BaseRegisterViewModel<Caliper>
    {
        public RegCaliperViewModel() : base(new CaliperRepository())
        {
            CurrentEntity.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);
        }

        public ICommand AddCaliperCommand { get; } = new AddCaliperCommand();


        public IEnumerable<KeyValuePair<bool, string>> ApprovedOptions { get; } =
        new[]
        {
                new KeyValuePair<bool, string>(true, "Godkendt"),
                new KeyValuePair<bool, string>(false, "Ikke godkendt")
        };


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
