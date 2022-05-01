using CommandLine;
using Manifold.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledToBinary
{
    public class Options
    {
        private static class Help
        {
            public const string Input =
                "The input Tiled XML file";

            public const string SizeInt =
                "How many bytes integers are serialized with.";

            public const string EndiannessStr =
                "How many bytes integers are serialized with.";
        }

        [Option('i', "input", Required = true, HelpText = Help.Input)]
        public string Input { get; set; } = string.Empty;

        [Option('s', "sizeInt", Min = 1, Max = 4, HelpText = Help.SizeInt)]
        public byte SizeInt { get; set; } = 1;

        [Option('e', "Endianness", HelpText = Help.EndiannessStr)]
        public string EndiannessStr{ get; set; } = "be";


        public Endianness Endianness => EndiannessStrToEndianness(EndiannessStr);


        private Endianness EndiannessStrToEndianness(string EndiannessStr)
        {
            var endiannessArg = EndiannessStr.ToLower();

            /**/ if (endiannessArg == "be")
                return Endianness.BigEndian;
            else if (endiannessArg == "le")
                return Endianness.LittleEndian;
            else 
                throw new ArgumentException();
        }
    }
}
