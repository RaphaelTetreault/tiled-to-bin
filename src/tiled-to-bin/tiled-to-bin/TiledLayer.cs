namespace tiled_to_bin
{
    public class TiledLayer
    {
        public string Name { get; set; } = string.Empty;
        public int ID { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public TiledEncoding Encoding { get; set; }
        public ushort[] TileIndexes { get; set; } = new ushort[0];

        public int TotalTiles => Width * Height;
    }
}
