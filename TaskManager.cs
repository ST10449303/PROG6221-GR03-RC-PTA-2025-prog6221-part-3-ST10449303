using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatbotWpfPOE;

namespace ChatbotWpfPOE
{
    internal class TaskManager
    {
    


        private readonly List<TaskItem> tasks = new List<TaskItem>();

        public void AddTask(TaskItem task)
        {
            tasks.Add(task);
        }

        public List<TaskItem> GetTasks()
        {
            return new List<TaskItem>(tasks);
        }
    }
}
