using System.Runtime.CompilerServices;

namespace HtmlGenerator
{
    class GlobalVariables
    {
        private static string rootPath = "";
        private static bool verboseDebug = false;

        public static string GetRootPath() { return rootPath; }
        public static void SetRootPath(string value)
        {
            if (value != null) { rootPath = value; }           
        }

        public static bool GetVerboseDebug() { return verboseDebug; }
        public static void SetVerboseDebug(bool value) { verboseDebug = value; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            InputErrorHandler();
            CLineFlagHandler();

            foreach (string f in Directory.GetFiles(GlobalVariables.GetRootPath(), "*.*", SearchOption.AllDirectories))
            {
                Console.WriteLine(f);
            }
        }

        /// <summary> Method <c>InputErrorHandler()</c> checks if user given a command line argument and if it was a directory. </summary>
        private static void InputErrorHandler()
        {
            try
            {
                if (!File.GetAttributes(Environment.GetCommandLineArgs()[1]).HasFlag(FileAttributes.Directory))
                {
                    Console.WriteLine("[Error] " + CurrentTime() + " Not a directory");
                    Environment.Exit(2);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("[Error] " + CurrentTime() + " Not a directory");
                Console.WriteLine();
                Environment.Exit(1);
            }


            GlobalVariables.SetRootPath(Environment.GetCommandLineArgs()[1]);
        }

        /// <summary> Method <c>CLineFlagHandler</c>  </summary>
        private static void CLineFlagHandler()
        {
            if (Environment.GetCommandLineArgs().Length > 2 && Environment.GetCommandLineArgs()[2] == "-v")
            {
                GlobalVariables.SetVerboseDebug(true);
                Console.WriteLine("[DEBUG] " + CurrentTime() + " Verbose debugging is turned on");
            }
        }

        /// <summary>Returns the current time in 'HH:mm:ss' format.</summary>
        /// <returns>The current time with string type.</returns>
        private static string CurrentTime() 
        { 
            return DateTime.Now.ToString("HH:mm:ss");
        }
    }
}