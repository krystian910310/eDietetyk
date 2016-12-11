using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eDietetyk.Services
{
    public class MealsServices
    {
        /// <summary>
        /// Dostęp do bazy danych
        /// </summary>
        private EfContext db;

        /// <summary>
        /// Kontruktor
        /// </summary>
        public MealsServices()
        {
            db = new EfContext();
        }

        public List<KeyValuePair<int,string>> GetMealsByName(string key)
        {
            var result = db.Meals
                .Where(x=>x.Name.Contains(key))
                .ToList()
                .ToDictionary(item => item.Id, item => item.Name)
                .ToList();
            return result;
        }

    }
}