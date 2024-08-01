using Microsoft.EntityFrameworkCore;
using Pomodoro.Domain.Entities;
using Pomodoro.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Infraestructure.Repositories
{
    public class TaskDurationRepository : Repository<TaskDuration>, ITaskDurationRepository
    {
        public TaskDurationRepository(AppDbContext appDbContext, DbSet<TaskDuration> dbSet) : base(appDbContext, dbSet)
        {
        }

        public async Task<IEnumerable<TaskDuration>> GetTaskDurationByIds(int taskId, int userId)
        {
            return await _dbSet.Where(td => td.TaskId == taskId && td.UserId == userId).ToListAsync();
        }

        public async Task<TaskDuration> GetTaskDurationByIdsWithoutEnd(int taskId, int userId)
        {
            return await _dbSet.Where(td => td.TaskId == taskId && td.UserId == userId && td.EndTime == null).FirstOrDefaultAsync();
        }
    }
}
