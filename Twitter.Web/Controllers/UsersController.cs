using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using Twitter.Data.UnitOfWork;
using Twitter.Models;
using Twitter.Web.Models.ViewModels;

namespace Twitter.Web.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        public UsersController(ITwitterData data)
            : base(data)
        {
        }

        [ActionName("Profile")]
        public ActionResult UserProfile(string username)
        {
            var user = GetCurrentUser(username);

            var profileData = new UserProfileViewModel()
            {
                UserName = user.UserName,
                PictureUrl = user.PictureUrl,
                Tweets = user.Tweets.OrderByDescending(t => t.CreatedOn),                    
                Followers = user.Followers.Select(f => new UserTweetViewModel()
                {
                    UserName = user.UserName,
                    PictureUrl = user.PictureUrl
                })
            };

            return View("UserProfile", profileData);
        }

        public ActionResult Follow(string username)
        {
            var userId = this.User.Identity.GetUserId();

            var userToBeFollowed = this.data.Users
                .All()
                .FirstOrDefault(u => u.UserName == username);
            var userWantingToFollow = this.data.Users.Find(userId);

            userToBeFollowed.Followers.Add(userWantingToFollow);
            userWantingToFollow.Following.Add(userToBeFollowed);

            this.data.Users.SaveChanges();

            // Send notification
            var currentUsername = this.User.Identity.GetUserName();
            var notification = new Notification()
            {
                UserId = userToBeFollowed.Id,
                CauseUserId = userWantingToFollow.Id,
                Content = currentUsername + " has just followed you!",
                Date = DateTime.Now
            };

            this.data.Notifications.Add(notification);

            this.data.Notifications.SaveChanges();

            this.TempData["followUserSuccess"] = "User followed successfully";

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Unfollow(string username)
        {
            var userId = this.User.Identity.GetUserId();

            var userToBeUnFollowed = this.data.Users
                .All()
                .FirstOrDefault(u => u.UserName == username);
            var userWantingToUnfollow = this.data.Users.Find(userId);

            userToBeUnFollowed.Followers.Remove(userWantingToUnfollow);
            userWantingToUnfollow.Following.Remove(userWantingToUnfollow);

            this.data.Users.SaveChanges();

            // Send notification
            var currentUsername = this.User.Identity.GetUserName();
            var notification = new Notification()
            {
                UserId = userToBeUnFollowed.Id,
                CauseUserId = userWantingToUnfollow.Id,
                Content = currentUsername + " has just unfollowed you.",
                Date = DateTime.Now
            };

            this.data.Notifications.Add(notification);

            this.data.Notifications.SaveChanges();

            this.TempData["unfollowUserSuccess"] = "User unfollowed successfully";

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Tweets(string username)
        {
            var user = GetCurrentUser(username);

            var tweets = user.Tweets
                .OrderByDescending(t => t.CreatedOn)
                .Select(t => new TweetViewModel()
                {
                    Content = t.Content,
                    CreatedOn = t.CreatedOn,
                    Id = t.Id,
                    User = new UserTweetViewModel()
                    {
                        UserName = t.User.UserName,
                        PictureUrl = t.User.PictureUrl
                    },
                    FavoritedBy = t.FavoritedBy
                });

            return PartialView("~/Views/Users/_UserRetweets.cshtml", tweets);
        }

        public ActionResult Retweets(string username)
        {
            var user = GetCurrentUser(username);

            var retweets = user.Retweets
                .OrderByDescending(t => t.CreatedOn)
                .Select(t => new TweetViewModel()
                {
                    Content = t.Content,
                    CreatedOn = t.CreatedOn,
                    Id = t.Id,
                    User = new UserTweetViewModel()
                    {
                        UserName = t.User.UserName,
                        PictureUrl = t.User.PictureUrl
                    },
                    FavoritedBy = t.FavoritedBy
                });

            return View("~/Views/Users/_UserRetweets.cshtml", retweets);
        }

        public ActionResult Following(string username)
        {
            var user = GetCurrentUser(username);

            var following = user.Following
                .OrderBy(u => u.UserName)
                .Select(f => new UserTweetViewModel()
                {
                    UserName = f.UserName,
                    PictureUrl = f.PictureUrl
                });

            return PartialView("_Following", following);
        }

        public ActionResult Followers(string username)
        {
            var user = GetCurrentUser(username);

            var followers = user.Followers
                .OrderBy(u => u.UserName)
                .Select(f => new UserTweetViewModel()
                {
                    UserName = f.UserName,
                    PictureUrl = f.PictureUrl
                });

            return PartialView("_Followers", followers);
        }

        public ActionResult Favorites(string username)
        {
            var user = GetCurrentUser(username);

            var favoriteTweets = user.Favorites
                .OrderByDescending(t => t.CreatedOn)
                .Select(f => new TweetViewModel()
                {
                    Id = f.Id,
                    User = new UserTweetViewModel()
                    {
                        UserName = f.User.UserName,
                        PictureUrl = f.User.PictureUrl
                    },
                    Content = f.Content,
                    CreatedOn = f.CreatedOn,
                    FavoritedBy = f.FavoritedBy
                });

            return PartialView("~/Views/Users/_FavoriteTweets.cshtml", favoriteTweets);
        }

        private User GetCurrentUser(string username)
        {
            var user = this.data.Users
                .All()
                .FirstOrDefault(u => u.UserName == username);

            return user;
        }
    }
}