using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace houseAgency.mothedCls
{
    class clsLoginMethed
    {

        ClsCon con = new ClsCon();
        //�Ƿ���Ե�½
        public string select_table(clsLogin cf)
        {
            try
            {
                con.ConDatabase();
                SqlCommand cmd = new SqlCommand("proc_login_select", con.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlParameter[] prams =
                {
						new SqlParameter("@login_name", SqlDbType.VarChar, 50),
                		new SqlParameter("@login_pwd",  SqlDbType.VarChar, 50),
                        new SqlParameter("@ReturnInfo", SqlDbType.VarChar, 50, ParameterDirection.Output,true, 0, 0, string.Empty,DataRowVersion.Default, null)
				};
                prams[0].Value = cf.LName;
                prams[1].Value = cf.LPwd;

                // ��Ӳ���
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
                cmd.ExecuteNonQuery();
                string strResult = cmd.Parameters["@ReturnInfo"].Value.ToString();
                con.closeCon();
                return strResult;
            }
            catch (Exception ey)
            {
                con.closeCon();
                return ey.Message.ToString();
            }
        }
    }
}
