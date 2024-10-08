using System;
using System.Collections.Generic;

namespace FTRuntime.Internal
{
	internal class SwfList<T>
	{
		private T[] _data;

		private int _size;

		private static readonly T[] _emptyData = new T[0];

		private const int _defaultCapacity = 4;

		public T this[int index]
		{
			get
			{
				if ((uint)index >= (uint)_size)
				{
					throw new IndexOutOfRangeException();
				}
				return _data[index];
			}
			set
			{
				if ((uint)index >= (uint)_size)
				{
					throw new IndexOutOfRangeException();
				}
				_data[index] = value;
			}
		}

		public int Count => _size;

		public int Capacity
		{
			get
			{
				return _data.Length;
			}
			set
			{
				if (value < _size)
				{
					throw new ArgumentOutOfRangeException("capacity");
				}
				if (value == _data.Length)
				{
					return;
				}
				if (value > 0)
				{
					T[] array = new T[value];
					if (_size > 0)
					{
						Array.Copy(_data, array, _size);
					}
					_data = array;
				}
				else
				{
					_data = _emptyData;
				}
			}
		}

		public SwfList()
		{
			_data = _emptyData;
			_size = 0;
		}

		public SwfList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "capacity must be >= 0");
			}
			if (capacity == 0)
			{
				_data = _emptyData;
				_size = 0;
			}
			else
			{
				_data = new T[capacity];
				_size = 0;
			}
		}

		public void Push(T value)
		{
			if (_size == _data.Length)
			{
				T[] array = new T[(_size == 0) ? 4 : (_size * 2)];
				Array.Copy(_data, array, _size);
				_data = array;
			}
			_data[_size++] = value;
		}

		public T Pop()
		{
			if (_size == 0)
			{
				throw new InvalidOperationException("empty list");
			}
			T result = _data[--_size];
			_data[_size] = default(T);
			return result;
		}

		public T Peek()
		{
			if (_size == 0)
			{
				throw new InvalidOperationException("empty list");
			}
			return _data[_size - 1];
		}

		public void Clear()
		{
			Array.Clear(_data, 0, _size);
			_size = 0;
		}

		public T UnorderedRemoveAt(int index)
		{
			if ((uint)index >= (uint)_size)
			{
				throw new IndexOutOfRangeException();
			}
			T val = _data[_size - 1];
			_data[index] = val;
			_data[--_size] = default(T);
			return val;
		}

		public void AssignTo(List<T> list)
		{
			list.Clear();
			if (list.Capacity < Count)
			{
				list.Capacity = Count * 2;
			}
			int i = 0;
			for (int count = Count; i < count; i++)
			{
				list.Add(this[i]);
			}
		}

		public void AssignTo(SwfList<T> list)
		{
			if (list._data.Length < _size)
			{
				T[] array = new T[_size * 2];
				Array.Copy(_data, array, _size);
				list._data = array;
				list._size = _size;
				return;
			}
			if (_size < list._size)
			{
				Array.Clear(list._data, _size, list._size - _size);
			}
			if (_size > 0)
			{
				Array.Copy(_data, list._data, _size);
			}
			list._size = _size;
		}
	}
}
