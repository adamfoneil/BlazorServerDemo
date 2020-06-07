using AO.Models;
using AO.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("AspNetUsers")]
    [Identity(nameof(UserId))]
    public class UserProfile : IUserBase
    {
        public int UserId { get; set; }

        [MaxLength(256)]
        public string UserName { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        public string Name => UserName;

        public int TimeZoneOffset { get; set; }

        public DateTime LocalTime => (TimeZoneOffset < 24) ?
            DateTime.UtcNow.AddHours(TimeZoneOffset) :
            DateTime.UtcNow.AddMinutes(TimeZoneOffset);
    }
}
