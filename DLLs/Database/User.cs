/**************************************************************************
 *                                                                        *
 *  File:        User.cs                                                  *
 *  Copyright:   (c) 2020, Zalinca Claudiu                                *
 *  E-mail:      claudiu-serban.zalinca@student.tuiasi.ro                 *
 *  Website:     http://127.0.0.1                                         *
 *  Description: The model for database                                   *
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
    /// Class used as a model for the user informations in the database
    /// It is used to manipulate the informations provided by the user
    /// </summary>
    public class User
    {
        [SQLite.PrimaryKey,SQLite.AutoIncrement]
        public int Id { get; set; }
        [SQLite.NotNull]
        public string Nume { get; set; }
        [SQLite.NotNull]
        public string Email { get; set; }
        [SQLite.NotNull]
        public string Adresa  { get; set; }
        [SQLite.NotNull]
        public string Telefon { get; set; }
        [SQLite.NotNull]
        public string Parola { get; set; }

        
    
    }
}