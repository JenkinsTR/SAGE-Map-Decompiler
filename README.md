# SAGE Map Decompiler
 Experimental .map decompiler for SAGE engine games

 This program expects the map file to be passed as a command line argument.

 It reads the file, checks the header to make sure it is a valid map file, and then reads the compressed data and decompressed size from the file.

 It then calls the Decompress function to decompress the data using SharpZipLib, and finally saves the decompressed data to a text file.
 
 Usage: `SAGEMapDecompiler.exe <map_file>`
 