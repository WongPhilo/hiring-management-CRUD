using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using App.Database;

namespace RazorPagesWebServer.Pages.Interview
{
    namespace Index
    {
        public class BaseModel : PageModel
        {
            [BindProperty]
            public IEnumerable<App.Models.Interview> InterviewList { get; set; }

            public IActionResult OnGet()
            {
                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());
                InterviewList = ctx.Interviews;
                return Page();
            }
            
            public IActionResult OnPostDelete(int id)
            {
                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());
                var tmp = ctx.Interviews.FirstOrDefault(e => e.ID.Equals(id));
                ctx.Interviews.DeleteOnSubmit(tmp);
                ctx.SubmitChanges();
                return RedirectToPage("/Interview/Index");
            }
        }
    }

    namespace Create
    {
        public class BaseModel : PageModel
        {
            [BindProperty]
            public App.Models.Interview Interview { get; set; }

            // Dropdown list
            public IEnumerable<App.Models.Candidate> CandidateList { get; set; }
            public IEnumerable<App.Models.Job> JobList { get; set; }

            public string currentDateTime;

            public IActionResult OnGet()
            {
                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());

                // You need a candidate and a vacancy for an interview to be created
                CandidateList = ctx.Candidates;
                if(CandidateList.Count() == 0)
                {
                    return RedirectToPage("/Candidate/Create");
                }

                JobList = ctx.Jobs;
                if(JobList.Count() == 0)
                {
                    return RedirectToPage("/Job/Create");
                }
                
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
                ctx.Interviews.InsertOnSubmit(Interview);
                ctx.SubmitChanges();
                return RedirectToPage("/Interview/Index");
            }
        }
    }

    namespace Edit
    {
        public class BaseModel : PageModel
        {
            [BindProperty(SupportsGet=true)]
            public App.Models.Interview Interview { get; set; }
            public IEnumerable<App.Models.Candidate> CandidateList { get; set; }
            public IEnumerable<App.Models.Job> JobList { get; set; }

            public string currentDateTime;
            
            public IActionResult OnGet()
            {
                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());
                CandidateList = ctx.Candidates;
                if(CandidateList.Count() == 0)
                {
                    return RedirectToPage("/Candidate/Create");
                }

                JobList = ctx.Jobs;
                if(JobList.Count() == 0)
                {
                    return RedirectToPage("/Job/Create");
                }

                Interview = ctx.Interviews.FirstOrDefault(e => e.ID.Equals(Interview.ID));
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
                var tmp = ctx.Interviews.FirstOrDefault(e => e.ID.Equals(Interview.ID));
                tmp.CandidateID = Interview.CandidateID;
                tmp.JobID = Interview.JobID;
                tmp.Date = Interview.Date;
                tmp.Remarks = Interview.Remarks;
                tmp.Hired = Interview.Hired;
                ctx.SubmitChanges();
                return RedirectToPage("/Interview/Index");
            }
        }
    }
}