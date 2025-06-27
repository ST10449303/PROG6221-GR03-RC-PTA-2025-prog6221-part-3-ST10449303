using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatbotWpfPOE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        private Chatbot chatbot;
        private TaskManager taskManager;
        private ActivityLog activityLog;
        private QuizManager quizManager;
        private SentimentAnalyzer sentimentAnalyzer;

        private bool askedUserName = false;
        private bool gotUserName = false;
        private string userName = "";
        private bool askedHowAreYou = false;
        private bool askedAboutCyberAwareness = false;
        private int quizScore = 0;

        public MainWindow()
        {
            InitializeComponent();

            chatbot = new Chatbot();
            taskManager = new TaskManager();
            activityLog = new ActivityLog();
            quizManager = new QuizManager();
            sentimentAnalyzer = new SentimentAnalyzer();

            AddChatMessage("Bot", "👋 Hello! Welcome to the Cybersecurity Awareness Chatbot.");
            AddChatMessage("Bot", "Let's start by getting to know each other.");
            AskUserName();

            ShowChatPanel(null, null);
        }

        private void AskUserName()
        {
            AddChatMessage("Bot", "What's your name?");
            askedUserName = true;
        }

        private void AskHowAreYou()
        {
            AddChatMessage("Bot", $"Nice to meet you, {userName}! How are you doing today?");
            askedHowAreYou = true;
        }

        private void AskAboutCyberAwareness()
        {
            AddChatMessage("Bot", $"Do you know much about cybersecurity awareness, {userName}?");
            askedAboutCyberAwareness = true;
        }

        private void AddChatMessage(string sender, string message)
        {
            ChatList.Items.Add($"{sender}: {message}");
            ChatList.ScrollIntoView(ChatList.Items[ChatList.Items.Count - 1]);
            activityLog.AddEntry($"{sender}: {message}");
            RefreshActivityLog();
        }

        private void RefreshActivityLog()
        {
            ActivityLogList.Items.Clear();
            foreach (var entry in activityLog.GetEntries())
            {
                ActivityLogList.Items.Add(entry);
            }
        }

        private void RefreshTaskList()
        {
            TaskListBox.Items.Clear();
            foreach (var task in taskManager.GetTasks())
            {
                string reminderText = task.Reminder != DateTime.MinValue ? task.Reminder.ToShortDateString() : "No reminder";
                TaskListBox.Items.Add($"{task.Title} - Due: {reminderText}\n{task.Details}");
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserInputBox.Text.Trim();
            if (string.IsNullOrEmpty(userInput))
                return;

            try
            {
                AddChatMessage("You", userInput);

                // Analyze sentiment
                string sentiment = sentimentAnalyzer.Analyze(userInput);

                if (askedUserName && !gotUserName)
                {
                    userName = userInput;
                    gotUserName = true;
                    AddChatMessage("Bot", $"Great to meet you, {userName}!");
                    AskHowAreYou();
                }
                else if (askedHowAreYou && !askedAboutCyberAwareness)
                {
                    if (sentiment == "negative")
                    {
                        AddChatMessage("Bot", "I'm sorry to hear you're feeling that way. Remember, taking breaks and staying safe online is important!");
                    }
                    else
                    {
                        AddChatMessage("Bot", "Thanks for sharing! 😊");
                    }
                    AskAboutCyberAwareness();
                }
                else if (askedAboutCyberAwareness)
                {
                    string lowerInput = userInput.ToLower();

                    if (sentiment == "negative")
                    {
                        AddChatMessage("Bot", "It sounds like something might be worrying you. If you want to talk about cybersecurity or anything else, I'm here to help.");
                    }
                    else if (lowerInput.Contains("yes") || lowerInput.Contains("i do") || lowerInput.Contains("know"))
                    {
                        AddChatMessage("Bot", "That's awesome! Let's see how much you know with a quick quiz later.");
                    }
                    else
                    {
                        AddChatMessage("Bot", "No worries! I'm here to help you learn about staying safe online.");
                    }

                    askedUserName = false;
                    gotUserName = false;
                    askedHowAreYou = false;
                    askedAboutCyberAwareness = false;

                    ProvideTip();
                }
                else
                {
                    if (sentiment == "negative")
                    {
                        AddChatMessage("Bot", "I'm here for you! If something's bothering you, feel free to share.");
                    }
                    else
                    {
                        string response = chatbot.GetResponse(userInput);
                        AddChatMessage("Bot", response);
                    }
                }

                UserInputBox.Clear();
            }
            catch (Exception ex)
            {
                string errorMessage = ErrorHandler.Handle(ex);
                AddChatMessage("Bot", $"⚠️ {errorMessage}");
            }
        }

        private void ProvideTip()
        {
            string tip = chatbot.GetRandomTip();
            AddChatMessage("Bot", $"💡 Tip of the Day: {tip}");
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = TaskTitleBox.Text.Trim();
                string details = TaskDetailBox.Text.Trim();
                DateTime? reminder = TaskReminderPicker.SelectedDate;

                if (string.IsNullOrEmpty(title))
                {
                    MessageBox.Show("Please enter a task title.", "Missing Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DateTime reminderValue = reminder ?? DateTime.MinValue;
                var task = new TaskItem(title, details, reminderValue);
                taskManager.AddTask(task);
                AddChatMessage("Bot", $"Task '{title}' added{(reminder.HasValue ? $" with reminder on {reminder.Value.ToShortDateString()}" : "")}.");

                TaskTitleBox.Clear();
                TaskDetailBox.Clear();
                TaskReminderPicker.SelectedDate = null;

                RefreshTaskList();
            }
            catch (Exception ex)
            {
                string errorMessage = ErrorHandler.Handle(ex);
                AddChatMessage("Bot", $"⚠️ {errorMessage}");
            }
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            quizManager.LoadQuestions();
            quizScore = 0;
            QuizFeedbackText.Text = "";
            ShowNextQuizQuestion();
        }

        private void ShowNextQuizQuestion()
        {
            var question = quizManager.GetNextQuestion();
            if (question == null)
            {
                QuizQuestionText.Text = "🎉 Quiz complete! Well done!";
                QuizOptionsList.Items.Clear();
                QuizFeedbackText.Text = $"Your final score is {quizScore} out of {quizManager.TotalQuestions}.";
                return;
            }

            QuizQuestionText.Text = question.Text;
            QuizOptionsList.Items.Clear();
            foreach (var option in question.Options)
            {
                QuizOptionsList.Items.Add(option);
            }

            QuizFeedbackText.Text = "";
        }

        private void SubmitQuizAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (QuizOptionsList.SelectedItem == null)
            {
                MessageBox.Show("Please select an answer before submitting.", "No Answer Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selected = QuizOptionsList.SelectedItem.ToString();
            string feedback = quizManager.SubmitAnswer(selected);

            if (feedback.StartsWith("Correct"))
            {
                quizScore++;
                QuizFeedbackText.Text = feedback + " You're doing great! 👍";
                AddChatMessage("Bot", feedback + " Keep it up!");
            }
            else
            {
                QuizFeedbackText.Text = feedback + " Don't worry, you'll get the next one!";
                AddChatMessage("Bot", feedback + " Let's try another question.");
            }

            ShowNextQuizQuestion();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void ShowChatPanel(object sender, RoutedEventArgs e)
        {
            ChatPanel.Visibility = Visibility.Visible;
            TaskPanel.Visibility = Visibility.Collapsed;
            QuizPanel.Visibility = Visibility.Collapsed;
            ActivityLogPanel.Visibility = Visibility.Collapsed;
            UserInputBox.Visibility = Visibility.Visible;
            SendButton.Visibility = Visibility.Visible;
            ExitButton.Visibility = Visibility.Visible;
        }

        private void ShowTaskPanel(object sender, RoutedEventArgs e)
        {
            ChatPanel.Visibility = Visibility.Collapsed;
            TaskPanel.Visibility = Visibility.Visible;
            QuizPanel.Visibility = Visibility.Collapsed;
            ActivityLogPanel.Visibility = Visibility.Collapsed;
            UserInputBox.Visibility = Visibility.Collapsed;
            SendButton.Visibility = Visibility.Collapsed;
            ExitButton.Visibility = Visibility.Visible;
        }

        private void ShowQuizPanel(object sender, RoutedEventArgs e)
        {
            ChatPanel.Visibility = Visibility.Collapsed;
            TaskPanel.Visibility = Visibility.Collapsed;
            QuizPanel.Visibility = Visibility.Visible;
            ActivityLogPanel.Visibility = Visibility.Collapsed;
            UserInputBox.Visibility = Visibility.Collapsed;
            SendButton.Visibility = Visibility.Collapsed;
            ExitButton.Visibility = Visibility.Visible;
        }

        private void ShowActivityLogPanel(object sender, RoutedEventArgs e)
        {
            ChatPanel.Visibility = Visibility.Collapsed;
            TaskPanel.Visibility = Visibility.Collapsed;
            QuizPanel.Visibility = Visibility.Collapsed;
            ActivityLogPanel.Visibility = Visibility.Visible;
            UserInputBox.Visibility = Visibility.Collapsed;
            SendButton.Visibility = Visibility.Collapsed;
            ExitButton.Visibility = Visibility.Visible;
        }
    }
}

