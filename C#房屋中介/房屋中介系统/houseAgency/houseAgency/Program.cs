﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace houseAgency
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string []args)
        {
            Application.Run(new frmUserLogin());
        }
    }
}