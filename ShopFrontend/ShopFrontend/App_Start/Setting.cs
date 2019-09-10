using ShopFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopFrontend.App_Start
{
    public class Setting
    {
        public static Dictionary<string, string> Value;
        static Setting()
        {
            shop2Entities db = new shop2Entities();
            if (Value == null)
            {
                Value = new Dictionary<string, string>();
                var _settings = db.oc_setting.Where(r => r.store_id == 1).ToList();
                foreach (var _setting in _settings)
                {
                    Value.Add(_setting.key, _setting.value);
                }
            }
            db.Dispose();
        }
    }
}