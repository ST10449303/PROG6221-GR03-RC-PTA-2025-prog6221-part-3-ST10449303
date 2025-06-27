using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotWpfPOE
{
    internal class SentimentAnalyzer
    {
    
        private readonly HashSet<string> positiveWords = new HashSet<string> { "happy", "good", "great", "excellent", "love", "fun", "helpful" };
        private readonly HashSet<string> negativeWords = new HashSet<string> { "angry", "frustrated", "bad", "sad", "upset", "hate", "annoyed" };

        public string Analyze(string input)
        {
            // Basic split on spaces, convert to lower case, trim punctuation
            var words = input.ToLower()
                             .Split(new char[] { ' ', '.', ',', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

            int positiveCount = words.Count(w => positiveWords.Contains(w));
            int negativeCount = words.Count(w => negativeWords.Contains(w));

            if (positiveCount > negativeCount)
                return "positive";
            if (negativeCount > positiveCount)
                return "negative";
            return "neutral";
        }
    }
}
