using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eDietetyk.Services
{
    public class ChartServices
    {
        private EfContext _db;

        public ChartServices()
        {
            _db = new EfContext();
        }
    }
}