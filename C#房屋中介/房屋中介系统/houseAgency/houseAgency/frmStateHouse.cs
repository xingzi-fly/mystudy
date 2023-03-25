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
    public partial class frmStateHouse : Form
    {
        string strSql = "select * from view_house";
        string strSqlWhereState = string.Empty;
        string strThis = string.Empty;
        string strID = string.Empty;
        ClsCon con = new ClsCon();
        public frmStateHouse()
        {
            InitializeComponent();
        }

        private void frmStateHouse_Load(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            ListInfo(strSql);
            con.ConDatabase();
        } 
        
        private void button1_Click(object sender, EventArgs e)
        {
            ListInfo(strSql + strSqlWhereState);
        }

        #region//��̬����
        private void ListInfo(string SQL)
        {
            con.ConDatabase();
            this.listView1.Items.Clear();
            SqlDataAdapter da = new SqlDataAdapter(SQL, con.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ListViewItem lv;
                    if (dr[11].ToString() == "none")
                    {
                        lv = new ListViewItem(dr[0].ToString(), 0);//˵��û��Ҫ��Ҫ��
                    }
                    else if (dr[11].ToString() == "remark")
                    {
                        lv = new ListViewItem(dr[0].ToString(), 1);//����Ԥ����
                    }
                    else
                    {
                        lv = new ListViewItem(dr[0].ToString(), 2);//�����ȥ��
                    }
                    lv.SubItems.Add(dr[1].ToString());
                    lv.SubItems.Add(dr[2].ToString());
                    lv.SubItems.Add(dr[5].ToString());
                    lv.SubItems.Add(dr[6].ToString());
                    lv.SubItems.Add(dr[7].ToString());
                    lv.SubItems.Add(dr[8].ToString());
                    this.listView1.Items.Add(lv);
                }
            }
            else
            {
                MessageBox.Show("��ѯ���Ϊ��");
            }
            this.listView1.Columns[0].Width =120;
        }
        #endregion

        #region//LISTVIEW����ʾģʽ
        private void ƽ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.LargeIcon;
        }

        private void ͼ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.SmallIcon;
        }

        private void �б�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.List;
        }

        private void ��ϸ��ϢToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.Details;
        }
        #endregion

        #region//˫��listView�鿴��ϸ��Ϣ
        private void listView1_DoubleClick(object sender, EventArgs e)
        {           
            string str = this.listView1.SelectedItems[0].Text.ToString();
            frmHouse fh = new frmHouse();
            fh.M_str_Show = str;
            this.Close();
            fh.Show();
        }
        #endregion

        #region//�鿴���з�Դ��Ϣ
        private void button2_Click(object sender, EventArgs e)
        {
            ListInfo(strSql);
        }
        #endregion

        #region//�鿴����״̬��where ����
        private void rbHave_CheckedChanged(object sender, EventArgs e)
        {
                if (this.rbHave.Checked)
                    {
                        this.strSqlWhereState = string.Empty;
                            strSqlWhereState += " where ״̬='success' ";
                            this.button1.Enabled = true;
                    }
        }

        private void rbNone_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbNone.Checked)
            {
                this.strSqlWhereState =string.Empty;
                strSqlWhereState += " where ״̬='none' ";
                this.button1.Enabled = true;
            }
        }

        private void rbRemark_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbRemark.Checked)
            {
                this.strSqlWhereState = string.Empty;
                strSqlWhereState += " where ״̬='Remark' ";
                this.button1.Enabled = true;
            }
        }
        #endregion

        #region//����listViewʱ����
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            string strText=this.listView1.Columns[e.Column].Text.ToString();
            string strSort=string.Empty;
            if (strText == strThis)
            {
                strSort = " order by " + strText + " desc";
                strThis = string.Empty;
            }
            else
            {
                strSort = " order by " + strText + " asc";
                strThis = strText;
            }
            ListInfo(this.strSql + strSort);
            strSort = string.Empty;
        }
        #endregion

        #region//���ҷ�Դ
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    //���û���ȥ������˰ѷ��ݵı�ŷ����������Ϣ��ȥ
                    ClsCon con = new ClsCon();
                    con.ConDatabase();
                    con.closeCon();
                    //��Ҫ�ⷿ���˲�Ҫ��Ҫ�������Ҳ���ȥ
                    SqlCommand cmd = new SqlCommand("select Max(user_names+'����֤����Ϊ:'+user_cardid) from tb_User where user_phone='" + textBox1.Text.Trim().ToString() + "' and user_type<>'lend'", con.conn);
                    con.conn.Open();
                    string strRe = cmd.ExecuteScalar().ToString();
                    con.closeCon();

                    if (strRe != "")
                    {
                        SqlCommand cmdl = new SqlCommand("select Max(house_ID) from tb_User where user_phone='" + textBox1.Text.Trim().ToString() + "'", con.conn);
                        cmdl.Connection.Open();
                        string strReS = cmdl.ExecuteScalar().ToString();
                        con.closeCon();

                        if (strReS == "none")
                        {
                            MessageBox.Show(strRe + "����ȨԤ����Դ");
                            this.button3.Enabled = true;
                            this.button4.Enabled = false;
                        }
                        else
                        {
                            this.button3.Enabled = false;
                            this.button4.Enabled = true;
                        }
                        SendKeys.Send("{Tab}");

                    }
                    else
                    {
                        MessageBox.Show("�绰���𲻴���");
                        this.textBox1.Select(0, this.textBox1.Text.Length);
                    }
                  
                }
                catch (Exception ey)
                {
                    MessageBox.Show(ey.Message);
                    con.conn.Close();
                }
            }
        }
        #endregion

        #region//ȡ��Ԥ��
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim().ToString() == "")
            {
                this.epInfo.SetError(this.textBox1, "����������ֻ���");
            }
            else
            {

                SqlCommand cmdUS = new SqlCommand("select Max(house_id) from tb_user  where user_phone='" + this.textBox1.Text.Trim().ToString() + "'", con.conn);
                con.conn.Open();
                string strTempID=cmdUS.ExecuteScalar().ToString();
                con.closeCon();

                SqlCommand cmd = new SqlCommand("update tb_house set house_state='none' where house_ID='" + strTempID + "'", con.conn);
                con.conn.Open();
                cmd.ExecuteNonQuery();
                con.closeCon();



                SqlCommand cmdU = new SqlCommand("update tb_user set house_id='none' where user_phone='" + this.textBox1.Text.Trim().ToString() + "'", con.conn);
                con.conn.Open();
                cmdU.ExecuteNonQuery();
                con.closeCon();

                //�û���������Ǹ��üǶ����Ǹ�����
               

                this.epInfo.SetError(this.textBox1, "");
                ListInfo(strSql);
                MessageBox.Show("���ѳɹ�ȡ��"+strTempID+"�ŷ�Դ��Ԥ��");
                this.button4.Enabled = false;
            }
        }
        #endregion

        #region//Ԥ����Դ
        private void button3_Click(object sender, EventArgs e)
        {
            if (strID != string.Empty)
            {//���ݵ�״̬�仯
                //SqlCommand cmdS= new SqlCommand("select Max(house_state) from tb_house house_ID='" + strID + "'",con.conn);
                //con.conn.Open();
                //string strState=cmdS.ExecuteScalar().ToString();
                //con.closeCon();
                //if (strState == "none")
                //{
                    SqlCommand cmd = new SqlCommand("update tb_house set house_state='remark' where house_ID='" + strID + "'", con.conn);
                    con.conn.Open();
                    cmd.ExecuteNonQuery();
                    con.closeCon();
                    //�û���������Ǹ��üǶ����Ǹ�����
                    SqlCommand cmdU = new SqlCommand("update tb_user set house_id='" + strID + "' where user_phone='" + this.textBox1.Text.Trim().ToString() + "'", con.conn);
                    con.conn.Open();
                    cmdU.ExecuteNonQuery();
                    con.closeCon();
                    ListInfo(strSql);
                    MessageBox.Show("�ɹ�Ԥ��" + strID + "�ŷ�Դ");
                    this.button3.Enabled = false;
                //}
                //else
                //{
                //    MessageBox.Show("�˷�Դ����Ԥ��");
                //}
            }
            else
            {
                MessageBox.Show("�뵥����Դ��ѡȡ");
            }

        }
        #endregion

        #region //������Ƿ���Զ�
        private void listView1_Click(object sender, EventArgs e)
        {
            con.ConDatabase();
            strID = this.listView1.SelectedItems[0].Text.ToString();
            SqlCommand cmd = new SqlCommand("select Max(house_state) from tb_house where house_ID='" + strID + "'", con.conn);
            cmd.Connection.Open();
            string strTemp = cmd.ExecuteScalar().ToString();
            con.closeCon();
            if (strTemp == "none")
            {
                strID = this.listView1.SelectedItems[0].Text.ToString();
            }
            else
            {
                strID = string.Empty;
                strID = this.listView1.SelectedItems[0].Text.ToString();
                SqlCommand cmdU = new SqlCommand("select Max(User_IDS) from tb_User where house_ID='" + strID + "'", con.conn);
                cmdU.Connection.Open();
                string strTempName = cmdU.ExecuteScalar().ToString();
                con.closeCon();
                if (strTempName != "")
                {
                    strID = string.Empty;
                    MessageBox.Show(strTempName + "�ţ��û���Ԥ���˴˷�");
                }
                else
                {
                    strID = string.Empty;
                    MessageBox.Show("�˷��ѱ�����");
                }
            }
        }
        #endregion
   
    }
}