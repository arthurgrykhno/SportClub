using Dapper;
using Microsoft.Data.SqlClient;
using SportClub.Interfaces;
using SportClub.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SportClub.Repositories
{
    public class TrainerRepository : IGenericRepository<Trainer>
    {
        private readonly string _connectionString;

        public TrainerRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public bool Create(Trainer trainer)
        {
            if (trainer == null)
            {
                throw new ArgumentNullException(nameof(trainer), "Parameter is null.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO [Trainer] (FullName, Qualification, Age) " +
                    " VALUES(@FullName, @Qualification, @Age)";
                int result = db.Execute(query, trainer);

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
                var query = "DELETE FROM [Trainer] WHERE [Id] = @id";
                int result = db.Execute(query, new { id });

                return result == 1;
            }
        }

        public Trainer Get(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id), "Parameter must be greater tahn zero.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Trainer] WHERE [Id] = @id";
                var trainer = db.Query<Trainer>(query, new { id }).FirstOrDefault();

                if (trainer == null)
                {
                    throw new NullReferenceException("Item with current id does not exist.");
                }

                return trainer;
            }
        }

        public List<Trainer> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Trainer]";
                var trainers = db.Query<Trainer>(query).ToList();

                return trainers;
            }
        }

        public bool Update(Trainer trainer)
        {
            if (trainer == null)
            {
                throw new ArgumentNullException(nameof(trainer), "Parameter is null.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "UPDATE [Trainer] SET [FullName] = @FullName, " +
                    "[Qualification] = @Qualification, [Age] = @Age WHERE [Id] = @Id";
                var result = db.Execute(query, trainer);

                return result == 1;
            }
        }

        public List<Trainer> GetAllOrderedByQualification(bool isAscending)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string condition = isAscending == true ? "ASC" : "DESC";
                var query = "SELECT * FROM [Trainer] ORDER BY Qualification " + condition;
                var trainers = db.Query<Trainer>(query).ToList();

                return trainers;
            }
        }

        public List<Trainer> GetAllForTraining(int trainingId)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Trainer] WHERE " +
                            "[Id] IN (SELECT  [TrainerId] FROM [Schedule] WHERE [TrainingId] = @trainingId)";

                var trainers = db.Query<Trainer>(query, new { trainingId }).ToList();

                return trainers;
            }   
        }

        public int GetCountOfTrainers()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT COUNT(*) FROM [Trainer]";

                int count = db.Query<int>(query).FirstOrDefault();

                return count;
            }
        }

        public int GetCountOfGroupsOfCurrentTrainer(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT COUNT(DISTINCT[GroupId]) FROM[Schedule] WHERE[TrainerId] = @id";

                int count = db.Query<int>(query, new { id }).FirstOrDefault();

                return count;
            }
        }

        public List<object> GetQualificationCount()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT [Qualification], COUNT(*) AS Count " +
                            "FROM[Trainer] GROUP BY[Qualification]";

                var result = db.Query<object>(query).ToList();

                return result;
            }
        }

        public List<Trainer> GetAllTrainingsMoreThan(int count)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Trainer] WHERE [Id] IN" +
                    "(SELECT[TrainerId] FROM[Schedule] GROUP BY[TrainerId]" +
                    " HAVING COUNT(TrainerId) > @count)";

                var trainers = db.Query<Trainer>(query, new { count }).ToList();

                return trainers;
            }
        }

        public List<Trainer> GetEldest()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT [FullName], [Age] FROM [Trainer]" +
                    " WHERE Age = (SELECT MAX([Age]) FROM Trainer)";

                var trainers = db.Query<Trainer>(query).ToList();

                return trainers;
            }
        }
        
        public List<Trainer> GetWithMinlQualification()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT [FullName], [Qualification] FROM [Trainer] WHERE [Qualification] = 'CMS' ";

                var trainers = db.Query<Trainer>(query).ToList();

                return trainers;
            }
        }
    }
}
