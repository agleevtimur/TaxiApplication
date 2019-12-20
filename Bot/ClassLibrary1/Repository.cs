using System;  // C# , ADO.NET
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataBase.Classes;
using DataBase.Repository;
using Microsoft;
using DT = System.Data;            // System.Data.dll  
using QC = Microsoft.Data.SqlClient;  // System.Data.dll  

namespace Library
{
    public class Repository : IRepository
    {
        public static void Start()
        {
            using (var connection = new QC.SqlConnection(
                "Server = tcp:taxibotapp20191219011147dbserver.database.windows.net,1433; Initial Catalog = TaxiBotApp20191219011147_db; Persist Security Info = False; User ID = Timur; Password = 29N05a96r; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;"
                ))
            {
                connection.Open();
                //CreateTable(connection);
                InsertLocations(connection);
            }
        }

        private static void CreateTable(QC.SqlConnection connection)
        {
            using (var command = new QC.SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                command.CommandText = @"  
CREATE TABLE IF NOT EXISTS Location
(
    Id SERIAL PRIMARY KEY, 
    NameOfPoint VARCHAR(255) NOT NULL
);
CREATE TABLE IF NOT EXISTS User
(
    Id SERIAL PRIMARY KEY, 
    Nickname VARCHAR(100) NOT NULL,
    CountOfTrip INTEGER DEFAULT 0
);
CREATE TABLE IF NOT EXISTS Request
(
    Id SERIAL PRIMARY KEY, 
    DeparturePointId INTEGER,
    PlaceOfArrivalId INTEGER,
    CountOfPeople INTEGER DEFAULT 0,
    DepartureTime DATE,
    RequestTime DATE,
    UserId INTEGER REFERENCES User (Id),
    FOREIGN KEY (DeparturePointId, PlaceOfArrivalId) REFERENCES Location (Id)
);
CREATE TABLE IF NOT EXISTS HistoryOfRequest
(
    Id SERIAL PRIMARY KEY, 
    DeparturePointId INTEGER,
    PlaceOfArrivalId INTEGER,
    CountOfPeople INTEGER DEFAULT 0,
    DepartureTime DATE,
    RequestTime DATE,
    UserId INTEGER,
);
CREATE TABLE IF NOT EXISTS HistoryOfLocation
(
    Id SERIAL PRIMARY KEY, 
    NameOfPoint VARCHAR(255) NOT NULL,
    CountOfDepartures INTEGER DEFAULT 0,
    CountOfArrivals INTEGER DEFAULT 0,
);
";
                command.ExecuteScalar();

            }
        }

        private static void InsertLocations(QC.SqlConnection connection)
        {


            using (var command = new QC.SqlCommand())
            {
                var list = new List<string>
            {
                "Деревня Универсиады",
                "Кремлевская 35А",
                "КСК Уникс",
                "ТЦ Кольцо",
                "Кремлевская 16А",
                "Казанский Кремль",
                "Пушкина 32А",
                "Оренбургский тракт 10А",
                "Улица Баумана",
                "Центр семьи Казан",
                "Национальный музей Республики Татарстан",
                "Ак Барс Банк Арена",
                "Проспект Победы 91",
                "Кремлевская 18",
                "улица Межлаука",
                "улица Лево-Булочная",
                "Бустан",
                "Карла Маркса 10",
                "Карла Маркса 68"
            };
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                var builder = new StringBuilder();
                for (var i = 0; i < list.Count; i++)
                {
                    builder.Append("INSERT INTO Location (Id, NameOfPoint) VALUES (" + i + ",'" + list[i] + "');");
                }
                command.CommandText = builder.ToString();
                command.ExecuteScalar();
                builder.Clear();

                for (var i = 0; i < list.Count; i++)
                {
                    builder.Append("INSERT INTO HistoryOfLocation (Id, NameOfPoint) VALUES (" + i + ",'" + list[i] + "');");
                }
                command.CommandText = builder.ToString();
                command.ExecuteScalar();
            }
        }

