using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskManager;
using Spectre.Console;

namespace TaskPad
{
    internal class getInputs
    {
        TaskClass taskManager;
        string name;

        public getInputs(TaskClass taskManager)
        {
            this.taskManager = taskManager;
        }

        public int getOption()
        {
            while (true)
            {
                try
                {
                    var val = Convert.ToInt32(Console.ReadLine());
                    if(val<=0 | val > 9)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        return val;
                    }
                }
                catch
                {
                    AnsiConsole.MarkupLine("[red]Try again with correct option number[/]".ToUpper());
                    continue;
                }
            }
        }

        public bool getFlag()
        {
            return AnsiConsole.Confirm("[green](Give your answer as Y/N or y/n)[/]");
        }

        public string getName()
        {
            if(name == null)
            {
                AnsiConsole.Markup(" -> Your [green]NAME[/] Please : ");
                while (true)
                {
                    try
                    {
                        var name = Console.ReadLine();
                        if (name != null && Regex.IsMatch(name, @"^[a-zA-Z ]+$"))
                        {
                            this.name = name;
                            Console.WriteLine();
                            return name.ToUpper();
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        Console.WriteLine();
                        AnsiConsole.MarkupLine("[red]Write a real name.[/]".ToUpper());
                        continue;
                    }
                }
            }
            else
            {
                return name;
            }
        }

        public int getId()
        {
            while (true)
            {
                Console.WriteLine();
                var tasks = taskManager.getIdTitle();
                try
                {
                    var framework = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title(" -> Please select the ID of task")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more frameworks)[/]")
                        .AddChoices(tasks));
                    return Array.IndexOf(tasks, framework)+1;
                }
                catch
                {
                    AnsiConsole.MarkupLine("[red]Try again with proper id.[/]".ToUpper());
                    continue;
                }

            }
        }

        public string getTitle()
        {
            Console.WriteLine(" -> Please give title for the Task");
            while (true)
            {
                try
                {
                    AnsiConsole.Markup(" >> ");
                    var title = Console.ReadLine();
                    if(title!=null && Regex.IsMatch(title, @"^[a-zA-Z0-9-_ ]+$"))
                    {
                        return title;
                    }
                    else
                    {
                        throw new Exception() ;
                    }
                }
                catch
                {
                    AnsiConsole.MarkupLine("[red]try again with proper title.[/]".ToUpper());
                    continue;
                }
            }
        }

        public string getDescription()
        {
            Console.WriteLine(" -> Please give Description for the Task");
            while (true)
            {
                try
                {
                    AnsiConsole.Markup(" >> ");
                    var desc = Console.ReadLine();
                    if (desc!=null && desc.Contains(' '))
                    {
                        return desc;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    AnsiConsole.MarkupLine("[red]Try agin with proper description[/]".ToUpper());
                    continue;
                }
            }
        }

        public DateTime getDate()
        {
            Console.WriteLine(" -> Write the due date for the task");
            while (true)
            {
                try
                {
                    AnsiConsole.Markup(" >> ");
                    var date = Convert.ToDateTime(Console.ReadLine());
                    if (date != null && (date - DateTime.Today).TotalDays>=0)
                    {
                        return date;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    AnsiConsole.MarkupLine("[red]Try again with a proper date format[/]".ToUpper());
                    continue;
                }
            }
        }

        public int getPriorityLevel()
        {
            Console.WriteLine(" -> Give priority level for the task (1,2,3 -> 1 as highest and 3 as lowest)");
            while (true)
            {
                try
                {
                    AnsiConsole.Markup(" >> ");
                    int pLevel = Convert.ToInt32(Console.ReadLine());
                    if (new int[] { 1, 2, 3 }.Contains(pLevel))
                    {
                        return pLevel;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    AnsiConsole.MarkupLine("[red]Try again with proper priority level[/]".ToUpper());
                    continue;
                }
            }
        }

    }
}
