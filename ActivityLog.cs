using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotWpfPOE
{
   

    public class ActivityLog
    {

        private readonly List<string> logEntries = new List<string>();
        private const int MaxEntries = 50;

        public void AddEntry(string entry)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            logEntries.Add($"[{timestamp}] {entry}");

            if (logEntries.Count > MaxEntries)
                logEntries.RemoveAt(0);
        }

        public List<string> GetEntries()
        {
            return new List<string>(logEntries);
        }
    }
}

