using System;
using System.ComponentModel;
using EasyChat.Helpers;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace EasyChat.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private IAppSettings _appSettings { get; }

        public SettingsPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILoggerFacade logger,
                                     IAppSettings appSettings)
            : base(navigationService, pageDialogService, logger)
        {
            _appSettings = appSettings;
            _appSettings.PropertyChanged += OnSettingChanged;
        }

        public string IncommingColor
        {
            get => _appSettings.IncommingColor;
            set => _appSettings.IncommingColor = value;
        }

        public string OutgoingColor
        {
            get => _appSettings.OutgoingColor;
            set => _appSettings.OutgoingColor = value;
        }

        public override void Destroy()
        {
            _appSettings.PropertyChanged -= OnSettingChanged;
        }

        private void OnSettingChanged(object sender, PropertyChangedEventArgs e)
        {
            // TODO: Be sure to update the style sheet when the user updates a value
            RaisePropertyChanged(e.PropertyName);
        }
    }
}
