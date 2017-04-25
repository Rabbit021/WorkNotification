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
        private static LiteRepository _repository;
        public static LiteRepository repository
        {
            get
            {
                if (_repository == null)
                {
                    var str = ConfigurationManager.AppSettings["connection"].ToString();
                    _repository = new LiteRepository(str);
                }
                return _repository;
            }
        }
    }

}