using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerDemo.Services
{
    public partial class Data
    {
        public async Task<IEnumerable<KeyValuePair<int, string>>> GetIterationsAsync(int count = 5)
        {
            var ws = await GetWorkspaceAsync();
            var schedule = await GetAsync<IterationSchedule>(ws.IterationScheduleId ?? 0);
            if (schedule == null) return Enumerable.Empty<KeyValuePair<int, string>>();
            return GetIterationsInner(schedule, count);
        }

        private IEnumerable<KeyValuePair<int, string>> GetIterationsInner(IterationSchedule schedule, int count)
        {
            yield return new KeyValuePair<int, string>(-1, "Backlog");

            int index = 0;
            DateTime seedDate = schedule.StartDate;
            string dateFormat = "M/d";
            do
            {
                DateTime endDate = seedDate.AddDays(schedule.TotalDays - 1);
                if (endDate.Year > seedDate.Year) dateFormat = "M/d/yy";
                string name = $"{getName(index)} - {endDate.ToString(dateFormat)}";
                yield return new KeyValuePair<int, string>(index, name);
                index++;
                seedDate = endDate.AddDays(1);
            } while (index <= count);

            string getName(int index)
            {
                return
                    (index == 0) ? "Current" :
                    (index == 1) ? "Next" :
                    $"+{index}";
            }
        }
    }
}
