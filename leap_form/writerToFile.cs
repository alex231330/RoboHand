using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace leap_form
{
    class writerToFile
    {
        int count = 0;
        FileStream fileStream;
        StreamWriter streamWriter;
        public void writeInFile(int[] array, int size, bool status)
        {
            int i, u = 1;
            if (u == 1)
            {
                fileStream = new FileStream("a1.txt", FileMode.Open);
                streamWriter = new StreamWriter(fileStream);
                u = 0;
            }
            streamWriter.BaseStream.Seek(fileStream.Length, SeekOrigin.Begin);
            for (i = 0; i < size; i++)
            {
                streamWriter.Write(" " + array[i]);
            }
            if (status == false)
            {
                streamWriter.Close();
                fileStream.Close();
            }
        }
        public byte[] readFile()
        {
            int count1 = 0, ten = 1;
            string[] line = File.ReadAllLines("a1.txt");
            byte[] lineBZero = {255, 255, 255, 255, 255, 255};
            byte[] lineB = new byte[6];
               
                    for (int i = 0; i < 6; i++)
                    {
                        count = 0;
                        ten = 1;
                       // try
                       // {
                            while (Convert.ToInt32(line[line.Length - i - count1 - count - 1]) != 32)
                            {
                                lineB[i] += (byte)(Convert.ToInt32(line[line.Length - i - count1 - count]) * ten);
                                count++;
                                ten *= 10;
                            }
                            count1 += count;
                     //   }catch(Exception)
                     //   {
                     //       return lineBZero;
                     //   }
                    }
            return lineB;
        }
    }
}

