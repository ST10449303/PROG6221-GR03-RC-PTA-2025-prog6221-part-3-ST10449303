using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotWpfPOE
{
    
    public class Question
    {


        public string Text { get; }
        public List<string> Options { get; }
        public string CorrectAnswer { get; }

        public Question(string text, List<string> options, string correctAnswer)
        {
            Text = text;
            Options = options;
            CorrectAnswer = correctAnswer;
        }
    }
}

