using System;
using Prism.Mvvm;

namespace EasyChat.Models
{
    public class ChatMessage : BindableBase
    {
        public string Text { get; set; }

        public string Name { get; set; }

        public DateTime MessagDateTime { get; set; }
    }
}
