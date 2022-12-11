using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlProject
{
    class HtmlCode
    {
        public static bool GenerateHtmlForImage(List<string> imageFilePaths)
        {
            CreateHtmlFiles(FilePathExtensionTrimmer(imageFilePaths));

            return false;
        }

        private static List<string> FilePathExtensionTrimmer(List<string> untrimmedImageFilePaths)
        {
            return untrimmedImageFilePaths.Select(x => x.Replace(Path.GetExtension(x), "")).ToList();
        }

        private static void CreateHtmlFiles(List<string> paths)
        {

            string path = paths.Last();
            Console.WriteLine(path);

            string fileName = path.Split('/')[^1];

            using (StreamWriter sw = new StreamWriter(path))
            {
                //  Write hmtl to file here
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");
                sw.WriteLine("<title>" + fileName + "</title>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");
                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
            }

            /*foreach (var path in paths)
            {
                {
                    
                }
            }*/
        }
    }
}
