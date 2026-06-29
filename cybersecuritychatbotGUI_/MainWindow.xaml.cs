using CyberSecurityChatbotGUI.Models;
using CyberSecurityChatbotGUI.Services;
using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace CyberSecurityChatbotGUI
{
    public partial class MainWindow : Window
    {
        ActivityLogService activity =
            new ActivityLogService();

        NLPService nlp =
            new NLPService();

        QuizService quiz =
            new QuizService();

        DatabaseService database =
            new DatabaseService();

        Chatbot bot =
            new Chatbot();

        private int chatState = 0;

        private string userName = "";

        private string feeling = "";

        int currentQuestion = 0;
        int score = 0;

        public MainWindow()
        {
            InitializeComponent();

            AudioHelper.PlayGreeting();

            txtAsciiArt.Text = @"
     _____
    /     \
   |SECURE|
    \_____/
       ||
      [🔒]
";

            // hide ONLY these tabs initially

            TaskTab.Visibility =
            Visibility.Collapsed;

            QuizTab.Visibility =
            Visibility.Collapsed;

            LogTab.Visibility =
            Visibility.Collapsed;

            AddMessage(
            "Bot",
            "Welcome to Cybersecurity Awareness Assistant");

            AddMessage(
            "Bot",
            "Please enter your name");
        }
        private void btnSend_Click(
 object sender,
 RoutedEventArgs e)
        {
            string input =
            txtUserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
                return;

            AddMessage(
            "You",
            input);

            switch (chatState)
            {
                // USER NAME

                case 0:

                    userName = input;

                    UserProfile user =
                    database.GetUser(userName);

                    if (user != null)
                    {
                        AddMessage(
                        "Bot",
                        "Welcome back "
                        + user.Name);

                        AddMessage(
                        "Bot",
                        "Last time you felt "
                        + user.LastFeeling);
                    }
                    else
                    {
                        AddMessage(
                        "Bot",
                        "Nice to meet you "
                        + userName);
                    }

                    AddMessage(
                    "Bot",
                    "How are you feeling today?");

                    chatState = 1;

                    break;


                // FEELINGS

                case 1:

                    feeling =
                    input.ToLower();

                    database.SaveUser(
                    userName,
                    feeling);

                    if (feeling.Contains("worried"))
                    {
                        AddMessage(
                        "Bot",
                        "I understand online threats can be worrying.");
                    }

                    else if (feeling.Contains("happy"))
                    {
                        AddMessage(
                        "Bot",
                        "Great! Positive energy helps learning.");
                    }

                    else if (feeling.Contains("frustrated"))
                    {
                        AddMessage(
                        "Bot",
                        "Cybersecurity can be difficult. I will help.");
                    }

                    else
                    {
                        AddMessage(
                        "Bot",
                        "Thank you for sharing.");
                    }

                    AddMessage(
                    "Bot",
                    "How can I help you today?");

                    AddMessage(
                    "Bot",
                    "Topics:");

                    AddMessage(
                    "Bot",
                    "• Password");

                    AddMessage(
                    "Bot",
                    "• Phishing");

                    AddMessage(
                    "Bot",
                    "• Privacy");

                    AddMessage(
                    "Bot",
                    "• Malware");

                    AddMessage(
                    "Bot",
                    "• VPN");

                    AddMessage(
                    "Bot",
                    "• Exit");


                    TaskTab.Visibility =
                    Visibility.Visible;

                    QuizTab.Visibility =
                    Visibility.Visible;

                    LogTab.Visibility =
                    Visibility.Visible;

                    chatState = 2;

                    break;


                default:

                    string intent =
                    nlp.DetectIntent(input);

                    switch (intent)
                    {
                        case "quiz":

                            activity.Add(
                            "Quiz Started");

                            ShowActivityLog();

                            MainTabs.SelectedIndex = 2;

                            break;


                        case "task":

                            MainTabs.SelectedIndex = 1;

                            AddMessage(
                            "Bot",
                            "Opening Task Assistant");

                            break;


                        case "log":

                            MainTabs.SelectedIndex = 3;

                            ShowActivityLog();

                            break;


                        default:

                            string response =
                            bot.GetResponse(
                            input,
                            userName);

                            AddMessage(
                            "Bot",
                            response);

                            activity.Add(
                            "Chat: "
                            + input);

                            ShowActivityLog();

                            break;
                    }

                    break;
            }

            txtUserInput.Clear();
        }
        private void txtTaskTitle_GotFocus(
object sender,
RoutedEventArgs e)
        {
            if (txtTaskTitle.Text == "Enter task title")
            {
                txtTaskTitle.Text = "";
            }
        }

        private void txtTaskTitle_LostFocus(
        object sender,
        RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtTaskTitle.Text))
            {
                txtTaskTitle.Text =
                "Enter task title";
            }
        }


        private void txtDescription_GotFocus(
        object sender,
        RoutedEventArgs e)
        {
            if (txtDescription.Text ==
                "Enter task description")
            {
                txtDescription.Text = "";
            }
        }

        private void txtDescription_LostFocus(
        object sender,
        RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtDescription.Text))
            {
                txtDescription.Text =
                "Enter task description";
            }
        }


        private void AddTask()
        {
            TaskItem task =
            new TaskItem
            {
                Title = txtTaskTitle.Text,
                Description = txtDescription.Text,
                ReminderDate = dpReminder.SelectedDate,
                Completed = false
            };

            database.AddTask(task);

            TaskList.ItemsSource = null;
            TaskList.ItemsSource =
            database.GetTasks();

            activity.Add(
            "Task Added: "
            + task.Title);

            ShowActivityLog();

            AddMessage(
            "Bot",
            "Task added successfully");


            // Clear fields

            txtTaskTitle.Text =
            "Enter task title";

            txtDescription.Text =
            "Enter task description";

            dpReminder.SelectedDate = null;
        }
        private void AddMessage(
string sender,
string message)
        {
            Paragraph p =
            new Paragraph();

            Run senderRun =
            new Run(sender + ": ");

            senderRun.FontWeight =
            FontWeights.Bold;

            if (sender == "Bot")
            {
                senderRun.Foreground =
                System.Windows.Media.Brushes.Cyan;
            }
            else
            {
                senderRun.Foreground =
                System.Windows.Media.Brushes.DeepSkyBlue;
            }

            Run messageRun =
            new Run(message);

            messageRun.Foreground =
            System.Windows.Media.Brushes.White;

            p.Inlines.Add(senderRun);

            p.Inlines.Add(messageRun);

            rtbChat.Document
            .Blocks.Add(p);

            rtbChat.ScrollToEnd();

            if (userName != "")
            {
                File.AppendAllText(
                GetUserFile(),
                sender +
                ": " +
                message +
                Environment.NewLine);
            }
        }


        private string GetUserFile()
        {
            return userName
            + "_conversation.txt";
        }



        void StartQuiz()
        {
            currentQuestion = 0;
            score = 0;

            LoadQuestion();
        }



        void LoadQuestion()
        {
            var q =
            quiz.Questions[
            currentQuestion];

            txtQuestion.Text =
            q.Question;

            lstAnswers.Items.Clear();

            foreach (var answer
            in q.Answers)
            {
                lstAnswers.Items.Add(
                answer);
            }
        }



        private void btnQuizAnswer_Click(
object sender,
RoutedEventArgs e)
        {
            if (currentQuestion >= quiz.Questions.Count)
                return;

            var q =
            quiz.Questions[currentQuestion];

            if (lstAnswers.SelectedIndex == -1)
            {
                MessageBox.Show(
                "Select an answer first");

                return;
            }

            if (lstAnswers.SelectedIndex
            == q.CorrectIndex)
            {
                score++;

                AddMessage(
                "Bot",
                "Correct! "
                + q.Explanation);
            }
            else
            {
                AddMessage(
                "Bot",
                "Incorrect! "
                + q.Explanation);
            }

            currentQuestion++;

            if (currentQuestion <
            quiz.Questions.Count)
            {
                LoadQuestion();
            }
            else
            {
                AddMessage(
                "Bot",

                "Quiz Complete");

                activity.Add(
               "Quiz Score: "
               + score);

                string motivation = "";

                if (score == 10)
                {
                    motivation =
                    "10/10 Excellent! You know cybersecurity very well.";
                }

                else if (score >= 8)
                {
                    motivation =
                    score +
                    "/10 Great work! You understand cybersecurity well.";
                }

                else if (score >= 5)
                {
                    motivation =
                    score +
                    "/10 Good effort. Keep learning cybersecurity.";
                }

                else
                {
                    motivation =
                    score +
                    "/10 Keep practicing. You can improve.";
                }

                AddMessage(
                "Bot",
                motivation);

                btnStartQuiz.Visibility =
Visibility.Visible;

                btnStartQuiz.Content =
                "START QUIZ AGAIN";

                txtQuestion.Visibility =
                Visibility.Collapsed;

                lstAnswers.Visibility =
                Visibility.Collapsed;

                btnSubmitQuiz.Visibility =
                Visibility.Collapsed;
            }
        }
        private void btnStartQuiz_Click(
 object sender,
 RoutedEventArgs e)
        {
            btnStartQuiz.Visibility =
            Visibility.Collapsed;

            txtQuestion.Visibility =
            Visibility.Visible;

            lstAnswers.Visibility =
            Visibility.Visible;

            btnSubmitQuiz.Visibility =
            Visibility.Visible;

            StartQuiz();
        }


        void ShowActivityLog()
        {
            lstLogs.Items.Clear();

            var logs =
            activity.GetRecent();

            if (logs.Count == 0)
            {
                lstLogs.Items.Add(
                "No activities yet");

                return;
            }

            foreach (var item in logs)
            {
                lstLogs.Items.Add(
                item.TimeStamp.ToString("HH:mm:ss")
                + " - "
                + item.Description);
            }
        }
        private void btnAddTask_Click(
          object sender,
         RoutedEventArgs e)
        {
            AddTask();
        }

        private void btnSubmitQuiz_Click(
        object sender,
        RoutedEventArgs e)
        {
            btnQuizAnswer_Click(
            sender,
            e);
        }

        private void txtUserInput_KeyDown(
         object sender,
         KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSend_Click(sender, e);
            }
        }
    }
}