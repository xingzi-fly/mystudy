using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using houseAgency.mothedCls;
namespace houseAgency
{
    public partial class frmChangYouSelfPwd : Form
    {
        public string M_str_Name = string.Empty;
        ClsCon con = new ClsCon();

        public frmChangYouSelfPwd()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.ConDatabase(); 
            if (this.txtNewPwd.Text.Trim().ToString() != this.txtNewPwdTwo.Text.Trim().ToString())
            {
                this.erInfo.SetError(this.txtNewPwdTwo, "������������󣡣���");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select Max(login_id) from tb_login where login_name='" + M_str_Name + "' and login_pwd='" + this.txtOldPwd.Text.Trim().ToString() + "'", con.conn);
                con.closeCon();
                con.conn.Open();
                string str = cmd.ExecuteScalar().ToString();
                con.closeCon();
                if (str != "")
                {
                    SqlCommand cmdL = new SqlCommand("update  tb_login set login_pwd='" + this.txtNewPwdTwo.Text.Trim().ToString() + "' where login_name='" + M_str_Name + "' ", con.conn);
                    cmdL.Connection.Open();
                    cmdL.ExecuteNonQuery();
                    con.closeCon();
                    MessageBox.Show("�ѳɹ�����������룡����");
                }
                else
                {
                    MessageBox.Show("�����������������룡����");
                }
            }
        }
    }
}