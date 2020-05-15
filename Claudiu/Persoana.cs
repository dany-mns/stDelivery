using DatabaseStDeliveryLibrary;
using stDelivery.Kitchen;

/**************************************************************************
 *                                                                        *
 *  File:        Persoana.cs                                              *
 *  Copyright:   (c) 2020, Zalinca Claudiu                                *
 *  E-mail:      claudiu-serban.zalinca@student.tuiasi.ro                 *
 *  Website:     http://127.0.0.1                                         *
 *  Description: Class for contains all the informations needed to        *
 *               process client request                                   *
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
    /// Class used to store all the informations of the current user that the application needs to process
    /// </summary>
    public class Persoana
    {
        public User Client { get; set; }
        public Food ShoppingCart { get => _shoppingCart; set => _shoppingCart = value; }

        private Food _shoppingCart;

        public Persoana(User user)
        {
            this.Client = user;
            _shoppingCart = new Food();
        }

        
    }

    /// <summary>
    /// Class used for global variables
    /// Provides the current user instance to all activities
    /// </summary>
    public static class GlobalVariables
    {
        public static Persoana currentUser;
    }
}