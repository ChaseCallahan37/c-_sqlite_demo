using System.Data.SQLite;

string connectionString = @"Data Source=/home/chase/projects/221/test/sqlite/books.db;Version=3;";

var databaseConnection = new SQLiteConnection(connectionString);

databaseConnection.Open();

// GetSQLVersion(databaseConnection);

CreateBooksTable(databaseConnection);

AddBooks(databaseConnection);

ReadBooks(databaseConnection);

UpdateBooks(databaseConnection);

ReadBooks(databaseConnection);

databaseConnection.Close();


static void GetSQLVersion(SQLiteConnection conn)
{
    conn.Open();

    string sqlStatement = "SELECT SQLITE_VERSION()";

    using var sqlCommand = new SQLiteCommand(sqlStatement, conn);

    string result = sqlCommand.ExecuteScalar().ToString();

    System.Console.WriteLine($"SQLite version: {result}");

    conn.Close();
}

static void CreateBooksTable(SQLiteConnection conn)
{

    using var cmd = new SQLiteCommand(conn);

    cmd.CommandText = "DROP TABLE IF EXISTS books";
    cmd.ExecuteNonQuery();

    cmd.CommandText = @"
    CREATE TABLE books(
        id INTEGER PRIMARY KEY,
        title TEXT,
        author TEXT
    );";
    cmd.ExecuteNonQuery();

}

static void AddBooks(SQLiteConnection conn)
{
    using var cmd = new SQLiteCommand(conn);

    cmd.CommandText = @"INSERT INTO books(title, author)
                        VALUES('Mistborn','Brandon Sanderson')";
    cmd.ExecuteNonQuery();


    cmd.CommandText = @"INSERT INTO books(title, author)
                        VALUES('Oathbringer','Brandon Sanderson')";
    cmd.ExecuteNonQuery();

}

static void ReadBooks(SQLiteConnection conn)
{
    using var cmd = new SQLiteCommand(conn);

    cmd.CommandText = @"SELECT * FROM books;";

    using var rdr = cmd.ExecuteReader();

    while (rdr.Read())
    {
        System.Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetString(2)}");
    }
}

static void UpdateBooks(SQLiteConnection conn)
{
    using var cmd = new SQLiteCommand(conn);

    cmd.CommandText = @"UPDATE books
                        SET 
                            title = 'Green Eggs & Ham', 
                            author = 'Dr. Suess'
                        WHERE id = 1";

    cmd.ExecuteNonQuery();
}