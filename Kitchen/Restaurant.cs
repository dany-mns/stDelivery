using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

/**************************************************************************
 *                                                                        *
 *  File:        FoodMenuActivity.cs                                      *
 *  Copyright:   (c) 2020, Manastireanu Dany                              *
 *  E-mail:      andrei-dany.manastireanu@student.tuiasi.ro               *
 *  Website:     http://127.0.0.1                                         *
 *  Description: Used for restaurant creation base on input file          *
 *                                                                        *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

namespace stDelivery.Kitchen
{

    /// <summary>
    /// Main root json deserialization
    /// </summary>
    public class Restaurant
    {
        public Pizza Pizza { get; set; }
        public Hamburger Hamburger { get; set; }
        public Ciorba Ciorba { get; set; }
        public Paste Paste { get; set; }
        public Salata Salata { get; set; }
        public Desert Desert { get; set; }
    }
}