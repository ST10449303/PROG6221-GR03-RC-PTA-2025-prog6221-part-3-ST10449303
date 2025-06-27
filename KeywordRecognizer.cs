using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotWpfPOE
{
    internal class KeywordRecognizer
    {


        private readonly Dictionary<string, string> keywordResponses = new Dictionary<string, string>
        {
            {"password", " Use strong, unique passwords and a password manager."},
            {"phishing", " Beware of phishing emails asking for personal info."},
            {"malware", " Keep your antivirus updated to protect against malware."},
            {"firewall", " A firewall protects your network from unauthorized access."},
            {"2fa", "Two-Factor Authentication adds extra security."},
            {"encryption", " Encryption protects your data by making it unreadable without a key."},
            {"wifi", " Avoid using public Wi-Fi for sensitive activities."}
        };

        public bool TryGetResponse(string input, out string response)
        {
            response = null;
            input = input.ToLower();
            foreach (var key in keywordResponses.Keys)
            {
                if (input.Contains(key))
                {
                    response = keywordResponses[key];
                    return true;
                }
            }
            return false;
        }
    }
}
