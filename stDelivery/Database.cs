using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace stDelivery
{
    public class Database
    {

        private static Database instance;
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        private Database()
        {

        }

        public static Database Instance()
        {
            if (instance == null)
                instance = new Database();
            return instance;
        }
        public bool createDatabase()
        {
            try
            {
                var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(path,"users.db"));
                connection.CreateTable<User>();
                return true;
            }
            catch (SQLite.SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool InsertUserIntoTable(User user)
        {
            try
            {
                using (var connection=new SQLite.SQLiteConnection(System.IO.Path.Combine(path, "users.db")))
                {
                    connection.Insert(user);
                }
                Log.Debug("my app","Introdus cu succes");
                return true;
            }
            catch (SQLite.SQLiteException e)
            {
                Log.Info("Eroare la introducere userului!!!", e.Message);
                return false;
            }
        }

        public List<User> SelectUsers(User user)
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(path, "users.db")))
                {
                    return connection.Table<User>().ToList();
                }
                
            }
            catch (SQLite.SQLiteException e)
            {
                Log.Info("Eroare la introducere userului!!!", e.Message);
                return null;
            }
        }
        
        public bool UpdateUserIntoTable(User user)
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(path, "users.db")))
                {
                    connection.Query<User>("UPDATE User set Nume=?,Email=?,Telefon=?,Adresa=? Where Id=?", user.Nume, user.Email, user.Telefon, user.Adresa, user.Id);
                }
                return true;
            }
            catch (SQLite.SQLiteException e)
            {
                Log.Info("Eroare la introducere userului!!!", e.Message);
                return false;
            }
        }

        public bool DeleteUserIntoTable(User user)
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(path, "users.db")))
                {
                    connection.Delete(user);
                }
                return true;
            }
            catch (SQLite.SQLiteException e)
            {
                Log.Info("Eroare la introducere userului!!!", e.Message);
                return false;
            }
        }

        public bool SelectUserById(int Id)
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(path, "users.db")))
                {
                    connection.Query<User>("SELECT * FROM User where Id=?",Id);
                }
                return true;
            }
            catch (SQLite.SQLiteException e)
            {
                Log.Info("Eroare la introducere userului!!!", e.Message);
                return false;
            }
        }

        public int userExists(string email, string parola)
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(path, "users.db")))
                {
                    List<User> users=connection.Query<User>("SELECT * FROM User where Email=? AND Parola=?", email,parola);
                    return users.Count;
                }
            }
            catch (SQLite.SQLiteException e)
            {
                Log.Info("Eroare la introducere userului!!!", e.Message);
                return -1;
            }
        }

        public User getCurrentUser(string email, string parola)
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(path, "users.db")))
                {
                    List<User> users = connection.Query<User>("SELECT * FROM User where Email=? AND Parola=?", email, parola);
                    if (users.Count == 1)
                    {
                        return users[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (SQLite.SQLiteException e)
            {
                Log.Info("Eroare la introducere userului!!!", e.Message);
                return null;
            }
        }

        public bool clearDatabase()
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(path, "users.db")))
                {
                    connection.Execute("DROP TABLE User");
                    return true;
                }
            }
            catch (SQLite.SQLiteException e)
            {
                Log.Info("Eroare la introducere userului!!!", e.Message);
                return false;
            }
        }

        public int emailExist(string email)
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(path, "users.db")))
                {
                    List<User> users = connection.Query<User>("SELECT * FROM User where Email=?", email);
                    return users.Count;
                }
            }
            catch (SQLite.SQLiteException e)
            {
                Log.Info("Eroare la introducere userului!!!", e.Message);
                return -1;
            }
        }

        public int userNumber()
        {
            try
            {
                using (var connection = new SQLite.SQLiteConnection(System.IO.Path.Combine(path, "users.db")))
                {
                    List<User> users = connection.Query<User>("SELECT * FROM User");
                    return users.Count;
                }
            }
            catch (SQLite.SQLiteException e)
            {
                Log.Info("Eroare la introducere userului!!!", e.Message);
                return -1;
            }
        }

    }
}