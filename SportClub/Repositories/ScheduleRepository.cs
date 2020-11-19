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
    public class ScheduleRepository : IGenericRepository<Schedule>
    {
        private readonly string _connectionString;

        public ScheduleRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public bool Create(Schedule schedule)
        {
            if (schedule == null)
            {
                throw new ArgumentNullException(nameof(schedule), "Parameter is null.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO [Schedule] (Date, Time, TrainerId, TrainingId, GroupId)" +
                    " VALUES (@Date, @Time, @TrainerId, @TrainingId, @GroupId)";
                int result = db.Execute(query, schedule);

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
                var query = "DELETE FROM [Schedule] WHERE [Id] = @id";
                int result = db.Execute(query, new { id });

                return result == 1;
            }
        }

        public Schedule Get(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id), "Parameter must be greater tahn zero.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Schedule] WHERE [Id] = @id";
                var schedule = db.Query<Schedule>(query, new { id }).FirstOrDefault();

                if (schedule == null)
                {
                    throw new NullReferenceException("Item with current id does not exist.");
                }

                return schedule;
            }
        }

        public List<Schedule> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Schedule]";
                var schedule = db.Query<Schedule>(query).ToList();

                return schedule;
            }
        }

        public bool Update(Schedule schedule)
        {
            if (schedule == null)
            {
                throw new ArgumentNullException(nameof(schedule), "Parameter is null.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "UPDATE [Schedule] SET [Date] = @Date, [Time] = @Time," +
                    " [TrainerId] = @TrainerId, [TrainingId] = @TrainingId, [GroupId] = @GroupId" +
                    " WHERE [Id] = @Id";
                var result = db.Execute(query, schedule);

                return result == 1;
            }
        }

        public List<Schedule> GetScheduleForTrainer(int trainerId, bool isAscending)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string condition = isAscending == true ? "ASC" : "DESC";
                var query = "SELECT * FROM [Schedule] WHERE [TrainerId] = @trainerId ORDER BY [Date] " + condition;

                var schedules = db.Query<Schedule>(query, new { trainerId }).ToList();

                return schedules;
            }
        }

        public int GetPopulationAtCurrentTraining(int groupId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT COUNT(*) FROM [Subscription] WHERE [GroupId] =" +
                            "(SELECT DISTINCT[GroupId] FROM[Schedule] WHERE[GroupId] = @groupId)";

                int count = db.Query<int>(query, new { groupId }).FirstOrDefault();

                return count;
            }
        }
    }
}
