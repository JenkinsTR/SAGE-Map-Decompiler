using System;
using System.IO;
using ICSharpCode.SharpZipLib.BZip2;

namespace SAGEMapDecompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check if a file was specified
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: SAGEMapDecompiler.exe <map_file>");
                return;
            }

            // Open the file
            string mapFile = args[0];
            if (!File.Exists(mapFile))
            {
                Console.WriteLine("Error: File not found: " + mapFile);
                return;
            }
            FileStream fs = new FileStream(mapFile, FileMode.Open, FileAccess.Read);

            // Read the file header
            BinaryReader reader = new BinaryReader(fs);
            string header = new string(reader.ReadChars(4));
            if (header != "CNC3")
            {
                Console.WriteLine("Error: Invalid map file header: " + header);
                reader.Close();
                fs.Close();
                return;
            }

            // Read the map data
            int decompressedSize = reader.ReadInt32();
            int compressedSize = reader.ReadInt32();
            byte[] compressedData = reader.ReadBytes(compressedSize);
            reader.Close();
            fs.Close();

            // Decompress the map data
            byte[] decompressedData = Decompress(compressedData, decompressedSize);

            // Save the decompressed data to a file
            string outputFile = Path.ChangeExtension(mapFile, ".txt");
            File.WriteAllBytes(outputFile, decompressedData);
            Console.WriteLine("Map file successfully decompiled: " + outputFile);
        }

        static byte[] Decompress(byte[] data, int decompressedSize)
        {
            using (MemoryStream inputStream = new MemoryStream(data))
            using (MemoryStream outputStream = new MemoryStream(decompressedSize))
            {
                BZip2.Decompress(inputStream, outputStream, false);
                return outputStream.ToArray();
            }
        }
    }
}
