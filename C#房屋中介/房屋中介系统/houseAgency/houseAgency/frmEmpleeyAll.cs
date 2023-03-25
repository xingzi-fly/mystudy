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
    public partial class frmEmpleeyAll : Form
    {
        ClsCon con = new ClsCon();
        string strTemp = string.Empty;
        public frmEmpleeyAll()
        {
            InitializeComponent();
        }

        private void frmEmpleeyAll_Load(object sender, EventArgs e)
        {
            showAll();
            this.cobPower.Items.Add("Ա��");
            this.cobPower.Items.Add("����");
        }

        #region //��ʾ��ĵ���
        private void tp_OK_Click(object sender, EventArgs e)
        {
            string power = string.Empty;
            if (this.cobPower.Text == "Ա��")
            {
                power = "0";
            }
            else if (this.cobPower.Text == "����")
            {
                power = "1";
            }

            #region//���ô�����ȥ����
            if (strTemp == "Update")
            {
                float fmoney = Convert.ToSingle(this.txtBasePay.Text.Trim().ToString());
                SqlCommand cmd = new SqlCommand("update view_empleey set Ȩ��='" + power + "',�绰='" + this.txtPhone.Text.Trim().ToString() + "',����='" +fmoney+ "' where ����='" + this.txtName.Text.Trim().ToString() + "'", con.conn);
                con.conn.Open();
                cmd.ExecuteNonQuery();
                con.closeCon();
                showAll();
                MessageBox.Show("�ɹ�����");
                strTemp = string.Empty;

            }
            else if( strTemp ==string.Empty)
            {
                MessageBox.Show("û��ѡȡҪ��˭����");
            }
            #endregion

        }
        private void tsAll_Click(object sender, EventArgs e)
        {
            showAll();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            selectInfo();
        }


        private void tb_delete_Click(object sender, EventArgs e)
        {
            #region//������ɾ��
            if (MessageBox.Show("ȷ��ɾ�����Ա����", "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //��Ա������ɾͬʱɾ�������й�����ͼ�Ĵ�����
                
                SqlCommand cmd = new SqlCommand("delete from view_empleey where ����='" + this.txtName.Text.Trim().ToString() + "'", con.conn);
                con.conn.Open();
                cmd.ExecuteNonQuery();
                con.closeCon();
                showAll();
                MessageBox.Show("�ɹ�ɾ��");
            }
            #endregion
            showAll();

        }

        private void tb_update_Click(object sender, EventArgs e)
        {
            //��Ա��������ͬʱɾ�������й�����ͼ�Ĵ�����
            strTemp = "Update";
        }

        private void tp_cancel_Click(object sender, EventArgs e)
        {
            this.strTemp = string.Empty;
            this.Close();
        }
        #endregion

        #region//����������Ϣ
        /*
         * ���Ӧ��д��һһ��������ÿ�ζ�ȥ���þͿ���
         *
         * 
         * */
        private void IsNum(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                return;
            }
            else if (e.KeyChar > '9' || e.KeyChar < '0' )
            {
                e.Handled = true;
                MessageBox.Show("��Ч�ַ�");
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
            {
                IsNum(sender, e);
            }
            else
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void txtBasePay_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar== '.')
            {
               
            }
            else
            {
                IsNum(sender, e);
            }
        }
        #endregion

        #region//��д��Ϣ����ʾ����
        private void selectInfo()
        {
            try
            {
                string str = this.dataGridView1.SelectedCells[0].Value.ToString();
                //������������Ϣ
                SqlCommand cmd = new SqlCommand("select ����,�绰,Ȩ��,���� from view_empleey where Ա�����='" + str + "'", con.conn);
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtName.Text = dr[0].ToString();
                    txtPhone.Text = dr[1].ToString();
                    txtBasePay.Text = dr[3].ToString();

                    if (dr[2].ToString() == "0")
                    {
                        cobPower.Text = "Ա��";
                    }
                    else
                    {
                        cobPower.Text = "����";
                    }
                }
                dr.Close();
                con.closeCon();
            }
            catch
            {//have hide error
            }
        }

        private void showAll()
        {
            con.ConDatabase();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_empleey", con.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt.DefaultView;
        }
        #endregion
    }
}