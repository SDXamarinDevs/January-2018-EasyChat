using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyChat.Helpers;
using EasyChat.Models;
using Newtonsoft.Json;
using Prism.Mvvm;

namespace EasyChat.Services
{
    public class ChatRelayService : BindableBase, IChatRelayService
    {
        private IAppSettings _settings { get; }

        private ClientWebSocket _client { get; }

        private CancellationTokenSource _cts { get; }

        public ChatRelayService(IAppSettings settings)
        {
            _settings = settings;
            _client = new ClientWebSocket();
            _cts = new CancellationTokenSource();
        }

        public bool IsConnected => _client.State == WebSocketState.Open;

        public async Task<ChatMessage> SendMessageAsync(string message)
        {
            if (!CanSendMessage(message))
                return null;

            var chatMessage = new ChatMessage
            {
                Text = message,
                Name = _settings.UserName,
                MessagDateTime = DateTime.Now
            };

            string serializedMessage = JsonConvert.SerializeObject(chatMessage);

            var byteMessage = Encoding.UTF8.GetBytes(serializedMessage);
            var segmnet = new ArraySegment<byte>(byteMessage);

            await _client.SendAsync(segmnet, WebSocketMessageType.Text, true, _cts.Token);

            return chatMessage;
        }

        public async Task StartRelayAsync(IList<ChatMessage> messages)
        {
            await _client.ConnectAsync(new Uri(AppConstants.WebSocketHost), _cts.Token);
            RaisePropertyChanged(nameof(IsConnected));

            await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    WebSocketReceiveResult result;
                    var message = new ArraySegment<byte>(new byte[4096]);
                    do
                    {
                        result = await _client.ReceiveAsync(message, _cts.Token);
                        var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                        string serializedMessage = Encoding.UTF8.GetString(messageBytes);

                        try
                        {
                            var msg = JsonConvert.DeserializeObject<ChatMessage>(serializedMessage);
                            messages.Add(msg);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Invalide message format. {ex.Message}");
                        }

                    } while (!result.EndOfMessage);
                }
            }, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public Task StopRelayAsync() =>
            _client.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, _cts.Token);

        public bool CanSendMessage(string message) => IsConnected && !string.IsNullOrEmpty(message);
    }
}
