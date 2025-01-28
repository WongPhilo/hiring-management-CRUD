using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using App.Database;

namespace RazorPagesWebServer.Pages.Candidate
{
    namespace Index
    {
        public class BaseModel : PageModel
        {   
            [BindProperty]
            public IEnumerable<App.Models.Candidate> CandidateList { get; set; }
            public IActionResult OnGet()
            {
                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());
                CandidateList = ctx.Candidates;
                return Page();
            }
            
            public IActionResult OnPostDelete(int id)
            {   
                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());
                var dependentList = ctx.Interviews.Where(e => e.CandidateID.Equals(id)).Select(e => e.ID).ToList();
                if (dependentList.Count != 0) {
                    return RedirectToPage("/Candidate/Index");
                } else {
                    var tmp = ctx.Candidates.FirstOrDefault(e => e.ID.Equals(id));
                    ctx.Candidates.DeleteOnSubmit(tmp);
                    ctx.SubmitChanges();
                    return RedirectToPage("/Candidate/Index");
                }
            }
        }
    }

    namespace Create
    {
        public partial class BaseModel : PageModel
        {
            [BindProperty]
            public App.Models.Candidate Candidate { get; set; }
            public string currentDateTime;

            public IActionResult OnGet()
            {
                currentDateTime = $"{ DateTime.Now }";
                return Page();
            }

            public IActionResult OnPost()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());
                ctx.Candidates.InsertOnSubmit(Candidate);
                ctx.SubmitChanges();
                return RedirectToPage("/Candidate/Index");
            }
        }
    }

    namespace Edit
    {
        public partial class BaseModel : PageModel
        {
            [BindProperty(SupportsGet=true)]
            public App.Models.Candidate Candidate { get; set; }

            public string currentDateTime;

            public IActionResult OnGet()
            {
                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());
                Candidate = ctx.Candidates.FirstOrDefault(e => e.ID.Equals(Candidate.ID));
                currentDateTime = $"{ DateTime.Now }";
                return Page();
            }
            
            public IActionResult OnPost()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());
                var tmp = ctx.Candidates.FirstOrDefault(e => e.ID.Equals(Candidate.ID));
                tmp.Name = Candidate.Name;
                tmp.Location = Candidate.Location;
                tmp.Email = Candidate.Email;
                tmp.Phone = Candidate.Phone;
                tmp.RegistrationDate = Candidate.RegistrationDate;
                ctx.SubmitChanges();
                return RedirectToPage("/Candidate/Index");
            }
        }
    }
}