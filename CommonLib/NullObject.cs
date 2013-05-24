namespace CommonLib
{
	public class NullObject
	{
		public static implicit operator bool(NullObject obj)
		{	
			return !Equals(obj, null);
		}
	}
}