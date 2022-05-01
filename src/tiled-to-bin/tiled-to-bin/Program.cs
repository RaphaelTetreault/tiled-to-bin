using CommandLine;
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

            string xml = File.ReadAllText(inputPath);
            var document = new XmlDocument();
            document.LoadXml(xml);

            var map = document.GetElementsByTagName("map")[0];
            Console.WriteLine(map.Name);
            Console.WriteLine(map.InnerText);

            var layers = document.GetElementsByTagName("layer");
            foreach (XmlNode layer in layers)
            {
                var dataNode = layer.FirstChild;
                var isDataNode = dataNode.Name == "data";
                if (!isDataNode)
                    throw new TiledParseException();

                var name = layer?.Attributes?.GetNamedItem("name")?.InnerText;
                var id_str = layer?.Attributes?.GetNamedItem("id")?.InnerText;
                var width_str = layer?.Attributes?.GetNamedItem("width")?.InnerText;
                var height_str = layer?.Attributes?.GetNamedItem("height")?.InnerText;
                var encoding_str = dataNode?.Attributes?.GetNamedItem("encoding")?.InnerText;

                var id = int.Parse(id_str);
                var width = int.Parse(width_str);
                var height = int.Parse(height_str);
                var encoding = Enum.Parse<TiledEncoding>(encoding_str, true);
                var tileIndexes = ParseIndices(encoding, dataNode?.InnerText);

                var tiledLayer = new TiledLayer()
                {
                    Name = name,
                    ID = id,
                    Width = width,
                    Height = height,
                    Encoding = encoding,
                    TileIndexes = tileIndexes,
                };

                Console.WriteLine(tiledLayer.Name);
                Console.WriteLine(tiledLayer.ID);
                Console.WriteLine(tiledLayer.Width);
                Console.WriteLine(tiledLayer.Height);
                Console.WriteLine(tiledLayer.TotalTiles);
                Console.WriteLine(tiledLayer.Encoding);
                Console.WriteLine(tiledLayer.TileIndexes.Length);
                Console.WriteLine();
            }
        }

        public static ushort[] ParseIndices(TiledEncoding encoding, string data)
        {
            switch (encoding)
            {
                case TiledEncoding.CSV: return ParseIndicesCSV(data);

                case TiledEncoding.Base64:
                case TiledEncoding.Base64_glib:
                case TiledEncoding.Base64_zlib:
                case TiledEncoding.XML:
                default:
                    throw new NotImplementedException();
            }
        }

        public static ushort[] ParseIndicesCSV(string data)
        {
            var cells = data.Split(',');
            var indexes = new ushort[cells.Length];
            for (int i = 0; i < indexes.Length; i++)
                indexes[i] = ushort.Parse(cells[i]);
            return indexes;
        }

    }
}