using System.ComponentModel.DataAnnotations;

namespace Twitter.Web.Models.ViewModels.BindingModels
{
    public class PostTweetBindingModel
    {
        [Required]
        [MinLength(0), MaxLength(140)]
        public string Content { get; set; }
    }

    public class PostReplyBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(0), MaxLength(140)]
        public string Content { get; set; }
    }
}