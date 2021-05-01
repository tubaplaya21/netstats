using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Netstats.ViewModels.Commands
{
    public class ConnectDeviceCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;


        public ShellViewModel ViewModel { get; set; }

        public ConnectDeviceCommand(ShellViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.ViewModel.ConnectDevice((string)parameter);
        }
    }
}