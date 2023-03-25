using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace houseAgency.mothedCls
{
    class clsMethedMethod
    {
        ClsCon con = new ClsCon();

        public string insert_table(clsMethed cf)
        {
            try
            {
                con.ConDatabase();
                SqlCommand cmd = new SqlCommand("proc_mothed_insert", con.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlParameter[] prams =
                {
						new SqlParameter("@house_mothedID", SqlDbType.VarChar, 50),
                		new SqlParameter("@mothed_name",  SqlDbType.VarChar, 50),
                		new SqlParameter("@mothed_remark",  SqlDbType.VarChar, 50),
                        new SqlParameter("@proc_info", SqlDbType.VarChar, 50, ParameterDirection.Output,true, 0, 0, string.Empty,DataRowVersion.Default, null)
				};
                prams[0].Value = cf.id;
                prams[1].Value = cf.name;
                prams[2].Value = cf.remark;
                // 添加参数
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
                cmd.ExecuteNonQuery();
                string strResult = cmd.Parameters["@proc_info"].Value.ToString();
                con.closeCon();
                return strResult;
            }
            catch (Exception ey)
            {
                con.closeCon();
                return ey.Message.ToString();
            }
        }


        public bool update_table(clsMethed cf)
        {
            try
            {
                con.ConDatabase();
                SqlCommand cmd = new SqlCommand("proc_mothed_update", con.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlParameter[] prams =
                {
						new SqlParameter("@house_mothedID", SqlDbType.VarChar, 50),
                		new SqlParameter("@mothed_name",  SqlDbType.VarChar, 50),
                		new SqlParameter("@mothed_remark",  SqlDbType.VarChar, 50)
                       
				};
                prams[0].Value = cf.id;
                prams[1].Value = cf.name;
                prams[2].Value = cf.remark;
                // 添加参数
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
                cmd.ExecuteNonQuery();
              
                con.closeCon();
                return true;
            }
            catch (Exception ey)
            {
                con.closeCon();
                return false;
            }
        }


        public bool delete_table(clsMethed cf)
        {
            try
            {
                con.ConDatabase();
                SqlCommand cmd = new SqlCommand("proc_mothed_delete", con.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlParameter[] prams =
                {
						new SqlParameter("@house_mothedID", SqlDbType.VarChar, 50)
                        
				};
                prams[0].Value = cf.id;
                // 添加参数
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
                cmd.ExecuteNonQuery();
        
                con.closeCon();
                return true;
            }
            catch (Exception ey)
            {
                con.closeCon();
                return false;
            }
        }
    }
}
