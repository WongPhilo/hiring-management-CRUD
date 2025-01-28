using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using App.Database;

namespace RazorPagesWebServer.Pages.Job
{
    namespace Index
    {
        public class BaseModel : PageModel
        {
            public IEnumerable<App.Models.Job> JobList { get; set; }
            public IActionResult OnGet()
            {   
                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());
                JobList = ctx.Jobs;
                return Page();
            }
            
            public IActionResult OnPostDelete(int id)
            {   
                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());
                var dependentList = ctx.Interviews.Where(e => e.CandidateID.Equals(id)).Select(e => e.ID).ToList();
                if (dependentList.Count != 0) {
                    return RedirectToPage("/Job/Index");
                } else {
                    var tmp = ctx.Jobs.FirstOrDefault(e => e.ID.Equals(id));
                    ctx.Jobs.DeleteOnSubmit(tmp);
                    ctx.SubmitChanges();
                    return RedirectToPage("/Job/Index");
                }
            }
        }
    }

    namespace Create
    {
        public class BaseModel : PageModel
        {
            [BindProperty]
            public App.Models.Job Job { get; set; }

            public IActionResult OnGet()
            {
                return Page();
            }

            public IActionResult OnPost()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                
                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());
                ctx.Jobs.InsertOnSubmit(Job);
                ctx.SubmitChanges();
                return RedirectToPage("/Job/Index");
            }
        }
    }

    namespace Edit
    {
        public class BaseModel : PageModel
        {
            [BindProperty(SupportsGet=true)]
            public App.Models.Job Job { get; set; }

            public IActionResult OnGet()
            {
                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());
                Job = ctx.Jobs.FirstOrDefault(e => e.ID.Equals(Job.ID));
                return Page();
            }
            
            public IActionResult OnPost()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());
                var tmp = ctx.Jobs.FirstOrDefault(e => e.ID.Equals(Job.ID));
                tmp.Title = Job.Title;
                tmp.Location = Job.Location;
                tmp.Salary = Job.Salary;
                tmp.Description = Job.Description;
                tmp.Active = Job.Active;
                ctx.SubmitChanges();
                return RedirectToPage("/Job/Index");
            }
        }
    }
}