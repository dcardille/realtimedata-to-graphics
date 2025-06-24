using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes; // For IClassicDesktopStyleApplicationLifetime
using Avalonia.ReactiveUI;
using System;

// Replace with your application's namespace
using ControlRoom;              
using ControlRoom.ViewModels;
using ControlRoom.Views;


namespace ControlRoom
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            BuildAvaloniaApp()
                .Start(AppMain, args); // Use the non-generic Start method and pass your AppMain delegate
        }


        // Example of initializing ReactiveUI in Program.cs
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI(); // Initialize ReactiveUI
        }


        // Application entry point where Avalonia is fully initialized.
        static void AppMain(Application app, string[] args)
        {
            var lifetime = new ClassicDesktopStyleApplicationLifetime
            {
                ShutdownMode = ShutdownMode.OnMainWindowClose // Or other shutdown modes
            };

            var mainWindowViewModel = new MainWindowViewModel();
            var mainWindow = new MainWindow { DataContext = mainWindowViewModel };

            lifetime.MainWindow = mainWindow;
            app.ApplicationLifetime = lifetime;

            lifetime.Start(args);
        }
    }
}

