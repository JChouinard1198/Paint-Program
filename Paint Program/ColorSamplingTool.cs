﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    class ColorSamplingTool : ITool
    {

        private Graphics graphics;
        private int width, height;
        private SharedSettings settings;
        private bool bActive, bMouseDown, bInit;

        private Bitmap bLayer;

        public ColorSamplingTool()
        {

        }

        public void init(Graphics g, int w, int h, SharedSettings s)
        {
            graphics = g;
            width = w;
            height = h;
            settings = s;
            bActive = false;
            bInit = true;
            bMouseDown = false;
        }

        public Bitmap getCanvas()
        {
            //Not used
            return null;
        }

        public string getToolIconPath()
        {
            return @"..\..\Images\sampler.png";
        }

        public string getToolTip()
        {
            return "Pencil Tool";
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = true;
                Color c = bLayer.GetPixel(e.Location.X, e.Location.Y);

                if (e.Button == MouseButtons.Left) {
                    settings.setPrimaryBrushColor(c);
                    Console.WriteLine("Main Color Set to " + c.ToString());
                }
                else if(e.Button == MouseButtons.Right)
                {
                    settings.setSecondaryBrushColor(c);
                }

            }
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {

        }

        public void onMouseUp(object sender, MouseEventArgs e)
        {
            if (graphics != null)
            {
                bMouseDown = false;

            }
        }

        public bool isInitalized()
        {
            return bInit;
        }

        public Bitmap getToolLayer()
        {
            return null;
        }

        public bool requiresLayerData()
        {
            return true;
        }

        public void setLayerData(Bitmap bit)
        {
            bLayer = bit;
        }
    }
}
