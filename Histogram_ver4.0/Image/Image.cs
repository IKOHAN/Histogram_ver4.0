using System;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace Histogram_ver4._0
{
    class Image
    {
        IColor scheme;
        readonly int width;
        readonly int height;
        Mask map;
        Pixel[,] data;
        public Image()
        {
        }
        public Image(IColor state, int w, int h)
        {
            scheme = state;
            width = w;
            height = h;
            data = new Pixel[h,w];
            map = new Mask(w, h);
        }
        public Image(BitmapSource bmps, IColor state)
        {
            try
            {
                scheme = state;
                var Img_source = new WriteableBitmap(bmps);
                width = Img_source.PixelWidth;
                height = Img_source.PixelHeight;
                map = new Mask(width, height);
                data = new Pixel[height,width];
                PixelHunt(Img_source);
            }
            catch (Exception)
            {
                throw new Exception("Метод вернувший ошибку: RGBImg.RGBImg");
            }
        }
        public Image(Image img, Mask map)
        {
            width = map.Width;
            height = map.Height;
            int wstart = map.GetWBound.X;
            int hstart = map.GetHBound.X;
            scheme = img.GetState;
            this.map = map;
            data = new Pixel[Height, Width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    if (map[i,j])
                    {
                        data[i,j] = img[i,j];
                    }
                    else
                        data[i,j] = null;
                }
        }
        public Image(IColor state, Mask map, int w, int h)
        {
            width = w;
            height = h;
            scheme = state;
            this.map = map;
            data = new Pixel[Height, Width];           
        }
        public Pixel[,] Data
        {
            get => data;
        }
        public Pixel this[int r, int c]
        {
            get {
                Ok(r, c);
                if (map[r, c])
                    return data[r, c];
                else
                    throw new OutOfRange();
            }
            set {
                Ok(r, c);
                if (map[r, c])
                    data[r, c] = value;
                else
                    throw new OutOfRange();
            }
        }
        public Pixel this[int x]
        {
            get {

                if (map[x / width, x % width])
                    return data[x / width, x % width];
                else
                    throw new OutOfRange();
            }
            set {
                if (map[x / width, x % width])
                    data[x / width, x % width] = value;
                else
                    throw new OutOfRange();
            }
        }
        public int Width
        {
            get => width;
        }
        public int Height
        {
            get => height;
        }
        //public bool IsEmpty()
        //{
        //    if (map.Length > 0)
        //        return false;
        //    else
        //        return true;
        //}
        public int GetLength()
        {
            return map.Length;
        }
        public Image Wider_img(Image img, int offset)
        {
            try
            {
                int width = img.Width + 2 * offset;
                int height = img.Height + 2 * offset;
                IColor tmpstate = img.GetState;
                var res = new Image(tmpstate,width, height);
                for (int y = 0, ysource = 0; y < height; y++)
                {
                    ysource += ( y > offset && y < img.Height + offset - 1 ) ? 1 : 0;
                    for (int x = 0, xsource = 0; x < width; x++)
                    {
                        xsource += ( x > offset && x < img.Width + offset - 1 ) ? 1 : 0;
                        res[y, x] = img[ysource,xsource];
                    }
                }
                return res;
            }
            catch (Exception)
            {
                throw new Exception("Метод вернувший ошибку: RGBImg.Copywithoffset.");
            }
        }
        public Image(Image img)
        {
            try
            {
                width = img.Width;
                height = img.Height;
                scheme = img.GetState;
                map = img.GetMap;
                data = new Pixel[height, width];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        data[ y,x] = img[y,x];
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Метод вернувший ошибку: RGBImg.Copywithoffset.");
            }
        }
        void PixelHunt(WriteableBitmap bmp)
        {
            bmp.Lock();
            try
            {
                unsafe
                {
                    int bytesPerPixel = ( bmp.Format.BitsPerPixel ) / 8;
                    int heightInPixels = bmp.PixelHeight;
                    int widthInBytes = bmp.PixelWidth * bytesPerPixel;
                    byte* ptrFirstPixel = (byte*) bmp.BackBuffer;
                    for (int y = 0; y < heightInPixels; y++)
                    {
                        byte* currentLine = ptrFirstPixel + ( y * bmp.BackBufferStride );
                        for (int x = 0, x1 = 0; x < widthInBytes; x = x + bytesPerPixel, x1++)
                        {
                            byte B = currentLine[x];
                            byte G = currentLine[x + 1];
                            byte R = currentLine[x + 2];
                            IColor color = new RGB().Create(R, G, B);
                            this[y,x1] = new Pixel(new Point(y, x1), scheme.Create(color));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Метод вернувший ошибку: RGBImg.PixelHunt.");
            }
            finally { bmp.Unlock(); }
        }
        bool Ok(int r, int c)
        {
            if (c < 0 || c >= width || r < 0 || r >= height)
                throw new IndexOutOfRangeException(string.Format("Trying to access pixel ({0}, {1}) in {2}x{3} image", r, c, width, height));
            return true;
        }
        public IColor GetState
        {
            get => scheme;
            set => scheme = value;
        }
        public Mask GetMap
        {
            get => map;
        }
    }
}
