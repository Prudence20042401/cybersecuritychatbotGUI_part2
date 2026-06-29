using System;
using System.Collections.Generic;

namespace CyberSecurityChatbotGUI.Services
{
    public class ActivityItem
    {
        public DateTime TimeStamp { get; set; }

        public string Description { get; set; }
    }

    public class ActivityLogService
    {
        private List<ActivityItem> activities =
            new List<ActivityItem>();


        public void Add(string description)
        {
            activities.Add(
                new ActivityItem
                {
                    TimeStamp = DateTime.Now,
                    Description = description
                });
        }


        public List<ActivityItem> GetRecent()
        {
            return activities;
        }
    }
}