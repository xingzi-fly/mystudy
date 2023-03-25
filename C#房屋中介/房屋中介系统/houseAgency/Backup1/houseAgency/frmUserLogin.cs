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
    public partial class frmUserLogin : Form
    {
        ClsCon con = new ClsCon();                  //ʵ�������Ӷ���con
        clsLoginMethed cm = new clsLoginMethed();   //ʵ������¼�Ƿ�cm
        clsLogin cl=new clsLogin();                 //ʵ������¼����cl
        string ErrorNum = string.Empty;             //��¼��¼ʱ�û���
        int Num = 0;                                //��¼�������
        public frmUserLogin()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            #region//�û��������Ƿ���ȷ����ȷ�����λ���Ȼ��ر�
         
            cl.LName=this.cobName.Text;
            cl.LPwd=this.txtPwd.Text.Trim().ToString();
            string power=cm.select_table(cl);
            if (power != "none")
            {
                //��һ����������ɹ������̨����ǵ���½������
                frmMain fm = new frmMain();
                this.Hide();
                fm.M_str_Power = this.cobName.Text + "@" + power;
                fm.Show();
            }
            else if(this.txtPwd.Text=="" && this.cobName.Text=="")
            {//��������Ϣ��û��ʱ����һ�����
                frmMain fm = new frmMain();
                this.Hide();  
                fm.Show();
            }
            else
            {
                if (ErrorNum == cl.LName)
                {
                    Num += 1;
                    if (Num >= 3)
                    {
                        this.Close();
                    }
                }
                else
                {
                    ErrorNum = cl.LName;
                    Num += 1;
                }
                MessageBox.Show("��������,���κ��Զ��ر�,���ǵ�"+Num+"��");
                this.txtPwd.Text = string.Empty;
                this.txtPwd.Focus();

            }
            #endregion
        }



        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.btnLogin.Focus();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.clearText();
        }
        
            #region//clerarTextBox
        private void clearText()
        {
            foreach (Control cont in this.gbLogin.Controls)
            {
                if (cont.GetType().ToString() == "System.Windows.Forms.TextBox")
                {
                    cont.Text = string.Empty;
                   
                }
            }
        }
        #endregion

        private void frmUserLogin_Load(object sender, EventArgs e)
        {
            con.ConDatabase();
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("select * from tb_login", con.conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    this.cobName.DataSource = dt.DefaultView;
                    cobName.DisplayMember = "login_name";
                }
                catch (Exception ey)
                {
                    MessageBox.Show(ey.Message);
                }
            }
        }  
    }
}