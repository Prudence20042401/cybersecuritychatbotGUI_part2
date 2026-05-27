using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace CyberSecurityChatbotGUI
{
    public partial class MainWindow : Window
    {
        // chatbot object
        Chatbot bot = new Chatbot();

        // user data
        string userName = "";
        bool returningUser = false;
        bool feelingAnswered = false;

        public MainWindow()
        {
            InitializeComponent();

            AudioHelper.PlayGreeting();

            txtAsciiArt.Text = @"
          _______
         /       \
        /         \
       |           | <SECURED!>
        \_________/
        |         |
        |   ___   |
        |  | o |  |
        |  |___|  |
        |         |
        |_________|
";

            AddMessage("Bot",
                "Welcome to the Cybersecurity Awareness Bot.");

            AddMessage("Bot",
                "Please enter your name.");
        }

        
        // GET USER FILE
        
        private string GetUserFile()
        {
            return userName + "_conversation.txt";
        }

        
        // SEND MESSAGE
       
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            string input = txtUserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Please type something.");
                return;
            }

            AddMessage("You", input);

            // EXIT
            if (input.ToLower() == "exit")
            {
                AddMessage("Bot", "Goodbye " + userName + ". Stay safe online.");
                Application.Current.Shutdown();
                return;
            }

            
            // USERNAME
            
            if (userName == "")
            {
                userName = input;

                if (File.Exists("username.txt"))
                {
                    string savedName = File.ReadAllText("username.txt");

                    if (savedName.ToLower() == userName.ToLower())
                    {
                        returningUser = true;

                        AddMessage("Bot", "Welcome back " + userName + ".");
                    }
                    else
                    {
                        AddMessage("Bot", "Hello " + userName + ".");
                        File.WriteAllText("username.txt", userName);
                    }
                }
                else
                {
                    File.WriteAllText("username.txt", userName);
                    AddMessage("Bot", "Hello " + userName + ".");
                }

                AddMessage("Bot", "How are you feeling today?");
                txtUserInput.Clear();
                return;
            }

            
            //SENTIMENTAL DETECTION
            
            if (!feelingAnswered)
            {
                feelingAnswered = true;

                if (input.ToLower().Contains("worried"))
                    AddMessage("Bot", "It is normal to feel worried about online threats.");

                else if (input.ToLower().Contains("curious"))
                    AddMessage("Bot", "It is good to want to learn about safety.");

                else if (input.ToLower().Contains("frustrated"))
                    AddMessage("Bot", "Cybersecurity can feel confusing sometimes.");

                else if (input.ToLower().Contains("happy"))
                    AddMessage("Bot", "That is great to hear.");

                else
                    AddMessage("Bot", "Thank you for sharing.");

                AddMessage("Bot", "What would you like to know?");
                AddMessage("Bot", "Passwords, Phishing, Browsing, Scam, history");
                AddMessage("Bot", "Type 'exit' to end the conversation.");

                txtUserInput.Clear();
                return;
            }

            
            // HISTORY
          
            if (input.ToLower() == "history")
            {
                string fileName = GetUserFile();

                if (File.Exists(fileName))
                {
                    string history = File.ReadAllText(fileName);

                    if (string.IsNullOrWhiteSpace(history))
                    {
                        AddMessage("Bot", "No history found yet.");
                    }
                    else
                    {
                        AddMessage("Bot", "Here is your recent activity:");
                        AddMessage("History", history);
                    }
                }
                else
                {
                    AddMessage("Bot", "No history found for this user.");
                }

                AddMessage("Bot", "Ask something about cybersecurity:");
                AddMessage("Bot", "Passwords, Phishing, Browsing, Scam");
                AddMessage("Bot", "Type 'exit' to end.");

                txtUserInput.Clear();
                return;
            }
         // CHATBOT RESPONSE
            
            string response = bot.GetResponse(input, userName);
            AddMessage("Bot", response);

            AddMessage("Bot",
                "Anything else? Passwords, Phishing, Browsing, Scam");

            txtUserInput.Clear();
        }

        
        // DISPLAY MESSAGE
     
        private void AddMessage(string sender, string message)
        {
            Paragraph paragraph = new Paragraph();

            Run senderRun = new Run(sender + ": ");

            if (sender == "Bot")
                senderRun.Foreground = System.Windows.Media.Brushes.Cyan;
            else if (sender == "You")
                senderRun.Foreground = System.Windows.Media.Brushes.Yellow;
            else if (sender == "History")
                senderRun.Foreground = System.Windows.Media.Brushes.LightGreen;
            else
                senderRun.Foreground = System.Windows.Media.Brushes.White;

            senderRun.FontWeight = FontWeights.Bold;

            Run messageRun = new Run(message);
            messageRun.Foreground = System.Windows.Media.Brushes.White;

            paragraph.Inlines.Add(senderRun);
            paragraph.Inlines.Add(messageRun);

            rtbChat.Document.Blocks.Add(paragraph);
            rtbChat.ScrollToEnd();

            
            // SAVE PER USER HISTORY
            
            File.AppendAllText(
                GetUserFile(),
                sender + ": " + message + Environment.NewLine);
        }
    }
}