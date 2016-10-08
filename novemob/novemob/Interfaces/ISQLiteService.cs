using System;
using SQLite;

namespace novemob
{
	//interface para conexão com databaser SQLite
	public interface ISQLiteService
	{
		SQLiteConnection GetConnection(string databaseName);
		long GetSize(string databaseName);
	}
}
