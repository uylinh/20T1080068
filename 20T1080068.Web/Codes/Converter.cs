using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using _20T1080068.DomainModels;
using Newtonsoft.Json;

namespace _20T1080068.Web
{
    /// <summary>
    /// DMYStringToDateTime(string s, string format = "d/M/yyyy")
    /// </summary>
    /// <param name="s"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public class Converter
    {
        public static DateTime? DYMStringToDateTime(string s, string format = "d/M/yyyy")
        {
            try
            {
                return DateTime.ParseExact(s, format, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }
        public static UserAccount CookiesToUserAccount(string s)
        {
            {
                try
                {
                    return JsonConvert.DeserializeObject<UserAccount>(s);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}