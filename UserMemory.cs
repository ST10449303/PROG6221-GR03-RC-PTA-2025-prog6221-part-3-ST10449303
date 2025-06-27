using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotWpfPOE
{
    internal class UserMemory
    {
    

        private List<string> userEmotions = new List<string>();

        public void RememberEmotion(string emotion)
        {
            userEmotions.Add(emotion);
            if (userEmotions.Count > 10) // limit size
                userEmotions.RemoveAt(0);
        }

        public IReadOnlyList<string> GetEmotions()
        {
            return userEmotions.AsReadOnly();
        }
    }
}

