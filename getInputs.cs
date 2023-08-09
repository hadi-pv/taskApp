using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskManager;

namespace TaskPad
{
    internal class getInputs
    {
        TaskClass taskManager;

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
                        Console.WriteLine("Give correct option");
                    }
                    else
                    {
                        return val;
                    }
                }
                catch
                {
                    Console.WriteLine("Give correct option number");
                    continue;
                }
            }
        }

        public bool getFlag()
        {
            while (true)
            {
                Console.WriteLine("(Give your answer as Y/N or y/n)");
                var flag = Console.ReadLine();
                if (flag == "Y" | flag=="y")
                {
                    return true;
                }
                Console.WriteLine("Please Try Again by giving ans correctly (as Y/N)");
                return false;
            }
        }

        public string getName()
        {
            while (true)
            {
                var name = Console.ReadLine();
                if (name != null && Regex.IsMatch(name, @"^[a-zA-Z ]+$"))
                {
                    return name;
                }
                else
                {
                    Console.WriteLine("Write a real name");
                    continue;
                }
            }
        }

        public int getId()
        {
            while (true)
            {
                Console.WriteLine("Please Enter ID number of task");
                try
                {
                    var val = Convert.ToInt32(Console.ReadLine());
                    if (val < 0)
                    {
                        Console.WriteLine("Please Give a positive value");
                        continue;
                    }
                    else if (val > taskManager.ViewTask().Count)
                    {
                        Console.WriteLine("Give correct ID Number");
                        continue;
                    }
                    else
                    {
                        return val;
                    }
                }
                catch
                {
                    Console.WriteLine("Try again with proper id");
                    continue;
                }
            }
        }

        public string getTitle()
        {
            Console.WriteLine("Please give title for the Task");
            while (true)
            {
                try
                {
                    var title = Console.ReadLine();
                    if(title!=null && Regex.IsMatch(title, @"^[a-zA-Z0-9-_ ]+$"))
                    {
                        return title;
                    }
                    else
                    {
                        Console.WriteLine("Give a proper title");
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("Try giving a proper title again");
                    continue;
                }
            }
        }

        public string getDescription()
        {
            Console.WriteLine("Please give Description for the Task");
            while (true)
            {
                try
                {
                    var desc = Console.ReadLine();
                    if (desc!=null && desc.Contains(' '))
                    {
                        return desc;
                    }
                    else
                    {
                        Console.WriteLine("Give a proper description");
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("Try giving a proper description again");
                    continue;
                }
            }
        }

    }
}
