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

        public List<Meals> GetMealsByName(string name)
        {
            var result = db.Meals.ToList();
            return result;
        }

    }
}