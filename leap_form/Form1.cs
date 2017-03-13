using System;
using System.Drawing;
using System.Windows.Forms;
using Leap;
using MonoBrick.NXT;
using System.Threading;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace leap_form
{
    public partial class Form1 : Form
    {
        int i1 = 0, k1;
        int i = 0, n = 0, j = 0;
        LeapMotion leap;
        Graphics g;
        byte[] array = new byte[9999999];
        bool mode = false;
        Thread drwH;
        Thread Drawing;
        int f1, f2, f3, f4, f5;
        Controller controller;
        bool reMode = false;
        static Brick<Sensor, Sensor, Sensor, Sensor> nxt;
        static Brick<Sensor, Sensor, Sensor, Sensor> nxt2;
        static byte[] mes;
        static byte[] mes2;
        byte[] mes3 = { 0,107 };
        LocalNetwork ln = new LocalNetwork();
        float[] cordsX = new float[5];
        float[] cordsY = new float[5];
        int[] wtf = new int[6];
        int count = 0;
        String[] ReaderArray = new String[6];
        int pref5 = 0;
        int xx = 0, zz = 0, yy = 0;
        int handRotationState = 0;
        byte[] preMes1 = new byte[5];
        byte[] preMes2 = new byte[5];
        byte[,] preArray = new byte[5,8];
        int prevT = 0;

        public Form1()
        {
            InitializeComponent();
            leap = new LeapMotion();
            controller = new Controller();
            drwH = new Thread(drwHand);
        }

        /// <summary>
        /// Buttons function
        /// </summary>
        
        private void Button_Click_1(object sender, EventArgs e)
        {
            nxt = new Brick<Sensor, Sensor, Sensor, Sensor>("COM15");
            nxt2 = new Brick<Sensor, Sensor, Sensor, Sensor>("COM17");
           // try
          // {
             //   nxt.Connection.Open();
                // nxt2.Connection.Open();
           // }
         //   catch (Exception)
          //  {
             //   nxt.Connection.Open();
                //  nxt2.Connection.Open();
         //   }
            mes = new byte[] { 10, 0, 0, 0 };
            mes2 = new byte[] { 0, 0, 0, 0, 0 };
        }

        void send_massage_func()
        {
            nxt.Mailbox.Send(mes, Box.Box1);
            nxt.Mailbox.Send(mes2, Box.Box2);
            //  nxt2.Mailbox.Send(mes3, Box.Box1);
        }

        void drawRect()
        {
            g = pictureBox1.CreateGraphics();
            Brush brush = new SolidBrush(System.Drawing.Color.Cyan);
            Rectangle rect = new Rectangle(0, 0, 640, 480);
            g.FillRectangle(brush, rect);
        }

        void drawRound(Graphics g, Pen pen, int size, int xr, int yr, int W, int H)
        {
            int x1R = 0, y1R = 0;
            for (y1R = -size; y1R < size; y1R++)
                for (x1R = -size; x1R < size; x1R++)
                    if ((x1R * x1R + y1R * y1R) <= size * size && x1R + xr < W && y1R + yr < H && x1R + xr > 0 && y1R + yr > 0)
                    {
                        g.DrawLine(pen, x1R + xr, y1R + yr, x1R + xr + 1, y1R + yr + 1);
                    }
        }

        public void drawLine(Pen pen, Point[] points, int Width, int xLine1, int yLine1, int xLine2, int yLine2, int W, int H)
        {
            int x1, y1;
            Graphics g = pictureBox1.CreateGraphics();
            if (yLine1 > yLine2)
            {
                int tmp = yLine1;
                yLine1 = yLine2;
                yLine2 = tmp;
                tmp = xLine1;
                xLine1 = xLine2;
                xLine2 = tmp;
            }
            for (y1 = yLine1; y1 < yLine2; y1++)
            {
                x1 = (((y1 - yLine1) * (xLine2 - xLine1)) / (yLine2 - yLine1)) + xLine1;
                for (int i = 0; i < Width; i++)
                { 
                    for (int kk = 0; kk < Width; kk++)
                    {
                        if ((y1 + i) * W + x1 + kk < H * W - 4 && (y1 + i) * W + x1 + kk > 0)
                        {
                            g.DrawLine(pen, x1, y1, x1, y1);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //drwH.Start();
                Drawing.Start();
            }
            catch (Exception)
            {
                //drwH.Start();   
                Drawing.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "start write")
            {
                button2.Text = "writing...\n(stop)";
                mode = true;
            }
            else
            {
                button2.Text = "start write"; mode = false;
            } 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (count == 1) count = 0; else count = 1;
            if (count == 1)
            {
                button3.Text = "Stop remake";
                reMode = true;
            }
            else if (count == 0)
            {
                reMode = false;
                button3.Text = "Remake";
            }
        }


        void drawWLine(int x, int y, int x1, int y1)
        {
            g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(System.Drawing.Color.White);
            g.DrawLine(pen, x, y, x1, y1);
        }

        int vectorlength(float x1, float y1, float x2, float y2)
        {
            return (int)(Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2))); ;
        }
        int vectorlength1(float x1, float y1, float x2, float y2, float z1, float z2)
        {
            return (int)(Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2) * (z1 - z2))); 
        }

        void drwHand()
        {
         //   preMes = (int)mes2[0];
            pref5 = f5;
            while (true)
            {
                prevT = (int)leap.wrist[2];
                if (count == 0)
                {
                        f1 = (vectorlength(leap.cordX15, leap.cordY15, leap.cordX4, leap.cordY4) - 70) * 6 - 100;
                        f2 = (vectorlength(leap.cordX15, leap.cordY15, leap.cordX2, leap.cordY2) - 70) * 5;
                        f3 = (vectorlength(leap.cordX15, leap.cordY15, leap.cordX12, leap.cordY12) - 70) * 5;
                        f4 = (220 - (int)(vectorlength(leap.cordX15, leap.cordY15, leap.cordX8, leap.cordY8) * 2)) * 3 - 100;
                        f5 = (220 - (int)(vectorlength1(leap.cordX14, leap.cordY14, leap.cordX17, leap.cordY17, leap.cordZ14, leap.cordZ17)) * 3) * 3 + 100;
                        handRotationState = 255 - (int)((leap.h[1] - leap.cordZ11) * 13 + 140);
                        xx = Math.Abs((int)leap.cordX11 - 150);
                        zz = Math.Abs((int)leap.cordZ11 - 390);
                        yy = (int)leap.cordY11 / 3;
                    // rotation = ((int)(leap.cordZ11 - leap.wrist[1]) * 4 + 140);
                    
                     
                        if (f1 <= 0)
                            mes[0] = 250;
                        else if (f1 >= 250)
                            mes[0] = 0;
                        else
                            mes[0] = (byte)(250 - f1);
                        if (f2 <= 0)
                            mes[1] = 0;
                        else if (f2 > 250)
                            mes[1] = 250;
                        else
                            mes[1] = (byte)(f2);
                        if (f3 <= 0)
                            mes[2] = 0;
                        else if (f3 > 250)
                            mes[2] = 250;
                        else
                            mes[2] = (byte)(f3);
                        if (f4 <= 0)
                            mes[3] = 0;
                        else if (f4 >= 250)
                            mes[3] = 250;
                        else
                            mes[3] = (byte)(f4);
                        if (f5 > 0)
                            mes2[1] = 250;
                        else if (f5 < 0)
                            mes2[1] = 0;
                        else
                            mes2[1] = (byte)(f5);
                        mes2[0] = 0;
                        mes2[2] = (byte)((Math.Sqrt((xx * xx + zz * zz))) * 2 / 5 + 25);
                        mes2[3] = (byte)((Math.Sqrt(((270 - zz) * (270 - zz) + xx * xx))) * 2 / 5 + 25);
                        if (handRotationState < 0)
                            mes2[4] = 0;
                        else if (handRotationState > 100 && handRotationState < 180)
                            mes2[4] = 35;
                        else
                            mes2[4] = (byte)(handRotationState / 4);
                        mes3[1] = (byte)yy;

                    if (!(preMes1[0] == mes[0] && preMes1[1] == mes[1] && preMes1[2] == mes[2] && preMes1[3] == mes[3] && preMes1[4] == mes2[1]))
                    {
                        n = 0;

                        send_massage_func();
                        i = 1;
                    }
                    else if (n < 5)
                    {
                        n++;
                        send_massage_func();
                        i = 1;
                    }
                    else { i = 0; n++; }
                        preMes1[0] = mes[0];
                        preMes1[1] = mes[1];
                        preMes1[2] = mes[2];
                        preMes1[3] = mes[3];
                        preMes1[4] = mes2[1];

                    for (int co = 4; co > 0; co--)
                    {
                        for (int c = 0; c < 8; c++)
                        {
                            preArray[co, c] = preArray[co - 1, c];
                        }     
                    }
                    preArray[0, 0] = mes[0];
                    preArray[0, 1] = mes[1];
                    preArray[0, 2] = mes[2];
                    preArray[0, 3] = mes[3];
                    preArray[0, 4] = mes2[1];
                    preArray[0, 5] = mes2[2];
                    preArray[0, 6] = mes2[3];
                    preArray[0, 7] = mes2[4];

                    if (n > 20)
                        n = 20;
                }

                if (mode == true)
                {
                    k1 = i1 + 4;
                    for (; i1 < k1; i1++)
                    {
                        array[i1] = mes[i1 % 7];
                    }
                    for (; i1 < k1 + 3; i1++)
                    {
                        array[i1] = mes2[i1 % 7 - 4];
                    }
                }
                if (reMode == true)
                {
                    for (int i = 0; i < i1;)
                    {
                        mes[0] = array[i];
                        i++;
                        mes[1] = array[i];
                        i++;
                        mes[2] = array[i];
                        i++;
                        mes[3] = array[i];
                        i++;
                        mes2[0] = array[i];
                        i++;
                        mes2[1] = array[i];
                        i++;
                        mes2[2] = array[i];
                        i++;
                        send_massage_func();
                    }
                }
                }
            }
            }
    }