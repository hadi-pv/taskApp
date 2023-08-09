using FileHandler;
using TaskManager;

namespace TaskPad
{
    internal class Program
    {
        //Delegates
        public delegate void AddTaskDel(int id,string uuid,string title,string desc,DateTime dueD, int priorityL);
        public delegate void showTasksDel();
        public delegate void showSingleTaskDel(int id);
        public delegate void updateTaskDel(int id,string title,string desc, DateTime DueD,int priorityL);
        public delegate void updateTaskStatusDel(int id);
        public delegate void deleteTaskDel(int id);
        public delegate void saveTaskDel(List<TaskItem> tasks);


        static void Main(string[] args)
        {
            FileClass fileClass = new FileClass();
            TaskClass taskManager = new TaskClass(fileClass.readFile());
            getInputs getInputs = new getInputs(taskManager);
            UIDashboard uiDashboard = new UIDashboard(taskManager, getInputs, fileClass);

            AddTaskDel addTaskDel = new AddTaskDel(taskManager.AddTask);
            addTaskDel += uiDashboard.addTask;

            showTasksDel showTasksDel = new showTasksDel(uiDashboard.showTasks);

            showSingleTaskDel showSingleTaskDel = new showSingleTaskDel(uiDashboard.showSingleTask);

            updateTaskDel updateTaskDel = new updateTaskDel(uiDashboard.updateTask);
            updateTaskDel += taskManager.UpdateTask;

            updateTaskStatusDel updateTaskStatusDel = new updateTaskStatusDel(uiDashboard.updateTaskStatus);
            updateTaskStatusDel += taskManager.UpdateTaskStatus;

            deleteTaskDel deleteTaskDel = new deleteTaskDel(taskManager.DeleteTask);
            deleteTaskDel += uiDashboard.deleteTask;

            saveTaskDel saveTaskDel = new saveTaskDel(uiDashboard.saveAllData);
            saveTaskDel += fileClass.saveAllData;

            Console.WriteLine("Your Name Please");
            var name = getInputs.getName();

            do
            {
                switch (uiDashboard.showMenu(name))
                {
                    case 1:
                        Guid uuid = Guid.NewGuid();
                        addTaskDel(taskManager.getTaskCount() + 1, uuid.ToString(), getInputs.getTitle(), getInputs.getDescription(), getInputs.getDate(), getInputs.getPriorityLevel());
                        break;
                    case 2:
                        showTasksDel();
                        break;
                    case 3:
                        showSingleTaskDel(getInputs.getId());
                        break;
                    case 4:
                        updateTaskStatusDel(getInputs.getId());
                        break;
                    case 5:
                        updateTaskDel(getInputs.getId(), getInputs.getTitle(), getInputs.getDescription(), getInputs.getDate(), getInputs.getPriorityLevel());
                        break;
                    case 6:
                        deleteTaskDel(getInputs.getId());
                        break;
                    case 7:
                        saveTaskDel(taskManager.ViewTask());
                        break;
                    case 8:
                        uiDashboard.refreshApp();
                        Main(new string[] { });
                        break;
                    case 9:
                        uiDashboard.ExitApp(saveTaskDel);
                        break;
                }

            } while (uiDashboard.showFlag());

            uiDashboard.ExitApp(saveTaskDel);

        }
    }
}