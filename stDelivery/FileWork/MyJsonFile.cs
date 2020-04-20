using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stDelivery.Kitchen;

namespace stDelivery.FileWork
{
    public sealed class MyJsonFile
    {
        private static MyJsonFile _mjf = null;
        private Restaurant _restaurant;
        private string _content;

        public Restaurant Restaurant { get => _restaurant; set => _restaurant = value; }

        private MyJsonFile(string fileName, AssetManager asset)
        {
            try
            {
                using (StreamReader sr = new StreamReader(asset.Open(fileName)))
                {
                    _content = sr.ReadToEnd();
                    Restaurant = JsonConvert.DeserializeObject<Restaurant>(_content);
                }
            }
            catch (IOException ioe)
            {
                Log.Info("myapp", "Fail on read: " + ioe.Message);
            }
        }


        public static MyJsonFile GetInstance(AssetManager asset)
        {
            if (_mjf == null)
                _mjf = new MyJsonFile("food-menu.json", asset);
            return _mjf;
        }
    }
}