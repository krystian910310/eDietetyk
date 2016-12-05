using eDietetyk.Models;
using System;
using System.Linq;

namespace eDietetyk.Services
{
    public class DietsServices
    {
        private EfContext _db;

        public DietsServices()
        {
            _db = new EfContext();
        }

        public DietsViewModel GetCurrentDiet(string userName, DateTime day)
        {
            var user = _db.AspNetUsers.First(x => x.UserName == userName);
            var diet = new DietsViewModel();
            var userMeals = user.UserMeals.Where(x => x.CreateDate.Year == day.Year && x.CreateDate.Month == day.Month && x.CreateDate.Day == day.Day).ToList();
            diet.CurrentCalories = userMeals.Sum(x => (int)((x.Weight/100.0) * x.Meals.Calories));
            diet.TargetCalories = 2400;

            return diet;
        }
    }
}