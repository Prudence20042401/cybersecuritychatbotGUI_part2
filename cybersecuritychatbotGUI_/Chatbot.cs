using System;
using System.Collections.Generic;

namespace CyberSecurityChatbotGUI
{
    class Chatbot
    {
        // random object for random responses
        Random random = new Random();

        // remembers previous topic
        string lastTopic = "";

        // remembers favourite topic
        string favouriteTopic = "";

        // chatbot responses
        Dictionary<string, List<string>> responses =
            new Dictionary<string, List<string>>()
        {
            {
                "password",
                new List<string>()
                {
                    "Use strong passwords with letters, numbers and symbols. Do not share your password.",
                    "Avoid using the same password for multiple accounts.",
                    "Change your passwords regularly for better security."
                }
            },

            {
                "phishing",
                new List<string>()
                {
                    "Phishing is when scammers trick you into giving personal information through fake emails or links.",
                    "Always check suspicious emails before clicking links.",
                    "Scammers often pretend to be trusted companies."
                }
            },

            {
                "browsing",
                new List<string>()
                {
                    "Always use secure websites and avoid clicking unknown links.",
                    "Do not download files from untrusted websites.",
                    "Safe browsing helps protect your personal information."
                }
            }


            {
                "scam",
                new List<string>()
                {
                    "scammers is Scammers often pretend to be someone you trust, such as a bank official, tech support, government agency, or delivery service."
                }
            },
        };

        // chatbot response method
        public string GetResponse(string input, string userName)
        {
            input = input.ToLower();

            // chatbot responses
            if (input.Contains("how are you"))
            {
                return "I am fine and ready to help you stay safe online.";
            }

            if (input.Contains("purpose"))
            {
                return "My purpose is to teach you about cybersecurity and online safety.";
            }

            if (input.Contains("advace"))
            {
                return "stay safe and dont let anyone scam you.";
            }
            if (input.Contains("what can i ask"))
            {
                return "You can ask me about passwords, phishing or safe browsing or scammers.";
            }

            // sentiment detection
            if (input.Contains("worried"))
            {
                return "It is normal to feel worried about online threats.";
            }

            if (input.Contains("scared"))
            {
                return "It is normal to feel scared about online threats especially when you didnt protect you information proper.";
            }
            if (input.Contains("frustrated"))
            {
                return "Cybersecurity can feel confusing sometimes, but you are learning step by step.";
            }

            if (input.Contains("curious"))
            {
                return "Curiosity helps people learn more about staying safe online.";
            }

            // memory feature
            if (input.Contains("i like"))
            {
                if (input.Contains("password"))
                {
                    favouriteTopic = "password";
                }

                else if (input.Contains("phishing"))
                {
                    favouriteTopic = "phishing";
                }

                else if (input.Contains("browsing"))
                {
                    favouriteTopic = "browsing";
                }

                return "I will remember that you are interested in " + favouriteTopic + ".";
            }

            // memory recall
            if (input.Contains("remember"))
            {
                if (favouriteTopic != "")
                {
                    return "I remember that you are interested in " + favouriteTopic + ".";
                }

                return "I do not remember anything yet.";
            }

            // conversation flow
            if (input.Contains("tell me more") ||
                input.Contains("another tip") ||
                input.Contains("explain more"))
            {
                if (lastTopic != "")
                {
                    List<string> moreResponses = responses[lastTopic];

                    return moreResponses[random.Next(moreResponses.Count)];
                }

                return "Please ask about a topic first.";
            }

            // keyword recognition
            foreach (var keyword in responses.Keys)
            {
                if (input.Contains(keyword))
                {
                    lastTopic = keyword;

                    List<string> selectedResponses = responses[keyword];

                    return selectedResponses[random.Next(selectedResponses.Count)];
                }
            }

            // exit response
            if (input == "exit")
            {
                return "Goodbye " + userName + ". Stay safe online.";
            }

            // unknown response
            return "I did not understand that. Try asking about passwords or phishing.";
        }
    }
}