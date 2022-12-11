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

            using (FileStream fs = File.Create(path + ".html"))
            { 
                //  Write hmtl to file here
                //fs.Wri
            }

            /*foreach (var path in paths)
            {
                {
                    
                }
            }*/
        }
    }
}
