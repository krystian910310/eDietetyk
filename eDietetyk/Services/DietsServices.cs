using eDietetyk.Models;
using System;
using System.Collections.Generic;
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
            var userMetrics = user.Metrics.Where(x => x.IsTarget == false).OrderByDescending(x => x.CreateDate).FirstOrDefault();
            var userTarget = user.Metrics.Where(x => x.IsTarget == true).OrderByDescending(x => x.CreateDate).FirstOrDefault();

            if (userMetrics != null && userTarget != null)
            {
                diet.CurrentCalories = CalculateCurrentCalories(userMeals);
                diet.TargetCalories = CalculateTargetCalories(userMetrics, userTarget);
                diet.Bmi = CalculateBmi(userMetrics);
                diet.IsData = true;
            }
            else
            {
                diet.IsData = false;
            }

            return diet;
        }

        private string CalculateCurrentCalories(List<UserMeals> userMeals)
        {
            var calories = userMeals.Sum(x => (int)((x.Weight / 100.0) * x.Meals.Calories));
            return calories.ToString();
        }

        private string CalculateTargetCalories(Metrics current, Metrics target)
        {
            //TODO ŁK: change 25 to real age, change if statement to real sex value, get activity (1.2) from real data.

            var caroriesDemand = 0.0;
            if (current.IdUser == "woman")
            {
                caroriesDemand = 655.0 + (9.6 * (double)current.Weight) +(1.85 * (double)current.Height);
                caroriesDemand -= (4.7 * 25.0);
            } else
            {
                caroriesDemand = 66.5 + (13.7 * (double)current.Weight) +(5.0 * (double)current.Height);
                caroriesDemand -= (6.8 * 25.0);
            }
            caroriesDemand = caroriesDemand * 1.2;
            return Math.Round(caroriesDemand, 1).ToString();
        }

        private string CalculateBmi(Metrics current)
        {
            var bmi = (double)current.Weight / (current.Height/100.0 * current.Height/100.0);
            return Math.Round(bmi, 2).ToString();
        }
    }
}