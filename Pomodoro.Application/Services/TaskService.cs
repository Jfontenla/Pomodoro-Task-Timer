using Pomodoro.Domain.Entities;
using Pomodoro.Domain.IRepositories;
using Labor = Pomodoro.Domain.Entities.Task;
using Task = System.Threading.Tasks.Task;

namespace Pomodoro.Application.Services
{
    public class TaskService
    {
        private readonly IRepository<Labor> _taskRepository;
        private readonly IRepository<User> _userRepository;
        private readonly ITaskDurationRepository _taskDurationRepository;

        public TaskService(IRepository<Labor> taskRepository, IRepository<User> userRepository, ITaskDurationRepository taskDurationRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _taskDurationRepository = taskDurationRepository;
        }

        public async Task CreateTaskAsync(CreationTaskDto creationTaskDto)
        {
            var task = new Labor
            {
                Title = creationTaskDto.Title,
                Descpription = creationTaskDto.Description,
                Code = creationTaskDto.Code,
                Estimate = creationTaskDto.Estimate > 0 ? creationTaskDto.Estimate : 0
            };

            await _taskRepository.AddAsync(task);

        }

        public async Task IniciarTareaAsync(int taskId, int userId)
        {
            var labor = await _taskRepository.GetByIdAsync(taskId);
            if (labor == null) throw new Exception("Task doesn't found");

            var durationTask = await _taskDurationRepository.GetTaskDurationByIdsWithoutEnd(taskId, userId);
            if (durationTask == null)
            {
                durationTask = new TaskDuration
                {
                    TaskId = taskId,
                    UserId = userId,
                    StartTime = DateTime.UtcNow
                };
                await _taskDurationRepository.AddAsync(durationTask);
            }
            else
            {
                throw new Exception("This Task is existing, you only will be finished");
            }

        }

        public async Task FinalizarTareaAsync(int taskId, int userId)
        {
            var labor = await _taskRepository.GetByIdAsync(taskId);
            if (labor == null) throw new Exception("Task doesn't found");

            var durationTask = await _taskDurationRepository.GetTaskDurationByIdsWithoutEnd(taskId, userId);

            if (durationTask != null)
            {
                durationTask = new TaskDuration
                {
                    TaskId = taskId,
                    UserId = userId,
                    EndTime = DateTime.UtcNow
                };
                await _taskDurationRepository.AddAsync(durationTask);
            }
            else
            {
                throw new Exception("This Task doesn't exist");
            }
        }

        public async Task<IEnumerable<Labor>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<Labor> GetTaskById(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }
    }
}
