using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatbotWpfPOE;

namespace ChatbotWpfPOE
{
    internal class Chatbot
    {
    
        private readonly Dictionary<string, string> emotionResponses = new Dictionary<string, string>
        {
            {"angry", " I'm sorry you're feeling angry. Want a quick tip or fun fact?"},
            {"frustrated", " It's okay to feel frustrated. Let's try a helpful cybersecurity tip."},
            {"curious", " I love your curiosity! Ask me anything about staying safe online."},
            {"happy", " Glad you're happy! Want a cool tip or quiz question?"}
        };

        private KeywordRecognizer keywordRecognizer = new KeywordRecognizer();
        private SentimentAnalyzer sentimentAnalyzer = new SentimentAnalyzer();
        private ResponseGenerator responseGenerator = new ResponseGenerator();
        private UserMemory userMemory = new UserMemory();
        private List<string> tips = new List<string>
        {
            "Use a password manager to create and store strong passwords.",
            "Enable two-factor authentication on your accounts.",
            "Avoid clicking links in unexpected emails.",
            "Keep your software and antivirus up to date.",
            "Lock your screen when away.",
            "Use secure Wi-Fi connections only."
        };

        private Random rand = new Random();

        public string GetResponse(string input)
        {
            string lowerInput = input.ToLower();

            // Check for emotion keywords
            foreach (var emotion in emotionResponses.Keys)
            {
                if (lowerInput.Contains(emotion))
                {
                    userMemory.RememberEmotion(emotion);
                    return emotionResponses[emotion];
                }
            }

            // Check for known keywords
            if (keywordRecognizer.TryGetResponse(lowerInput, out string keywordResponse))
            {
                return keywordResponse;
            }

            // Sentiment analysis
            string sentiment = sentimentAnalyzer.Analyze(lowerInput);
            userMemory.RememberEmotion(sentiment);

            // Return random tip sometimes if positive or neutral
            if (sentiment == "positive" || sentiment == "neutral")
            {
                return tips[rand.Next(tips.Count)];
            }

            // Default fallback
            return responseGenerator.GetDefaultResponse();
        }

        public string GetRandomTip()
        {
            return tips[rand.Next(tips.Count)];
        }
    }
}

