using System.Collections.Generic;
using Twitter.Models;

namespace Twitter.Web.Models.ViewModels
{
    public class UserTweetViewModel
    {
        public string UserName { get; set; }
        public string PictureUrl { get; set; }
    }

    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public string PictureUrl { get; set; }
        public IEnumerable<Tweet> Tweets { get; set; }
        public IEnumerable<UserTweetViewModel> Followers { get; set; }
    }
}