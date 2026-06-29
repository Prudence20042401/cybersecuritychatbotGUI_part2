namespace CyberSecurityChatbotGUI.Services
{
    class NLPService
    {
        public string DetectIntent(string input)
        {
            input = input.ToLower();

            if (
            input.Contains("task") ||
            input.Contains("remind me") ||
            input.Contains("remember to") ||
            input.Contains("set reminder") ||
            input.Contains("add task"))
            {
                return "task";
            }

            if (
            input.Contains("quiz") ||
            input.Contains("play") ||
            input.Contains("game") ||
            input.Contains("start quiz"))
            {
                return "quiz";
            }

            if (
            input.Contains("activity") ||
            input.Contains("history") ||
            input.Contains("log") ||
            input.Contains("what have you done"))
            {
                return "log";
            }

            if (input.Contains("password"))
                return "password";

            if (input.Contains("phishing"))
                return "phishing";

            if (input.Contains("privacy"))
                return "privacy";

            if (input.Contains("vpn"))
                return "vpn";

            if (input.Contains("malware"))
                return "malware";

            return "chat";
        }
    }
}