using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eDietetyk.Models;

namespace eDietetyk.Services
{
    public class ProgressServices
    {
        private EfContext _db;

        public ProgressServices()
        {
            _db = new EfContext();
        }

        public ProgressModel GetData(string userName)
        {
            var user = _db.AspNetUsers.First(x => x.UserName == userName);
            var model = new ProgressModel
            {
                Weight = new List<int>(),
                Description = new List<string>()
            };

            var userMetrics = user.Metrics.Where(x => x.IsTarget == false).OrderByDescending(x => x.CreateDate).Take(5).ToList().OrderBy(x=>x.CreateDate).ToList();

            if (userMetrics.Count >= 2)
            {
                model.IsData = true;
                foreach (var metric in userMetrics)
                {
                    model.Weight.Add((int)metric.Weight);
                    model.Description.Add(metric.CreateDate.ToString("dd.MM"));
                }
            }

            return model;
        }
    }
}