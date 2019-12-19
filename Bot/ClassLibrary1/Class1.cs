using System;  // C# , ADO.NET
using System.Data;
using System.Text;
using Microsoft;
using DT = System.Data;            // System.Data.dll  
using QC = Microsoft.Data.SqlClient;  // System.Data.dll  

namespace Library
{
    public static class Init
    {
        public static void Start()
        {
            using (var connection = new QC.SqlConnection(
                "Server = tcp:taxibotapp20191219011147dbserver.database.windows.net,1433; Initial Catalog = TaxiBotApp20191219011147_db; Persist Security Info = False; User ID = Timur; Password = 29N05a96r; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;"
                ))
            {
                connection.Open();
                Init.TestInsert(connection);
                info = TestSelect(connection);
            }
        }

        public static string info;
        public static string TestSelect(QC.SqlConnection connection)
        {
            using (var command = new QC.SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                command.CommandText = @"  
SELECT test.colonka FROM test; ";

                QC.SqlDataReader reader = command.ExecuteReader();
                var builder = new StringBuilder();
                while (reader.Read())
                {
                    builder.Append(reader.GetInt32(0).ToString());
                }
                return builder.ToString();
            }
        }

         public static void TestInsert(QC.SqlConnection connection)
        {
            using (var command = new QC.SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                command.CommandText = @"  
INSERT INTO test (colonka)
VALUES (12);
INSERT INTO test (colonka)
VALUES (182);
INSERT INTO test (colonka)
VALUES (1882);
INSERT INTO test (colonka)
VALUES (18882);
INSERT INTO test (colonka)
VALUES (1992);
INSERT INTO test (colonka)
VALUES (162);";

                command.ExecuteScalar();
            }
        }

    }
}