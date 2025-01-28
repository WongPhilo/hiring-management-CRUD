using System.Data.Linq;
using App.Models;
using System.Configuration;

namespace App.Database
{
    public class MainDataContext : DataContext
    {
        public Table<Job> Jobs;
        public Table<Candidate> Candidates;
        public Table<Interview> Interviews;
        public MainDataContext(string connection) : base(connection)
        {
            
        }
    }

    public class DatabaseManager
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.AppSettings["Database"];
        }
    }
}