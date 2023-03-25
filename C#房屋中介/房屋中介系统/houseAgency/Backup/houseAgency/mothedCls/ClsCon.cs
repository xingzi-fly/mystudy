using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace houseAgency.mothedCls
{
    class ClsCon
    {
        public SqlConnection conn;
        /// <summary>
        /// Connection method
        /// </summary>
        public void ConDatabase()
        {
            conn = new SqlConnection("server=.;pwd=;uid=sa;database=db_showHouse");
        }
        /// <summary>
        /// close Connection method
        /// </summary>
        /// <returns></returns>
        public bool closeCon()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
