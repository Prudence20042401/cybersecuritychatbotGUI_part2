using System.Collections.Generic;

namespace CyberSecurityChatbotGUI.Models
{
    public class QuizQuestion
    {
        public string Question { get; set; }

        public List<string> Answers { get; set; }

        public int CorrectIndex { get; set; }

        public string Explanation { get; set; }
    }
}