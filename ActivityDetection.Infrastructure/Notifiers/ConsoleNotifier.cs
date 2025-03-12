using System;
using ActivityDetection.Application.Interfaces;

namespace ActivityDetection.Infrastructure.Notifiers
{
    public class ConsoleNotifier : INotifier
    {
        public void Notify(string message)
        {
            Console.WriteLine($"[ALERT] {message}");
        }
    }
}