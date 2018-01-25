using System;
using EasyChat.Models;
using EasyChat.Services;
using MvvmHelpers;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace EasyChat.ViewModels
{
    public class ChatPageViewModel : ViewModelBase
    {
        IChatRelayService _chatRelay { get; }
        public ChatPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILoggerFacade logger,
                                 IChatRelayService chatRelay)
            : base(navigationService, pageDialogService, logger)
        {
            _chatRelay = chatRelay;
            Messages = new ObservableRangeCollection<ChatMessage>();
            SendCommand = new DelegateCommand(OnSendCommandExecuted, () => _chatRelay.CanSendMessage(MessageText) && IsNotBusy)
            .ObservesProperty(() => MessageText)
            .ObservesProperty(() => IsNotBusy);
            IsBusy = true;
            //.ObservesProperty(() => _chatRelay.IsConnected);
            Title = "Easy Chat";
            LogoutCommand = new DelegateCommand(async () => await _navigationService.NavigateAsync("/LoginPage"));
        }

        public ObservableRangeCollection<ChatMessage> Messages { get; set; }

        public string MessageText { get; set; }

        public DelegateCommand SendCommand { get; }

        public DelegateCommand LogoutCommand { get; }

        public override async void OnAppearing()
        {
            try
            {
                IsBusy = false;
                if (!_chatRelay.IsConnected)
                    await _chatRelay.StartRelayAsync(Messages);
            }
            catch (Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync(ex.GetType().Name, ex.Message, "Ok");
            }
        }

        public override async void OnDisappearing()
        {
            try
            {
                IsBusy = true;
                await _chatRelay.StopRelayAsync();
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString(), Category.Exception, Priority.None);
            }
        }

        private async void OnSendCommandExecuted()
        {
            await _chatRelay.SendMessageAsync(MessageText);
            MessageText = string.Empty;
        }
    }
}
