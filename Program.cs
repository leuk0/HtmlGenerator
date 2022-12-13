using static HtmlProject.DirAccess;

namespace HtmlProject
{
    public class DirAccess
    {
        private string path;
        private List<string> folderNames = new();
        private List<string> imageNames = new();

        public DirAccess(string path)
        {
            this.path = path;
            AddFolder();
            AddImage();
        }

        public string GetPath()
        {
            return this.path;
        }

        public List<string> GetFolderNames()
        {
            return this.folderNames;
        }
        public void AddFolderName(string folder)
        {
            this.folderNames.Add(folder);
        }

        public List<string> GetImageNames()
        {
            return this.imageNames;
        }
        public void AddImageName(string image)
        {
            this.imageNames.Add(image);
        }

        private void AddFolder()
        {
            List<string> folders = Directory.GetDirectories(GetPath()).ToList();
            foreach (var folder in folders)
            { 
                AddFolderName(folder);
            }
        }

        private void AddImage()
        {
            List<string> images = Directory.GetFiles(GetPath()).ToList();
            List<string> supportedFileExtensions = new() { ".png", ".jpg", ".jpeg" };

            foreach (var image in images)
            {
                if (supportedFileExtensions.Any(x => x.Equals(Path.GetExtension(image).ToLower())))
                {
                    AddImageName(image);
                }
            }
        }

        private static List<DirAccess> dirs = new();
        public static List<DirAccess> GetDirs()
        {
            return dirs;
        }
        private static void AddDir(DirAccess o)
        {
            dirs.Add(o);
        }

        public static void FolderStructure(string path)
        {
            DirAccess rootFolderObject = new(path);
            AddDir(rootFolderObject);

            if (rootFolderObject.GetFolderNames().Count() <= 0) { return; }

            foreach (var subFolder in rootFolderObject.GetFolderNames())
            {
                FolderStructure(subFolder);
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            InputErrorHandler();
            FolderStructure(args[0]);
            HtmlCode.IndexHtmlFiles();
        }

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
                Environment.Exit(1);
            }
        }
        private static string CurrentTime() 
        { 
            return DateTime.Now.ToString("HH:mm:ss");
        }
    }
}