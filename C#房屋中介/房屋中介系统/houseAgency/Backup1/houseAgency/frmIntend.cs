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
    public partial class frmIntend : Form
    {
        ClsCon con = new ClsCon();
        
        public frmIntend()
        {
            InitializeComponent();
            con.ConDatabase();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           if (e.KeyChar == 13)
            {
                con.ConDatabase();
                this.button1.Enabled = true;
                this.button1.PerformClick();
            }
        }

        private void frmIntend_Load(object sender, EventArgs e)
        {
            con.ConDatabase();
        }

        #region//   ��ʾ��Ϣ
        private void button1_Click(object sender, EventArgs e)
        {
            //�鿴�Ƿ�������ˣ����û����˵û��
            //��������������û�ȷ��һ��
            con.ConDatabase();
            SqlCommand cmd = new SqlCommand("select Max(house_id) from tb_user where user_phone='" + this.textBox1.Text.Trim().ToString() + "' and user_type='want'", con.conn);
            cmd.Connection.Open();
            string strInfo = cmd.ExecuteScalar().ToString();
            con.closeCon();
            if (strInfo == "none")
            {
                con.ConDatabase();
                SqlCommand cmdID= new SqlCommand("select Max(user_ids) from tb_user where user_phone='" + this.textBox1.Text.Trim().ToString() + "' and user_type='want'", con.conn);
                con.conn.Open();
                string IDInfo = cmdID.ExecuteScalar().ToString();
                con.closeCon();
                frmIntendInfo fiti = new frmIntendInfo();
                fiti.M_str_userID = IDInfo;
                fiti.Show();
                this.Hide();
              
            }
            else if (strInfo == "")
            {
                MessageBox.Show("��û��ע�������Ϣ����ע��");
            }
            else
            {
                MessageBox.Show("�㻹��Ԥ����Դû�д��������Բ���ʹ���������");
            }
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}