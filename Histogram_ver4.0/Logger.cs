using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Histogram_ver4._0
{
    class Logger
    {
        string path = Path.GetFullPath(@" Image.txt");
        string path1 = Path.GetFullPath(@" Image2.txt");
        public Logger(StatsForUnite s1, StatsForUnite s2)
        {
            var file = new FileStream(path, FileMode.OpenOrCreate);
            var sw = new StreamWriter(file);
            int size = s1.GetImage.Width * s2.GetImage.Height;
            for (int i = 0; i < size; i++)
            {
                if(s1.GetImage.GetMap[i])
                sw.WriteLine(string.Format("   {0}   {1}   {2}   ", s1.GetImage[i].GetColor.GetVal1, s1.GetImage[i].GetColor.GetVal2, s1.GetImage[i].GetColor.GetVal3));
            }
            sw.Close();
            file.Close();
            file = new FileStream(path1, FileMode.OpenOrCreate);
            sw = new StreamWriter(file);
            sw.WriteLine();
            for (int i = 0; i < size; i++)
            {
                if (s2.GetImage.GetMap[i])
                    sw.WriteLine(string.Format("   {0}   {1}   {2}   ", s2.GetImage[i].GetColor.GetVal1, s2.GetImage[i].GetColor.GetVal2, s2.GetImage[i].GetColor.GetVal3));
            }
            sw.Close();
            file.Close();
        }
    }
}
