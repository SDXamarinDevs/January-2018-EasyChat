using System;
using EasyChat.Helpers;
using EasyChat.ViewModels;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace EasyChat.Auth.ViewModels
{
    public class SplashPageViewModel : AuthModuleViewModelBase
    {
        public SplashPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILoggerFacade logger,
                                   IEventAggregator eventAggregator, IAppSettings settings)
            : base(navigationService, pageDialogService, logger, eventAggregator, settings)
        {
            Title = "Loading Page";
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            await System.Threading.Tasks.Task.Delay(2000);
            var userName = _settings.UserName;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var authenticated = await _pageDialogService.DisplayAlertAsync("Login", $"Continue as '{userName}'?", "Yes", "No");
                if (authenticated)
                {
                    RaiseAuthenticatedEvent();
                    return;
                }
            }
            await _navigationService.NavigateAsync("/LoginPage");
        }
    }
}
