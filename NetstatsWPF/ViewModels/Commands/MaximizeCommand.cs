﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Netstats.ViewModels.Commands
{
    public class MaximizeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;


        public ShellViewModel ViewModel { get; set; }

        public MaximizeCommand(ShellViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.ViewModel.Maximize();
        }
    }
}