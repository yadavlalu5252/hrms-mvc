using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketsService ts;
        public TicketsController(ITicketsService ts)
        {
            this.ts = ts;
        }

        public async Task<IActionResult> Index()
        {
            var allTickets = await ts.GetAllTickets();
            ViewBag.Users = await ts.GetAllUsers();
            return View(allTickets);
        }

        public async Task<IActionResult> AddTicket()
        {
            ViewBag.Users = await ts.GetAllUsers();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTicket(Tickets ticket)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var allTickets = await ts.GetAllTickets();
            var nextTicketId = 1;

            if (allTickets.Count > 0)
            {
                nextTicketId = allTickets.Max(x => x.TicketId) + 1;
            }

            ticket.TicketNo = "TKT-" + (10000 + nextTicketId);
            ticket.RaisedBy = userId.Value;
            ticket.Status = "Open";
            ticket.CreatedDate = DateTime.Now;

            ModelState.Remove("TicketNo");
            ModelState.Remove("RaisedBy");

            ModelState.Remove("Status");
            ModelState.Remove("CreatedDate");

            if (!ModelState.IsValid)
            {
                ViewBag.Users = await ts.GetAllUsers();
                return View("Index", await ts.GetAllTickets());
            }

            await ts.AddTicket(ticket);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditTicket(Tickets ticket)
        {
            if (!ModelState.IsValid)
            {
                var allTickets = await ts.GetAllTickets();
                ViewBag.Users = await ts.GetAllUsers();

                return View("Index", allTickets);
            }

            var oldTicket = await ts.GetTicketById(ticket.TicketId);

            if (oldTicket == null)
            {
                return NotFound();
            }

            oldTicket.TicketNo = ticket.TicketNo;
            oldTicket.Subject = ticket.Subject;
            oldTicket.Description = ticket.Description;
            oldTicket.Priority = ticket.Priority;
            oldTicket.RaisedBy = ticket.RaisedBy;
            oldTicket.AssignedTo = ticket.AssignedTo;
            oldTicket.AssignedBy = ticket.AssignedBy;
            oldTicket.Status = ticket.Status;
            oldTicket.CreatedDate = ticket.CreatedDate;
            oldTicket.AssignedDate = ticket.AssignedDate;
            oldTicket.StartedDate = ticket.StartedDate;
            oldTicket.ResolvedDate = ticket.ResolvedDate;
            oldTicket.ClosedDate = ticket.ClosedDate;

            await ts.UpdateTicket(oldTicket);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AssignTicket(Tickets ticket, string? AssignmentComment)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var oldTicket = await ts.GetTicketById(ticket.TicketId);
            if (oldTicket == null)
            {
                return NotFound();
            }

            oldTicket.AssignedTo = ticket.AssignedTo;
            oldTicket.AssignedBy = userId.Value;
            oldTicket.AssignedDate = DateTime.Now;
            oldTicket.Status = "Assigned";

            await ts.UpdateTicket(oldTicket);
            if (!string.IsNullOrEmpty(AssignmentComment))
            {
                TicketComment comment = new TicketComment();

                comment.TicketId = ticket.TicketId;
                comment.CommentBy = userId.Value;
                comment.CommentText = AssignmentComment;
                comment.CommentDate = DateTime.Now;

                await ts.AddComment(comment);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> StartWork(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var ticket = await ts.GetTicketById(id);

            if (ticket == null)
            {
                return NotFound();
            }

            if (ticket.AssignedTo != userId.Value)
            {
                return Unauthorized();
            }
            ticket.Status = "In Progress";
            ticket.StartedDate = DateTime.Now;

            await ts.UpdateTicket(ticket);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(TicketComment comment)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var ticket = await ts.GetTicketById(comment.TicketId);
            if (ticket == null)
            {
                return NotFound();
            }

            if (ticket.AssignedTo != userId.Value)
            {
                return Unauthorized();
            }

            comment.CommentBy = userId.Value;
            comment.CommentDate = DateTime.Now;

            await ts.AddComment(comment);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ResolveTicket(TicketResolution resolution, IFormFile? resolutionAttachment)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var ticket = await ts.GetTicketById(resolution.TicketId);
            if (ticket == null)
            {
                return NotFound();
            }

            if (ticket.AssignedTo != userId.Value)
            {
                return Unauthorized();
            }

            resolution.ResolvedBy = userId.Value;
            resolution.ResolvedDate = DateTime.Now;

            ticket.Status = "Resolved";
            ticket.ResolvedDate = DateTime.Now;

            await ts.AddResolution(resolution);

            if (resolutionAttachment != null)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "tickets");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = Guid.NewGuid().ToString() + "_" + resolutionAttachment.FileName;
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await resolutionAttachment.CopyToAsync(stream);
                }

                TicketAttachment attachment = new TicketAttachment();

                attachment.TicketId = resolution.TicketId;
                attachment.FileName = resolutionAttachment.FileName;
                attachment.FilePath = "/uploads/tickets/" + fileName;
                attachment.UploadedBy = userId.Value;
                attachment.UploadedDate = DateTime.Now;

                await ts.AddAttachment(attachment);
            }

            await ts.UpdateTicket(ticket);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            await ts.DeleteTicket(id);
            return RedirectToAction("Index");
        }
    }
}