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
    public class SubscriptionRepository : IGenericRepository<Subscription>
    {
        private readonly string _connectionString;

        public SubscriptionRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public bool Create(Subscription subscription)
        {
            if (subscription == null)
            {
                throw new ArgumentNullException(nameof(subscription), "Parameter is null.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO [Subscription] (FullName, GroupId, Attends)" +
                    " VALUES(@FullName, @GroupId, @Attends)";
                int result = db.Execute(query, subscription);

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
                var query = "DELETE FROM [Subscription] WHERE [Id] = @id";
                int result = db.Execute(query, new { id });

                return result == 1;
            }
        }

        public Subscription Get(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id), "Parameter must be greater tahn zero.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Subscription] WHERE [Id] = @id";
                var subscription = db.Query<Subscription>(query, new { id }).FirstOrDefault();

                if (subscription == null)
                {
                    throw new NullReferenceException("Item with current id does not exist.");
                }

                return subscription;
            }
        }

        public List<Subscription> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Subscription]";
                var subscriptions = db.Query<Subscription>(query).ToList();

                return subscriptions;
            }
        }

        public bool Update(Subscription subscription)
        {
            if (subscription == null)
            {
                throw new ArgumentNullException(nameof(subscription), "Parameter is null.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "UPDATE [Subscription] SET [FullName] = @FullName, " +
                    "[GroupId] = @GroupId, [Attends] = @Attends WHERE [Id] = @Id";
                var result = db.Execute(query, subscription);

                return result == 1;
            }
        }

        public List<Subscription> GetListOfGroup(string groupName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [Subscription] WHERE [GroupId] IN " +
                    "(SELECT [Id] FROM [Group] WHERE [Name] = @groupName)";
                
                var groupList = (List<Subscription>)db.Query<Subscription>(query, new { groupName });
                return groupList;
            }
        }

        public List<Subscription> GetAllWithAttendsSorting(int attends)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Subscription] WHERE [Attends] > @attends";

                var subscriptions = (List<Subscription>)db.Query<Subscription>(query, new { attends });

                return subscriptions;
            }
        }

        public int GetCountOfSubscriptions()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT COUNT(*) FROM [Subscription]";

                int subscriptions = db.Query<int>(query).FirstOrDefault();

                return subscriptions;
            }
        }

    }
}
