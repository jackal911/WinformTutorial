using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FindMine
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Button a;
			for (int i = 0; i < 100; i++)
			{
				a = new Button();
				a.Margin = new System.Windows.Forms.Padding(0);
				a.Size = new System.Drawing.Size(20, 20);
				a.Text = i.ToString();
				this.flowLayoutPanel1.Controls.Add(a);
			}
// 			this.tableLayoutPanel1.ColumnCount = columnCount;
// 
// 			this.tableLayoutPanel1.ColumnStyles.Clear();
// 			this.tableLayoutPanel1.RowStyles.Clear();
// 
// 			for (int i = 0; i < columnCount; i++)
// 			{
// 				this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / columnCount));
// 			}
// 			for (int i = 0; i < rowCount; i++)
// 			{
// 				this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / rowCount));
// 			}
// 
// 			for (int i = 0; i < rowCount; i++)
// 			{
// 				for (int j = 0; j < columnCount; j++)
// 				{
// 
// 					var button = new Button();
// 					button.Text = string.Format("{0}{1}", i, j);
// 					button.Name = string.Format("button_{0}{1}", i, j);
// 					button.Dock = DockStyle.Fill;
// 					this.tableLayoutPanel1.Controls.Add(button, j, i);
// 				}
// 			}
		}

	}
}
