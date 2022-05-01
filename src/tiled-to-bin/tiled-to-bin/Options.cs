using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tiled_to_bin
{
    public class Options
    {
        private static class Help
        {
            public const string Input =
                "The input Tiled XML file";

            public const string SizeInt =
                "How many bytes integers are serialized with.";
        }

        [Option('i', "input", Required = true, HelpText = Help.Input)]
        public string Input { get; set; } = string.Empty;

        [Option('s', "sizeInt", Min = 1, HelpText = Help.Input)]
        public byte SizeInt { get; set; } = 1;
    }
}
