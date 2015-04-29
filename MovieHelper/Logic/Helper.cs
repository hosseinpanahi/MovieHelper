using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MovieHelper.Properties;

namespace MovieHelper.Logic
{
    public class CoreHelper : ICoreHelper
    {
        public Encoding SaveEncoding { get; set; }

        public CoreHelper()
        {
            SaveEncoding =  Encoding.UTF8;
        }

        public void ChangeSubtitleEncoding(string directory, bool saveWithSameFileName, bool log = false)
        {
            var fullFileNames = GetSubtitleFiles(directory);

            foreach (var fullFileName in fullFileNames)
            {
                if (log)
                {
                    Console.WriteLine(fullFileName);
                }

                var file = ReadFile(fullFileName);

                if (saveWithSameFileName)
                {
                    SaveWithSameName(fullFileName, file);
                }
                else
                {
                    SaveWithNewName(fullFileName, file);
                }
            }
        }

        public void ChangeAllFilesName(string directory, string newName)
        {
            var files = GetNotHiddenFiles(directory);

            foreach (var filePath in files)
            {
                File.Move(filePath, filePath.Replace(Path.GetFileNameWithoutExtension(filePath), newName));
            }
        }

        public void PrintHelp()
        {
            Console.WriteLine(
                "-c [command] \n available command(s) : sub \n\n -w [work] \n availalbe work(s) : mv -n [new name] \n\n --log : logging");
        }

        private string ReadFile(string fullFileName)
        {
            string file;

            using (var reader =
                new StreamReader(fullFileName, Encoding.GetEncoding(Settings.Default.DefaultEncoding), true))
            {
                file = reader.ReadToEnd();
                reader.Close();
            }

            return file;
        }

        private void SaveWithSameName(string fullFileName, string file)
        {
            File.WriteAllText(fullFileName, file, SaveEncoding);
        }

        private void SaveWithNewName(string fullFileName, string file)
        {
            var index = 1;
            string fileName;

            do
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullFileName);

                if (fileNameWithoutExtension != null)
                {
                    fileName = fullFileName.Replace(fileNameWithoutExtension,
                        string.Format("{0} {1}", fileNameWithoutExtension, index++));
                }
                else
                {
                    throw new Exception("Bad File Name");
                }
            } while (File.Exists(fileName));

            File.WriteAllText(fileName, file, SaveEncoding);
        }

        private static IEnumerable<string> GetSubtitleFiles(string directory)
        {
            var files = Directory.GetFiles(directory);

            var wantedFiles =
                files.Where(
                    file =>
                        Settings.Default.TextSubtitleExtensions.Cast<string>()
                            .Any(textSubtitleExtension => Path.GetExtension(file) == textSubtitleExtension)).ToList();

            return wantedFiles;
        }

        private static IEnumerable<string> GetNotHiddenFiles(string directory)
        {
            var directoryInfo = new DirectoryInfo(directory);
            var files = directoryInfo.GetFiles();

            var filtered = files.Select(f => f)
                .Where(f => (f.Attributes & FileAttributes.Hidden) == 0).Select(s => s.FullName);

            return filtered;
        }
    }
}
