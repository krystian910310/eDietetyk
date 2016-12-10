﻿using eDietetyk.Models;
using System;
using System.Linq;

namespace eDietetyk.Services
{
    /// <summary>
    /// Serwis do zarządzania metrykami
    /// </summary>
    public class MetricsServices
    {
        /// <summary>
        /// Dostęp do bazy danych
        /// </summary>
        private EfContext db;

        /// <summary>
        /// Kontruktor
        /// </summary>
        public MetricsServices()
        {
            db = new EfContext();
        }

        /// <summary>
        /// Pobiera aktualną metrykę użytkownika
        /// </summary>
        /// <param name="userName">Nazwa użytkownika</param>
        /// <returns></returns>
        public MetricsModel GetCurrentMetric(string userName)
        {
            var user = db.AspNetUsers.First(x => x.UserName == userName);
            var current = db.Metrics.Where(x => x.IdUser == user.Id && x.IsTarget==false).OrderByDescending(x => x.CreateDate).FirstOrDefault();
            var target = db.Metrics.Where(x => x.IdUser == user.Id && x.IsTarget == true).OrderByDescending(x => x.CreateDate).FirstOrDefault();

            var result = new MetricsModel
            {
                Current = current ?? new Metrics(),
                Target = target ?? new Metrics()
            };
            return result;
        }

        /// <summary>
        /// Dodaje nową metrykę
        /// </summary>
        /// <param name="data">Dane metryki</param>
        /// <param name="userName">Nazwa użytkownia</param>
        public bool AddMetric(Metrics data, string userName)
        {
            if (IsValid(data))
            {
                var user = db.AspNetUsers.First(x => x.UserName == userName);
                data.AspNetUsers = user;
                data.CreateDate = DateTime.Now;
                db.Metrics.Add(data);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddTarget(Metrics data, string userName)
        {
            data.IsTarget = true;
            if (IsValid(data))
            {
                var user = db.AspNetUsers.First(x => x.UserName == userName);
                data.AspNetUsers = user;
                data.CreateDate = DateTime.Now;
                db.Metrics.Add(data);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        private bool IsValid(Metrics data)
        {
            
            if (!data.IsTarget && (data.Height < 50 || data.Height>250)) { return false; }
            if (data.Weight < 20 || data.Weight>300) { return false; }
            if (data.Waist.HasValue)
            {
                if(data.Waist.Value<20|| data.Waist > 200) { return false; }
            }
            if (data.Arm.HasValue)
            {
                if (data.Arm.Value < 20 || data.Arm > 200) { return false; }
            }
            if (data.Thigh.HasValue)
            {
                if (data.Thigh.Value < 10 || data.Thigh > 100) { return false; }
            }
            if (data.Chest.HasValue)
            {
                if (data.Chest.Value < 50 || data.Chest > 200) { return false; }
            }
            if (data.Arm.HasValue)
            {
                if (data.Arm.Value < 50 || data.Chest > 200) { return false; }
            }

            return true;

        }

    }
}