namespace RideSharing.Models.Comments
{
    using System.ComponentModel.DataAnnotations;
    public class EditCommentFormModel
    {
        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        public DateTime LastEditedOn { get; set; }
    }
}
