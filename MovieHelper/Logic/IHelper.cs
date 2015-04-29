using System.Text;

namespace MovieHelper.Logic
{
    public interface ICoreHelper
    {
        Encoding SaveEncoding { get; set; }
        void ChangeSubtitleEncoding(string directory, bool saveWithSameFileName, bool log = false);
        void ChangeAllFilesName(string directory, string newName);

        void PrintHelp();
    }
}