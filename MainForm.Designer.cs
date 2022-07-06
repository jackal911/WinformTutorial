namespace GenericView
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.ui_canvas2D = new Canvas.Canvas2D();
            this.ui_toolbar = new System.Windows.Forms.ToolStrip();
            this.ui_toolOpen = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ui_toolbar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ui_canvas2D);
            this.splitContainer1.Size = new System.Drawing.Size(979, 507);
            this.splitContainer1.SplitterDistance = 326;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(3, 66);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(320, 395);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // ui_canvas2D
            // 
            this.ui_canvas2D.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ui_canvas2D.CanvasBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ui_canvas2D.CommandText = null;
            this.ui_canvas2D.Cursor = System.Windows.Forms.Cursors.Cross;
            this.ui_canvas2D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_canvas2D.DrawCanvasTextSize = 15;
            this.ui_canvas2D.DrawLastToolMode = -1;
            this.ui_canvas2D.DrawToolMode = -1;
            this.ui_canvas2D.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ui_canvas2D.ForeColor = System.Drawing.SystemColors.Window;
            this.ui_canvas2D.IsAreaProccessing = false;
            this.ui_canvas2D.IsCenterAxis = true;
            this.ui_canvas2D.IsDraw3DAxis = false;
            this.ui_canvas2D.IsMoveEntitySelection = true;
            this.ui_canvas2D.IsOSnapEnable = true;
            this.ui_canvas2D.IsPickProccessing = false;
            this.ui_canvas2D.IsShortcutKeyMode = false;
            this.ui_canvas2D.IsStartOrthogonal = false;
            this.ui_canvas2D.LastOSnap = null;
            this.ui_canvas2D.Location = new System.Drawing.Point(0, 0);
            this.ui_canvas2D.Name = "ui_canvas2D";
            this.ui_canvas2D.SelectPartCtrl = null;
            this.ui_canvas2D.ShowInputText = false;
            this.ui_canvas2D.ShowProperty = true;
            this.ui_canvas2D.Size = new System.Drawing.Size(648, 507);
            this.ui_canvas2D.SymbolName = null;
            this.ui_canvas2D.TabIndex = 0;
            this.ui_canvas2D.ZoomMode = -1;
            // 
            // ui_toolbar
            // 
            this.ui_toolbar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ui_toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ui_toolOpen});
            this.ui_toolbar.Location = new System.Drawing.Point(0, 0);
            this.ui_toolbar.Name = "ui_toolbar";
            this.ui_toolbar.Size = new System.Drawing.Size(1005, 25);
            this.ui_toolbar.TabIndex = 1;
            this.ui_toolbar.Text = "toolStrip1";
            // 
            // ui_toolOpen
            // 
            this.ui_toolOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ui_toolOpen.Image = ((System.Drawing.Image)(resources.GetObject("ui_toolOpen.Image")));
            this.ui_toolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ui_toolOpen.Name = "ui_toolOpen";
            this.ui_toolOpen.Size = new System.Drawing.Size(23, 22);
            this.ui_toolOpen.Text = "toolStripButton1";
            this.ui_toolOpen.ToolTipText = "Open";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Location = new System.Drawing.Point(14, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(979, 507);
            this.panel1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 552);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ui_toolbar);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Generic View";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ui_toolbar.ResumeLayout(false);
            this.ui_toolbar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip ui_toolbar;
        private System.Windows.Forms.ToolStripButton ui_toolOpen;
        private System.Windows.Forms.Panel panel1;
        private Canvas.Canvas2D ui_canvas2D;
        private System.Windows.Forms.TreeView treeView1;
    }
}

