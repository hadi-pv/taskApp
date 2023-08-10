using FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager;
using static System.Net.Mime.MediaTypeNames;
using static TaskPad.Program;

namespace TaskPad
{
    internal class UIDashboard
    {
        TaskClass taskManager;
        getInputs getInputs;
        FileClass fileClass;

        public UIDashboard(TaskClass taskManager,getInputs getInputs, FileClass fileClass) {
            this.taskManager = taskManager;
            this.getInputs = getInputs;
            this.fileClass = fileClass;
        }

        public void welcomeMsg()
        {
            Console.WriteLine("Welcome " + getInputs.getName() + " to the TODO application.");
        }

        public void NotificationSystem()
        {
            Console.WriteLine("Important Notifications");
            Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-");
            foreach (TaskItem task in taskManager.getNotification())
            {
                Console.WriteLine();
                Console.WriteLine($"The Id of task is {task.Id}");
                Console.WriteLine($"The Title of task is {task.Title}");
                Console.WriteLine($"The Description of task is {task.Description}");
                Console.WriteLine($"The CompletedStatus of task is {task.CompletedStatus}");
                Console.WriteLine($"The Due Date of task is {task.DueDate.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"The Priority Level of task is {task.PriorityLevel}");
                Console.WriteLine($"The days remaining is {(task.DueDate - DateTime.Today).TotalDays}");
                Console.WriteLine("-------------------------------------------------------");
            }

        }

        public void showMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Select number of any of the operations below");
            Console.WriteLine("1.Add a Task");
            Console.WriteLine("2.View all tasks");
            Console.WriteLine("3.View a specific task");
            Console.WriteLine("4.Mark a task as completed");
            Console.WriteLine("5.Update a task");
            Console.WriteLine("6.Delete a task");
            Console.WriteLine("7.Save all data");
            Console.WriteLine("8.Refresh app and load data again");
            Console.WriteLine("9.Exit Application");
        }

        public void addTask(int id,string uuid,string title,string description, DateTime dueD, int priorityL)
        {
            Console.WriteLine($"Task {id} with uid {uuid} added with title : {title} and description : {description}");
        }

        public void showTasks()
        {
            Console.WriteLine("Showing All Tasks");
            foreach (TaskItem task in taskManager.ViewTask())
            {
                Console.WriteLine();
                Console.WriteLine($"The Id of task is {task.Id}");
                Console.WriteLine($"The Title of task is {task.Title}");
                Console.WriteLine($"The Description of task is {task.Description}");
                Console.WriteLine($"The CompletedStatus of task is {task.CompletedStatus}");
                Console.WriteLine($"The Due Date of task is {task.DueDate.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"The Priority Level of task is {task.PriorityLevel}");
                Console.WriteLine("-------------------------------------------------------");
            }
        }

        public void showSingleTask(int id)
        {
            TaskItem task = taskManager.ViewTask(id);
            Console.WriteLine();
            Console.WriteLine($"The Id of task is {task.Id}");
            Console.WriteLine($"The Title of task is {task.Title}");
            Console.WriteLine($"The Description of task is {task.Description}");
            Console.WriteLine($"The CompletedStatus of task is {task.CompletedStatus}");
            Console.WriteLine($"The Due Date of task is {task.DueDate.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"The Priority Level of task is {task.PriorityLevel}");
            Console.WriteLine("-------------------------------------------------------");
        }

        public void updateTask(int id, string title, string description, DateTime dueD, int priorityL) 
        {
            Console.WriteLine($"New Task with ID {id} is");
            showSingleTask(id);
        }

        public void updateTaskStatus(int id)
        {
            Console.WriteLine("This is the task you are trying to complete");
            showSingleTask(id);
            Console.WriteLine("You sure you want to change status");
            if (!getInputs.getFlag())
            {
                return;
            }
        }

        public void deleteTask(int id)
        {
            Console.WriteLine($"Task with {id} deleted");
        }

        public bool showFlag()
        {
            Console.WriteLine("Do you want to try start a new operation.");
            if (getInputs.getFlag())
            {
                Console.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void refreshApp()
        {
            Console.WriteLine("Refreshing App");
            Thread.Sleep(1000);
            Console.Clear();
        }

        public void saveAllData(List<TaskItem> tasks)
        {
            Console.WriteLine("Saving all tasks");
            Thread.Sleep(1000);
        }

        public void ExitApp(saveTaskDel saveTaskDel)
        {
            if(taskManager.ViewTask().Count != fileClass.readFile().Count)
            {
                Console.WriteLine("Data is not saved, do you want to save data.");
                if(getInputs.getFlag())
                {
                    saveTaskDel(taskManager.ViewTask());
                }
                else
                {
                    Console.WriteLine("Not saving the data.");
                }
            }
            Console.WriteLine("You are exiting the app");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }

    }
}
