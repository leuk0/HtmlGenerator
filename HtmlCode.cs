using System.Text;

namespace HtmlProject
{
    class HtmlCode
    {
        public static void IndexHtmlFiles()
        {
            string indexPage, rootDirectory, trimmedSubDirectory, rootPageLink, subFolder, subFolderName, imageName;
            int slashCounter;

            Console.WriteLine($"Root Directory: {DirAccess.GetDirs()[0].GetPath()}");

            foreach (var lib in DirAccess.GetDirs())
            {
                indexPage = lib.GetPath() + "\\index.html";

                StringBuilder sb = new();
                sb.Append($"<!DOCTYPE html>\n<html lang=\"hu\">\n\t<head>\n\t\t<meta charset=\"utf-8\">\n\t</head>\n\t<body>\n\t\t<h1><a href=\"");
                
                rootDirectory = DirAccess.GetDirs()[0].GetPath();
                trimmedSubDirectory = indexPage[rootDirectory.Length..];
                
                slashCounter = 0;

                foreach (var c in trimmedSubDirectory)
                {
                    if (c == '\\') { slashCounter++; }
                }

                StringBuilder sb2 = new();

                for (int i = slashCounter; i > 0; i = i - 1)
                {
                    if (i > 1) { sb2.Append("../"); }
                    else { sb2.Append("."); }
                }

                sb2.Append("/index.html");
                rootPageLink = sb2.ToString();

                sb.AppendLine($"{rootPageLink}\">Root Directory</a></h1>");
                sb.AppendLine("\t\t<hr>\n\t\t<h1>Directories:</h1>\n\t\t<ul>");

                if (DirAccess.GetDirs().IndexOf(lib) != 0)
                {
                    sb.AppendLine("\t\t\t<li><a href=\"../index.html\"> << </a></li>");
                }

                for (int i = 0; i < lib.GetFolderNames().Count(); i++)
                {
                    subFolder = lib.GetFolderNames()[i];
                    subFolderName = subFolder[subFolder.LastIndexOf("\\")..].Replace("\\", "");
                    sb.AppendLine($"\t\t\t<li><a href=\"./{subFolderName}/index.html\">" + subFolderName.Replace("\\", "") + "</a></li>");
                }

                sb.AppendLine("\t\t</ul>\n\t\t<hr>\n\t\t<h1>Images:</h1>\n\t\t<ul>\n");

                foreach (var image in lib.GetImageNames())
                {
                    imageName = image[image.LastIndexOf("\\")..].Replace("\\", "");
                    sb.AppendLine($"\t\t\t<li><a href=\"./{imageName.Replace(imageName[imageName.LastIndexOf(".")..], ".html")}\">" + imageName + "</a></li>");
                }

                sb.AppendLine("\t\t</ul>\n\t</body>\n</html>");

                try
                {
                    using (StreamWriter sw = new(indexPage))
                    {
                        sw.WriteLine(sb.ToString());
                    }
                    ImageHtmlFiles(lib, rootPageLink);
                }
                catch (Exception)
                {
                    Console.WriteLine("[ERROR] Error in file creation. ");
                    break;
                }

                sb.Clear();
                sb2.Clear();
            }
        }

        public static void ImageHtmlFiles(DirAccess lib, string rootPageLink)
        {
            string file, fileExtension, htmlPath, fileName, nextFile, prevFile;
            int fileNameN;

            for (int i = 0; i < lib.GetImageNames().Count(); i++)
            {
                file = lib.GetImageNames()[i];
                fileExtension = file[file.LastIndexOf(".")..];
                htmlPath = file.Replace(fileExtension, ".html");

                StringBuilder sb = new();
                sb.AppendLine($"<!DOCTYPE html>\n<html lang=\"hu\">\n\t<head>\n\t\t<meta charset=\"utf-8\">\n\t</head>");
                sb.AppendLine($"\t<body>\n\t\t<h1><a href=\"{rootPageLink}\">Root Directory</a></h1>\n\t\t<hr>");
                sb.AppendLine($"\t\t<h3><a href=\"./index.html\">^^</a></h3>");

                fileNameN = file.LastIndexOf("\\") + 1;
                fileName = file[fileNameN..];

                if (i == 0)
                {
                    if (lib.GetImageNames().Count() == 1)
                    {
                        sb.Append($"\t\t<h3>{fileName}</h3>\n");
                        sb.Append($"\t\t<img src=\"./{fileName}\">\n</body>\n</html>");
                        continue;
                    }

                    nextFile = ChangeFileExtensionToHtml(lib.GetImageNames().Skip(1).First());
                    sb.AppendLine($"\t\t<h3>{fileName} <a href=\"./{nextFile}\"> >> </a></h3>");
                    sb.Append($"\t\t<a href=\"./{nextFile}\"><img src=\"./{fileName}\"  style=\"width: 15%; height: auto;\"></a>\n</body>\n</html>");
                }
                else if (i == lib.GetImageNames().Count() - 1)
                {
                    prevFile = ChangeFileExtensionToHtml(lib.GetImageNames()[i - 1]);
                    sb.AppendLine($"\t\t<h3><a href=\"./{prevFile}\"> << </a> {fileName}</h3>");
                    sb.Append($"\t\t<img src=\"./{fileName}\" style=\"width: 15%; height: auto;\">\n</body>\n</html>");
                }
                else
                {
                    nextFile = ChangeFileExtensionToHtml(lib.GetImageNames()[i + 1]);
                    prevFile = ChangeFileExtensionToHtml(lib.GetImageNames()[i - 1]);
                    sb.AppendLine($"\t\t<h3><a href=\"./{prevFile}\"> << </a> {fileName} <a href=\"./{nextFile}\"> >> </a></h3>");
                    sb.Append($"\t\t<a href=\"./{nextFile}\"><img src=\"./{fileName}\"  style=\"width: 15%; height: auto;\"></a>\n\t</body>\n</html>");
                }

                try
                {
                    using (StreamWriter sw = new(htmlPath))
                    {
                        sw.WriteLine(sb.ToString());
                        Console.WriteLine("Processed: " + htmlPath);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("[ERROR] Error in file creation.");
                    break;
                }

                sb.Clear();
            }
        }

        public static string ChangeFileExtensionToHtml(string eleres)
        {
            return Path.GetFileNameWithoutExtension(eleres) + ".html"; ;
        }
    }
}
