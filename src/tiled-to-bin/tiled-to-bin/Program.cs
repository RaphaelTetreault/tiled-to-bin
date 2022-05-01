using CommandLine;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace tiled_to_bin
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

            string xml = File.ReadAllText(inputPath);
            var document = new XmlDocument();
            document.LoadXml(xml);

            var map = document.GetElementsByTagName("map")[0];
            Console.WriteLine(map.Name);
            Console.WriteLine(map.InnerText);

            var layers = document.GetElementsByTagName("layer");
            foreach (XmlNode layer in layers)
            {
                var name = layer.Attributes.GetNamedItem("name").InnerText;
                Console.WriteLine(name);
                Console.WriteLine(layer.InnerText);
            }
        }

    }
}