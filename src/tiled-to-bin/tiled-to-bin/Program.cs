using CommandLine;
using Manifold.IO;
using Manifold.Tiled;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace TiledToBinary
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions);
        }

        public static void RunOptions(Options options)
        {
            string inputPath = Path.GetFullPath(options.Input);
            var isInvalidFile = !File.Exists(inputPath);
            if (isInvalidFile)
            {
                Console.WriteLine("Input file is invalid!", inputPath);
                Console.WriteLine(inputPath);
                return;
            }

            var tmx = TMX.FromFile(inputPath);
            Console.WriteLine("Success!");
        }
    }

}