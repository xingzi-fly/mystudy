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
    public partial class frmPeopleInfo : Form
    {

        ClsCon con;
        public string strID = string.Empty; 
        string strNewID = string.Empty;
        clsUser cu = new clsUser();
      
        public frmPeopleInfo()
        {
            InitializeComponent();
        }

        private void frmPeopleInfo_Load(object sender, EventArgs e)
        {
             con= new ClsCon();
            if (strID == "lend")
            {
                this.butIHouse.Visible = true;
            }

            cbSex.Items.Add("��");
            cbSex.Items.Add("Ů");
            cbSex.SelectedIndex = 0;

            //�����Զ����
            con.ConDatabase();
            SqlCommand cmd = new SqlCommand("select Max(User_IDS) from tb_User where user_type='" + strID + "'", con.conn);
            con.conn.Open();
            string strResult=cmd.ExecuteScalar().ToString();
            if (strResult != "")
            {
                strNewID = strResult.Substring(4);
            }
            else
            {
                strNewID = "1000";
            }
            strID = strID +Convert.ToString(int.Parse(strNewID)+1);
        }
        private void button2_Click(object sender, EventArgs e)
                {
                    this.Close();
                }

        private void butIHouse_Click(object sender, EventArgs e)
        {
            frmHouse fh = new frmHouse();
            frmMain fm=new frmMain();
            fh.M_str_temp = cu.id;
         
            fh.Show();
        }

        #region//�����븶ֵ
        private void button1_Click(object sender, EventArgs e)
        {
            if(IfCando())
            {
                cu.id = strID;
                cu.name = this.txtName.Text.Trim().ToString();
                cu.sex = this.cbSex.Text.Trim().ToString();
                cu.birth =this.dtBirthday.Text.Trim().ToString();
                cu.phone = this.txtPhone.Text.Trim().ToString();
                cu.homePhone = this.txtHomePhone.Text.Trim().ToString();
                cu.email = this.txtEmail.Text.Trim().ToString();
                cu.cardID = this.txtIDCard.Text.Trim().ToString();
                cu.type = strID.Substring(0, 4).ToString();
                cu.recordDate = DateTime.Now.ToString();
                clsUserMethod cum = new clsUserMethod();
                if (cum.insert_table(cu))
                {
                    //if (strID.Substring(0, 4) == "lend")
                    //{
                        this.button1.Enabled = false;
                    //}//������ṩ���ӵ�����Ҫ�Ѱѵķ�Դд��ȥ���Ұ��˺ͷ���ϵ������һ���˿����ж������
                    MessageBox.Show("�ѳɹ��Ǽ�");
                    this.butIHouse.Enabled = true;
                }
            }
        }
        #endregion 

        #region//���Ƹ�ʽ

        private void IsNum(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                return;
            }
            
            else if (e.KeyChar > '9' || e.KeyChar < '0')
            {
                e.Handled = true;
                MessageBox.Show("��Ч�ַ�");
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (txtPhone.Text.Trim().ToString().Length == 8 || txtPhone.Text.Trim().ToString().Length == 11)
            {
                if (e.KeyChar == 13)
                    SendKeys.Send("{Tab}");
            }
            else
            {
                IsNum(sender, e);
                this.txtPhone.Focus();
            }

        }

        private void txtIDCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtIDCard.Text.Trim().ToString().Length == 15 || txtIDCard.Text.Trim().ToString().Length == 18)
            {
                 if (e.KeyChar == 13)
                     SendKeys.Send("{Tab}");
            }
            else
            {
                IsNum(sender, e);
                this.txtIDCard.Focus();
            }
           
        }

        private void txtHomePhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtHomePhone.Text.Trim().ToString().Length == 8)
            {
                if (e.KeyChar == 13)
                    SendKeys.Send("{Tab}");
            }
            else
            {
                IsNum(sender, e);
                txtHomePhone.Focus();
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }
        #endregion

        #region//one concle method
    private bool IfCando()
        { 
            int i=0;
            if (this.txtName.Text.Trim().ToString() == "")
            {
                this.eperror.SetError(this.txtName, "���Ǳ�д��Ϣ");
                i += 1;
            }
            else
            {
                this.eperror.SetError(this.txtName, "");
            }

            if (this.txtPhone.Text.Trim().ToString() == "")
            {
                this.eperror.SetError(this.txtPhone, "���Ǳ�д��Ϣ");
                i += 1;
            }
            else
            {
                this.eperror.SetError(this.txtPhone, "");
            }

            if (this.txtHomePhone.Text.Trim().ToString() == "")
            {
                this.eperror.SetError(this.txtHomePhone, "���Ǳ�д��Ϣ");
                i += 1;
            }
            else
            {
                this.eperror.SetError(this.txtHomePhone, "");
            }

            if (this.txtIDCard.Text.Trim().ToString() == "")
            {
                this.eperror.SetError(this.txtIDCard, "���Ǳ�д��Ϣ");
                i += 1;
            }
            else
            {
                this.eperror.SetError(this.txtIDCard, "");
            }

            if(i>0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    #endregion
    }
}