/**************************************************************************
 *                                                                        *
 *  File:        Activity.cs                                              *
 *  Copyright:   (c) 2020, Stratulat Stefan                               *
 *  E-mail:      stefanc.stratulat@gmail.com                              *
 *  Website:     -                                                        *
 *  Description: The activty class, base for concrete activities          *
 *               ShoppingCartActivity and FinishOrderActivity             *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using Android.Support.V7.App;

namespace StDelivery
{
    /// <summary>
    /// An abstract class, base for the concrete activities : ShoppingCartActivity and FinishOrderActivity
    /// </summary>
    abstract class Activity
    {
        /// <summary>
        /// The constructor of the abstract class, base for building the concrete classes
        /// </summary>
        /// <param name="mainActivity">The main activity context</param>
        public Activity(AppCompatActivity mainActivity) { }
    }
}