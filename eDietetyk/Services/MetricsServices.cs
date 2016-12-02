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
        public Metrics GetCurrentMetric(string userName)
        {
            var user = db.AspNetUsers.First(x => x.UserName == userName);
            var result = db.Metrics.Where(x => x.IdUser == user.Id).OrderByDescending(x => x.CreateDate).FirstOrDefault();
            if (result == null)
            {
                result = new Metrics();
            }
            return result;
        }

        /// <summary>
        /// Dodaje nową metrykę
        /// </summary>
        /// <param name="data">Dane metryki</param>
        /// <param name="userName">Nazwa użytkownia</param>
        public void AddMetric(Metrics data, string userName)
        {
            var user = db.AspNetUsers.First(x => x.UserName == userName);
            data.AspNetUsers = user;
            data.CreateDate = DateTime.Now;
            db.Metrics.Add(data);
            db.SaveChanges();
        }
    }
}