        public void SaveUser(User user)
        {
            using (var connection = new QC.SqlConnection(
                "Server = tcp:taxibotapp20191219011147dbserver.database.windows.net,1433; Initial Catalog = TaxiBotApp20191219011147_db; Persist Security Info = False; User ID = Timur; Password = 29N05a96r; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;"
                ))
            {
                connection.Open();
                var command = new QC.SqlCommand();
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                command.CommandText = @"
SELECT Nickname FROM User WHERE Nickname = " + user.Nickname ;
                QC.SqlDataReader reader = command.ExecuteReader();
                if (reader.GetString(0) == null)
                {
                    command.CommandText = @"  
INSERT INTO User
(
    Nickname
)
VALUES  
(
    @Nickname
); 
";
                    QC.SqlParameter parameter;
                    parameter = new QC.SqlParameter("@Nickname", DT.SqlDbType.NVarChar, 100);
                    parameter.Value = user.Nickname;
                    command.Parameters.Add(parameter);
                }
                else 
                {
                    command.CommandText = @"
UPDATE User
SET CountOfTrip = CountOfTrip + 1,
WHERE Nickname = " + user.Nickname + ";";
                }
                command.ExecuteScalar();
            }
        }

        public void SaveRequest(Request request)
        {
            using (var connection = new QC.SqlConnection(
                "Server = tcp:taxibotapp20191219011147dbserver.database.windows.net,1433; Initial Catalog = TaxiBotApp20191219011147_db; Persist Security Info = False; User ID = Timur; Password = 29N05a96r; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;"
                ))
            {
                connection.Open();
                var command = new QC.SqlCommand();
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                command.CommandText = @"  
INSERT INTO Request 
(
    DeparturePointId,
    PlaceOfArrivalId,
    CountOfPeople,
    DepartureTime,
    RequestTime,
    UserId,
)
VALUES  
(
    @DeparturePointId,
    @PlaceOfArrivalId,
    @CountOfPeople,
    @DepartureTime,
    @RequestTime,
    @UserId,  
); 
";
                QC.SqlParameter parameter;
                parameter = new QC.SqlParameter("@PlaceOfArrivalId", DT.SqlDbType.Int);
                parameter.Value = request.PlaceOfArrivalId;
                command.Parameters.Add(parameter);
                parameter = new QC.SqlParameter("@DeparturePointId", DT.SqlDbType.Int);
                parameter.Value = request.DeparturePointId;
                command.Parameters.Add(parameter);
                parameter = new QC.SqlParameter("@CountOfPeople", DT.SqlDbType.Int);
                parameter.Value = request.CountOfPeople;
                command.Parameters.Add(parameter);
                parameter = new QC.SqlParameter("@DepartureTime", DT.SqlDbType.Date);
                parameter.Value = request.DepartureTime;
                command.Parameters.Add(parameter);
                parameter = new QC.SqlParameter("@RequestTime", DT.SqlDbType.Date);
                parameter.Value = request.RequestTime;
                command.Parameters.Add(parameter);
                parameter = new QC.SqlParameter("@UserId", DT.SqlDbType.Int);
                parameter.Value = request.UserId;
                command.Parameters.Add(parameter);
                command.CommandText = @"
UPDATE HistoryOfLocation
SET CountOfDepartures = CountOfDepartures + 1,
AS his
WHERE EXISTS (SELECT * FROM Request WHERE his.Id = DeparturePointId);

UPDATE HistoryOfLocation
SET CountOfArrivals = CountOfArrivals + 1,
AS his
WHERE EXISTS (SELECT * FROM Request WHERE his.Id = PlaceOfArrivalId);

";
                command.ExecuteScalar();
            }
        }

