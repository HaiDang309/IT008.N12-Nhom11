using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CinemaManagement.Views
{
    public partial class MessageBoxCustom : Window
    {
        public bool DialogResult { get; set; }

        public MessageBoxCustom(string Title, string Message, MessageType Type, MessageButtons Buttons)
        {
            InitializeComponent();
            string _title = Title;
            string _msg = Message;
            MessageBoxButton msgBtn = MessageBoxButton.OK;
            MessageBoxImage msgImg = MessageBoxImage.None;
            switch (Type)
            {

                case MessageType.Info:
                    msgImg = MessageBoxImage.Information;
                    break;
                case MessageType.Success:
                    msgImg = MessageBoxImage.None;
                    break;
                case MessageType.Warning:
                    msgImg = MessageBoxImage.Warning;
                    break;
                case MessageType.Error:
                    msgImg = MessageBoxImage.Error;
                    break;
            }
            switch (Buttons)
            {
                case MessageButtons.OKCancel:
                    msgBtn = MessageBoxButton.OKCancel;
                    break;
                case MessageButtons.YesNo:
                    msgBtn = MessageBoxButton.YesNo;
                    break;
                case MessageButtons.OK:
                    msgBtn = MessageBoxButton.OK;
                    break;
            }

            MessageBoxResult rs = System.Windows.MessageBox.Show(_msg, _title, msgBtn, msgImg);
            if (rs == MessageBoxResult.Yes)
            {
                this.DialogResult = true;
            }
            else if (rs == MessageBoxResult.Cancel)
            {
                this.DialogResult = false;
            }
            else if (rs == MessageBoxResult.No)
            {
                this.DialogResult = false;
            }
            else if (rs == MessageBoxResult.OK)
            {
                this.DialogResult = true;
            }
        }
    }
    public enum MessageButtons
    {
        OKCancel,
        YesNo,
        OK,
    }
    public enum MessageType
    {
        Info,
        Success,
        Warning,
        Error,
    }
}
