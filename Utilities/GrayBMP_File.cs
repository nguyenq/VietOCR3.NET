/* Create by: Muhammad Chishty Asheque
 * Date: Friday, April 02, 2010
 * Contact: twinkle_rip@hotmail.com 
 */

using System;
using System.Drawing;
using System.IO;

static class GrayBMP_File
{
    static byte[] BMP_File_Header = new byte[14];
    static byte[] DIB_header = new byte[40];
    static byte[] Color_palette = new byte[1024]; //a palette containing 256 colors
    static byte[] Bitmap_Data = null;

    //creates byte array of 256 color grayscale palette
    static byte[] create_palette()
    {
        byte[] color_palette = new byte[1024];
        for (int i = 0; i < 256; i++)
        {
            color_palette[i * 4 + 0] = (byte)(i); //blue
            color_palette[i * 4 + 1] = (byte)(i); //green
            color_palette[i * 4 + 2] = (byte)(i); //red
            color_palette[i * 4 + 3] = (byte)0; //padding
        }
        return color_palette;
    }
    //create different part of a bitmap file
    static void create_parts(Image img)
    {
        //Create Bitmap Data
        Bitmap_Data = ConvertToGrayscale(img);
        //Create Bitmap File Header (populate BMP_File_Header array)
        Copy_to_Index(BMP_File_Header, new byte[] { (byte)'B', (byte)'M' }, 0); //magic number
        Copy_to_Index(BMP_File_Header, BitConverter.GetBytes(BMP_File_Header.Length
                        + DIB_header.Length + Color_palette.Length + Bitmap_Data.Length), 2); //file size
        Copy_to_Index(BMP_File_Header, new byte[] { (byte)'M', (byte)'C', (byte)'A', (byte)'T' }, 6); //reserved for application generating the bitmap file (not imprtant)
        Copy_to_Index(BMP_File_Header, BitConverter.GetBytes(BMP_File_Header.Length
                        + DIB_header.Length + Color_palette.Length), 10); //bitmap raw data offset
        //Create DIB Header (populate DIB_header array)
        Copy_to_Index(DIB_header, BitConverter.GetBytes(DIB_header.Length), 0); //DIB header length
        Copy_to_Index(DIB_header, BitConverter.GetBytes(((Bitmap)img).Width), 4); //image width
        Copy_to_Index(DIB_header, BitConverter.GetBytes(((Bitmap)img).Height), 8); //image height
        Copy_to_Index(DIB_header, new byte[] { (byte)1, (byte)0 }, 12); //color planes. N.B. Must be set to 1
        Copy_to_Index(DIB_header, new byte[] { (byte)8, (byte)0 }, 14); //bits per pixel
        Copy_to_Index(DIB_header, BitConverter.GetBytes(0), 16); //compression method N.B. BI_RGB = 0
        Copy_to_Index(DIB_header, BitConverter.GetBytes(Bitmap_Data.Length), 20); //lenght of raw bitmap data
        Copy_to_Index(DIB_header, BitConverter.GetBytes(1000), (int) img.HorizontalResolution); //horizontal resolution N.B. not important
        Copy_to_Index(DIB_header, BitConverter.GetBytes(1000), (int) img.VerticalResolution); //vertical resolution N.B. not important
        Copy_to_Index(DIB_header, BitConverter.GetBytes(256), 32); //number of colors in the palette
        Copy_to_Index(DIB_header, BitConverter.GetBytes(0), 36); //number of important colors used N.B. 0 = all colors are imprtant
        //Create Color palett
        Color_palette = create_palette();
    }

    /// <summary>
    /// Convert the color pixels of Source image into a grayscale bitmap (raw data).
    /// Improved version by David Carta.
    /// </summary>
    /// <param name="Source"></param>
    /// <returns></returns>
    static byte[] ConvertToGrayscale(Image Source)
    {
        Bitmap source = (Bitmap)Source;
        int padding = (source.Width % 4) != 0 ? 4 - (source.Width % 4) : 0; //determine padding needed for bitmap file
        byte[] bytes = new byte[source.Width * source.Height + padding * source.Height]; //create array to contain bitmap data with padding
        for (int y = 0; y < source.Height; y++)
        {
            int iTmp = (source.Height - 1 - y) * source.Width + (source.Height - 1 - y) * padding;
            for (int x = 0; x < source.Width; x++)
            {
                Color c = source.GetPixel(x, y);
                int g = Convert.ToInt32(0.3 * c.R + 0.59 * c.G + 0.11 * c.B); //grayscale shade corresponding to rgb
                bytes[iTmp + x] = (byte)g;
            }
            //add the padding
            iTmp = (source.Height - y) * source.Width + (source.Height - 1 - y) * padding;
            for (int i = 0; i < padding; i++)
            {
                bytes[iTmp + i] = (byte)0;
            }
        }
        return bytes;
    }

    //creates a grayscale bitmap file of Image specified by Path
    static public bool CreateGrayBitmapFile(Image Image, string Path)
    {
        try
        {
            create_parts(Image);
            //Write to file
            FileStream oFileStream;
            oFileStream = new FileStream(Path, System.IO.FileMode.OpenOrCreate);
            oFileStream.Write(BMP_File_Header, 0, BMP_File_Header.Length);
            oFileStream.Write(DIB_header, 0, DIB_header.Length);
            oFileStream.Write(Color_palette, 0, Color_palette.Length);
            oFileStream.Write(Bitmap_Data, 0, Bitmap_Data.Length);
            oFileStream.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }
    //returns a byte array of a grey scale bitmap image
    static public byte[] CreateGrayBitmapArray(Image image)
    {
        try
        {
            create_parts(image);
            //Create the array
            byte[] bitmap_array = new byte[BMP_File_Header.Length + DIB_header.Length
                                            + Color_palette.Length + Bitmap_Data.Length];
            Copy_to_Index(bitmap_array, BMP_File_Header, 0);
            Copy_to_Index(bitmap_array, DIB_header, BMP_File_Header.Length);
            Copy_to_Index(bitmap_array, Color_palette, BMP_File_Header.Length + DIB_header.Length);
            Copy_to_Index(bitmap_array, Bitmap_Data, BMP_File_Header.Length + DIB_header.Length + Color_palette.Length);

            return bitmap_array;
        }
        catch
        {
            return new byte[1]; //return a null single byte array if fails
        }
    }
    //adds dtata of Source array to Destinition array at the Index
    static bool Copy_to_Index(byte[] destination, byte[] source, int index)
    {
        try
        {
            for (int i = 0; i < source.Length; i++)
            {
                destination[i + index] = source[i];
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}