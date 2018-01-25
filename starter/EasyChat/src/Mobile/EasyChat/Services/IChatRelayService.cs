using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyChat.Models;

namespace EasyChat.Services
{
    public interface IChatRelayService
    {
        bool IsConnected { get; }
        Task StartRelayAsync(IList<ChatMessage> messages);
        Task StopRelayAsync();
        Task<ChatMessage> SendMessageAsync(string message);
        bool CanSendMessage(string message);
    }
}
