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
                GetConnect = connection;
                //CreateTable(connection);
                InsertLocations(connection);
            }
        }

        private static QC.SqlConnection GetConnect;

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
                "Оренkkkбургский тракт 10А",
                "Улица Баумана",
                "Центр kсемьи Казан",
                "Национаkkkльный музей Республики Татарстан",
                "Ак Барс Банк Арена",
                "Проспекkт Победы 91",
                "Кремлевская 18",
                "улица Мkkkежлаука",
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
                    builder.Append("INSERT INTO Location (Id, NameOfPoint) VALUES (" + i + ",'" + list[i] + "');");//add single quotes
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

        public void SaveUser(Client user)
        {
            using (var connection = new QC.SqlConnection(
                "Server = tcp:taxibotapp20191219011147dbserver.database.windows.net,1433; Initial Catalog = TaxiBotApp20191219011147_db; Persist Security Info = False; User ID = Timur; Password = 29N05a96r; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;"
                ))
            {
                connection.Open();
                //                var commandSelect = new QC.SqlCommand();
                //                commandSelect.Connection = connection;
                //                commandSelect.CommandType = DT.CommandType.Text;
                //                commandSelect.CommandText = @"
                //SELECT Nickname FROM _User WHERE Nickname = @Nickname;";
                //                QC.SqlParameter parameterSelect;
                //                parameterSelect = new QC.SqlParameter("@Nickname", DT.SqlDbType.NVarChar, 100);
                //                parameterSelect.Value = user.Nickname;
                //                commandSelect.Parameters.Add(parameterSelect);
                //                QC.SqlDataReader reader = commandSelect.ExecuteReader();
                var commandAction = new QC.SqlCommand();
                commandAction.Connection = connection;
                commandAction.CommandType = DT.CommandType.Text;
                //hardcode
                //if (reader.GetString(0) == null)//new user
                //{

                commandAction.CommandText = @"  
INSERT INTO _User
(
    Id, Nickname
)
VALUES  
(
   NEXT VALUE FOR serial, @Nickname
); 
";
                QC.SqlParameter parameterAction;
                parameterAction = new QC.SqlParameter("@Nickname", DT.SqlDbType.NVarChar, 100);
                parameterAction.Value = "YADEBIL";
                commandAction.Parameters.Add(parameterAction);
                //                }
                //                else
                //                {
                //                    commandAction.CommandText = @"
                //UPDATE _User
                //SET CountOfTrip = CountOfTrip + 1,
                //WHERE Nickname = @Nickname;";
                //                    QC.SqlParameter parameterAction;
                //                    parameterAction = new QC.SqlParameter("@Nickname", DT.SqlDbType.NVarChar, 100);
                //                    parameterAction.Value = user.Nickname;
                //                    commandAction.Parameters.Add(parameterAction);
                //                }
                commandAction.ExecuteScalar();
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
    Id
    DeparturePointId,
    PlaceOfArrivalId,
    CountOfPeople,
    DepartureTime,
    RequestTime,
    UserId,
)
VALUES  
(
NEXT VALUE FOR serialR,
    @DeparturePointId,
    @PlaceOfArrivalId,
    @CountOfPeople,
    @DepartureTime,
    @RequestTime,
    @UserId,  
);


UPDATE HistoryOfLocation
SET CountOfDepartures = CountOfDepartures + 1,
AS his
WHERE EXISTS (SELECT * FROM Request WHERE his.Id = DeparturePointId);

UPDATE HistoryOfLocation
SET CountOfArrivals = CountOfArrivals + 1,
AS his
WHERE EXISTS (SELECT * FROM Request WHERE his.Id = PlaceOfArrivalId);
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