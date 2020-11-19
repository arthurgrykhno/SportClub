using Microsoft.Data.SqlClient;
using SportClub.Interfaces;
using SportClub.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using Dapper;

namespace SportClub.Repositories
{
    public class GroupRepository : IGenericRepository<Group>
    {
        private readonly string _connectionString;

        public GroupRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public bool Create(Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group), "Parameter is null.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO [Group] (Name) VALUES(@Name)";
                int result = db.Execute(query, group);

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
                var query = "DELETE FROM [Group] WHERE [Id] = @id";
                int result = db.Execute(query, new { id });

                return result == 1;
            }
        }

        public Group Get(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id), "Parameter must be greater tahn zero.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Group] WHERE [Id] = @id";
                var group = db.Query<Group>(query, new { id }).FirstOrDefault();

                if (group == null)
                {
                    throw new NullReferenceException("Item with current id does not exist.");
                }

                return group;
            }
        }

        public List<Group> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [Group]";
                var groups = db.Query<Group>(query).ToList();

                return groups;
            }
        }

        public bool Update(Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group), "Parameter is null.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "UPDATE [Group] SET [Name] = @Name WHERE [Id] = @Id";
                var result = db.Execute(query, group);

                return result == 1;
            }
        }

        public List<object> GetGroupItemsCount()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT [Qualification], COUNT(*) AS Count " +
                            "FROM[Trainer] GROUP BY[Qualification]";

                var result = db.Query<object>(query).ToList();

                return result;
            }
        }

        public List<object> TrainingsCount()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT [Name], COUNT([Group].[Id]) AS [Count] FROM [Group] " +
                    "JOIN [Schedule] ON [Group].[Id] = [GroupId] GROUP BY [Name]";

                var result = db.Query<object>(query).ToList();

                return result;
            }
        }
    }
}
