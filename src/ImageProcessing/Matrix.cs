using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class Matrix<T>
    {
        private int _width;
        private int _height;
        private T[] _data;

        public int Width { get { return _width; } set { _width = value; } }
        public int Height { get { return _height; } set { _height = value; } }
        public int Length { get { return _height * _width; } }
        public T[] Data { get { return _data; } }

        public T this[int row, int column]
        {
            get
            {
                if (_data.Length != 0 && _data.Length >= (row + 1) * (column + 1))
                    return _data[row*_width + column];
                return default(T);
            }
            set
            {
                if (_data.Length != 0 && _data.Length >= (row + 1) * (column + 1))
                    _data[row * _width + column] = value;
            }
        }

        public Matrix(int width, int height)
        {
            _width = width;
            _height = height;
            _data = new T[width * height];
        }

        public T GetItem(int row, int column)
        {
            return _data[row * _width + column];
        }

        public void SetItem(T item, int row, int column)
        {
            _data[row * _width + column] = item;
        }

        public Matrix<T> Fill(T value)
        {
            for (int i = 0; i < _data.Length; i++)
                _data[i] = value;

            return this;
        }
    }
}
