<Query Kind="Program" />

class Animal { }

class Dog : Animal
{
	public void Woof() { Console.WriteLine ("woof!"); }
}

void Main()
{
	Dog d = new Dog();
	//статическое связывание
	d.Woof();
	//поиск метода с соответствующей сигнатурой, затем поиск распространиется на 
	//методы, принимающие необязательные параметры, методы базового класса и расширяющие методы,
	//которые принимают в качестве первого параметра Dog
	//связывание выплняется компилятором и полностью определяется типом операнда d(получатель)
	
	Animal x = new Dog();//тип времени компиляции Animal 
}