using System;
using System.Security.Cryptography;
using System.Text;

/**************************************************************************
 *                                                                        *
 *  File:        Crypt.cs                                                 *
 *  Copyright:   (c) 2020, Zalinca Claudiu                                *
 *  E-mail:      claudiu-serban.zalinca@student.tuiasi.ro                 *
 *  Website:     http://127.0.0.1                                         *
 *  Description: Class that contains the function for crypting the        *
 *               password                                                 *
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
    /// Class that contains methods for crypting
    /// </summary>
    public class Crypt
    {
        /// <summary>
        /// Method that crypts a string using SHA256 algorithm
        /// Method used to introduce the crypted password in database and to check it in case of login
        /// </summary>
        /// <param name="pass">The string we want to crypt</param>
        /// <returns>the password crypted</returns>
        public static string SHA256hash(string pass)
        {
            SHA256 hash = SHA256Managed.Create();
            Byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(pass));
            StringBuilder sb = new StringBuilder();
            foreach (Byte b in result)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        
    }
}