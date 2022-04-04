namespace RideSharing.Controllers
{
    using RideSharing.Services.Comments;
    using RideSharing.Services.Trips;
    using RideSharing.Services.Riders;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using RideSharing.Infrastructure;
    using RideSharing.Models.Comments;

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
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (!this.comments.IsByRider(id, riderId))
            {
                return BadRequest();
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
                return BadRequest();
            }

            var edited = this.comments.Edit(
                id,
                comment.Description.Trim(),
                lastEditedOn: DateTime.Now
                );

            if (!edited)
            {
                return BadRequest();
            }

            return Redirect($"../../Trips/Details/{tripId}");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var riderId = this.riders.IdByUser(this.User.Id());
            var tripId = this.comments.IdOfTrip(id);

            if (!this.comments.IsByRider(id, riderId))
            {
                return BadRequest();
            }

            var deleted = this.comments.Delete(id);

            if (!deleted)
            {
                return BadRequest();
            }

            return Redirect($"../../Trips/Details/{tripId}");
        }
    }
}
