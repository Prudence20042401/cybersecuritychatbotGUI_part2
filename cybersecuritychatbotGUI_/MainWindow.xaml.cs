using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace CyberSecurityChatbotGUI
{
    public partial class MainWindow : Window
    {
        // create chatbot object
        Chatbot bot = new Chatbot();

        // store username
        string userName = "";

        // checks if feeling was asked
        bool feelingAsked = false;

        public MainWindow()
        {
            InitializeComponent();

            // play greeting sound
            AudioHelper.PlayGreeting();

            // ASCII art
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
          *  *  *  *
          _  _  _  _
";

            // load previous username
            if (File.Exists("username.txt"))
            {
                userName = File.ReadAllText("username.txt");

                AddMessage("Bot",
                    "Welcome back " + userName + ".");

                AddMessage("Bot",
                    "How are you feeling today?");
            }

            else
            {
                AddMessage("Bot",
                    "Welcome to the Cybersecurity Awareness Bot.");
                AddMessage("Bot",
                    "Type 'exit' anytime you want to end the conversation.");

                AddMessage("Bot",
                    "Please enter your name.");
            }
        }

        // send button
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        // enter key
        private void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();

            }
        }

        // chatbot logic
        private void SendMessage()
        {
            string input = txtUserInput.Text.Trim();

            // validation
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Please type something.");
                return;
            }

            // display user message
            AddMessage("You", input);

            // exit chatbot
            if (input.ToLower() == "exit")
            {
                AddMessage("Bot",
                    "Goodbye " + userName +
                    ". Stay safe online.");

                Application.Current.Shutdown();
                return;
            }

            // ask username first
            if (userName == "")
            {
                userName = input;

                // save username
                File.WriteAllText(
                    "username.txt",
                    userName);

                AddMessage("Bot",
                    "Hello " + userName + ".");

                AddMessage("Bot",
                    "How are you feeling today?");

                txtUserInput.Clear();
                return;
            }

            // ask feelings second
            if (feelingAsked == false)
            {
                feelingAsked = true;

                if (input.ToLower().Contains("worried"))
                {
                    AddMessage("Bot",
                        "It is normal to feel worried about online threats.");
                }

                else if (input.ToLower().Contains("curious"))
                {
                    AddMessage("Bot",
                        "Curiosity helps people learn online safety.");
                }

                else if (input.ToLower().Contains("frustrated"))
                {
                    AddMessage("Bot",
                        "Cybersecurity can feel confusing sometimes.");
                }

                else if (input.ToLower().Contains("scared"))
                {
                    AddMessage("Bot",
                        "Learning cybersecurity helps you stay protected.");
                }

                else
                {
                    AddMessage("Bot",
                        "Thank you for sharing your thoughts with me.");
                }

                AddMessage("Bot",
                    "What would you like to ask about cybersecurity awareness? ");

                txtUserInput.Clear();
                return;
            }

            // normal chatbot responses
            string response =
                bot.GetResponse(input, userName);

            AddMessage("Bot", response);

            txtUserInput.Clear();

            // show conversation history
            if (input.ToLower() == "history" ||
    input.ToLower() == "show history" ||
    input.ToLower() == "show activity log")
            {
                if (File.Exists("conversation.txt"))
                {
                    string history =
                        File.ReadAllText("conversation.txt");

                    AddMessage("Bot",
                        "Here is your recent activity:");

                    // blank line
                    AddMessage(" ", " ");

                    // display history
                    AddMessage("History", history);

                    // blank line
                    AddMessage(" ", " ");

                    // ask user another question
                    AddMessage("Bot",
                        "Do you have any questions about cybersecurity awareness?");
                }

                else
                {
                    AddMessage("Bot",
                        "No history found.");

                    AddMessage("Bot",
                        "Do you have any questions about cybersecurity awareness?");
                }

                txtUserInput.Clear();
                return;
            }
        }
        

        // display messages
        private void AddMessage(string sender, string message)
        {
            Paragraph paragraph = new Paragraph();

            paragraph.Inlines.Add(
                new Bold(
                    new Run(sender + ": ")));

            paragraph.Inlines.Add(
                new Run(message));

            rtbChat.Document.Blocks.Add(paragraph);

            rtbChat.ScrollToEnd();

            // save conversation
            File.AppendAllText(
                "conversation.txt",
                sender + ": " + message +
                Environment.NewLine);
        }
    }
}