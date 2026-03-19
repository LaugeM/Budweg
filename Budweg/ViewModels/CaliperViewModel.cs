using System;
using System.Collections.Generic;
using System.Text;
using Budweg.Models;

namespace Budweg.ViewModels
{
    public class CaliperViewModel : BaseViewModel<Caliper>
    {
        private Caliper caliper;

        private string _stampNumber = string.Empty;
        public string StampNumber
        {
            get => _stampNumber;
            set
            {
                if (_stampNumber == value) return;
                _stampNumber = value;
                OnPropertyChanged();
            }
        }

        private string _manufacturer = string.Empty;
        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                if (_manufacturer == value) return;
                _manufacturer = value;
                OnPropertyChanged();
            }
        }

        private bool _approval;
        public bool Approval
        {
            get => _approval;
            set
            {
                if (_approval == value) return;
                _approval = value;
                OnPropertyChanged();
            }
        }

        private string _modelNumber = string.Empty;
        public string ModelNumber
        {
            get => _modelNumber;
            set
            {
                if (_modelNumber == value) return;
                _modelNumber = value;
                OnPropertyChanged();
            }
        }

        private int? _timesRenovated;
        public int? TimesRenovated
        {
            get => _timesRenovated;
            set
            {
                if (_timesRenovated == value) return;
                _timesRenovated = value;
                OnPropertyChanged();
            }
        }

        public CaliperViewModel(Caliper caliper) : base(caliper)
        {
            this.caliper = caliper;
            _stampNumber = caliper.StampNumber;
            _manufacturer = caliper.Manufacturer;
            _approval = caliper.Approval;
            _modelNumber = caliper.ModelNumber;
            _timesRenovated = caliper.TimesRenovated;
        }

    }
}
