using System;
using EasyChat.Auth.Events;
using EasyChat.Helpers;
using EasyChat.ViewModels;
using Prism.Events;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace EasyChat.Auth.ViewModels
{
    public class AuthModuleViewModelBase : ViewModelBase
    {
        protected IEventAggregator _eventAggregator { get; }
        protected IAppSettings _settings { get; }

        public AuthModuleViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService, ILoggerFacade logger,
                                       IEventAggregator eventAggregator, IAppSettings settings)
            : base(navigationService, pageDialogService, logger)
        {
            _eventAggregator = eventAggregator;
            _settings = settings;
        }

        protected void RaiseAuthenticatedEvent() =>
            _eventAggregator.GetEvent<AuthenticatedEvent>().Publish(true);
    }
}
