using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorServerDemo.Services
{
    public class TimeZoneListing
    {
        public IEnumerable<TimeZoneInfo> GetTimeZones() => TimeZoneInfo.GetSystemTimeZones().OfType<TimeZoneInfo>();

        public IEnumerable<SelectListItem> GetTimeZoneItems() => GetTimeZones().Select(tz => new SelectListItem() { Value = tz.Id, Text = tz.DisplayName });
    }
}
