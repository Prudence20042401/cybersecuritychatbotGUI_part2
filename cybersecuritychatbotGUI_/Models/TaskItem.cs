using System;

namespace CyberSecurityChatbotGUI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ReminderDate { get; set; }

        public bool Completed { get; set; }

        public override string ToString()
        {
            return
            "Title: " + Title
            + "\nDescription: " + Description
            + "\nReminder: "
            + (ReminderDate.HasValue
            ? ReminderDate.Value.ToShortDateString()
            : "None");
        }
    }
}