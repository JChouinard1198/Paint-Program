﻿using System.Drawing;
using System.Windows.Forms;

namespace Paint_Program
{
    class MoveTool:ITool
    {
        private Graphics graphics;
        private int width, height;
        private bool bInit;

        private Point pOld, pNew;
        

        public MoveTool()
        {

        }

        public void init()
        {
            graphics = SharedSettings.getActiveGraphics();
            width = SharedSettings.getCanvasWidth();
            height = SharedSettings.getCanvasHeight();
            bInit = true;
        }

        public string getToolIconPath()
        {
            return @"..\..\Images\move.png";
        }

        public void onMouseDown(object sender, MouseEventArgs e)
        {
            pOld = e.Location;
        }

        public void onMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pNew = e.Location;
                Point p = SharedSettings.pSelectionPoint;
                SharedSettings.pSelectionPoint = new Point(p.X + (pNew.X - pOld.X), p.Y + (pNew.Y - pOld.Y));
            }
        }

        public void onMouseUp(object sender, MouseEventArgs e)
        {
            pOld.X = 0;
            pOld.Y = 0;
            pNew.X = 0;
            pNew.Y = 0;
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
            return false;
        }

        public void setLayerData(Bitmap bit)
        {
        }

        public string getToolTip()
        {
            return SharedSettings.getGlobalString("tooltip_move");
        }

        public void updateInterfaceLayer()
        {
        }
    }
}
