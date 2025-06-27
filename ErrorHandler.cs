using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotWpfPOE
{
    
    internal static class ErrorHandler
    {
        public static string Handle(Exception ex)
        {
            // You can expand this to write to a log file or other error management systems
            Console.WriteLine($"[Error]: {ex.Message}");
            return ex.Message; // Return the message for display in UI
        }
    }
}
