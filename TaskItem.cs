using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotWpfPOE
{
    internal class TaskItem
    {
    

        public string Title { get; }
        public string Details { get; }
        public DateTime Reminder { get; }

        public TaskItem(string title, string details, DateTime reminder)
        {
            Title = title;
            Details = details;
            Reminder = reminder;
        }
    }
}
