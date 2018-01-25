using System;
using EasyChat.Helpers;
using EasyChat.Models;
using Xamarin.Forms;

namespace EasyChat.Controls
{
    public class ChatMessageDataTemplateSelector : DataTemplateSelector<ChatMessage>
    {
        static DataTemplate IncomingTemplate => new DataTemplate(typeof(IncomingMessage));
        static DataTemplate OutgoingTemplate => new DataTemplate(typeof(OutgoingMessage));
        static IAppSettings AppSettings => Settings.Current;

        protected override DataTemplate OnSelectTemplate(ChatMessage item, BindableObject container)
        {
            return item.Name == AppSettings.UserName ? OutgoingTemplate : IncomingTemplate;
        }
    }
}
