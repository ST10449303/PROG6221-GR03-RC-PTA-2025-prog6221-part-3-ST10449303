using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotWpfPOE
{
    internal class ResponseGenerator
    {
    


        private readonly List<string> defaultResponses = new List<string>
        {
            "I'm not sure I understand. Could you try asking differently?",
            "Sorry, I don't have info on that yet. Try asking about passwords or phishing.",
            "I am learning new things every day! Please ask about cybersecurity."
        };

        private Random random = new Random();

        public string GetDefaultResponse()
        {
            int index = random.Next(defaultResponses.Count);
            return defaultResponses[index];
        }
    }
}
