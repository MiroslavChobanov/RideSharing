namespace RideSharing.Controllers
{
    using RideSharing.Services.Comments;
    using RideSharing.Services.Trips;
    using RideSharing.Services.Riders;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using RideSharing.Infrastructure;
    using RideSharing.Models.Comments;
    using RideSharing.Constants;

    public class CommentsController : Controller
    {
        private readonly ICommentService comments;
        private readonly ITripService trips;
        private readonly IRiderService riders;

        public CommentsController(ICommentService comments, ITripService trips, IRiderService riders)
        {
            this.comments = comments;
            this.trips = trips;
            this.riders = riders;
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var riderId = this.riders.IdByUser(this.User.Id());

            if (riderId == 0)
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a rider!";
                return RedirectToAction(nameof(RidersController.Join), "Riders");
            }

            if (!this.comments.IsByRider(id, riderId))
            {
                TempData[MessageConstants.ErrorMessage] = "This is not your comment!";
                return Redirect(Request.Path);
            }

            var commentForm = this.comments.EditViewData(id);

            return View(commentForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, EditCommentFormModel comment)
        {
            var riderId = this.riders.IdByUser(this.User.Id());
            var tripId = this.comments.IdOfTrip(id);

            if (!this.comments.IsByRider(id, riderId))
            {
                TempData[MessageConstants.ErrorMessage] = "This is not your comment!";
                return Redirect(Request.Path);
            }

            var edited = this.comments.Edit(
                id,
                comment.Description.Trim(),
                lastEditedOn: DateTime.Now
                );

            if (!edited)
            {
                TempData[MessageConstants.ErrorMessage] = "Something went wrong!";
                return Redirect(Request.Path);
            }

            TempData[MessageConstants.SuccessMessage] = "You have successfully edited your comment!";

            return Redirect($"../../Trips/Details/{tripId}");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var riderId = this.riders.IdByUser(this.User.Id());
            var tripId = this.comments.IdOfTrip(id);

            if (!this.comments.IsByRider(id, riderId))
            {
                TempData[MessageConstants.ErrorMessage] = "This is not your comment!";
                return Redirect(Request.Path);
            }

            var deleted = this.comments.Delete(id);

            if (!deleted)
            {
                TempData[MessageConstants.ErrorMessage] = "Something went wrong!";
                return Redirect(Request.Path);
            }

            TempData[MessageConstants.SuccessMessage] = "You have successfully deleted your comment!";

            return Redirect($"../../Trips/Details/{tripId}");
        }
    }
}
