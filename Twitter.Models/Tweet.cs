using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Twitter.Models
{
    public class Tweet : IComparable<Tweet>
    {
        private ICollection<Tweet> replies;
        private ICollection<User> favoritedBy;
        private ICollection<User> retweetedBy;

        public Tweet()
        {
            this.replies = new HashSet<Tweet>();
            this.favoritedBy = new HashSet<User>();
            this.retweetedBy = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(140)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string UserId { get; set; }
        
        public virtual User User { get; set; }

        public virtual ICollection<Tweet> Replies { get; set; }
        public virtual ICollection<User> FavoritedBy { get; set; }
        public virtual ICollection<User> RetweetedBy { get; set; }
        
        public int CompareTo(Tweet other)
        {
            if (other == null)
            {
                return -1;
            }

            if (this.CreatedOn.CompareTo(other.CreatedOn) > 0)
            {
                return -1;
            }

            return 1;
        }
    }
}
