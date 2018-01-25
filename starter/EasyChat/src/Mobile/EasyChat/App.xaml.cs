using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using EasyChat.Auth;
using EasyChat.Auth.Events;
using EasyChat.Services;
using EasyChat.Views;
using Prism.Unity;
using Prism.Events;
using Prism.Ioc;
using Prism.Logging;
using Prism.Modularity;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EasyChat
{
    public partial class App : PrismApplication
    {
        public App()
            : base(null)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            var ea = Container.Resolve<IEventAggregator>();
            ea.GetEvent<AuthenticatedEvent>().Subscribe(OnUserAuthenticated);

            //NavigationService.NavigateAsync("SplashPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ILoggerFacade, Services.DebugLogger>();
            containerRegistry.Register<IChatRelayService, ChatRelayService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ChatPage>();
            containerRegistry.RegisterForNavigation<SettingsPage>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // Note that you can change this sample to use Prism.Unity.Forms without any
            // changes in the AuthModule as the Module only relies on Prism.Forms
            moduleCatalog.AddModule<AuthModule>();
        }

        private async void OnUserAuthenticated(bool authenticated)
        {
            // TODO: Initialize the StyleSheets once you have setup the Azure Function
            await NavigationService.NavigateAsync("/NavigationPage/ChatPage");
        }
    }
}
