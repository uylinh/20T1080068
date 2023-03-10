using _20T1080068.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace _20T1080068.DataLayers.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public class CountryDAL : _BaseDAL, ICountryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CountryDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<Country> ICountryDAL.List()
        {
            List<Country> data = new List<Country>();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Countries";
                cmd.CommandType = CommandType.Text;
                SqlDataReader dbReader = cmd.ExecuteReader();
                while (dbReader.Read())
                {
                    data.Add(new Country()
                    {
                        CountryName = Convert.ToString(dbReader["CountryName"]),
                    }); 
                }
            
            }
            return data;
        }
    }
}