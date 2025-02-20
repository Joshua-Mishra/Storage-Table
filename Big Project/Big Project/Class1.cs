/************************
 *                      *
 * name:  Josh          *
 *                      *
 * Date: 2022/11/29     *
 * Project: Database    *
 ************************/




using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Big_Project
{
    public class inputs
    {

        private static void clear()
        {
            int top = Console.CursorTop;
            Thread.Sleep(500);
            Console.SetCursorPosition(0, top - 1);
            Console.Write("                                                          ");
            Console.Write("\n                                                       ");
            Console.SetCursorPosition(0, top - 1);
        }
        public static bool YesOrNo()
        {
            string inp = "";
            bool b = false, done = false;
            while (done == false)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.KeyChar == 'y')
                {
                    return true;
                }
                else if (key.KeyChar == 'n') 
                {
                    return false;
                }
            }

            return b;
        }
        public static string getString()
        {
            bool done = false;
            int top;
            String s = "";
            while (done == false)
            {
                s = Console.ReadLine();
                if (s == null)
                {
                    done = false;
                    output.Typewriter("Please enter a value");
                    clear();
                }
                else if (s.Contains(" "))//strings with spaces will break the program so i have them disabled
                {
                    done = false;
                    output.Typewriter("no spaces please");
                    clear();
                }
                else
                {
                    done = true;
                }
            }
            return s;
        }
        public static int getInt()
        {
            int i = 0;
            int top;
            bool done = false;
            while (done == false)
            {
                try
                {
                    i = Convert.ToInt32(Console.ReadLine());
                    if (i >= 0)
                    {
                        done = true;
                    }
                    else
                    {
                        output.Typewriter("Try again, no negatives");
                        clear();
                    }

                }
                catch
                {

                    output.Typewriter("Try again... No letters, decimals or overflow");
                    clear();
                }

            }
            return i;
        }
        public static double getDouble()
        {
            double d = 0;
            bool done = false;
            int top;
            while (done == false)
            {
                try
                {
                    d = Convert.ToDouble(Console.ReadLine());
                    if (d >= 0)
                    {
                        done = true;
                    }
                    else
                    {
                        output.Typewriter("Try again, no negatives");
                        clear();
                    }
                }
                catch
                {
                    
                    output.Typewriter("Try again... No letters");
                    clear();
                }

            }
            return d;
        }


    }
    public class output
    {
        public static void humber(string s)
        {   
                Console.WriteLine(s);
        }
        public static void repeater(string s, int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write(s);
        }
        public static void Typewriter(String s)
        {
            for (int i = 0; i < s.Length; i++)
            {

                Console.Write(s[i]);
                Thread.Sleep(15);
            }

        }
        public static void sTypeWriter(String s, int iq) 
        {
            for (int i = 0; i < s.Length; i++)
            {

                Console.Write(s[i]);
                Thread.Sleep(iq);
            }
        }
    }
}
 