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
    public partial class frmRestore : Form
    {
        public frmRestore()
        {
            InitializeComponent();
        }

        private void frmRestore_Load(object sender, EventArgs e)
        {

        }

        #region//���÷���
        private void button1_Click(object sender, EventArgs e)
        {
            Restore();
        }
        #endregion

       
        public void Restore()
        {
            try
            {
                ofd.InitialDirectory = Application.StartupPath + "\\";
                ofd.Filter = "(*.bak)|*.bak|(�����ļ�)|*.*";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = ofd.FileName.ToString();

                    #region//
                    if (this.RestoreBase(".", "sa", "", "db_showHouse", ofd.FileName.ToString()) == true)
                    {
                        MessageBox.Show("��ԭ�ɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        ClsCon con = new ClsCon();
                        con.ConDatabase();
                        con.conn.Open();
                    }
                    else
                    {
                        //MessageBox.Show("Error");
                        this.button1.PerformClick();
                    }
                    #endregion
                }
            }
            catch (Exception ey)
            {
                MessageBox.Show(ey.Message);
            }
        }

        #region//
        private bool RestoreBase(string serverName, string uID, string pWD, string dataBase, string bakPath)
        {	//���������������Ϊ�����Ժ���Զ�̬��ʹ��(��һ���Ƿ�������,�û���,����,���ݿ�,���л�ԭ�ļ���·��)
            SqlConnection RestoreCon = new SqlConnection("server=" + serverName + ";uid=" + uID + ";pwd=" + pWD + ";database=master");
            SqlCommand RestoreCmd = new SqlCommand("killspid", RestoreCon);						//���ô洢����
            RestoreCmd.CommandType = CommandType.StoredProcedure;								//����洢������Ϊ�˹ر����д򿪵Ŀ�
            RestoreCmd.Parameters.Add("@dbname",SqlDbType.VarChar,50);
            RestoreCmd.Parameters["@dbname"].Value ="'"+dataBase+"'";
            try
            {
                RestoreCon.Open();
                RestoreCmd.ExecuteNonQuery();
                SqlCommand RestoreCmd1 = new SqlCommand();
                RestoreCmd1.CommandText = "RESTORE DATABASE " + dataBase + " FROM DISK='" + bakPath + "'";
                RestoreCmd1.Connection = RestoreCon;
                RestoreCmd1.ExecuteNonQuery();
                RestoreCon.Close();
                return true;
            }
            catch(Exception ey)
            {
                MessageBox.Show(ey.Message);
                RestoreCon.Close();
                return false;
            }
        }
        #endregion
    }
}