        public void DeleteRequest(int id)
        {
            using (var connection = new QC.SqlConnection(
                "Server = tcp:taxibotapp20191219011147dbserver.database.windows.net,1433; Initial Catalog = TaxiBotApp20191219011147_db; Persist Security Info = False; User ID = Timur; Password = 29N05a96r; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;"
                ))
            {
                connection.Open();
                var command = new QC.SqlCommand();
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                command.CommandText = @"
SELECT * FROM Request WHERE Id = " + id.ToString() + ";";

                QC.SqlDataReader reader = command.ExecuteReader();
                var request = new Request(
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3),
                    reader.GetDateTime(4),
                    reader.GetDateTime(5),
                    reader.GetInt32(0),
                    reader.GetInt32(6));
                //var request = new Request { 
                //    Id = reader.GetInt32(0),
                //    DeparturePointId = reader.GetInt32(1),
                //    PlaceOfArrivalId = reader.GetInt32(2),
                //    CountOfPeople = reader.GetInt32(3),
                //    DepartureTime = reader.GetDateTime(4),
                //    RequestTime = reader.GetDateTime(5),
                //    UserId = reader.GetInt32(6)
                //};
                command.CommandText = @"  
DELETE FROM Request
WHERE Id = " + id.ToString() + ";";

                command.CommandText = @"  
INSERT INTO HistoryOfRequest 
(
    Id,
    DeparturePointId,
    PlaceOfArrivalId,
    CountOfPeople,
    DepartureTime,
    RequestTime,
    UserId,
)
VALUES  
(
    @Id,
    @DeparturePointId,
    @PlaceOfArrivalId,
    @CountOfPeople,
    @DepartureTime,
    @RequestTime,
    @UserId,  
); 
";
                QC.SqlParameter parameter;
                parameter = new QC.SqlParameter("@PlaceOfArrivalId", DT.SqlDbType.Int);
                parameter.Value = request.PlaceOfArrivalId;
                command.Parameters.Add(parameter);
                parameter = new QC.SqlParameter("@DeparturePointId", DT.SqlDbType.Int);
                parameter.Value = request.DeparturePointId;
                command.Parameters.Add(parameter);
                parameter = new QC.SqlParameter("@CountOfPeople", DT.SqlDbType.Int);
                parameter.Value = request.CountOfPeople;
                command.Parameters.Add(parameter);
                parameter = new QC.SqlParameter("@DepartureTime", DT.SqlDbType.Date);
                parameter.Value = request.DepartureTime;
                command.Parameters.Add(parameter);
                parameter = new QC.SqlParameter("@RequestTime", DT.SqlDbType.Date);
                parameter.Value = request.RequestTime;
                command.Parameters.Add(parameter);
                parameter = new QC.SqlParameter("@UserId", DT.SqlDbType.Int);
                parameter.Value = request.UserId;
                command.Parameters.Add(parameter);
                parameter = new QC.SqlParameter("@Id", DT.SqlDbType.Int);
                parameter.Value = request.Id;
                command.Parameters.Add(parameter);                
                command.ExecuteScalar();
            }
        }

        public List<Location> GetLocations()
        {
            using (var connection = new QC.SqlConnection(
                "Server = tcp:taxibotapp20191219011147dbserver.database.windows.net,1433; Initial Catalog = TaxiBotApp20191219011147_db; Persist Security Info = False; User ID = Timur; Password = 29N05a96r; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;"
                ))
            {
                connection.Open();
                var command = new QC.SqlCommand();
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                command.CommandText = @"
SELECT * FROM Location";

                QC.SqlDataReader reader = command.ExecuteReader();
                var list = new List<Location>();
                while (reader.Read())
                {
                    list.Add(new Location { Id = reader.GetInt32(0), NameOfPoint = reader.GetString(1) });
                }
                return list;
            }
        }

        public List<Request> GetRequests()
        {
            using (var connection = new QC.SqlConnection(
                "Server = tcp:taxibotapp20191219011147dbserver.database.windows.net,1433; Initial Catalog = TaxiBotApp20191219011147_db; Persist Security Info = False; User ID = Timur; Password = 29N05a96r; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;"
                ))
            {
                connection.Open();
                var command = new QC.SqlCommand();
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                command.CommandText = @"
SELECT * FROM Request";

                QC.SqlDataReader reader = command.ExecuteReader();
                var list = new List<Request>();
                while (reader.Read())
                {

                    var request = new Request(
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetInt32(3),
                        reader.GetDateTime(4),
                        reader.GetDateTime(5),
                        reader.GetInt32(0),
                        reader.GetInt32(6));
                    list.Add(request
                    //    new Request
                    //{
                    //    Id = reader.GetInt32(0), 
                    //    DeparturePointId = reader.GetInt32(1), 
                    //    PlaceOfArrivalId = reader.GetInt32(2), 
                    //    CountOfPeople = reader.GetInt32(3),
                    //    DepartureTime = reader.GetDateTime(4),
                    //    RequestTime = reader.GetDateTime(5),
                    //    UserId = reader.GetInt32(6)
                    //}
                        );
                }
                return list;
            }
        }
    }
}