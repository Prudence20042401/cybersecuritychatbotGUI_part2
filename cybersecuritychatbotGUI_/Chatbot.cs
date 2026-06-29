using System;
using System.Collections.Generic;

namespace CyberSecurityChatbotGUI
{
    class Chatbot
    {
        Random random = new Random();

        string lastTopic = "";
        string favouriteTopic = "";

        Dictionary<string, List<string>> responses =
            new Dictionary<string, List<string>>()
        {
            {
                "password",
                new List<string>()
                {
                    "Passwords are secret words used to protect your accounts.",
                    "Use strong passwords with letters, numbers and symbols.",
                    "Avoid using the same password for multiple accounts."
                }
            },

            {
                "phishing",
                new List<string>()
                {
                    "Phishing is a cyberattack that tricks people into revealing personal information.",
                    "Never click suspicious links in emails.",
                    "Always verify messages before entering personal information."
                }
            },

            {
                "privacy",
                new List<string>()
                {
                    "Privacy means protecting personal information online.",
                    "Do not share passwords or sensitive information publicly.",
                    "Always check app permissions."
                }
            },

            {
                "malware",
                new List<string>()
                {
                    "Malware is harmful software that damages devices.",
                    "Viruses and ransomware are types of malware.",
                    "Install antivirus software."
                }
            },

            {
                "vpn",
                new List<string>()
                {
                    "VPN means Virtual Private Network.",
                    "A VPN encrypts internet traffic.",
                    "VPNs improve privacy when using public WiFi."
                }
            },

            {
                "scam",
                new List<string>()
                {
                    "Scams are fake tricks used to steal money.",
                    "Never trust requests for personal details.",
                    "Scammers pretend to be trusted people."
                }
            }
        };

        public string GetResponse(string input, string userName)
        {
            input = input.ToLower();

            if (input == "exit")
            {
                return "Goodbye " + userName +
                       ". Stay safe online.";
            }

            foreach (var keyword in responses.Keys)
            {
                if (input.Contains(keyword))
                {
                    lastTopic = keyword;

                    List<string> selected =
                    responses[keyword];

                    string response =
                    selected[random.Next(selected.Count)];

                    return response +

                    "\n\nDo you want to ask anything else?\n" +
                    "Options:\n" +
                    "• Password\n" +
                    "• Phishing\n" +
                    "• Privacy\n" +
                    "• Malware\n" +
                    "• VPN\n" +
                    "• Exit";
                }
            }

            return
            "I did not understand.\n\n" +
            "Options:\n" +
            "• Password\n" +
            "• Phishing\n" +
            "• Privacy\n" +
            "• Malware\n" +
            "• VPN\n" +
            "• Exit";
        }
    }
}