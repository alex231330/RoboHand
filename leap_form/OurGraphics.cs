using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leap_form
{
    class OurGraphics
    {
        public void drawLine(byte[] pixels, int Width, int xLine1, int yLine1, int xLine2, int yLine2, int W, int H)
        {
            int x1, y1;
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
                    for (int kk = 0; kk < Width; kk++)
                    {
                        if ((y1 + i) * W + x1 + kk < H * W - 4 && (y1 + i) * W + x1 + kk > 0)
                        {
                            pixels[((y1 + i) * W + x1 + kk) * 4] = 100;
                            pixels[((y1 + i) * W + x1 + kk) * 4 + 1] = 0;
                            pixels[((y1 + i) * W + x1 + kk) * 4 + 2] = 100;
                        }
                    }
            }
        }

        public void drawRound(byte[] pixels, int size, int xr, int yr, int W, int H)
        {
            int x1, y1;
            for (y1 = -size; y1 < size; y1++)
                for (x1 = -size; x1 < size; x1++)
                    if ((x1 * x1 + y1 * y1) <= size * size && x1 + xr < W && y1 + yr < H && x1 + xr > 0 && y1 + yr > 0)
                    {
                        pixels[((y1 + yr) * W + x1 + xr) * 4 + 2] = 190;
                        pixels[((y1 + yr) * W + x1 + xr) * 4 + 1] = 160;
                        pixels[((y1 + yr) * W + x1 + xr) * 4] = 45;
                    }
        }
    }
}
