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
    public partial class frmBargin : Form
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbwhere = new StringBuilder();
        ClsCon con = new ClsCon();

        public frmBargin()
        {
            InitializeComponent();
            con.ConDatabase();
        }

        private void frmBargin_Load(object sender, EventArgs e)
        {
            con.conn.Open();
            showEmp();
        }

        #region//Ա����Ϣ��
        private void showEmp()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select employee_name,employee_ID from tb_employee", con.conn);
                DataTable dt = new DataTable();
                con.closeCon();
                con.conn.Open();
                da.Fill(dt);
                this.comboBox1.DataSource = dt.DefaultView;
                this.comboBox1.DisplayMember = "employee_name";
                this.comboBox1.ValueMember = "employee_ID";
            }
            catch { }

        }
        #endregion

        #region//����ת������
        private string date()
        {
            string date = this.dateTimePicker1.Text.Replace('��', '-').Replace('��', '-').Replace('��', ' ');
            string dateNew = string.Empty;
            char[] C ={ '-' };
            string[] dateinfo = date.Split(C);
            if (dateinfo[1].Trim().ToString().Length <= 1)
            {
                dateNew += dateinfo[0].ToString() + "-0" + dateinfo[1].ToString();
            }
            else
            {
                dateNew += dateinfo[0].ToString() + "-" + dateinfo[1].ToString();
            }

            if (dateinfo[2].Trim().ToString().Length <= 1)
            {
                dateNew += "-0" + dateinfo[2].ToString();
            }
            else
            {
                dateNew += "-" + dateinfo[2].ToString();
            }

            return dateNew;
        }
        #endregion 

        #region//ͳ����Ϣ
            private void button1_Click(object sender, EventArgs e)
            {

              
                sbwhere.Append(" where emp_name='" + this.comboBox1.Text.Trim().ToString() + "' and pay_date like '%" + date() + "%'");
                sb.Remove(0, sb.Length);
                sb.Append("select sum(Pay_Moeny) from tb_MoneyAndInfo ");
                sb.Append(sbwhere.ToString());

                SqlCommand cmd = new SqlCommand(sb.ToString(), con.conn);
                con.closeCon();
                cmd.Connection.Open();
                this.label5.Text = cmd.ExecuteScalar().ToString();
                con.closeCon();

                sb.Remove(0, sb.Length);
                sb.Append("select pay_moeny �����տ�,emp_name �տ���,pay_date �տ�����,");
                sb.Append("lend_name ��������,want_name �ͻ����� from tb_MoneyAndInfo ");
                sb.Append(sbwhere.ToString());
                sbwhere.Remove(0, sbwhere.Length);

                SqlDataAdapter da = new SqlDataAdapter(sb.ToString(), con.conn);
                DataTable dtS = new DataTable();
                da.Fill(dtS);
                this.label6.Text = dtS.Rows.Count.ToString();
                this.dataGridView1.DataSource = dtS.DefaultView;
                con.closeCon();
             
            }  
    #endregion
    }
}