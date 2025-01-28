using System.Data.Linq.Mapping;
using System;

namespace App.Models
{
    [Table(Name = "INTERVIEW")]
    public class Interview
    {
        [Column(Name="id", AutoSync=AutoSync.OnInsert, IsPrimaryKey=true, IsDbGenerated=true, DbType="INT NOT NULL IDENTITY")]
        public int ID { get; set; }

        [Column(Name="job_id")]
        public int JobID { get; set; }

        [Column(Name="candidate_id")]
        public int CandidateID { get; set; }

        [Column(Name="date")]
        public DateTime Date { get; set; }

        [Column(Name="remarks")]
        public string Remarks { get; set; }

        [Column(Name="hired")]
        public bool Hired { get; set; }
    }
}