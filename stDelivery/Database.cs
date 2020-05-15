using Android.Util;
using System.Collections.Generic;

/**************************************************************************
 *                                                                        *
 *  File:        Database.cs                                              *
 *  Copyright:   (c) 2020, Zalinca Claudiu                                *
 *  E-mail:      claudiu-serban.zalinca@student.tuiasi.ro                 *
 *  Website:     http://127.0.0.1                                         *
 *  Description: The class that handles the database                      *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

namespace stDelivery
{
    /// <summary>
    /// Class that provides methods to handle the database
    /// </summary>
    public class Database
    {
        
        private static Database instance;
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        private Database()
        {

        }

        /// <summary>
        /// Receive the instance of the database -> Singleton pattern
        /// </summary>
        public static Database Instance()
        {
            if (instance == null)
                instance = new Database();
            return instance;
        }

        /// <summary>
        /// Method that makes the connection with the database and creates a table after User class model in case it doesn't exist
        /// </summary>
        /// <returns>a bool that represents if the method has been executed successfully</returns>
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

        /// <summary>
        /// Method that introduce a new user in the database
        /// </summary>
        /// <param name="user">The user we want to introduce in the database</param>
        /// <returns>a bool that represents if the method has been executed successfully</returns>
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

        /// <summary>
        /// Method that returns all the user from database as a list
        /// </summary>
        /// <returns>list of all users from database</returns>
        public List<User> SelectUsers()
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
                Log.Info("Eroare la returnarea tuturor userilor!!!", e.Message);
                return null;
            }
        }

        /// <summary>
        /// Method that updates an user account from database
        /// </summary>
        /// <param name="user">The user we want to update with some new information</param>
        /// <returns>a bool that represents if the method has been executed successfully</returns>
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
                Log.Info("Eroare la update-ul userului!!!", e.Message);
                return false;
            }
        }

        /// <summary>
        /// Delete a specific user from table
        /// </summary>
        /// <param name="user">User to be deleted</param>
        /// <returns>a bool that represents if the method has been executed successfully</returns>
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
                Log.Info("Eroare la stergerea userului!!!", e.Message);
                return false;
            }
        }

        /// <summary>
        /// Method that returns the user with the id introduced as a argument
        /// </summary>
        /// <param name="Id">Id of the user needed to be returned</param>
        /// <returns>a bool that represents if the method has been executed successfully</returns>
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
                Log.Info("Eroare la returnarea userului!!!", e.Message);
                return false;
            }
        }

        /// <summary>
        /// Method that checks if exists an account with the specific email and password in the table
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <param name="parola">Password of the user</param>
        /// <returns>a bool that represents if exists an account with the informations introduced or not</returns>
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
                Log.Info("Eroare!!!", e.Message);
                return -1;
            }
        }

        /// <summary>
        /// Method that returns the user with the specific email and password 
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <param name="parola">Password of the user</param>
        /// <returns>the user specified if exists otherwise null</returns>
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
                Log.Info("Eroare la returnarea userului!!!", e.Message);
                return null;
            }
        }

        /// <summary>
        /// Method that drops the table with all the users registered
        /// </summary>
        /// <returns>a bool that represents if the method has been executed successfully</returns>
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
                Log.Info("Eroare la stergerea tabelei!!!", e.Message);
                return false;
            }
        }

        /// <summary>
        /// Method that returns the number of users which have the email specified in arguments
        /// </summary>
        /// <param name="email">the email needed to be cheked if exists</param>
        /// <returns>number of users with the email introduced as an argument, in case of error -1</returns>
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
                Log.Info("Eroare la verificare!!!", e.Message);
                return -1;
            }
        }

        /// <summary>
        /// Method that returns the number of the all users registered
        /// </summary>
        /// <returns>number of all users</returns>
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
                Log.Info("Eroare!!!", e.Message);
                return -1;
            }
        }

    }
}