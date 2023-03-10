using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1080068.DomainModels;

namespace _20T1080068.DataLayers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICountryDAL
    {

        /// <summary>
        /// Lấy danh sách quốc gia
        /// </summary>
        /// <returns></returns>
        IList<Country> List();

    }
}
