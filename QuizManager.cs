using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotWpfPOE
{
    public class QuizManager
    {
    
        private List<Question> questions = new List<Question>();
        private int currentIndex = -1;

        public int TotalQuestions
        {
            get { return questions.Count; }
        }

        public void LoadQuestions()
        {
            questions = new List<Question>
            {
                new Question("What is phishing?", new List<string>
                { "A fishing method", "A scam to get sensitive info", "A type of malware", "A firewall type" },
                "A scam to get sensitive info"),

                new Question("What does 2FA stand for?", new List<string>
                { "Two-Factor Authentication", "Two Firewall Access", "Two File Archive", "Two Fast Apps" },
                "Two-Factor Authentication"),

                new Question("Which one is a strong password?", new List<string>
                { "password123", "qwerty", "MyDog@2025!", "123456" },
                "MyDog@2025!"),

                new Question("Which of these is a type of malware?", new List<string>
                { "Firewall", "Antivirus", "Ransomware", "Router" },
                "Ransomware"),

                new Question("What does a firewall do?", new List<string>
                { "Heals your computer", "Blocks unauthorized access", "Stores passwords", "Scans for viruses" },
                "Blocks unauthorized access"),

                new Question("How can you recognize a secure website?", new List<string>
                { "Has ads", "Starts with http://", "Has a lock icon and https://", "Loads slowly" },
                "Has a lock icon and https://"),

                new Question("What should you do if you receive a suspicious email?", new List<string>
                { "Click the links", "Reply with your details", "Ignore or report it", "Forward to friends" },
                "Ignore or report it"),

                new Question("Which device is safest to use public Wi-Fi on?", new List<string>
                { "Unprotected phone", "Device with VPN", "Any device", "Smart TV" },
                "Device with VPN"),

                new Question("Why is updating your software important?", new List<string>
                { "To add new games", "For better graphics", "To fix bugs and patch security holes", "It’s not important" },
                "To fix bugs and patch security holes"),

                new Question("What is the main goal of cybersecurity?", new List<string>
                { "Protect physical property", "Protect user data and systems", "Block websites", "Enhance internet speed" },
                "Protect user data and systems")
            };

            currentIndex = -1;
        }

        public Question GetNextQuestion()
        {
            currentIndex++;
            if (currentIndex >= questions.Count)
                return null;
            return questions[currentIndex];
        }

        public string SubmitAnswer(string answer)
        {
            if (currentIndex < 0 || currentIndex >= questions.Count)
                return "No question active.";

            var question = questions[currentIndex];
            if (answer == question.CorrectAnswer)
            {
                return "Correct! 🎉";
            }
            else
            {
                return $"Incorrect. The correct answer was: {question.CorrectAnswer}";
            }
        }
    }
}
