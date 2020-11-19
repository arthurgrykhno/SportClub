using Dapper;
using Microsoft.Data.SqlClient;
using SportClub.Interfaces;
using SportClub.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClub.Repositories
{
    public class TrainingRepository : IGenericRepository<Training>
    {
        private readonly string _connectionString;

        public TrainingRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public bool Create(Training training)
        {
            if (training == null)
            {
                throw new ArgumentNullException(nameof(training), "Parameter is null.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO [Training] (Name) VALUES(@Name)";
                int result = db.Execute(query, training);

                return result == 1;
            }
        }

        public bool Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id), "Parameter must be greater tahn zero.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM [Training] WHERE [Id] = @id";
                int result = db.Execute(query, new { id });

                return result == 1;
            }
        }

        public Training Get(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id), "Parameter must be greater tahn zero.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Training] WHERE [Id] = @id";
                var training = db.Query<Training>(query, new { id }).FirstOrDefault();

                if (training == null)
                {
                    throw new NullReferenceException("Item with current id does not exist.");
                }

                return training;
            }
        }

        public List<Training> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Training]";
                var trainings = db.Query<Training>(query).ToList();

                return trainings;
            }
        }

        public bool Update(Training training)
        {
            if (training == null)
            {
                throw new ArgumentNullException(nameof(training), "Parameter is null.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "UPDATE [Training] SET [Name] = @Name WHERE [Id] = @Id";
                var result = db.Execute(query, training);

                return result == 1;
            }
        }

        public List<Training> GetTrainingsBetweenDateSet(int id, DateTime startDate, DateTime finishDate)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Training] WHERE [Id] IN " +
                            "(SELECT [TrainingId] FROM [Schedule] WHERE [GroupId] = " +
                            "(SELECT [GroupId] FROM [Subscription] WHERE [Id] = @id) " +
                            "AND [Date] BETWEEN @startDate AND @finishDate)";

                var trainings = db.Query<Training>(query, new { id, startDate, finishDate }).ToList();

                return trainings;
            }
        }

        public int CountOfTrainingsBetweenDates(DateTime startDate, DateTime finishDate)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT COUNT(*) FROM [Schedule] WHERE [Date] BETWEEN @startDate AND @finishDate";

                int count = db.Query<int>(query, new { startDate, finishDate }).FirstOrDefault();

                return count;
            }
        }

        public void InsertColumn()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "ALTER TABLE [Training] ADD [New] NVARCHAR(50) NULL";
                db.Execute(query);
            }
        }
    }
}
