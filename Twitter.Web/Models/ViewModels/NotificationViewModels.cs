using System;

namespace Twitter.Web.Models.ViewModels
{
    public class ListNotificationsViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public UserTweetViewModel CauseUser { get; set; }
        public DateTime Date { get; set; }
    }
}