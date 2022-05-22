using Application.Dto;
using Microsoft.AspNetCore.SignalR.Client;

namespace ClientConsoleService
{
    class Program
    {
        private static HubConnection _hubConnection;

        static async Task Main(string[] args)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/join")
                .Build();

            _hubConnection
                .On<string>("Notify", message => Console.WriteLine($"Message from server: {message}"));

            await _hubConnection.StartAsync();

            var joinRoomDto = new JoinRoomDto
            {
                UserId = Guid.Parse(Console.ReadLine()),
                GameRoomId = Guid.Parse(Console.ReadLine())
            };

            await _hubConnection.SendAsync("JoinRoom", joinRoomDto);

            var isExit = false;
            while (!isExit)
            {
                Console.WriteLine("JOINed THE ROOM");
                var message = Console.ReadLine();

                if (message == "exit")
                    isExit = true;
            }

            Console.ReadLine();
        }
    }
}