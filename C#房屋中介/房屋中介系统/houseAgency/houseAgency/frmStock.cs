using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace houseAgency
{
    public partial class frmStock : Form
    {
        public frmStock()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            beifenInfo();
        }

        public void beifenInfo()
        {
            try
            {
                sd.InitialDirectory = Application.StartupPath + "\\";//Ĭ��·��ΪD��//
                sd.Filter = "�����ļ� (*.bak)|*.bak|�����ļ� (*.*)|*.*";//ɸѡ���������ļ�����
                sd.FilterIndex = 1;								//Ĭ��ֵΪ��һ��
                sd.RestoreDirectory = true;						//���¶�λ����·��
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(sd.FileName.ToString()))
                    {
                        SqlConnection con = new SqlConnection();		//���ô���ʵ���������ݿ�
                        con.ConnectionString = "server=.;uid=sa;pwd=1234;database=db_showHouse";
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        this.textBox1.Text = sd.FileName.ToString();
                        com.CommandText = "BACKUP DATABASE db_showHouse TO DISK = '" + sd.FileName.ToString() + "'";
                        com.Connection = con;							//����
                        com.ExecuteNonQuery();						    //ִ��
                        con.Close();
                        con.Close();
                        con = null;
                        MessageBox.Show("���ݱ��ݳɹ���");
                    }
                    else
                    {
                        MessageBox.Show("������������");
                    }
                }
            }
            catch (Exception k)
            {
                MessageBox.Show(k.Message);
                return;
            }
        }
    }
}