
using ReactiveUI;
using System;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Text;
using Avalonia.Platform;
using System.Threading;
using System.Windows.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Microsoft.AspNetCore.SignalR.Client;

namespace ControlRoom.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    private string _speed = "0";
    private HubConnection connection;

    public MainViewModel()
    {
        connection = new HubConnectionBuilder()
        .WithUrl("http://localhost:5000/speedhub")
        .Build();

        Connect(connection);

        connection.On<string>("ReceiveSpeed", (speed) =>
        {
            Speed = speed;
        });
    }

    public string Speed
    {
        get { return _speed; }
        set { this.RaiseAndSetIfChanged(ref _speed, value); }
    }

    static async Task Connect(HubConnection connection)
    {
        try
        {
            await connection.StartAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

