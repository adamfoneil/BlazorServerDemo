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

        [MaxLength(50)]
        public string DisplayName { get; set; }

        [MaxLength(100)]
        public string TimeZoneId { get; set; }

        [References(typeof(Workspace))]
        public int? WorkspaceId { get; set; }

        public DateTime LocalTime => CurrentTime.GetLocal(TimeZoneId);

        public Workspace Workspace { get; set; }
    }

    public static class CurrentTime
    {
        public static DateTime GetLocal(string timeZoneId)
        {
            try
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                return TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);
            }
            catch
            {
                return DateTime.UtcNow;
            }
        }
    }
}
