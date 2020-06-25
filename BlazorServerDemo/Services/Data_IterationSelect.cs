using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerDemo.Services
{
    public partial class Data
    {
        public async Task<IEnumerable<SelectListItem>> GetIterationsAsync(int count = 5)
        {
            var ws = await GetWorkspaceAsync();
            var schedule = await GetAsync<IterationSchedule>(ws.IterationScheduleId ?? 0);
            if (schedule == null) return Enumerable.Empty<SelectListItem>();
            return GetIterationsInner(schedule, count);
        }

        private IEnumerable<SelectListItem> GetIterationsInner(IterationSchedule schedule, int count)
        {
            yield return new SelectListItem("Backlog", "-1");

            yield return new SelectListItem("Current", "0");
        }
    }
}
