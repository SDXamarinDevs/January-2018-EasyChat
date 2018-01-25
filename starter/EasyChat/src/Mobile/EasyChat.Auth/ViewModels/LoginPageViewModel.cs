using System;
using EasyChat.Helpers;
using Prism.Events;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;
using Xamarin.Forms;
using Prism.Commands;

namespace EasyChat.Auth.ViewModels
{
    public class LoginPageViewModel : AuthModuleViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILoggerFacade logger,
                                  IEventAggregator eventAggregator, IAppSettings settings)
            : base(navigationService, pageDialogService, logger, eventAggregator, settings)
        {
            LoginCommand = new DelegateCommand(OnLoginCommandExecuted, () => !string.IsNullOrWhiteSpace(UserName))
                .ObservesProperty(() => UserName);
            Title = "Login";
        }

        public string UserName { get; set; }

        public DelegateCommand LoginCommand { get; }

        private void OnLoginCommandExecuted()
        {
            _settings.UserName = UserName;
            RaiseAuthenticatedEvent();
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            UserName = _settings.UserName;
        }
    }
}
