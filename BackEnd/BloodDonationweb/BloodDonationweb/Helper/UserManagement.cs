using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationweb.Helper
{
    public static class UserManagement<T> where T : class
    {
        private static Dictionary<string, T> _loggedInUsers;
        private const string CookieKey = "blood-donation-tokken";

        public static void Authinticate(HttpResponse response, T user)
        {
            if (_loggedInUsers == null)
            {
                _loggedInUsers = new Dictionary<string, T>();
            }
            var userGuid = Guid.NewGuid().ToString();
            _loggedInUsers.Add(userGuid, user);

            var option = new CookieOptions();
            var expireTime = 60;
            option.Expires = DateTime.Now.AddMinutes(expireTime);
            response.Cookies.Append("blood-donation-tokken", userGuid, option);
        }
        public static T GetLoggedInUser(HttpRequest request)
        {
            if (!request.Cookies.ContainsKey(CookieKey))
            {
                return null;
            }
            var token = request.Cookies[CookieKey];
                if (!_loggedInUsers.ContainsKey(token))
                {
                    return null;
                }
            return _loggedInUsers[token];
        }

        public static void LogOut(HttpResponse response)
        {
            response.Cookies.Delete("blood-donation-tokken");
        }
    }
}
