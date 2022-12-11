using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using static HtmlProject.GlobalVariables;

namespace HtmlProject
{
    class GlobalVariables
    {
        private static string rootPath = "";
        private static bool verboseDebug = false;
        private static List<string> fileExtesions = new List<string>();

        public static List<string> GetFileExtensions() { return fileExtesions; }
        public static void AddFileExtension(string ext)
        {
            if (ext.Contains('.'))
            {
                fileExtesions.Add('.' + ext.Trim().ToLower());
            }
            
            fileExtesions.Add(ext.Trim().ToLower());
        }
        public static void AddFileExtension(List<string> extList)
        {
            if (extList.All(x => x.Contains('.')))
            { 
                fileExtesions.AddRange(extList.ConvertAll(x => "." + x.ToLower()).ToList());
            }

            fileExtesions.AddRange(extList.ConvertAll(x => x.ToLower()).ToList());
        }

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
            AddFileExtension(new List<string> { ".jpg", ".jpeg", ".png" });

            HtmlCode.GenerateHtmlForImage(GetImagePaths());

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


            SetRootPath(Environment.GetCommandLineArgs()[1]);
        }

        /// <summary> Method <c>CLineFlagHandler</c> checks for flags that given by the user with the second command line argument. </summary>
        private static void CLineFlagHandler()
        {
            if (Environment.GetCommandLineArgs().Length > 2 && Environment.GetCommandLineArgs()[2] == "-v")
            {
                SetVerboseDebug(true);
                Console.WriteLine("[DEBUG] " + CurrentTime() + " Verbose debugging is turned on");
            }
        }

        /// <summary>Returns the current time in 'HH:mm:ss' format. </summary>
        /// <returns>The current time with string type. </returns>
        private static string CurrentTime() 
        { 
            return DateTime.Now.ToString("HH:mm:ss");
        }
        /// <summary> Returns list which is containing the paths to the image files. </summary>
        /// <returns> A list which is containing the paths to the image files. </returns>
        private static List<string> GetImagePaths()
        {
            List<string> imageOnlyFilePaths = new();

            foreach (var path in Directory.GetFiles(GetRootPath(), "*.*", SearchOption.AllDirectories).ToList())
            {
                //if (GetFileExtensions().Any(x => path.ToLower().EndsWith(x)))
                if (GetFileExtensions().Any(x => x.Equals(Path.GetExtension(path).ToLower())))
                {
                    imageOnlyFilePaths.Add(path);
                }
            }

            return imageOnlyFilePaths;
        }
    }
}