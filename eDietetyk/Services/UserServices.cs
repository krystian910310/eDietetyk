using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eDietetyk.Models;

namespace eDietetyk.Services
{
    public class UserServices
    {
        private EfContext _db;

        public UserServices()
        {
            _db = new EfContext();
        }

        public UserInfo GetData(string userName)
        {
            var user = _db.AspNetUsers.First(x => x.UserName == userName);
            return user.UserInfo.FirstOrDefault();
        }

        public string Save(UserInfo data, string userName)
        {
            var errors = IsValid(data);
            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }
            var user = _db.AspNetUsers.First(x => x.UserName == userName);
            var info = user.UserInfo.FirstOrDefault();
            if (info != null)
            {
                info.FirstName = data.FirstName;
                info.Name = data.Name;
                info.BirthDate = data.BirthDate;
                info.Activity = data.Activity;
                info.SexType = data.SexType;
                data = null;
            }
            else
            {
                user.UserInfo.Add(new UserInfo
                {
                    Activity = data.Activity,
                    FirstName = data.FirstName,
                    Name = data.Name,
                    SexType = data.SexType,
                    BirthDate = data.BirthDate,
                    AspNetUsers = user,
                    CreateDate = DateTime.Now
            });
            }

            _db.SaveChanges();
            return string.Empty;
        }

        private string IsValid(UserInfo data)
        {
            if (string.IsNullOrEmpty(data.FirstName) || data.FirstName.Length>80) return "Wprowadzono niepoprawne imię.";
            if (string.IsNullOrEmpty(data.Name) || data.Name.Length > 80) return "Wprowadzono niepoprawne nazwisko.";
            if (data.BirthDate.Year<1920 || data.BirthDate > DateTime.Today) return "Data urodzenia jest niepoprawna.";
            return string.Empty;
        }

    }
}