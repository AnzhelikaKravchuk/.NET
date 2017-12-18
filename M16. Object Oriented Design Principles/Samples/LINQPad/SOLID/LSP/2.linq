<Query Kind="Program" />

#region Ошибочное наследование
//Мы хотим реализовать свой список с интерфейсом IList. 
//Его особенностью будет то, что все записи в нем дублируются.
#endregion

public class DoubleList<T> : IList<T>
{
	private readonly IList<T> innerList = new List<T>();//List : IList!!!
  
	public void Add(T item)
	{
		innerList.Add(item);
		innerList.Add(item);
	}
  
	//...
}

//Данная реализация не представляет никакой опасности, если
//рассматривать ее изолированно. Взглянем на использование этого 
//класса с точки зрения клиента. Клиент, абстрагируясь от 
//реализаций, пытается работать со всеми объектами типа IList одинаково:
[Fact]
public void CheckBehaviourForRegularList()
{
	IList<int> list = new List<int>();
  
	list.Add(1);
  
	Assert.Equal(1, list.Count);
}
  
[Fact]
public void CheckBehaviourForDoubleList()
{
	IList<int> list = new DoubleList<int>();
  
	list.Add(1);
  
	Assert.Equal(1, list.Count); // fail
}

//Поведение списка DoubleList отличается от типичных реализаций IList.
//Получается, что наш DoubleList не может быть заменен базовым типом. 
//Это и есть нарушение принципа замещения Лисков.

//Проблема заключается в том, что теперь клиенту необходимо знать 
//о конкретном типе объекта, реализующем интерфейс IList. В качестве
//такого объекта могут передать и DoubleList, а для него придется
//выполнять дополнительную логику и проверки.

//Правильным решением будет использовать свой собственный интерфейс, 
//например, IDoubleList. Этот интерфейс будет объявлять для пользователей 
//поведение, при котором добавляемые элементы удваиваются.

//Проектирование по контракту
//
//Есть формальный способ понять, что наследование является ошибочным. 
//Это можно сделать с помощью проектирования по контракту.
//Бернард Мейер, его автор, сформулировал следующий принцип:
//
//Наследуемый объект может заменить родительское пред-условие на 
//такое же или более слабое и родительское пост-условие на такое же
//или более сильное. (перефразировано)
//Рассмотрим пред- и постусловия для интерфейса IList. Для функции Add:
//
//пред-условие: item != null
//пост-условие: count = oldCount + 1

//Для нашего DoubleList и его функции Add:
//
//пред-условие: item != null
//пост-условие: count = oldCount + 2

//Другими словами, когда мы используем интерфейс IList, то как 
//пользователи этого базового класса знаем только его пред- и постусловия.
//Нарушая принцип проектирования по контракту мы меняем поведение 
//унаследованного объекта.
