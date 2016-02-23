//using Microsoft.AspNet.SignalR;
//using Microsoft.AspNet.SignalR.Hubs;
using Twitter.Models;

namespace Twitter.Web.Hubs
{
    //[HubName("tweets")]
    public class TweetsHub// : Hub
    {
        public void SendTweet(Tweet tweet)
        {
            //this.Clients.All.updateFeed(tweet);
        }
    }
}