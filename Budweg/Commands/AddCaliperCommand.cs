using Budweg.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Budweg.Commands
{
    public class AddCaliperCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            bool result = true;

            if (parameter is RegCaliperViewModel rvm)
            {
                if (rvm.CheckEntity() == false )
                {
                    result = false;
                }
            }
            return result;
        }

        public void Execute(object parameter)
        {
            if (parameter is RegCaliperViewModel rvm)
            {
                rvm.AddToRepo();
            }
            else
            {
                throw new ArgumentException("Illegal parameter type");
            }
        }
    }
}
