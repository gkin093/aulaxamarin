using System;
using SQLite;

namespace novemob
{
	public class BaseItem
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set;}

		public BaseItem()
		{
		}
	}
}
