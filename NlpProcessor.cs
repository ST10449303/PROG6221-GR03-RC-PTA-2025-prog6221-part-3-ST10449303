using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotWpfPOE
{
    internal class NlpProcessor
    {
    

        public enum Intent
        {
            None,
            AddTask,
            SetReminder,
            ShowSummary,
            StartQuiz,
            Unknown
        }

        // Process user input and return intent + extracted details
        public (Intent intent, string detail) ParseInput(string input)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("remind me") || lowerInput.Contains("set reminder") || lowerInput.Contains("reminder"))
            {
                // Extract task/reminder description, e.g. everything after "remind me to"
                string detail = ExtractAfterPhrase(lowerInput, new[] { "remind me to", "set reminder to", "reminder to" });
                return (Intent.SetReminder, detail);
            }

            if (lowerInput.Contains("add task") || lowerInput.Contains("create task") || lowerInput.Contains("new task"))
            {
                string detail = ExtractAfterPhrase(lowerInput, new[] { "add task", "create task", "new task" });
                return (Intent.AddTask, detail);
            }

            if (lowerInput.Contains("what have you done") || lowerInput.Contains("summary") || lowerInput.Contains("recent actions"))
            {
                return (Intent.ShowSummary, null);
            }

            if (lowerInput.Contains("quiz"))
            {
                return (Intent.StartQuiz, null);
            }

            return (Intent.None, null);
        }

        private string ExtractAfterPhrase(string input, string[] phrases)
        {
            foreach (var phrase in phrases)
            {
                int index = input.IndexOf(phrase);
                if (index != -1)
                {
                    int start = index + phrase.Length;
                    string extracted = input.Substring(start).Trim(new char[] { '.', ' ', ',' });
                    if (!string.IsNullOrEmpty(extracted))
                    {
                        // Capitalize first letter for nicer output
                        return char.ToUpper(extracted[0]) + extracted.Substring(1);
                    }
                }
            }
            return "";
        }
    }
}
