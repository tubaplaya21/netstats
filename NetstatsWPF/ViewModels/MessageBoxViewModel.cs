using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using Netstats.ViewModels.Commands;
using System.Windows;

namespace Netstats.ViewModels
{
    public class MessageBoxViewModel : Screen
    {
        public enum MessageType
        {
            Info,
            Confirmation,
            Success,
            Warning,
            Error,
        }

        public enum MessageButtons
        {
            OkCancel,
            YesNo,
            Ok,
        }
        public CloseCommand CloseCommand { get; set; }
        public ReturnYesCommand ReturnYesCommand { get; set; }
        public string Message { get; set; }
        public string Title { get; set; } = "";
        public Visibility OkVisibility { get; set; }
        public Visibility CancelVisibility { get; set; }
        public Visibility YesVisibility { get; set; }
        public Visibility NoVisibility { get; set; }
        public bool DialogResult { get; set; }

        public MessageBoxViewModel(string message, MessageType type, MessageButtons buttons)
        {
            this.CloseCommand = new CloseCommand(this);
            this.ReturnYesCommand = new ReturnYesCommand(this);
            Message = message;

            switch (type)
            {
                case MessageType.Info:
                    Title = "Info";
                    break;
                case MessageType.Confirmation:
                    Title = "Confirmation";
                    break;
                case MessageType.Success:
                    Title = "Success";
                    break;
                case MessageType.Warning:
                    Title = "Warning";
                    break;
                case MessageType.Error:
                    Title = "Error";
                    break;
                default:
                    break;
            }

            switch (buttons)
            {
                case MessageButtons.OkCancel:
                    YesVisibility = Visibility.Collapsed;
                    NoVisibility = Visibility.Collapsed;
                    OkVisibility = Visibility.Visible;
                    CancelVisibility = Visibility.Visible;
                    break;
                case MessageButtons.YesNo:
                    YesVisibility = Visibility.Visible;
                    NoVisibility = Visibility.Visible;
                    OkVisibility = Visibility.Visible;
                    CancelVisibility = Visibility.Visible;
                    break;
                case MessageButtons.Ok:
                    YesVisibility = Visibility.Collapsed;
                    NoVisibility = Visibility.Collapsed;
                    OkVisibility = Visibility.Visible;
                    CancelVisibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }

        public void SelectYes(Window window)
        {
            window.DialogResult = true;
            window.Close();
        }

        public void CloseWindow(Window window)
        {
            window.DialogResult = false;
            window.Close();
        }
    }
}
