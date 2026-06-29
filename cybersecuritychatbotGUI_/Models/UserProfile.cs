using System;
namespace CyberSecurityChatbotGUI.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastFeeling { get; set; }

        public DateTime LastVisit { get; set; }
    }
}
