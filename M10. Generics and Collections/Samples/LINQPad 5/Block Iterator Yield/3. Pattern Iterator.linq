<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Namespace>System.Collections.ObjectModel</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Json</Namespace>
  <Namespace>System.Xml.Serialization</Namespace>
</Query>

void Main()
{
	var collection = new CustomContainer<string>(new[]
	{
		"one", "two", "three", "four"
	});

	foreach (var t in collection)
	{
		Debug.WriteLine(t);
	}

	var collectionInt = new CustomContainer<int>(new[]
	{
		1, 2, 3, 4
	});

	foreach (var t in collectionInt)
	{
		Debug.WriteLine(t);
	}
}

public class CustomContainer<T> : IEnumerable<T>
{
	private readonly T[] container;

	public int Count { get { return container.Length; } }

	public T this[int index]
	{
		get { return container[index]; }
		set { container[index] = value; }
	}

	public CustomContainer(T[] array)
	{
		container = new T[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			container[i] = array[i];
		}
	}

	public IEnumerator<T> GetEnumerator()//!!!
	{
		return new CustomIterator(this);
	}

	IEnumerator IEnumerable.GetEnumerator()//!!!
	{
		throw new NotImplementedException();
	}

	private struct CustomIterator : IEnumerator<T>
	{
		private readonly CustomContainer<T> collection;
		private int currentIndex;

		public CustomIterator(CustomContainer<T> collection)
		{
			this.currentIndex = -1;
			this.collection = collection;
		}

		public T Current
		{
			get
			{
				if (currentIndex == -1 || currentIndex == collection.Count)
				{
					throw new InvalidOperationException();
				}
				return collection[currentIndex];
			}
		}

		object System.Collections.IEnumerator.Current
		{
			get { throw new NotImplementedException(); }
		}

		public void Reset()
		{
			currentIndex = -1;
			//throw new NotSupportedException();
		}

		public bool MoveNext()
		{
			return ++currentIndex < collection.Count;
		}

		void IDisposable.Dispose() { }
	}
}