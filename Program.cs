using FileHandler;
using TaskManager;
using Spectre.Console;

namespace TaskPad
{
    internal class Program
    {
        //Delegates
        public delegate void StartMenu();
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

            StartMenu startMenu = new StartMenu(uiDashboard.welcomeMsg);
            startMenu += uiDashboard.NotificationSystem;
            startMenu += uiDashboard.showMenu;

            AddTaskDel addTaskDel = new AddTaskDel(taskManager.AddTask);
            addTaskDel += uiDashboard.addTask;

            showTasksDel showTasksDel = new showTasksDel(uiDashboard.showTasks);

            showSingleTaskDel showSingleTaskDel = new showSingleTaskDel(uiDashboard.showSingleTask);

            updateTaskDel updateTaskDel = new updateTaskDel(uiDashboard.updateTask);
            updateTaskDel += taskManager.UpdateTask;

            updateTaskStatusDel updateTaskStatusDel = new updateTaskStatusDel(uiDashboard.updateTaskStatus);
            updateTaskStatusDel += taskManager.UpdateTaskStatus;

            deleteTaskDel deleteTaskDel = new deleteTaskDel(uiDashboard.deleteTask);
            deleteTaskDel += taskManager.DeleteTask;

            saveTaskDel saveTaskDel = new saveTaskDel(uiDashboard.saveAllData);
            saveTaskDel += fileClass.saveAllData;

            do
            {
                startMenu();
                switch (getInputs.getOption())
                {
                    case 1:
                        Guid uuid = Guid.NewGuid();
                        addTaskDel(taskManager.getLastIndex() + 1, uuid.ToString(), getInputs.getTitle(0), getInputs.getDescription(0), getInputs.getDate(0), getInputs.getPriorityLevel(0));
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
                        int id = getInputs.getId();
                        AnsiConsole.MarkupLine("[yellow] >> Leave the attribute empty if you dont want to update[/]");
                        Console.WriteLine();
                        updateTaskDel(id, getInputs.getTitle(id), getInputs.getDescription(id), getInputs.getDate(id), getInputs.getPriorityLevel(id));
                        break;
                    case 6:
                        deleteTaskDel(getInputs.getId());
                        break;
                    case 7:
                        saveTaskDel(taskManager.ViewTask());
                        break;
                    case 8:
                        uiDashboard.checkSave(saveTaskDel, uiDashboard.refreshApp);
                        Main(new string[] { });
                        break;
                    case 9:
                        uiDashboard.checkSave(saveTaskDel, uiDashboard.ExitApp);
                        break;
                }

            } while (uiDashboard.showFlag());

            uiDashboard.checkSave(saveTaskDel, uiDashboard.ExitApp );

        }
    }
}