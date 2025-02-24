using MainProject;
using Big_Project;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using Big_Project;

namespace MainProject
{
    public class Statistics //class to hold arrays
    {
        public string StatOne;
        public string StatTwo;
        public string StatThree;
        public string StatFour;
        public string StatFive;
    }

    public class Project
    {

        public static Statistics[] stat = new Statistics[1000];
        public static int count = 0;
        public static void MainMenu(bool saved)
        {
            Console.Clear();//menu screen that displays all options
            output.Typewriter("THE ULTIMATE INDEX VIEWER\n");
            output.Typewriter("Pick an option:\n");
            output.Typewriter("1) Display Data\n");
            output.Typewriter("2) Add Entry\n");
            output.Typewriter("3) Save\n");
            output.Typewriter("4) Exit\n");
            output.Typewriter("Type number to continue...\n");
            if (saved == false)
            {
                output.Typewriter("you are curently not saved...\n");
            }
        }


        public static void setup()//read in the file and saves it to an array so we can start to do stuff with it!
        {
            bool done = false;

            string input;
            string[] line = new string[1000];
            string[] split = new string[5];
            try
            {
                StreamReader mainFile = new StreamReader("Input.txt");
                while (done == false) // to make things slightly easier, we read the file and count the amount of lines
                {
                    input = mainFile.ReadLine();

                    if (input == null)
                    {
                        done = true;
                        mainFile.Close();
                    }
                    else
                    {
                        line[count] = input;
                        count++;
                    }
                }
            }
            catch { output.Typewriter("bad"); }

            for (int i = 0; i < 1000; i++)
            {
                stat[i] = new Statistics(); //declares our stat variable
            }
            for (int i = 0; i < count; i++)
            {
                split = line[i].Split(" ");//Splits up every string into 5 separate varables 
                stat[i].StatOne = split[0];
                stat[i].StatTwo = split[1];
                stat[i].StatThree = split[2];
                stat[i].StatFour = split[3];
                stat[i].StatFive = split[4];
            }
        }
        public static bool Display()
        {
            int start = 0;
            int end = 12;
            int Ypos;
            bool finish = false, done = false, done2 = false, saved = true;

            while (finish == false)
            {
                if (start < 0)
                {
                    start = 0;
                    end = 12;
                    if (count - 1 < 12)
                    {
                        end = count - 1;
                    }
                }
                if (end > count - 1)
                {
                    end = count - 1;
                    start = end - 12;
                    if (start < 0)
                    {
                        start = 0;
                    }
                }

                output.Typewriter("# Name\t\t\tHealth\t\t\tAge\t\t\tDefence\t\t\tHeight(M)\t\t\n");
                Ypos = 2;
                Console.WriteLine("----------------------------------------------------------------------------------------------------------\n");//added cute little borders to our table

                for (int i = start; i <= end; i++)
                {
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------\n");//added cute little borders to our table
                    Console.SetCursorPosition(0, Ypos);//Writes all values in an easy to read chart and displays them with adequate spacing
                    Console.Write($"{i} {stat[i].StatOne}                           ");
                    Console.SetCursorPosition(24, Ypos);
                    Console.Write($"{stat[i].StatTwo}                               ");
                    Console.SetCursorPosition(48, Ypos);
                    Console.Write($"{stat[i].StatThree}                             ");
                    Console.SetCursorPosition(72, Ypos);
                    Console.Write($"{stat[i].StatFour}                              ");
                    Console.SetCursorPosition(96, Ypos);
                    Console.Write($"{stat[i].StatFive}\n");
                    Ypos += 2;
                }
                Console.Write("----------------------------------------------------------------------------------------------------------\n");
                Console.Write("press d to scroll down, a to scroll up, q to quit, e to edit/delete a row, and  s to sort");
                done = false;
                while (done == false)
                {
                    ConsoleKeyInfo Key = Console.ReadKey(true);
                    done = true;

                    if (Key.KeyChar == 'd' && end != count - 1) // scroll down
                    {
                        Console.Clear();
                        start += 13;
                        end += 13;
                    }
                    else if (Key.KeyChar == 'a' && start != 0) // scroll up
                    {
                        start -= 13;
                        end -= 13;
                    }
                    else if (Key.KeyChar == 'q') //exit
                    {
                        Console.Clear();
                        finish = true;
                    }
                    else if (Key.KeyChar == 'e')
                    {
                        Console.CursorVisible = true;
                        done2 = false;
                        Console.SetCursorPosition(0,Console.CursorTop);
                        output.Typewriter("use 'w' 'a' 's' and 'd' to navigate. Press p to select what you would like to edit, and press 'x' to delete                 ");
                        Console.SetCursorPosition(2, 2);
                        while (done2 == false)
                        {
                            done2 = true;
                            saved = false;
                            
                            Key = Console.ReadKey(true);
                            if (Key.KeyChar == 'w' && Console.CursorTop > 2)
                            {
                                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 2);
                                done2 = false;
                            }
                            else if (Key.KeyChar == 's' && Console.CursorTop < 26)
                            {
                                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 2);
                                done2 = false;
                            }
                            else if (Key.KeyChar == 'a' && Console.CursorLeft > 2)
                            {
                                if (Console.CursorLeft == 24)
                                {
                                    Console.SetCursorPosition(2, Console.CursorTop);
                                }
                                else
                                {
                                    Console.SetCursorPosition(Console.CursorLeft - 24, Console.CursorTop);
                                }
                                done2 = false;
                            }
                            else if (Key.KeyChar == 'd' && Console.CursorLeft <= 84)
                            {
                                if (Console.CursorLeft == 2)
                                {
                                    Console.SetCursorPosition(24, Console.CursorTop);
                                }
                                else
                                {
                                    Console.SetCursorPosition(Console.CursorLeft + 24, Console.CursorTop);
                                }
                                done2 = false;
                            }
                            else if (Key.KeyChar == 'q')
                            {
                                done2 = true;
                            }
                            else if (Key.KeyChar == 'p')
                            {
                                done2 = true;
                                {
                                    int left = 0;
                                    if (Console.CursorLeft == 2)
                                    {
                                        left = 1;
                                    }
                                    else if (Console.CursorLeft == 24)
                                    {
                                        left = 2;
                                    }
                                    else if (Console.CursorLeft == 48)
                                    {
                                        left = 3;
                                    }
                                    else if (Console.CursorLeft == 72)
                                    {
                                        left = 4;
                                    }
                                    else if (Console.CursorLeft == 96)
                                    {
                                        left = 5;
                                    }
                                    edit(start + (Console.CursorTop/2 - 1  ),left);
                                }
                            }
                            else if (Key.KeyChar == 'x')
                            {
                                delete(start + Console.CursorTop / 2 - 1);
                            }
                            else
                            {
                                done2 = false;
                            }
                        }
                    }
                    else if (Key.KeyChar == 's')
                    {
                        sorting();
                        saved = false;
                    }
                    else
                    {
                        done = false;
                    }
                }
                Console.Clear();
            }
            return saved;
        }
        public static void AddEntry()
        {
            String temp1 = "";
            int temp2 = 0;
            double temp3 = 0;
            output.Typewriter("Enter a name\n");
            temp1 = inputs.getString();
            stat[count].StatOne = temp1;
            output.Typewriter("Enter a health value (int only)\n");
            temp2 = inputs.getInt();
            stat[count].StatTwo = Convert.ToString(temp2);
            output.Typewriter("Enter an age (int only)\n");
            temp2 = inputs.getInt();
            stat[count].StatThree = Convert.ToString(temp2);
            output.Typewriter("Enter a defence value (int only)\n");
            temp2 = inputs.getInt();
            stat[count].StatFour = Convert.ToString(temp2);
            output.Typewriter("enter a height value (rounds to 2 decimal places)\n");
            temp3 = inputs.getDouble();
            stat[count].StatFive = Convert.ToString(Math.Round(temp3, 2));
            count++;
            Console.Clear();
            output.Typewriter("New Entry Added!");
            Thread.Sleep(2000);
        }
        public static void sorting()
        {
            int count2,clowvar,c,h = 0;
            bool done2,done3 = false;
            c = 0;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine("                                                                                                         ");
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            output.Typewriter("\nWhat would you like to sort by?");
            output.Typewriter("\n1. Name    2. Health   3.Age    4. Defence   5. Height");

            
            while (done3 == false)
            {
                done3 = true;
                ConsoleKeyInfo Key = Console.ReadKey(true);
                if (Key.KeyChar == '1') { c = 1; }
                else if (Key.KeyChar == '2') { c = 2; }
                else if (Key.KeyChar == '3') { c = 3; }
                else if (Key.KeyChar == '4') { c = 4; }
                else if (Key.KeyChar == '5') { c = 5; }
                else { done3 = false; }

            }
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine("                                                                                                                 ");
            Console.WriteLine("                                                                                                                 ");
            Console.SetCursorPosition (0, Console.CursorTop - 2);
            done3 = false;
            output.Typewriter("High to Low? (1) or Low to High? (2)");
            while (done3 == false)
            {
                done3 = true;
                ConsoleKeyInfo Key = Console.ReadKey(true);
                if (Key.KeyChar == '1') { h = 1; }
                else if (Key.KeyChar == '2') { h = 2; }
                else { done3 = false; }

            }
            for (int i = 0; i < count - 1; i++)
            {
                count2 = 0;
                done2 = false;
                clowvar = 0;
                string temp1,temp2,temp3,temp4,temp5;

                while (done2 == false)
                {
                    if (h == 1)
                    {
                        if (compare(c, count2, clowvar, i) == 1)

                        {
                            clowvar = count2;
                        }
                    }
                    else if (h == 2)
                    {
                        if (compare(c, count2, clowvar, i) == -1)

                        {
                            clowvar = count2;
                        }
                    }
                    if (count2 + i == count - 1)
                    {
                        temp1 = stat[clowvar + i].StatOne;
                        temp2 = stat[clowvar + i].StatTwo;
                        temp3 = stat[clowvar + i].StatThree;
                        temp4 = stat[clowvar + i].StatFour;
                        temp5 = stat[clowvar + i].StatFive;
                        stat[clowvar + i].StatOne = stat[i].StatOne;
                        stat[clowvar + i].StatTwo = stat[i].StatTwo;
                        stat[clowvar + i].StatThree = stat[i].StatThree;
                        stat[clowvar + i].StatFour = stat[i].StatFour;
                        stat[clowvar + i].StatFive = stat[i].StatFive;
                        stat[i].StatOne = temp1;
                        stat[i].StatTwo = temp2;
                        stat[i].StatThree = temp3;
                        stat[i].StatFour = temp4;
                        stat[i].StatFive = temp5;
                        done2 = true;

                    }
                    else
                    {
                        count2++;
                    }
                }
            }


        }
        public static void delete(int row)
        {
            string temp1, temp2, temp3, temp4, temp5;
            for (int i = row; i < count - 1; i++)
            {
            stat[i].StatOne = stat[i + 1].StatOne;
            stat[i].StatTwo = stat[i + 1].StatTwo;
            stat[i].StatThree = stat[i + 1].StatThree;
            stat[i].StatFour = stat[i + 1].StatFour;
            stat[i].StatFive = stat[i + 1].StatFive;
            } count-= 1;
            




        }
        public static int compare(int c, int count2, int clowvar, int i)
        {
            int ans = 0;
            if (c == 1)
            {
                ans = stat[count2 + i].StatOne.ToLower().CompareTo(stat[clowvar + i].StatOne.ToLower());
            }
            else if (c == 2)
            {
                ans = Convert.ToInt32(stat[count2 + i].StatTwo).CompareTo(Convert.ToInt32(stat[clowvar + i].StatTwo));
            }
            else if (c == 3)
            {
                ans = Convert.ToInt32(stat[count2 + i].StatThree).CompareTo(Convert.ToInt32(stat[clowvar + i].StatThree));
            }
            else if (c == 4)
            {
                ans = Convert.ToInt32(stat[count2 + i].StatFour).CompareTo(Convert.ToInt32(stat[clowvar + i].StatFour));
            }
            else if (c == 5)
            {
                ans = Convert.ToDouble(stat[count2 + i].StatFive).CompareTo(Convert.ToDouble(stat[clowvar + i].StatFive));
            }

            return ans;
        }

        public static void Save()
        {
            string temp = "";
            try
            {
                StreamWriter savefile = new StreamWriter("input.txt");

                for (int i = 0; i < count; i++)
                {
                    temp = ($"{stat[i].StatOne} {stat[i].StatTwo} {stat[i].StatThree} {stat[i].StatFour} {stat[i].StatFive}");
                    savefile.WriteLine(temp);
                }
                savefile.Close();
            }
            catch (Exception e) { output.Typewriter(e.Message); }
            Console.Clear(); //some fun code that makes a saving animation
            Console.Write("saving\\");
            Thread.Sleep(250);
            Console.SetCursorPosition(6, 0);
            Console.Write("|");
            Thread.Sleep(250);
            Console.SetCursorPosition(6, 0);
            Console.Write("/");
            Thread.Sleep(250);
            Console.SetCursorPosition(6, 0);
            Console.Write("-");
            Thread.Sleep(250);
            Console.SetCursorPosition(6, 0);
            Console.Write("\\");
            Thread.Sleep(250);
            Console.SetCursorPosition(6, 0);            
            Console.Write("|");
            Thread.Sleep(250);
            Console.SetCursorPosition(6, 0);
            Console.Write("/");
            Thread.Sleep(250);
            Console.SetCursorPosition(6, 0);
            Console.Write("-");
            Thread.Sleep(250);
            Console.SetCursorPosition(6, 0);
            Console.Write("\\");
            Thread.Sleep(250);
            Console.SetCursorPosition(0, 0);
            Console.Write("Saved!!");
            Thread.Sleep(2000);
        }
        public static bool edit(int Row, int statnum)
        {
            int TempI;
            double TempD;
            string TempS;
            bool YesNo;
            Console.Clear();
            if (statnum == 1)
            {
                output.Typewriter("\nWhat would you like to change the name to? Current name is " + stat[Row].StatOne + "\n");
                TempS = inputs.getString();
                stat[Row].StatOne = TempS;
            }
            else if (statnum == 2)
            {
                output.Typewriter("What would you like to change the health value too? Current value is " + stat[Row].StatTwo + "\n");
                TempI = inputs.getInt();
                stat[Row].StatTwo = Convert.ToString(TempI);
            }
            else if (statnum == 3)
            {
                output.Typewriter("What would you like to change the age value too? Current value is " + stat[Row].StatThree + "\n");
                TempI = inputs.getInt();
                stat[Row].StatThree = Convert.ToString(TempI);
            }
            else if (statnum == 4)
            {
                output.Typewriter("\nWhat would you like to change the defence value to? Current value is " + stat[Row].StatFour + "\n");
                TempI = inputs.getInt();
                stat[Row].StatFour = Convert.ToString(TempI);
            }
            else if (statnum == 5) 
            {
                output.Typewriter("\nwhat would you like to change the height to? Current value is " + stat[Row].StatFive + " (will be rounded to two decimal places)\n");
                TempD = inputs.getDouble();
                stat[Row].StatFive= Convert.ToString(Math.Round(TempD,2));
            }
            Console.Clear();
            output.Typewriter("Edit Complete!");
            Thread.Sleep(2000);
            
            return false;
        }
        public static void Main()
        {
            bool finish = false;
            bool saved = true;
            Console.CursorVisible = false;
            bool done = false;
            int choice = 0;
            setup();

            while (finish == false)
            {
                MainMenu(saved);
               
                done = false;
                while (done == false)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    done = true;
                    if (key.KeyChar == '1')
                    {
                        Console.Clear();
                        saved = Display();
                    }
                    else if (key.KeyChar == '2')
                    {
                        Console.Clear();
                        AddEntry();
                        saved = false;
                    }
                    else if (key.KeyChar == '3')
                    {
                        Console.Clear();
                        Save();
                        saved = true;
                        
                    }
                    else if (key.KeyChar == '4')
                    {
                        finish = true;
                        Console.Clear();//neat 'good bye' text art
                        output.humber("  _______   ______     ______    _______     .______   ____    ____  _______ \r\n /  _____| /  __  \\   /  __  \\  |       \\    |   _  \\  \\   \\  /   / |   ____|\r\n|  |  __  |  |  |  | |  |  |  | |  .--.  |   |  |_)  |  \\   \\/   /  |  |__   \r\n|  | |_ | |  |  |  | |  |  |  | |  |  |  |   |   _  <    \\_    _/   |   __|  \r\n|  |__| | |  `--'  | |  `--'  | |  '--'  |   |  |_)  |     |  |     |  |____ \r\n \\______|  \\______/   \\______/  |_______/    |______/      |__|     |_______|\r\n                                                                             ");
                        Thread.Sleep(1500);
                    }
                    else
                    {
                        done = false;
                    }
                }
            }
        }

    }

}
