﻿using eDietetyk.Models;
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
            var userInfo = user.UserInfo.FirstOrDefault();

            if (userMetrics != null && userTarget != null && userInfo!=null)
            {
                diet.CurrentCalories = CalculateCurrentCalories(userMeals);
                diet.TargetCalories = CalculateTargetCalories(userMetrics, userTarget, userInfo);
                diet.Bmi = CalculateBmi(userMetrics);
                diet.BmiInfo = GetBmiInfo(diet.Bmi);
                diet.IsData = true;

                var leftCalories = diet.TargetCalories - diet.CurrentCalories;
                if (leftCalories < 0)
                {
                    diet.IsExceededCalories = true;
                    diet.LeftCalories = 0;
                }
                else
                {
                    diet.LeftCalories = leftCalories;
                }

                if (userTarget.Weight < userMetrics.Height)
                {
                    diet.Info = "Aby osiągnąć założone cele, twoja dieta powinna być dietą odchudzającą";
                }
                else
                {
                    diet.Info = "Aby osiągnąć założone cele, twoja dieta powinna być dietą na zwiększenie masy";
                }

            }
            else
            {
                diet.IsData = false;
            }

            return diet;
        }

        private int CalculateCurrentCalories(List<UserMeals> userMeals)
        {
            var calories = userMeals.Sum(x => (int)((x.Weight / 100.0) * x.Meals.Calories));
            return calories;
        }

        private int CalculateTargetCalories(Metrics current, Metrics target, UserInfo info)
        {
            //TODO ŁK: change 25 to real age, , get activity (1.2) from real data.

            var caroriesDemand = 0.0;
            double age = (int)(DateTime.Today - info.BirthDate).Days/365;
            double activity = (double) info.Activity/10;
            if (info.SexType==2)
            {
                caroriesDemand = (655.0 + (9.6 * (double)current.Weight) + (1.85 * (double)current.Height) - (4.7 * age)) * activity;
            } else
            {
                caroriesDemand = (66.5 + (13.7 * (double)current.Weight) +(5.0 * (double)current.Height) - (6.8 * age)) * activity;
            }
            if (current.Weight > target.Weight)
            {
                caroriesDemand = caroriesDemand * 0.9;
            } else
            {
                caroriesDemand = caroriesDemand * 1.1;
            }
            return (int)caroriesDemand;
        }

        private double CalculateBmi(Metrics current)
        {
            var bmi = (double)current.Weight / (current.Height/100.0 * current.Height/100.0);
            return Math.Round(bmi, 2);
        }

        private string GetBmiInfo(double bmi)
        {
            if (bmi <= 18)
            {
                return "niedowaga";
            }
            if (bmi > 18 && bmi <= 25)
            {
                return "waga prawidłowa";
            }
            if (bmi > 25 && bmi <= 30)
            {
                return "nadwaga";
            }
            if (bmi > 30 && bmi <= 35)
            {
                return "I stopień otyłości";
            }
            if (bmi > 35 && bmi <= 40)
            {
                return "II stopień otyłości";
            }
            if (bmi > 40)
            {
                return "otyłość skrajna";
            }
            return "";
        }
    }
}