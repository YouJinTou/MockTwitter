using Microsoft.AspNet.Identity;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Twitter.Data.UnitOfWork;
using Twitter.Web.Models.ViewModels;

namespace Twitter.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        public HomeController(ITwitterData data)
            : base(data)
        {
        }

        public ActionResult Index(int? page)
        {
            if (!Request.IsAuthenticated)
            {
                return View();
            }

            var tweets = this.data.Tweets
                .All()
                .OrderByDescending(t => t.CreatedOn)
                .Select(t => new TweetViewModel()
                {
                    Id = t.Id,
                    User = new UserTweetViewModel()
                    {
                        UserName = t.User.UserName,
                        PictureUrl = t.User.PictureUrl
                    },
                    Content = t.Content,
                    CreatedOn = t.CreatedOn,
                    FavoritedBy = t.FavoritedBy
                });

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            return View("Index", tweets.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Feed(int? page)
        {
            IQueryable<TweetViewModel> tweets = null;
            var userId = this.User.Identity.GetUserId();

            if (userId == null)
            {
                return new HttpUnauthorizedResult("You need to be logged in.");
            }
            else
            {
                var user = this.data.Users
                    .All()
                    .FirstOrDefault(u => u.Id == userId);

                // Returns multiple users and their tweets
                var userFollowingTweets = user.Following
                    .Select(f => f.Tweets);

                List<TweetViewModel> tweetsList = new List<TweetViewModel>();
                foreach (var tweetCollection in userFollowingTweets)
                {
                    tweetsList.AddRange(tweetCollection
                        .OrderByDescending(t => t.CreatedOn)
                        .Select(t => new TweetViewModel()
                        {
                            Id = t.Id,
                            User = new UserTweetViewModel()
                            {
                                UserName = t.User.UserName,
                                PictureUrl = t.User.PictureUrl
                            },
                            Content = t.Content,
                            CreatedOn = t.CreatedOn,
                            FavoritedBy = t.FavoritedBy
                        }));
                }

                tweets = tweetsList.AsQueryable();
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(tweets.ToPagedList(pageNumber, pageSize));
        }
    }
}