using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UnibrewREST
{
    public class SQLconnectionSingleton
    {
        private static SQLconnectionSingleton _instance = new SQLconnectionSingleton();

        public static SQLconnectionSingleton Instance => _instance;

        private SQLconnectionSingleton()
        {
            _dbConnection = new SqlConnection(ConnString);
            _dbConnection.Open();
        }

        private SqlConnection _dbConnection;


        private const String ConnString =
            @"Server=tcp:groenbechdatabase.database.windows.net,1433;Initial Catalog=UnibrewDB;Persist Security Info=False;User ID=lass813k;Password=*********;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public SqlConnection DbConnection => _dbConnection;
    }
}