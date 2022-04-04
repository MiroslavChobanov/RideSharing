namespace RideSharing.Models.Comments
{
    using System.ComponentModel.DataAnnotations;
    public class AddCommentFormModel
    {
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
    }
}
