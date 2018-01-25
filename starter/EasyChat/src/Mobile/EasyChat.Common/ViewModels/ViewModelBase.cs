using System;
using Prism.AppModel;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace EasyChat.ViewModels
{
    public abstract class ViewModelBase : BindableBase, INavigationAware, IPageLifecycleAware, IDestructible
    {
        protected INavigationService _navigationService { get; }
        protected IPageDialogService _pageDialogService { get; }
        protected ILoggerFacade _logger { get; }

        public ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService, ILoggerFacade logger)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _logger = logger;
            IsNotBusy = !IsBusy;
            System.Diagnostics.Debug.WriteLine($"Loaded {GetType().Name}");
        }

        public string Title { get; set; }

        public bool IsBusy { get; set; }

        public bool IsNotBusy { get; set; }

        private void OnIsBusyChanged() => IsNotBusy = !IsBusy;

        private void OnIsNotBusyChanged() => IsBusy = !IsNotBusy;

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        public virtual void Destroy()
        {
        }
    }
}
