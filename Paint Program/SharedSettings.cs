﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_Program
{
    public class SharedSettings
    {
        public static Color cPrimaryBrushColor { get; set; }

        public static Color cSecondaryBrushColor { get; set; }

        public static float fBrushSize { get; set; }

        public static float fScale { get; set; }

        public static int iBrushHardness { get; set; }

        public static int iTabletPressure { get; set; }

        public static int iMaxTabletPressure { get; set; }

        public static int iMaxTabletWidth { get; set; }

        public static int iMinTabletWidth { get; set; }

        public static int iCanvasWidth { get; set; }

        public static int iCanvasHeight { get; set; }

        public static bool bLoadFromSettings { get; set; }

        public static Bitmap bitmapCanvas { get; set; }

        public static Bitmap bitmapCurrentLayer { get; set; }

        public static Bitmap bitmapImportImage { get; set; }

        public static Bitmap[] Layers { get; set; }

        public static String[] LayerNames { get; set; }

        public SharedSettings()
        {
            cPrimaryBrushColor = Color.Black;

            cSecondaryBrushColor = Color.White;

            fBrushSize = 1.0F;

            iBrushHardness = 255;

            //No Tablet Input
            iTabletPressure = -1;

            iMaxTabletWidth = 25;

            iMinTabletWidth = 1;

            //standard max pressure
            iMaxTabletPressure = 1023;
        }


        public void setPrimaryBrushColor(Color c)
        {
            cPrimaryBrushColor = c;
        }

        public void setSecondaryBrushColor(Color c)
        {
            cSecondaryBrushColor = c;
        }

        public void setBrushSize(float f)
        {
            fBrushSize = f;
        }

        public void setBrushHardness(int f)
        {
            iBrushHardness = f;
        }

        public void setTabletPressure(int p)
        {
            iTabletPressure = p;
        }

        public void setMaxTabletPressure(int p)
        {
            iMaxTabletPressure = p;
        }

        public void setMaxTabletWidth(int w)
        {
            iMaxTabletWidth = w;
        }

        public void setMinTabletWidth(int w)
        {
            iMinTabletWidth = w;
        }

        public void setCanvasWidth(int w)
        {
            iCanvasWidth = w;
        }

        public void setCanvasHeight(int h)
        {
            iCanvasHeight = h;
        }

        public void setLoadFromSettings(bool b)
        {
            bLoadFromSettings = b;
        }

        public void setBitmapCanvas(Bitmap b)
        {
            bitmapCanvas = b;
        }

        public void setImportImage(Bitmap b)
        {
            bitmapImportImage = b;
        }

        public void setBitmapCurrentLayer(Bitmap b)
        {
            bitmapCurrentLayer = b;
        }

        public void setLayerBitmaps(Bitmap[] bitArr)
        {
            Layers = new Bitmap[bitArr.Length];
            for(int n = 0; n < bitArr.Length; n++)
            {
                Bitmap temp = (Bitmap)bitArr[n].Clone();
                Layers[n] = temp;
            }
        }

        public void setLayerNames(String[] names)
        {
            LayerNames = names;
        }

        public void setDrawScale(float s)
        {
            fScale = s;
        }


        public Color getPrimaryBrushColor()
        {
            return cPrimaryBrushColor;
        }

        public Color getSecondaryBrushColor()
        {
            return cSecondaryBrushColor;
        }

        public float getBrushSize()
        {
            return fBrushSize;
        }

        public int getBrushHardness()
        {
            return iBrushHardness;
        }

        public int getTabletPressure()
        {
            return iTabletPressure;
        }

        public int getMaxTabletPressure()
        {
            return iMaxTabletPressure;
        }

        public int getMaxTabletWidth()
        {
            return iMaxTabletWidth;
        }

        public int getMinTabletWidth()
        {
            return iMinTabletWidth;
        }

        public int getCanvasWidth()
        {
            return iCanvasWidth;
        }

        public int getCanvasHeight()
        {
            return iCanvasHeight;
        }

        public bool getLoadFromSettings()
        {
            return bLoadFromSettings;
        }

        public Bitmap getBitmapCanvas()
        {
            return bitmapCanvas;
        }

        public Bitmap getImportImage()
        {
            Bitmap tmp = bitmapImportImage;
            bitmapImportImage = null;
            return tmp;
        }

        public Bitmap getBitmapCurrentLayer(bool source)
        {
            if (source)
            {
                return bitmapCurrentLayer;
            }
            else
            {
                return(Bitmap) bitmapCurrentLayer.Clone();
            }
        }

        public Bitmap[] getLayerBitmaps()
        {
            return Layers;
        }

        public String[] getLayerNames()
        {
            return LayerNames;
        }

        public float getDrawScale()
        {
            return fScale;
        }

        public static int MapValue(
    int originalStart, int originalEnd, // original range
    int newStart, int newEnd, // desired range
    int value) // value to convert
        {
            double scale = (double)(newEnd - newStart) / (originalEnd - originalStart);
            return (int)(newStart + ((value - originalStart) * scale));
        }
    }
}
