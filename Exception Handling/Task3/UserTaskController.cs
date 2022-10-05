using System;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskController
    {
        private readonly UserTaskService _taskService;

        public UserTaskController(UserTaskService taskService)
        {
            _taskService = taskService;
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            try
            {
                _taskService.AddTaskForUser(userId, new UserTask(description));
                return true;
            }
            catch (ArgumentException ex)
            {
                model.AddAttribute("action_result", ex.Message);
                return false;
            }
        }
    }
}