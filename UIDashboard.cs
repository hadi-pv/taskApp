using FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager;
using static System.Net.Mime.MediaTypeNames;
using static TaskPad.Program;
using ConsoleTables;
using Spectre.Console;

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
            AnsiConsole.Write(
            new FigletText("TaskAlarm")
                .Color(Color.DarkSeaGreen4_1));
            Console.WriteLine();
            AnsiConsole.Markup(" -> Welcome [green]" + getInputs.getName() + "[/] to the TODO application.".ToUpper());
            Console.WriteLine();
        }

        public void NotificationSystem()
        {
            if (!taskManager.notificationFlag)
            {
                var table=new ConsoleTable("ID","TITLE","PRIORITY LEVEL","DAYS REMAINING");
                Console.WriteLine();
                AnsiConsole.Markup("  [red]IMPORTANT NOTIFICATION[/]  ");
                Console.WriteLine();
                foreach (TaskItem task in taskManager.getNotification())
                {
                    table.AddRow($"{task.Id}",
                        $"{task.Title}",
                        $"{task.PriorityLevel}",
                        $"{(task.DueDate - DateTime.Today).TotalDays}");
                }
                table.Write();
                AnsiConsole.Markup("[red]---------------------------------------------------[/]");
                Console.WriteLine();
                Thread.Sleep(1000);
            }

        }

        public void showMenu()
        {
            var table = new ConsoleTable("Option Number", "Description");
            table.AddRow("1","Add a Task");
            table.AddRow("2","View all tasks");
            table.AddRow("3","View a specific task");
            table.AddRow("4","Mark a task as completed");
            table.AddRow("5","Update a task");
            table.AddRow("6","Delete a task");
            table.AddRow("7","Save all data");
            table.AddRow("8","Refresh app and load data again");
            table.AddRow("9","Exit Application");
            table.Write();
            Console.WriteLine();
            AnsiConsole.Markup("[green] -> Select number of any of the operations above : [/]");
        }

        public void addTask(int id,string uuid,string title,string description, DateTime dueD, int priorityL)
        {
            Console.WriteLine();
            Console.WriteLine($" -> Task {id} with uid {uuid} added with title : {title} and description : {description}");
            Console.WriteLine();
        }

        public void showTasks()
        {
            Console.WriteLine();
            Console.WriteLine(" -> Showing All Tasks : ");
            Console.WriteLine();
            var table = new ConsoleTable("ID", "TITLE", "DESCRIPTION","COMPLETE STATUS","DUE DATE","PRIORITY LEVEL");
            foreach (TaskItem task in taskManager.ViewTask())
            {
                table.AddRow($"{task.Id}",
                    $"{task.Title}",
                    $"{task.Description}",
                    $"{task.CompletedStatus}",
                    $"{task.DueDate.ToString("dd/MM/yyyy")}",
                    $"{task.PriorityLevel}");
            }
            table.Write();
            Console.WriteLine();
        }

        public void showSingleTask(int id)
        {
            TaskItem task = taskManager.ViewTask(id);
            var table = new ConsoleTable("ID", "TITLE", "DESCRIPTION", "COMPLETE STATUS", "DUE DATE", "PRIORITY LEVEL");
            table.AddRow($"{task.Id}",
                $"{task.Title}",
                $"{task.Description}",
                $"{task.CompletedStatus}",
                $"{task.DueDate.ToString("dd/MM/yyyy")}",
                $"{task.PriorityLevel}");
            table.Write();
            Console.WriteLine();
        }

        public void updateTask(int id, string title, string description, DateTime dueD, int priorityL) 
        {
            Console.WriteLine($" -> New Task with ID {id} is");
            showSingleTask(id);
        }

        public void updateTaskStatus(int id)
        {
            Console.WriteLine(" -> This is the task you are trying to complete : ");
            showSingleTask(id);
            AnsiConsole.Markup("[green]You sure you want to change status[/]");
            if (!getInputs.getFlag())
            {
                Console.WriteLine();
                return;
            }
            Console.WriteLine();
        }

        public void deleteTask(int id)
        {
            Console.WriteLine(" -> This is the task you are trying to delete : ");
            showSingleTask(id);
            AnsiConsole.Markup("[green]You sure you want to delete this task[/]");
            if (!getInputs.getFlag())
            {
                Console.WriteLine();
                return;
            }
            Console.WriteLine();
        }

        public bool showFlag()
        {
            AnsiConsole.MarkupLine("[green]Do you want to try start a new operation.[/]");
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
            Console.WriteLine(" -> Refreshing App");
            Thread.Sleep(1000);
            Console.Clear();
        }

        public void saveAllData(List<TaskItem> tasks)
        {
            Console.WriteLine(" -> Saving all tasks");
            Thread.Sleep(1000);
        }

        public void ExitApp(saveTaskDel saveTaskDel)
        {
            if(taskManager.ViewTask().Count != fileClass.readFile().Count || taskManager.getLastIndex()!= taskManager.ViewTask().Count)
            {
                AnsiConsole.Markup("[green]Data is not saved, do you want to save data.[/]");
                if(getInputs.getFlag())
                {
                    saveTaskDel(taskManager.ViewTask());
                }
                else
                {
                    Console.WriteLine(" -> Not saving the data.");
                }
            }
            Console.WriteLine(" -> You are exiting the app");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }

    }
}
