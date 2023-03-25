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
    public partial class frmPeopleList : Form
    {
        StringBuilder sbSql = new StringBuilder();
        StringBuilder sbWhere = new StringBuilder();
        StringBuilder sbWhereInfo = new StringBuilder();
        ClsCon con = new ClsCon();
        string strTemp = string.Empty;
        public frmPeopleList()
        {
            InitializeComponent();
            con.ConDatabase();
        }

        private void frmPeopleList_Load(object sender, EventArgs e)
        {
            sbSql.Append("select User_IDs,User_names,User_homePhone,User_cardID,User_phone from tb_User");
            ListInfo(sbSql.ToString());
            UnAble();
        }

        private void tp_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region//ѡ���Ϣ��ʾ��SQL���
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Text == "������")
            {
                sbWhere.Append(" where user_type='lend'");
                ListInfo(sbSql.ToString() + sbWhere.ToString());
                sbWhere.Remove(0, sbWhere.Length);
            }
            else if (this.tabControl1.SelectedTab.Text == "������")
            {
                sbWhere.Append(" where user_type='want' ");
                ListInfo(sbSql.ToString() + sbWhere.ToString());
                sbWhere.Remove(0, sbWhere.Length);
            }
        }
        #endregion

        #region//��Ϣ����
        private void listView1_Click(object sender, EventArgs e)
        {
            string strID =this.listView1.SelectedItems[0].Text.ToString();
            string sql = "select User_IDs,User_names,User_homePhone,User_cardID,User_phone from tb_User where user_ids='" + strID + "'";
            SqlCommand cmd=new SqlCommand(sql,con.conn);
            con.closeCon();
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (strID.Substring(0, 4) == "lend")
            {
                this.tabControl1.SelectTab(0);
                while (dr.Read())
                {
                    this.textBox1.Text = dr[0].ToString();
                    this.textBox2.Text = dr[1].ToString();
                    this.textBox3.Text = dr[2].ToString();
                    this.textBox4.Text = dr[3].ToString();
                    this.textBox5.Text = dr[4].ToString();
                }
            }
            else 
            {
                this.tabControl1.SelectTab(1);
                while (dr.Read())
                {
                    this.textBox10.Text = dr[0].ToString();
                    this.textBox9.Text = dr[1].ToString();
                    this.textBox8.Text = dr[2].ToString();
                    this.textBox7.Text = dr[3].ToString();
                    this.textBox6.Text = dr[4].ToString();
                
                }
            }
            dr.Close();
            con.closeCon();
            tb_update.Enabled = true;
        }
        #endregion

        #region//DML����������SQL���

        private void tb_delete_Click(object sender, EventArgs e)
        {
            //���ô����� ɾ�����û�ʱȥɾ����Ӧ�ķ�Դ��Ϣ
            if (MessageBox.Show("�Ƿ�ɾ���û���", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("delete from tb_user where User_IDS='" +this.listView1.SelectedItems[0].Text.ToString()+ "'", con.conn);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                con.conn.Close();
                ListInfo(sbSql.ToString());
            }
        }

        private void tb_Select_Click(object sender, EventArgs e)
        {
            strTemp = "Select";
            this.EnAble();
            tb_update.Enabled = false;
        }

        private void tb_update_Click(object sender, EventArgs e)
        {
            strTemp = "Update";
            this.EnAble();
            this.textBox1.Enabled = false;
            this.textBox10.Enabled = false;
            
        }

        private void tp_OK_Click(object sender, EventArgs e)
        {
            if (strTemp == "Select")
            {
                #region//User_IDs,User_names,User_phone,User_cardID,User_homePhoneΪ��ѯ����������Ϣ
                if (this.tabControl1.SelectedTab.Text == "������")
                {
                    sbWhereInfo.Append(" where User_IDs like '%" + this.textBox1.Text.ToString() + "%'");
                    sbWhereInfo.Append(" and User_names like '%" + this.textBox2.Text.ToString() + "%'");
                    sbWhereInfo.Append(" and User_phone like '%" + this.textBox5.Text.ToString() + "%'");
                    sbWhereInfo.Append(" and User_cardID like '%" + this.textBox4.Text.ToString() + "%'");
                    sbWhereInfo.Append(" and User_homePhone like '%" + this.textBox3.Text.ToString() + "%'");
                }
                else
                {
                    sbWhereInfo.Append(" where User_IDs='%" + this.textBox10.Text.ToString() + "%'");
                    sbWhereInfo.Append(" and User_names like '%" + this.textBox9.Text.ToString() + "%'");
                    sbWhereInfo.Append(" and User_phone like '%" + this.textBox6.Text.ToString() + "%'");
                    sbWhereInfo.Append(" and User_cardID like '%" + this.textBox7.Text.ToString() + "%'");
                    sbWhereInfo.Append(" and User_homePhone like '%" + this.textBox8.Text.ToString() + "%'");
                
                }
                
                ListInfo(sbSql.ToString()+sbWhereInfo.ToString());
                sbWhereInfo.Remove(0, sbWhereInfo.Length);
                #endregion
            }
            else if (strTemp == "Update")
            {
                #region//User_IDs,User_names,User_phone,User_cardID,User_homePhoneΪ��������������Ϣ
                sbWhereInfo.Append("update  tb_User set ");
                if (this.tabControl1.SelectedTab.Text == "������")
                {
                    sbWhereInfo.Append(" User_names ='" + this.textBox2.Text.ToString() + "'");
                    sbWhereInfo.Append(" ,User_phone = '" + this.textBox5.Text.ToString() + "'");
                    sbWhereInfo.Append(" ,User_cardID='" + this.textBox4.Text.ToString() + "'");
                    sbWhereInfo.Append(" ,User_homePhone= '" + this.textBox3.Text.ToString() + "'");
                    sbWhereInfo.Append("where User_IDs='" + this.textBox1.Text.ToString() + "'");
                }
                else
                {
                 
                    sbWhereInfo.Append("  User_names = '" + this.textBox9.Text.ToString() + "'");
                    sbWhereInfo.Append(" ,User_phone ='" + this.textBox6.Text.ToString() + "'");
                    sbWhereInfo.Append(" ,User_cardID = '" + this.textBox7.Text.ToString() + "'");
                    sbWhereInfo.Append(" ,User_homePhone = '" + this.textBox8.Text.ToString() + "'");  
                    sbWhereInfo.Append(" where User_IDs='" + this.textBox10.Text.ToString() + "'");

                }
                Update(sbWhereInfo.ToString());
                sbWhereInfo.Remove(0, sbWhereInfo.Length);
                #endregion    
            }
            else
            {
                MessageBox.Show("��û��ѡ�Բ�����Ϣ"); 
            }
            this.UnAble();
        }
        #endregion

        #region//���ƿ�����
        private void UnAble()
        {
            foreach (Control ct in this.tabPage1.Controls)
            {
                if (ct.GetType().ToString() == "System.Windows.Forms.TextBox")
                    ct.Enabled = false;
            }

            foreach (Control ctT in this.tabPage2.Controls)
            {
                if (ctT.GetType().ToString() == "System.Windows.Forms.TextBox")
                    ctT.Enabled = false;
            }
           
        }

        private void EnAble()
        {
            foreach (Control ct in this.tabPage1.Controls)
            {
                if (ct.GetType().ToString() == "System.Windows.Forms.TextBox")
                    ct.Enabled = true;
            }

            foreach (Control ctT in this.tabPage2.Controls)
            {
                if (ctT.GetType().ToString() == "System.Windows.Forms.TextBox")
                    ctT.Enabled = true;
            }

        }
        #endregion

        #region//���ݿ���ݷ���
        private void ListInfo(string SQL)
        {
            try
            {
                con.ConDatabase();
                con.conn.Open();
                this.listView1.Items.Clear();
                SqlDataAdapter da = new SqlDataAdapter(SQL, con.conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    ListViewItem lv;
                    lv = new ListViewItem(dr[0].ToString());
                    lv.SubItems.Add(dr[1].ToString());
                    lv.SubItems.Add(dr[2].ToString());
                    lv.SubItems.Add(dr[3].ToString());
                    lv.SubItems.Add(dr[4].ToString());
                    this.listView1.Items.Add(lv);
                }
            }
            catch { }
         
        }

        private void Update(string SQL)
        {
            SqlCommand cmd = new SqlCommand(SQL, con.conn);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            con.closeCon();
            ListInfo(sbSql.ToString());
        }
        #endregion

       
    }
}