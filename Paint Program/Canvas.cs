﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WintabDN;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Paint_Program
{
    public partial class Canvas : UserControl
    {
        private Display p;
        private Panel pScaled;

        private Graphics g;

        private Bitmap bg;

        private Bitmap Grid;

        private const int GCperFrames = 100;
        private int GCCurrentFrame = 0;

        private int canvasWidth, canvasHeight;

        //Width and height of Parrent
        private int maxWidth, maxHeight;

        //width and height of the vertical and horizontal scroll bars respectivly
        private int scrollWidth, scrollHeight;

        private int tsWidth, menuHeight;

        //Index of the currently active tool
        private int iActiveTool;

        LayerView lv;
        ToolStrip ts;
        BrushSettings bs;
        ZoomControl zc;

        TabletInfo ti;

        private List<ITool> Tools;
        public List<ToolStripButton> ToolButtons;
        private bool ToolsShown = true;

        SharedSettings ss;

        public Canvas(int pw, int ph, SharedSettings settings)
        {
            InitializeComponent();

            ss = settings;

            canvasWidth = settings.getCanvasWidth();
            canvasHeight = settings.getCanvasHeight();
            maxWidth = pw;
            maxHeight = ph;

            this.Width = canvasWidth;
            this.Height = canvasHeight;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

        }

        public Canvas(int w, int h, int pw, int ph)
        {
            InitializeComponent();
            
            ss = new SharedSettings();

            canvasWidth = w;
            canvasHeight = h;
            
            maxWidth = pw;
            maxHeight = ph;

            ss.setCanvasWidth(canvasWidth);
            ss.setCanvasHeight(canvasHeight);

            this.Width = canvasWidth;
            this.Height = canvasHeight;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        public void initCanvas()
        {
            Tools = new List<ITool>();
            ToolButtons = new List<ToolStripButton>();

            try
            {
                ti = new TabletInfo(HandleTabletData);
                ss.setTabletConnected(true);
            }
            catch (Exception e)
            {
                ss.setTabletConnected(false);
                Console.WriteLine(e.InnerException);
            }

            tsWidth = 50;
            menuHeight = 25;

            scrollWidth = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            scrollHeight = System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight;

            lv = new LayerView(canvasWidth, canvasHeight, ss);
            lv.Location = new Point(maxWidth - (lv.Width + scrollWidth), maxHeight - (lv.Height + scrollHeight));

            this.Location = new Point((maxWidth / 2) - (this.Width / 2), (maxHeight / 2) - (this.Height / 2));
            this.Parent.Controls.Add(lv);

            ts = new ToolStrip();
            ts.Dock = DockStyle.None;
            ts.Location = new Point(0, menuHeight * 2);
            ts.AutoSize = false;
            ts.Height = maxHeight - menuHeight;
            ts.Width = tsWidth;
            ts.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            ts.ShowItemToolTips = true;

            this.Parent.Controls.Add(ts);
            this.Parent.Resize += handleParentResize;

            bs = new BrushSettings(ss);
            bs.Location = new Point(maxWidth - bs.Width, menuHeight * 2);
            this.Parent.Controls.Add(bs);

            zc = new ZoomControl(ss);
            zc.Location = new Point(tsWidth, maxHeight - SystemInformation.CaptionHeight - menuHeight- zc.Height);
            this.Parent.Controls.Add(zc);

            p = new Display();
            p.Size = new Size(canvasWidth, canvasHeight);
            p.Location = new Point(0, menuHeight);
            p.MouseDown += handleMouseDown;
            p.MouseUp += handleMouseUp;
            p.MouseMove += handleMouseMove;
            p.Paint += EDisplayPaint;

            pScaled = new Panel();
            pScaled.Size = new Size( (lv.Location.X - this.Location.X) - ts.Width - 30 ,(zc.Location.Y - this.Location.Y) - 165 );
            pScaled.MinimumSize = new Size(300, 300);
            pScaled.Location = new Point(0, 0);
            pScaled.BackColor = Color.FromArgb(64,64,64);
            pScaled.AutoScroll = true;
            
            this.Location = new Point(ts.Width + 15, SystemInformation.CaptionHeight + 15);
            pScaled.Controls.Add(p);

            bg = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format24bppRgb);
            Grid = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format24bppRgb);

            try
            {
                Bitmap bgTile = (Bitmap)Bitmap.FromFile(@"..\..\Images\transparent_texture.jpg");
                using (TextureBrush brush = new TextureBrush(bgTile, WrapMode.Tile))
                {
                    using (Graphics g = Graphics.FromImage(bg))
                    {
                        g.FillRectangle(brush, 0, 0, bg.Width, bg.Height);
                    }
                }
            }
            catch (Exception e)
            {
                Graphics.FromImage(bg).Clear(Color.White);
                p.BackColor = Color.White;
            }

            g = p.CreateGraphics();
            

            this.Controls.Add(pScaled);
            this.SendToBack();

            initTools();

            pScaled.Size = new Size((lv.Location.X - this.Location.X) - 15, (zc.Location.Y - this.Location.Y) - 15);
            this.Refresh();
        }

        private void initTools()
        {

            Tools.Add(new PencilTool());
            Tools.Add(new BrushTool());
            Tools.Add(new ColorSamplingTool());
            Tools.Add(new ErasoirTool());
            Tools.Add(new PaintBucketTool());
            Tools.Add(new SelectionTool());
            Tools.Add(new TextTool());

            foreach (ITool tool in Tools)
            {
                ToolStripButton temp = new ToolStripButton(Image.FromFile(tool.getToolIconPath()));
                temp.AutoSize = false;
                temp.Width = tsWidth;
                temp.Height = temp.Width;
                temp.Click += handleToolStripItemClick;
                ToolButtons.Add(temp);
                ts.Items.Add(temp);
            }


            /**/
        }

        private void HandleTabletData(object sender, MessageReceivedEventArgs e)
        {
            CWintabData m_wtData = ti.getWintabData();
            UInt32 m_maxPkts = ti.getMaxPackets();

            if (m_wtData == null)
            {
                return;
            }

            try
            {
                if (m_maxPkts == 1)
                {
                    uint pktID = (uint)e.Message.WParam;
                    WintabPacket pkt = m_wtData.GetDataPacket((uint)e.Message.LParam, pktID);

                    if (pkt.pkContext != 0)
                    {
                        int pressure = (int)pkt.pkNormalPressure;

                        ss.setTabletPressure(pressure);
                    }
                }
            }
            catch (Exception err)
            {
                ss.setTabletConnected(false);
                Console.WriteLine(err.InnerException);
            }

        }

        private void handleToolStripItemClick(object sender, EventArgs e)
        {
            iActiveTool = ToolButtons.IndexOf((ToolStripButton)sender);
            Tools[iActiveTool].init(ss);
        }

        private void handleParentResize(object sender, EventArgs e)
        {

            //Temporary Hack Fix - Please Find Better Solution
            //Fixes Second New Project Null Parent Reference Bug
            this.Parent = (System.Windows.Forms.Control) sender;
            
            //Updates Parent Width and Height Values
            maxWidth = this.Parent.Width;
            maxHeight = this.Parent.Height;
            
            //Moves all the Controls to their new location
            lv.Location = new Point(maxWidth - (lv.Width + scrollWidth), maxHeight - (lv.Height + scrollHeight));
            ts.Height = maxHeight - menuHeight;
            bs.Location = new Point(maxWidth - bs.Width, menuHeight);
            zc.Location = new Point(tsWidth, maxHeight -SystemInformation.CaptionHeight - menuHeight - zc.Height);

            pScaled.Size = new Size((lv.Location.X - this.Location.X) - 15 , (zc.Location.Y - this.Location.Y) - 15);

            //Prevent controls from not redrawing
            this.Parent.Refresh();
        }

        private MouseEventArgs scaleMouseEvent(MouseEventArgs e)
        {
            int offset = (int)ss.getDrawScale() / 2;
            if (!ss.getActiveSelection())
            {
                return new MouseEventArgs(e.Button, e.Clicks, (int)((e.X - offset) / ss.getDrawScale()), (int)((e.Y - offset) / ss.getDrawScale()), e.Delta);
            }
            else
            {
                Rectangle rect = new Rectangle(ss.getSelectionPoint(), ss.getSelectionSize());
                if (ss.getActiveSelection() && rect.Contains(e.X, e.Y))
                {
                    return new MouseEventArgs(e.Button, e.Clicks, (int)(((e.X - ss.getSelectionPoint().X) - offset) / ss.getDrawScale()), (int)(((e.Y - ss.getSelectionPoint().Y) - offset) / ss.getDrawScale()), e.Delta);
                }
                else
                {
                    return null;
                }
            }
        }

        public void handleMouseDown(object sender, MouseEventArgs e)
        {
            
            MouseEventArgs evt = scaleMouseEvent(e);
            //If there is a selected Tool
            if (iActiveTool >= 0)
            {
                Tools[iActiveTool].init(ss);
            }
            if (iActiveTool >= 0 && evt != null)
            {
                Tools[iActiveTool].onMouseDown(sender, evt);
            }
        }

        public void handleMouseUp(object sender, MouseEventArgs e)
        {
            MouseEventArgs evt = scaleMouseEvent(e);
            if (iActiveTool >= 0 && evt != null)
                Tools[iActiveTool].onMouseUp(sender, evt);
            lv.UpdateLayerInfoListener();
            bs.CheckChange();
        }

        public void handleMouseMove(object sender, MouseEventArgs e)
        {
            MouseEventArgs evt = scaleMouseEvent(e);

            if (iActiveTool >= 0 && evt != null)
                
                Tools[iActiveTool].onMouseMove(sender, evt);
            updateCanvas(g);
            Parent.Refresh();
        }

        private void EDisplayPaint(object sender, PaintEventArgs e)
        {
            updateCanvas(e.Graphics);
            lv.updateActiveLayer();
        }

        public void updateCanvas(Graphics k)
        {

            Bitmap bit = lv.getRender();
            Bitmap bit2 = (Bitmap)bg.Clone();

            Graphics temp = Graphics.FromImage(bit2);

            temp.DrawImage(bit, 0, 0);

            Bitmap iitmp = ss.getImportImage();
            if (iitmp != null)
            {
                lv.addImportImage(iitmp);
            }
            ss.setBitmapCanvas(bit);

            p.Invalidate();

            //if (GCCurrentFrame == GCperFrames)
            //{
            //    System.GC.Collect(); //Prevent OutOfMemory Execptions
            //}
            //GCCurrentFrame += 1;
            //GCCurrentFrame %= 100;

            System.GC.Collect();

            p.Width = (int) (ss.getDrawScale() * ss.getCanvasWidth());
            p.Height = (int) (ss.getDrawScale() * ss.getCanvasHeight());
            Rectangle source = new Rectangle(-1, -1, bit2.Width+1, bit2.Height+1);
            Rectangle dest = new Rectangle(0, 0, p.Width, p.Height);

            k.InterpolationMode = InterpolationMode.NearestNeighbor;

            if (ss.getGridToggle())
            {
                lv.GridDraw(temp);
            }

            if (ss.getRenderBitmapInterface() && ss.getInterfaceBitmap() != null)
            {
                temp.DrawImage(ss.getInterfaceBitmap(), 0, 0);
            }

            /*
            if (ss.getActiveSelection() && ss.getBitmapSelectionArea() != null)
            {
                temp.DrawImage(ss.getBitmapSelectionArea(), ss.getSelectionPoint().X, ss.getSelectionPoint().Y);
            }
            */

            if (ss.getActiveSelection() && ss.getBitmapSelectionArea() != null)
            {
                ss.getActiveLayerGraphics().DrawImage(ss.getBitmapSelectionArea(), ss.getSelectionPoint().X, ss.getSelectionPoint().Y);
                ss.setFlattenSelection(false);
            }

            k.DrawImage(bit2, dest, source, GraphicsUnit.Pixel);
            bit.Dispose();
            bit2.Dispose();
            if(iitmp != null)
                iitmp.Dispose();
        }

        public void setBitmap(Bitmap bit)
        {
            //Sets the Background Image
            p.BackgroundImage = bit; 
        }

        public Bitmap getBitmap()
        {
            //Return the image the user has been working on
            return lv.getRender();
        }

        public void ShowTools()
        {
            if (!ToolsShown)
            {
                ts.Visible = true;
                
            }
            ToolsShown = true;
            Parent.Refresh();
        }

        public void HideTools()
        {
            if (ToolsShown)
            {
                ts.Visible = false;
            }

            ToolsShown = false;
        }

        public SharedSettings getSharedSettings()
        {
            return ss;
        }

        public void Trash()
        {
            lv.Trash();
            ss.Trash();
            p.Dispose();
            pScaled.Dispose();
            g.Dispose();
            Tools.Clear();
            this.Dispose();
        }
    }
}
