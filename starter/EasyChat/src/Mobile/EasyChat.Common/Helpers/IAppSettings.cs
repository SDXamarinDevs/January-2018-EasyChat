using System;
using System.ComponentModel;

namespace EasyChat.Helpers
{
    public interface IAppSettings : INotifyPropertyChanged
    {
        string UserName { get; set; }
        string IncommingColor { get; set; }
        string OutgoingColor { get; set; }
    }
}
