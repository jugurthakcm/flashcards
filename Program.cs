// See https://aka.ms/new-console-template for more information

using System.Collections.Specialized;
using System.Configuration;
using Dapper;
using flashcards;
using Microsoft.Data.SqlClient;

using (var connection = new SqlConnection(Variables.defaultConnection))
{
    var sql =
        @"CREATE TABLE IF NOT EXISTS coding_tracker (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime TEXT,
                        EndTime TEXT,
                        Duration INTEGER
                        )";

    connection.Execute(sql);
}

Menu.ShowMenu();
