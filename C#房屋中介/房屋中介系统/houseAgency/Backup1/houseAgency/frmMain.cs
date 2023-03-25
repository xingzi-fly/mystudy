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
    public partial class frmMain : Form
    {
        public string M_str_Power = string.Empty;
        string Power = string.Empty;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //在加载时读出权限和用户名信息
            //通过对字符串函数的调用
            if (M_str_Power != string.Empty)
            {
                tspname.Text = M_str_Power.Substring(0, M_str_Power.IndexOf('@'));
                tspLoginTime.Text = DateTime.Now.ToLongTimeString();
                Power = M_str_Power.Substring(M_str_Power.IndexOf('@') + 1);
                if (Power == "0")
                {
                    this.tbEmpleey.Visible = false;
                }
                else
                {
                    this.tbEmpleey.Visible = true;
                }
            }
        }

        #region//常用工具

        private void 记事本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe");
        }
        private void 记算器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void eXECELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("EXCEL.EXE");
        }

        private void wORDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("WINWORD.EXE");
        }
        #endregion

        #region//窗体布局

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
            //层叠排列
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
            //垂直平铺

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
            //水平平铺
        }
        #endregion

        #region//垃极清理
        private void tspClear_Click(object sender, EventArgs e)
        {
            ClsCon con = new ClsCon();
            con.ConDatabase();
            //调用一个清理出租人和房源之间的垃极信息
            //如当出租人来要出租房时可是没有给出他房源信息这时出租人信息就没有用了
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.conn;
                cmd.Connection.Open();
                cmd.CommandText = "proc_clear";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.closeCon();
                MessageBox.Show("恭喜已清除！！！");
            }
            catch (Exception ey)
            {
                MessageBox.Show(ey.Message);
            }
        }
        #endregion

        #region//退出设置
        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认退出系统吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
               Application.Exit();

       }
        #endregion

        #region//调用其它窗体


       private void 朝向设置ToolStripMenuItem_Click(object sender, EventArgs e)
       {
           frmFavor ff = new frmFavor();
           ff.MdiParent = this;
           ff.Show();
       }

       private void 楼层设置ToolStripMenuItem_Click(object sender, EventArgs e)
       {
           frmFloor ff = new frmFloor();
           ff.MdiParent = this;
           ff.Show();
       }

       

       private void 求租人员信息ToolStripMenuItem_Click(object sender, EventArgs e)
       {
           frmPeopleInfo fp = new frmPeopleInfo();
           fp.strID = "want";
           fp.MdiParent = this;
           fp.Show();
       }

       private void l出租人员信息设置ToolStripMenuItem_Click(object sender, EventArgs e)
       {
           frmPeopleInfo fp = new frmPeopleInfo();
           fp.strID = "lend";
           fp.MdiParent = this;
           fp.Show();
       }


       private void timer1_Tick(object sender, EventArgs e)
       {
           this.tspNowTime.Text = DateTime.Now.ToLongTimeString();
       }

       private void 出租房源设置ToolStripMenuItem_Click(object sender, EventArgs e)
       {
           frmHouse fh = new frmHouse();
           fh.MdiParent = this;
           fh.Show();
       }

       private void tsmHouseType_Click(object sender, EventArgs e)
       {
           frmType ft = new frmType();
           ft.MdiParent = this;
           ft.Show();
       }

       private void tsmseat_Click(object sender, EventArgs e)
       {
           frmSeat fs = new frmSeat();
           fs.MdiParent = this;
           fs.Show();
       }

       private void tsmZhuang_Click(object sender, EventArgs e)
       {
           frmFitment ff = new frmFitment();
           ff.MdiParent = this;
           ff.Show();
       }

       private void tsmUser_Click(object sender, EventArgs e)
       {
           frmMothed fm = new frmMothed();
           fm.MdiParent = this;
           fm.Show();
       }

       private void 物业类型设置ToolStripMenuItem_Click(object sender, EventArgs e)
       {
           frmType ft = new frmType();
           ft.MdiParent = this;
           ft.Show();
       }
      

       private void tsSelectHouse_Click(object sender, EventArgs e)
        {
            frmSelect fs = new frmSelect();
            fs.MdiParent = this;
            fs.Show();
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void stStateHouse_Click(object sender, EventArgs e)
        {
            frmStateHouse fs = new frmStateHouse();
            fs.MdiParent = this;
            fs.Show();
        }

        private void tsmDataStock_Click(object sender, EventArgs e)
        {
            frmStock fs = new frmStock();
            fs.MdiParent = this;
            fs.Show();

        }

        private void tsmDataRestore_Click(object sender, EventArgs e)
        {
            frmRestore fr = new frmRestore();
            fr.MdiParent = this;
            fr.Show();
        }

       

        private void tsWantHouse_Click(object sender, EventArgs e)
        {
            frmIntend fi = new frmIntend();
            fi.MdiParent = this;
            fi.Show();
        }

        private void 录入员工信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmploeey fe = new frmEmploeey();
            fe.MdiParent = this;
            fe.Show();
           
        }

        private void 所有员工信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmpleeyAll fe = new frmEmpleeyAll();
            fe.MdiParent = this;
            fe.Show();
        }

        private void 口令设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangYouSelfPwd fcy = new frmChangYouSelfPwd();
            fcy.MdiParent = this;
            fcy.M_str_Name = this.tspname.Text.ToString();
            fcy.Show();
        }

        private void 收费设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMoney fm = new frmMoney();
            fm.MdiParent = this;
            fm.Show();
        }

        private void 交费统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMoneyRemark fmr = new frmMoneyRemark();
            fmr.MdiParent = this;
            fmr.Show();
        }

        private void smpbargin_Click(object sender, EventArgs e)
        {
            frmBargin fb = new frmBargin();
            fb.MdiParent = this;
            fb.Show();
        }

      
        private void tmPeoControl_Click(object sender, EventArgs e)
        {
            frmPeopleList fp = new frmPeopleList();
            fp.MdiParent = this;
            fp.Show();
        }
       #endregion

        private void 帮助文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\t你可以到明日科技网站\t\n\n\t  得到你想知道的\n\t    谢谢使用！！");
        }
    }
}