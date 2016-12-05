using eDietetyk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eDietetyk.Services
{
    public class UserMealsServices
    {
        /// <summary>
        /// Dostęp do bazy danych
        /// </summary>
        private EfContext db;

        /// <summary>
        /// Kontruktor
        /// </summary>
        public UserMealsServices()
        {
            db = new EfContext();
        }

        public List<UserMeals> GetMealsForDay(DateTime day, string userName)
        {
            var user = db.AspNetUsers.First(x => x.UserName == userName);
            var result = user.UserMeals.Where(x => x.CreateDate.Year == day.Year && x.CreateDate.Month == day.Month && x.CreateDate.Day == day.Day).ToList();
            return result;
        }

        public void AddUserMeals(UserMealsViewModel userMeal, string userName)
        {
            var user = db.AspNetUsers.First(x => x.UserName == userName);
            var meal = db.Meals.First(x => x.Id == userMeal.MealId);
            user.UserMeals.Add(new UserMeals
            {
                AspNetUsers = user,
                Meals = meal,
                Weight = userMeal.Weight,
                CreateDate = DateTime.Now
            });
            db.SaveChanges();
        }
    }
}