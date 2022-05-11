using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cursach.DTO;
using Cursach.Tools;
using Cursach.Данные;

namespace Cursach.Model
{
    public class SqlModel
    {
        private SqlModel() { }
        static SqlModel sqlModel;
        public static SqlModel GetInstance()
        {
            if (sqlModel == null)
                sqlModel = new SqlModel();
            return sqlModel;
        }

        public List<Employ> EmployCreate(int skip, int count)
        {
            var employs = new List<Employ>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `employer` LIMIT {skip},{count}";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        employs.Add(new Employ
                        {
                            ID = dr.GetInt32("id"),
                            FirstName = dr.GetString("firstname"),
                            LastName = dr.GetString("lastname"),
                            Ochestvo = dr.GetString("ochestvo"),
                            Adress = dr.GetString("adress"),
                            PostId = dr.GetInt32("post_id"),
                            DepartmentId = dr.GetInt32("department_id")
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return employs;
        }


        public List<User> SelectUser(int skip, int count)
        {
            var users = new List<User>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `user` LIMIT {skip},{count}";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        users.Add(new User
                        {
                            ID = dr.GetInt32("id"),
                            LastName = dr.GetString("lastname"),
                            PhoneNumber = dr.GetString("phonenumber")                        
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return users;
        }



        internal List<Post> SelectPostByDepartment(Department selectedDep)
        {
            int id = selectedDep?.ID ?? 0;
            var posts = new List<Post>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `post` WHERE department_id = {id}";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        posts.Add(new Post
                        {
                            ID = dr.GetInt32("id"),
                            Name = dr.GetString("name"),
                            DepartmentId = dr.GetInt32("department_id")                         
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return posts;
        }

        internal List<Employ> EmploysByPost(Post selectedPost)
        {
            int id = selectedPost?.ID ?? 0;
            var employs = new List<Employ>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `employer` WHERE post_id = {id}";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        employs.Add(new Employ
                        {
                            ID = dr.GetInt32("id"),
                            FirstName = dr.GetString("firstname"),
                            LastName = dr.GetString("lastname"),
                            Ochestvo = dr.GetString("ochestvo"),
                            Adress = dr.GetString("adress"),
                            PostId = dr.GetInt32("post_id")

                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return employs;
        }

        public List<Post> PostCreate(int skip, int count)
        {
            var posts = new List<Post>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `post` LIMIT {skip},{count}";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        posts.Add(new Post
                        {
                            ID = dr.GetInt32("id"),
                            Name = dr.GetString("name"),

                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return posts;
        }

        public List<Department> DepartmentCreate(int skip, int count)
        {
            var deps = new List<Department>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `department` LIMIT {skip},{count}";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        deps.Add(new Department
                        {
                            ID = dr.GetInt32("id"),
                            Title = dr.GetString("title"),
                            
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return deps;
        }

        public List<User> UserCreate(int skip, int count)
        {
            var users = new List<User>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `user` LIMIT {skip},{count}";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        users.Add(new User
                        {
                            ID = dr.GetInt32("id"),
                            LastName = dr.GetString("lastname"),
                            PhoneNumber = dr.GetString("phonenumber")
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return users;
        }

        public int Insert<T>(T value)
        {
            string table;
            List<MetaValue> values;
            GetMetaData(value, out table, out values);
            var query = CreateInsertQuery(table, values);
            var db = MySqlDB.GetDB();
            // лучше эти 2 запроса объединить в один с помощью транзакции
            int id = db.GetNextID(table);
            db.ExecuteNonQuery(query.Item1, query.Item2);
            return id;
        }
        // обновляет объект в бд по его id
        public void Update<T>(T value) where T : BaseDTO
        {
            string table;
            List<MetaValue> values;
            GetMetaData(value, out table, out values);
            var query = CreateUpdateQuery(table, values, value.ID);
            var db = MySqlDB.GetDB();
            db.ExecuteNonQuery(query.Item1, query.Item2);
        }

        public void Delete<T>(T value) where T : BaseDTO
        {
            var type = value.GetType();
            string table = GetTableName(type);
            var db = MySqlDB.GetDB();
            string query = $"delete from `{table}` where id = {value.ID}";
            db.ExecuteNonQuery(query);
        }

        public int GetNumRows(Type type)
        {
            string table = GetTableName(type);
            return MySqlDB.GetDB().GetRowsCount(table);
        }

        private static string GetTableName(Type type)
        {
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            return ((TableAttribute)tableAtrributes.First()).Table;
        }

        

        private static (string, MySqlParameter[]) CreateInsertQuery(string table, List<MetaValue> values)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"INSERT INTO `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            return (stringBuilder.ToString(), parameters.ToArray());
        }

        private static (string, MySqlParameter[]) CreateUpdateQuery(string table, List<MetaValue> values, int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            stringBuilder.Append($" WHERE id = {id}");
            return (stringBuilder.ToString(), parameters.ToArray());
        }

        private static List<MySqlParameter> InitParameters(List<MetaValue> values, StringBuilder stringBuilder)
        {
            var parameters = new List<MySqlParameter>();
            int count = 1;
            var rows = values.Select(s =>
            {
                parameters.Add(new MySqlParameter($"p{count}", s.Value));
                return $"{s.Name} = @p{count++}";
            });
            stringBuilder.Append(string.Join(",", rows));
            return parameters;
        }

        private static void GetMetaData<T>(T value, out string table, out List<MetaValue> values)
        {
            var type = value.GetType();
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            table = ((TableAttribute)tableAtrributes.First()).Table;
            values = new List<MetaValue>();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var columnAttributes = prop.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (columnAttributes.Length > 0)
                {
                    string column = ((ColumnAttribute)columnAttributes.First()).Column;
                    values.Add(new MetaValue { Name = column, Value = prop.GetValue(value) });
                }
            }
        }

        class MetaValue
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }
    }
}
