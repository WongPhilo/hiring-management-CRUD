using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System;

namespace App.Models
{
    [Table(Name = "CANDIDATE")]
    public class Candidate
    {
        [Column(Name="id", AutoSync=AutoSync.OnInsert, IsPrimaryKey=true, IsDbGenerated=true, DbType="INT NOT NULL IDENTITY")]
        public int ID { get; set; }

        [Column(Name="name")]
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Column(Name="location")]
        [Required, StringLength(50)]
        public string Location { get; set; }

        [Column(Name="email")]
        [Required, StringLength(50), RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\x22(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*\x22)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Column(Name="phone")]
        [Required, StringLength(10), RegularExpression("^[0-9]*$", ErrorMessage = "Input a phone number, consisting only of digits.")]
        public string Phone { get; set; }

        [Column(Name="registration_date")]
        public DateTime RegistrationDate { get; set; }
    }
}