// See https://aka.ms/new-console-template for more information

using System.Collections.Specialized;
using System.Configuration;
using Dapper;
using flashcards;
using Microsoft.Data.SqlClient;

using (var connection = new SqlConnection(Variables.defaultConnection))
{
    var sql =
        @"
    IF OBJECT_ID('Stacks', 'U') IS NULL
    BEGIN
        CREATE TABLE Stacks (
            Id INT IDENTITY(1,1) PRIMARY KEY,
            Name NVARCHAR(255) NOT NULL
        );
    END;

    IF OBJECT_ID('Flashcards', 'U') IS NULL
    BEGIN
        CREATE TABLE Flashcards (
            Id INT IDENTITY(1,1) PRIMARY KEY,
            Front NVARCHAR(255) NOT NULL,
            Back NVARCHAR(255) NOT NULL,
            StackId INT,
            FOREIGN KEY (StackId) REFERENCES Stacks(Id)
        );
    END;
";

    connection.Execute(sql);
    /*
        sql = "DELETE from Stacks";
        connection.Execute(sql);
    
        sql = "DELETE from Flashcards";
        connection.Execute(sql);
        */
}

Menu.ShowMenu();
