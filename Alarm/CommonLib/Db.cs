using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using LiteDB;

namespace Alarm.CommonLib
{
    public class Db
    {
        public static Db Instance = new Db();
        public LiteRepository repository { get; set; }
        public LiteDatabase database { get; set; }

        private Db()
        {
            var str = ConfigurationManager.AppSettings["connection"].ToString();
            repository = new LiteRepository(str);
            database = new LiteDatabase(str);
        }
    }
}