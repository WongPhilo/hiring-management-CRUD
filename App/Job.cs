using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace App.Models
{
    [Table(Name = "JOB")]
    public class Job
    {
        [Column(Name="id", AutoSync=AutoSync.OnInsert, IsPrimaryKey=true, IsDbGenerated=true, DbType="INT NOT NULL IDENTITY")]
        public int ID { get; set; }

        [Column(Name="title")]
        [Required, StringLength(100)]
        public string Title { get; set; }

        [Column(Name="location")]
        [Required, StringLength(50)]
        public string Location { get; set; }

        [Column(Name="salary")]
        public decimal Salary { get; set; }

        [Column(Name="description")]
        [Required]
        public string Description { get; set; }

        [Column(Name="active")]
        public bool Active { get; set; }
    }
}