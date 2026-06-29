using CyberSecurityChatbotGUI.Models;
using System.Collections.Generic;

namespace CyberSecurityChatbotGUI.Services
{
    class QuizService
    {
        public List<QuizQuestion> Questions =
            new List<QuizQuestion>()
        {
            new QuizQuestion
            {
                Question = "What is phishing?",

                Answers = new List<string>()
                {
                    "Antivirus",
                    "Scam email",
                    "Firewall",
                    "Password"
                },

                CorrectIndex = 1,

                Explanation =
                "Phishing attempts trick users into revealing sensitive information."
            },

            new QuizQuestion
            {
                Question = "True or False: Sharing passwords is safe.",

                Answers = new List<string>()
                {
                    "True",
                    "False"
                },

                CorrectIndex = 1,

                Explanation =
                "Passwords should never be shared."
            },

            new QuizQuestion
            {
                Question = "What does a strong password include?",

                Answers = new List<string>()
                {
                    "Only letters",
                    "Only numbers",
                    "Letters, symbols and numbers",
                    "Your name"
                },

                CorrectIndex = 2,

                Explanation =
                "Strong passwords should use mixed characters."
            },

            new QuizQuestion
            {
                Question = "What does VPN stand for?",

                Answers = new List<string>()
                {
                    "Virtual Private Network",
                    "Verified Password Number",
                    "Virus Protection Network",
                    "Virtual Public Network"
                },

                CorrectIndex = 0,

                Explanation =
                "VPN stands for Virtual Private Network."
            },

            new QuizQuestion
            {
                Question = "Which is safest on public WiFi?",

                Answers = new List<string>()
                {
                    "Online banking",
                    "Using a VPN",
                    "Sharing passwords",
                    "Downloading unknown files"
                },

                CorrectIndex = 1,

                Explanation =
                "A VPN helps protect your information."
            },

            new QuizQuestion
            {
                Question = "What is malware?",

                Answers = new List<string>()
                {
                    "Security software",
                    "A harmful program",
                    "A password",
                    "An email"
                },

                CorrectIndex = 1,

                Explanation =
                "Malware is harmful software."
            },

            new QuizQuestion
            {
                Question = "What should you do with suspicious links?",

                Answers = new List<string>()
                {
                    "Click them",
                    "Ignore safety warnings",
                    "Avoid clicking them",
                    "Share them"
                },

                CorrectIndex = 2,

                Explanation =
                "Suspicious links should be avoided."
            },

            new QuizQuestion
            {
                Question = "What is two-factor authentication?",

                Answers = new List<string>()
                {
                    "Two passwords",
                    "Extra security verification",
                    "Deleting accounts",
                    "Changing browsers"
                },

                CorrectIndex = 1,

                Explanation =
                "Two-factor authentication adds an extra security layer."
            },

            new QuizQuestion
            {
                Question = "Which is a social engineering attack?",

                Answers = new List<string>()
                {
                    "Firewall setup",
                    "Phishing email",
                    "Antivirus scan",
                    "Password update"
                },

                CorrectIndex = 1,

                Explanation =
                "Phishing is a form of social engineering."
            },

            new QuizQuestion
            {
                Question = "Why should software be updated?",

                Answers = new List<string>()
                {
                    "For appearance only",
                    "To fix security vulnerabilities",
                    "To slow down the computer",
                    "No reason"
                },

                CorrectIndex = 1,

                Explanation =
                "Updates often fix security problems."
            }
        };
    }
}