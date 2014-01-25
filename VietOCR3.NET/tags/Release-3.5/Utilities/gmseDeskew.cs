/**
 * (c) GMSE GmbH 2006
 * Algorithm to deskew an image.
 * http://www.codeproject.com/KB/graphics/Deskew_an_Image.aspx
 * C# translation: http://mdb-blog.blogspot.com/2010/10/c-how-to-deskew-image.html
 */

using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Diagnostics;

namespace VietOCR.NET.Utilities
{
    public class gmseDeskew
    {
        // Representation of a line in the image.
        public class HougLine
        {
            // Count of points in the line.
            public int Count;
            // Index in Matrix.
            public int Index;
            // The line is represented as all x,y that solve y*cos(alpha)-x*sin(alpha)=d
            public double Alpha;
            public double d;
        }

        // The Bitmap
        Bitmap cBmp;
        // The range of angles to search for lines
        double cAlphaStart = -20;
        double cAlphaStep = 0.2;
        int cSteps = 40 * 5;
        // Precalculation of sin and cos.
        double[] cSinA;
        double[] cCosA;
        // Range of d
        double cDMin;
        double cDStep = 1;
        int cDCount;
        // Count of points that fit in a line.
        int[] cHMatrix;

        public gmseDeskew(Bitmap bmp)
        {
            cBmp = bmp;
        }

        // Calculate the skew angle of the image cBmp.
        public double GetSkewAngle()
        {
            HougLine[] hl;
            double sum = 0;
            int count = 0;

            // Hough Transformation
            Calc();
            // Top 20 of the detected lines in the image.
            hl = GetTop(20);

            // Average angle of the lines
            for (int i = 0; i < 19; i++)
            {
                sum += hl[i].Alpha; 
                count++;
            }
            return sum / count;
        }

        // Calculate the Count lines in the image with most points.    
        private HougLine[] GetTop(int Count)
        {
            HougLine[] hl = new HougLine[Count];
            for (int i = 0; i < Count; i++)
            {
                hl[i] = new HougLine();
            }

            HougLine tmp;

            for (int i = 0; i < cHMatrix.Length - 1; i++)
            {
                if (cHMatrix[i] > hl[Count - 1].Count)
                {
                    hl[Count - 1].Count = cHMatrix[i];
                    hl[Count - 1].Index = i;
                    int j = Count - 1;
                    while (j > 0 && hl[j].Count > hl[j - 1].Count)
                    {
                        tmp = hl[j];
                        hl[j] = hl[j - 1];
                        hl[j - 1] = tmp;
                        j--;
                    }
                }
            }

            int AlphaIndex, dIndex;

            for (int i = 0; i < Count; i++)
            {
                dIndex = hl[i].Index / cSteps;
                AlphaIndex = hl[i].Index - dIndex * cSteps;
                hl[i].Alpha = GetAlpha(AlphaIndex);
                hl[i].d = dIndex + cDMin;
            }
            return hl;
        }

        // Hough Transforamtion:   
        private void Calc()
        {
            int hMin = cBmp.Height / 4;
            int hMax = cBmp.Height * 3 / 4;
            Init();

            for (int y = hMin; y < hMax; y++)
            {
                for (int x = 1; x < cBmp.Width - 2; x++)
                {
                    // Only lower edges are considered.           
                    if (IsBlack(x, y))
                    {
                        if (!IsBlack(x, y + 1))
                        {
                            Calc(x, y);
                        }
                    }
                }
            }
        }

        // Calculate all lines through the point (x,y). 
        private void Calc(int x, int y)
        {
            double d; 
            int dIndex; 
            int index;

            for (int alpha = 0; alpha < cSteps - 1; alpha++)
            {
                d = y * cCosA[alpha] - x * cSinA[alpha];
                dIndex = (int)CalcDIndex(d);
                index = dIndex * cSteps + alpha;
                try
                {
                    cHMatrix[index] += 1;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        private double CalcDIndex(double d)
        {
            return Convert.ToInt32(d - cDMin);
        }

        private bool IsBlack(int x, int y)
        {
            Color c = cBmp.GetPixel(x, y);
            double luminance = (c.R * 0.299) + (c.G * 0.587) + (c.B * 0.114);
            return luminance < 140;
        }

        private void Init()
        {
            double angle;
            // Precalculation of sin and cos.       
            cSinA = new double[cSteps - 1];
            cCosA = new double[cSteps - 1];
            for (int i = 0; i < cSteps - 1; i++)
            {
                angle = GetAlpha(i) * Math.PI / 180.0;
                cSinA[i] = Math.Sin(angle);
                cCosA[i] = Math.Cos(angle);
            }
            // Range of d:       
            cDMin = -cBmp.Width;
            cDCount = (int)(2 * (cBmp.Width + cBmp.Height) / cDStep);
            cHMatrix = new int[cDCount * cSteps];
        }

        public double GetAlpha(int Index)
        {
            return cAlphaStart + Index * cAlphaStep;
        }

        public static Bitmap RotateImage(Bitmap bmp, double angle)
        {
            Graphics g;
            Bitmap tmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);
            tmp.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
            g = Graphics.FromImage(tmp);
            try
            {
                g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
                g.RotateTransform((float)angle);
                g.DrawImage(bmp, 0, 0);
            }
            finally
            {
                g.Dispose();
            }
            return tmp;
        }
    }
}