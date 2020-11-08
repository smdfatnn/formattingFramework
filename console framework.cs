using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Configuration;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Specialized;

namespace framework.text
{
    public static class suffixes
    {
        public static string main_suffix = " | a suffix smd"; 
        public static string format_helper = " ";
    }

    public static class prefixes
    {
        public static string main_prefix = "~a framework - "; 
    }

    public static class console
    {
        public static void write(string text, [Optional, DefaultParameterValue(true)] bool wrap, [Optional, DefaultParameterValue(false)] bool include_prefix, [Optional, DefaultParameterValue(false)] bool include_suffix, [Optional, DefaultParameterValue(0)] int color_code)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException(framework.logs.error_codes.null_string, paramName: nameof(text));

            switch (color_code)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.White; break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red; break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Black; break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.DarkRed; break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case 9:
                    Console.ForegroundColor = ConsoleColor.Gray; break;
                case 10:
                    Console.ForegroundColor = ConsoleColor.Blue; break;
                case 11:
                    Console.ForegroundColor = ConsoleColor.Green; break;
                case 12:
                    Console.ForegroundColor = ConsoleColor.Cyan; break;
                case 13:
                    Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 14:
                    Console.ForegroundColor = ConsoleColor.Yellow; break;
                default:
                    throw new Exception(logs.error_codes.incorrect_color_code);
            }

            if (!wrap)
            {
                if (include_prefix)
                {
                    if (include_suffix)
                        Console.Write(prefixes.main_prefix + text + suffixes.main_suffix + suffixes.format_helper);
                    else
                        Console.Write(prefixes.main_prefix + text + suffixes.format_helper);
                }
                else
                {
                    if (include_suffix)
                        Console.Write(text + suffixes.main_suffix + suffixes.format_helper);
                    else
                        Console.Write(text + suffixes.format_helper);
                }
            }
            else
            {
                if (include_prefix)
                {
                    if (include_suffix)
                        Console.WriteLine(prefixes.main_prefix + text + suffixes.main_suffix + suffixes.format_helper);
                    else
                        Console.WriteLine(prefixes.main_prefix + text + suffixes.format_helper);
                }
                else
                {
                    if (include_suffix)
                        Console.WriteLine(text + suffixes.main_suffix + suffixes.format_helper);
                    else
                        Console.WriteLine(text + suffixes.format_helper);
                }
            }
        }

        public static int return_i(string text)
        {
            return Convert.ToInt32(text);
        }

        public static bool logicalParser_s(string text, string positive, string negative)
        {
            if (text == positive)
                return true;
            else
                return false;
        }

        public static bool logicalParser_i(string text, int positive, int negative)
        {
            if (return_i(text) == positive)
                return true;
            else
                return false;
        }


        public static string key_to_str(ConsoleKey key)
        {
            return key.ToString();
        }

        public static void read(int module, [Optional, DefaultParameterValue(false)] bool clipboard)
        {
            switch (module)
            {
                case 0:
                    {
                        Console.ReadKey();

                        if (clipboard)
                            Clipboard.SetText(Console.ReadKey().ToString());   
                    }
                    break;
                case 1:
                    {
                        Console.ReadLine();

                        if (clipboard)
                            Clipboard.SetText(Console.ReadLine().ToString());
                    }
                    break;
                default:
                    throw new Exception(logs.error_codes.incorrect_module);
            }
        }

        public static int return_random_index(int length)
        {
            Random rd = new Random();
            return rd.Next(1, length);
        }

        public static char random_char_from_string(string input)
        {
            return input.ToString()[return_random_index(input.Length)];
        }

        public static string tolowercase(string input, [Optional, DefaultParameterValue(-1)] int exception_index)
        {
            string returned = string.Empty;
            if(exception_index != -1)
            {
                char split_char = input[exception_index];

                string separated_fir = input.Split(split_char).First();
                string separated_lst = input.Split(split_char).Last();

                returned = separated_fir.ToLower() + split_char.ToString() + separated_lst.ToLower();
            }
            else
            {
                returned = input.ToLower();
            }

            return returned;
        }

        public static string touppercase(string input, [Optional, DefaultParameterValue(0)] int exception_index)
        {
            string returned = string.Empty;
            if (exception_index != 0)
            {
                char split_char = input[exception_index];

                string separated_fir = input.Split(split_char).First();
                string separated_lst = input.Split(split_char).Last();

                returned = separated_fir.ToUpper() + split_char.ToString() + separated_lst.ToUpper();
            }
            else
            {
                returned = input.ToUpper();
            }

            return returned;
        }

        public static void clear([Optional, DefaultParameterValue(0)] int delay)
        {
            Task.Delay(delay);
            Console.Clear();
        }

        public static void split_text(string text, char split_by, [Optional, DefaultParameterValue(false)] bool print, [Optional, DefaultParameterValue(false)] bool wrap, [Optional, DefaultParameterValue(false)] bool replace_with_space)
        {
            if (!replace_with_space)
                text.Split(split_by);
            else
                text.Replace(split_by, ' ');

            if (print)
            {
                if (!wrap)
                {
                    if (!replace_with_space)
                        console.write(String.Concat(text.Split(split_by)), false);
                    else
                        console.write(text.Replace(split_by, ' '), false);
                }
                else
                {
                    if(!replace_with_space)
                        console.write(String.Concat(text.Split(split_by)), true);
                    else
                        console.write(text.Replace(split_by, ' '), true);
                }
            }
        }

    }
}

namespace framework.logs
{
    public static class error_codes
    {
        public static string incorrect_module = "incorrect module";

        public static string null_string = "null string";

        public static string argument_null_string = "null argument string";

        public static string incorrect_color_code = "incorrect color code";

        public static string incorrect_string_format = "incorrect string format";
    }
  
}
