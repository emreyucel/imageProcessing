using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    /// <summary>
    /// Image sınıfı, Bitmap türünde bir nesneyi tutar ve bu nesnenin temsil ettiği görsel
    /// üzerinde işaretçi (pointer) aracılığıyla piksel bazında işlemler gerçekleştirir.
    /// Aynı zamanda işaretçiler ile görseldeki satırlara erişebilir ve üzerinde işlemler yapılabilir.
    /// </summary>
    public unsafe class Image
    {
        private Bitmap _source;
        private BitmapData _data;
        private byte[] _pixelData;
        private int _depth;
        private int _width;
        private int _height;
        private int _rowSize;
        private bool _isOpen = false;
        private byte* _startPoint;
        
        public Bitmap Source
        {
            get
            {
                Open(AccessMode.ReadWrite);
                Marshal.Copy(_pixelData, 0, (IntPtr)_startPoint, _pixelData.Length);
                Close();
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        public Pixel TopLeftCorner
        {
            get { return GetPixel(0, 0); }
        }
        public Pixel TopRightCorner
        {
            get { return GetPixel(0, _width - 1); }
        }
        public Pixel BottomLeftCorner
        {
            get { return GetPixel(_height - 1,0); }
        }
        public Pixel BottomRightCorner
        {
            get { return GetPixel(_height - 1, _width - 1); }
        }

        /// <summary>
        /// Görselin piksel derinliği için kullanılacak formatı belirtir.
        /// </summary>
        public PixelFormat PixelFormat { get { return _source.PixelFormat; } }

        /// <summary>
        /// Görselin piksel bazında genişliğini gösterir.
        /// </summary>
        public int Width
        {
            get { return _source.Width; }
        }

        /// <summary>
        /// Görselin piksel bazında yüksekliğini gösterir.
        /// </summary>
        public int Height
        {
            get { return _source.Height; }
        }

        /// <summary>
        /// Görselin piksel boyutunu gösterir. Pikselin derinliğidir.
        /// </summary>
        public int Depth { get { return _depth; } }

        /// <summary>
        /// Görseldeki her bir satırda yer alan byte sayısını belirtir.
        /// </summary>
        public int RowSize { get { return _rowSize; } }

        /// <summary>
        /// Image nesnesinin bir erişim moduyla başlatılığı başlatılmadığını gösterir.
        /// </summary>
        public bool IsOpen { get { return _isOpen; } }

        /// <summary>
        /// Görselde yer alan tüm byte'ları içeren dizidir.
        /// </summary>
        public byte[] PixelData { get { return _pixelData; } }

        public Image(string source) : this(new Bitmap(source))
        {
        }

        public Image(Bitmap bitmap)
        {
            _source = bitmap;
            InitImageData();
        }

        /// <summary>
        /// Bir pikseli değiştirmek için kullanılır.
        /// summary>
        /// <param name="color">Yeni piksele ait renk bilgilerini içerir.</param>
        /// <param name="rowNumber">Değiştirilecek satırı belirtir.</param>
        /// <param name="columnNumber">Değiştirilecek sütunu belirtir.</param>
        public void SetPixel(Color color, int rowNumber, int columnNumber)
        {
            int index = (rowNumber * _rowSize) + (columnNumber * _depth);
            _pixelData[index + 2] = color.R;
            _pixelData[index + 1] = color.G;
            _pixelData[index] = color.B;

            if (PixelFormat == PixelFormat.Format32bppArgb) _pixelData[index + 3] = color.A;
        }

        /// <summary>
        /// Bir pikseli değiştirmek için kullanılır.
        /// Pixel nesnesi içerisinde satır ve sütun değerleri bulundurur ve bu değerler ile
        /// görsel üzerinde piksel değişikliği gerçekleştirilir.
        /// </summary>
        /// <param name="pixel">Yeni verilerin bulunduğu Pixel nesnesidir.</param>
        public void SetPixel(Pixel pixel)
        {
            int index = (pixel.RowNumber * _rowSize) + (pixel.ColumnNumber * _depth);
            _pixelData[index+2] = pixel.R;
            _pixelData[index+1] = pixel.G;
            _pixelData[index] = pixel.B;

            if (PixelFormat == PixelFormat.Format32bppArgb) _pixelData[index + 3] = pixel.Alpha;
        }

        /// <summary>
        /// Belirtilen satır ve sütundaki pikseli döndürür. Belirteç numaraları 0'dan başlar.
        /// </summary>
        /// <param name="rowNumber">Satır belirteci.</param>
        /// <param name="columnNumber">Sütun belirteci.</param>
        /// <returns>Pixel türünde bir nesne döndürür.</returns>
        public Pixel GetPixel(int rowNumber, int columnNumber)
        {
            int index = (rowNumber * _rowSize) + (columnNumber * _depth); // RGB byte'larının başladığı adresi gösterir.

            Pixel pixel = new Pixel()
            {
                R = _pixelData[index+2],
                G = _pixelData[index + 1],
                B = _pixelData[index],
                ColumnNumber = columnNumber,
                RowNumber = rowNumber
            };

            if (PixelFormat == PixelFormat.Format32bppArgb) pixel.Alpha = _pixelData[index + 3];

            return pixel;
        }

        /// <summary>
        /// Görseldeki herhangi bir satıra erişim sağlar. 
        /// </summary>
        /// <param name="rowNumber">Erişilecek satırın numarasını belirtir.</param>
        /// <returns>İlgili satırın başlangıcını belirten işaretçi döndürür.</returns>
        public byte* GetRowBytes(int rowNumber)
        {
            fixed (byte* bptr = _pixelData)
            {
                return bptr + (rowNumber * _rowSize);
            }
        }

        /// <summary>
        /// Görselin belirtilen satırındaki verileri değiştirir.
        /// </summary>
        /// <param name="rowNumber">Erişilecek satırın numarasını belirtir.</param>
        /// <param name="rowData">Satıra ait yeni verileri temsil eden dizidir.</param>
        public void SetRowBytes(int rowNumber, byte[] rowData)
        {
            Array.Copy(rowData, 0, _pixelData, rowNumber * _rowSize, _rowSize);
        }

        /// <summary>
        /// Görsele ait bit'ler erişim moduna göre kilitlenir.
        /// </summary>
        /// <param name="mode">Bitlere erişim modunu belirtir.</param>
        public void Open(AccessMode mode)
        {
            if (!_isOpen)
            {
                ImageLockMode m = mode == AccessMode.ReadOnly ? ImageLockMode.ReadOnly : ImageLockMode.ReadWrite;

                Rectangle rect = new Rectangle(0, 0, _source.Width, _source.Height);
                _data = _source.LockBits(rect, m, _source.PixelFormat);
                _isOpen = true;
            }
        }

        /// <summary>
        /// Görsele ait kilitlenen bit'ler sertbest bırakılır.
        /// </summary>
        public void Close()
        {
            if (_isOpen)
            {
                _source.UnlockBits(_data);
                _isOpen = false;
            }
        }

        /// <summary>
        /// Image sınıfının üyeleri tanımlanır ve hazırlanır.
        /// </summary>
        /// <param name="mode">Hafızada yer alan görsele ait bit'lere nasıl erişileceğini belirtir.</param>
        private void InitImageData(AccessMode mode = 0)
        {
            Open(mode);
            _startPoint = (byte*)_data.Scan0;
            _rowSize = _data.Stride;
            _depth = System.Drawing.Image.GetPixelFormatSize(_source.PixelFormat) / 8;
            _height = _source.Height;
            _width = _source.Width;

            _pixelData = new byte[_rowSize * _height];
            Marshal.Copy((IntPtr)_startPoint, _pixelData, 0, _pixelData.Length);
            Close();
        }

    }

    public enum AccessMode
    {
        ReadOnly,
        ReadWrite
    }
}
