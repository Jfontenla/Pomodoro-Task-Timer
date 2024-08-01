using Pomodoro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Domain.IRepositories
{
    public interface ITaskDurationRepository : IRepository<TaskDuration>
    {
        Task<IEnumerable<TaskDuration>> GetTaskDurationByIds(int taskId, int userId);
        Task<TaskDuration> GetTaskDurationByIdsWithoutEnd(int taskId, int userId);
    }
}
