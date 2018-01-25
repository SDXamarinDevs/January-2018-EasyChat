using System;
using EasyChat.Auth.Views;
using EasyChat.Helpers;
using Prism;
using Prism.Ioc;
using Prism.Logging;
using Prism.Modularity;
using Prism.Navigation;
using Xamarin.Forms;

namespace EasyChat.Auth
{
    public class AuthModule : IModule
    {
        public async void OnInitialized(IContainerProvider containerProvider)
        {
            var navService = containerProvider.Resolve<INavigationService>(PrismApplicationBase.NavigationServiceName);
            await navService.NavigateAsync("SplashPage");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IAppSettings>(Settings.Current);
            containerRegistry.RegisterForNavigation<LoginPage>();
            containerRegistry.RegisterForNavigation<SplashPage>();
        }
    }
}
