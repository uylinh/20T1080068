using _20T1080068.Datalayers;
using _20T1080068.DomainModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1080068.BusinessLayers
{
    /// <summary>
    /// Các chức năng liên quan đến tài khoản
    /// </summary>
    public class UserAccountService
    {
        private static IUserAccountDAL EmployeesAcountDB;
        private static IUserAccountDAL CustommerAcountDB;

        static UserAccountService()
        {
            string conmectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            EmployeesAcountDB = new Datalayers.SQLServer.EmpolyeeAccountDAL(conmectionString);
            CustommerAcountDB = new Datalayers.SQLServer.CustommerAccountDAL(conmectionString);
        }
        public static UserAccount Authorize(AccountTypes acountType, string userName, string password)
        {
            if (acountType == AccountTypes.Employees)
                return EmployeesAcountDB.Authorize(userName, password);
            else
                return CustommerAcountDB.Authorize(userName, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public static bool ChangePassword(AccountTypes accountType, string userName, string oldPassword, string newPassword)
        {
            if (accountType == AccountTypes.Employees)
                return EmployeesAcountDB.ChangePassword(userName, oldPassword, newPassword);
            else
                return CustommerAcountDB.ChangePassword(userName, oldPassword, newPassword);
        }
    }
}