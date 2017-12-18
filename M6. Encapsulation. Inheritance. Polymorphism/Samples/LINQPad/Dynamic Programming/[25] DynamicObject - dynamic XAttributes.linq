<Query Kind="Program">
  
  <Namespace>System.Dynamic</Namespace>
</Query>

static class XExtensions
{
	public static dynamic DynamicAttributes (this XElement e)
	{
		return new XWrapper (e);
	}
	
	class XWrapper : DynamicObject
	{
		XElement _element;
		public XWrapper (XElement e) { _element = e; }
	
		public override bool TryGetMember (GetMemberBinder binder,
										out object result)
		{
			result = _element.Attribute (binder.Name).Value;
			return true;
		}
	
		public override bool TrySetMember (SetMemberBinder binder,
										object value)
		{
			_element.SetAttributeValue (binder.Name, value);
			return true;
		}
	}
}

void Main()
{
	XElement x = XElement.Parse (@"<Label Text=""Hello"" Id=""5""/>");
	dynamic da = x.DynamicAttributes();
	Console.WriteLine (da.Id);           // 5
	da.Text = "Foo";
	Console.WriteLine (x.ToString());    // <Label Text="Foo" Id="5" />
}
