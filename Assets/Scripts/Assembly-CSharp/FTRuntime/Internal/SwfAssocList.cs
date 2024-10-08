using System.Collections.Generic;

namespace FTRuntime.Internal
{
	internal class SwfAssocList<T>
	{
		private SwfList<T> _list;

		private Dictionary<T, int> _dict;

		private IEqualityComparer<T> _comp;

		public T this[int index] => _list[index];

		public int this[T item] => _dict[item];

		public int Count => _list.Count;

		public SwfAssocList()
		{
			_list = new SwfList<T>();
			_dict = new Dictionary<T, int>();
			_comp = EqualityComparer<T>.Default;
		}

		public SwfAssocList(int capacity)
		{
			_list = new SwfList<T>(capacity);
			_dict = new Dictionary<T, int>(capacity);
			_comp = EqualityComparer<T>.Default;
		}

		public bool Contains(T value)
		{
			return _dict.ContainsKey(value);
		}

		public void Add(T item)
		{
			if (!_dict.ContainsKey(item))
			{
				_dict.Add(item, _list.Count);
				_list.Push(item);
			}
		}

		public void Remove(T item)
		{
			if (_dict.TryGetValue(item, out var value))
			{
				_dict.Remove(item);
				T val = _list.UnorderedRemoveAt(value);
				if (!_comp.Equals(val, item))
				{
					_dict[val] = value;
				}
			}
		}

		public void Clear()
		{
			_list.Clear();
			_dict.Clear();
		}

		public void AssignTo(List<T> list)
		{
			_list.AssignTo(list);
		}

		public void AssignTo(SwfList<T> list)
		{
			_list.AssignTo(list);
		}
	}
}
