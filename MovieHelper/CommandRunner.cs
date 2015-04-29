using System;
using System.Text;
using MovieHelper.Logic;

namespace MovieHelper
{
    class CommandRunner
    {
        public static void RunCommands(ArgumentsParser arguments, ICoreHelper coreHelper, string directory)
        {
            if (!string.IsNullOrEmpty(arguments["help"]))
            {
                coreHelper.PrintHelp();
                return;
            }

            switch (arguments["c"])
            {
                case "sub":
                    if (!string.IsNullOrEmpty(arguments["enc"]))
                    {
                        coreHelper.SaveEncoding = Encoding.GetEncoding(arguments["enc"]);
                    }
                    if (Convert.ToBoolean(arguments["log"]))
                    {
                        coreHelper.ChangeSubtitleEncoding(directory, !Convert.ToBoolean(arguments["nf"]), true);
                    }
                    coreHelper.ChangeSubtitleEncoding(directory, !Convert.ToBoolean(arguments["nf"]));
                    break;
            }

            switch (arguments["w"])
            {
                case "mv":
                    if (!string.IsNullOrEmpty(arguments["n"]))
                    {
                        coreHelper.ChangeAllFilesName(directory, arguments["n"]);
                    }
                    break;
            }
        }
    }
}