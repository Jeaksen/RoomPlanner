using Lab2_winforms.Properties;

namespace Lab2_winforms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.add_furniture_box = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.coffee_button = new System.Windows.Forms.Button();
            this.table_button = new System.Windows.Forms.Button();
            this.sofa_button = new System.Windows.Forms.Button();
            this.bed_button = new System.Windows.Forms.Button();
            this.wall_button = new System.Windows.Forms.Button();
            this.created_furniture_box = new System.Windows.Forms.GroupBox();
            this.furnitureList = new System.Windows.Forms.ListBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.BlueprintMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.add_furniture_box.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.created_furniture_box.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.AutoScroll = true;
            this.splitContainer.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer.Size = new System.Drawing.Size(1069, 528);
            this.splitContainer.SplitterDistance = 730;
            this.splitContainer.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(730, 528);
            this.panel1.TabIndex = 1;
            this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(730, 528);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.add_furniture_box, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.created_furniture_box, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(335, 528);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // add_furniture_box
            // 
            this.add_furniture_box.Controls.Add(this.flowLayoutPanel1);
            this.add_furniture_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.add_furniture_box.Location = new System.Drawing.Point(3, 3);
            this.add_furniture_box.Name = "add_furniture_box";
            this.add_furniture_box.Size = new System.Drawing.Size(329, 258);
            this.add_furniture_box.TabIndex = 0;
            this.add_furniture_box.TabStop = false;
            this.add_furniture_box.Text = "Add furniture";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.coffee_button);
            this.flowLayoutPanel1.Controls.Add(this.table_button);
            this.flowLayoutPanel1.Controls.Add(this.sofa_button);
            this.flowLayoutPanel1.Controls.Add(this.bed_button);
            this.flowLayoutPanel1.Controls.Add(this.wall_button);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(323, 239);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // coffee_button
            // 
            this.coffee_button.BackColor = System.Drawing.Color.White;
            this.coffee_button.BackgroundImage = global::Lab2_winforms.Properties.Resources.coffee_table;
            this.coffee_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.coffee_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.coffee_button.Location = new System.Drawing.Point(3, 3);
            this.coffee_button.Name = "coffee_button";
            this.coffee_button.Size = new System.Drawing.Size(75, 75);
            this.coffee_button.TabIndex = 0;
            this.coffee_button.Tag = global::Lab2_winforms.Properties.Resources.coffee_table;
            this.coffee_button.UseVisualStyleBackColor = false;
            this.coffee_button.Click += new System.EventHandler(this.Furniture_Button_Click);
            // 
            // table_button
            // 
            this.table_button.BackColor = System.Drawing.Color.White;
            this.table_button.BackgroundImage = global::Lab2_winforms.Properties.Resources.table;
            this.table_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.table_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.table_button.Location = new System.Drawing.Point(84, 3);
            this.table_button.Name = "table_button";
            this.table_button.Size = new System.Drawing.Size(75, 75);
            this.table_button.TabIndex = 1;
            this.table_button.Tag = global::Lab2_winforms.Properties.Resources.table;
            this.table_button.UseVisualStyleBackColor = false;
            this.table_button.Click += new System.EventHandler(this.Furniture_Button_Click);
            // 
            // sofa_button
            // 
            this.sofa_button.BackColor = System.Drawing.Color.White;
            this.sofa_button.BackgroundImage = global::Lab2_winforms.Properties.Resources.sofa;
            this.sofa_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sofa_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sofa_button.Location = new System.Drawing.Point(165, 3);
            this.sofa_button.Name = "sofa_button";
            this.sofa_button.Size = new System.Drawing.Size(75, 75);
            this.sofa_button.TabIndex = 2;
            this.sofa_button.Tag = global::Lab2_winforms.Properties.Resources.sofa;
            this.sofa_button.UseVisualStyleBackColor = false;
            this.sofa_button.Click += new System.EventHandler(this.Furniture_Button_Click);
            // 
            // bed_button
            // 
            this.bed_button.BackColor = System.Drawing.Color.White;
            this.bed_button.BackgroundImage = global::Lab2_winforms.Properties.Resources.double_bed;
            this.bed_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bed_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bed_button.Location = new System.Drawing.Point(3, 84);
            this.bed_button.Name = "bed_button";
            this.bed_button.Size = new System.Drawing.Size(75, 75);
            this.bed_button.TabIndex = 3;
            this.bed_button.Tag = global::Lab2_winforms.Properties.Resources.double_bed;
            this.bed_button.UseVisualStyleBackColor = false;
            this.bed_button.Click += new System.EventHandler(this.Furniture_Button_Click);
            // 
            // wall_button
            // 
            this.wall_button.BackColor = System.Drawing.Color.White;
            this.wall_button.BackgroundImage = global::Lab2_winforms.Properties.Resources.wall;
            this.wall_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.wall_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.wall_button.Location = new System.Drawing.Point(84, 84);
            this.wall_button.Name = "wall_button";
            this.wall_button.Size = new System.Drawing.Size(75, 75);
            this.wall_button.TabIndex = 4;
            this.wall_button.Tag = global::Lab2_winforms.Properties.Resources.wall;
            this.wall_button.UseVisualStyleBackColor = false;
            // 
            // created_furniture_box
            // 
            this.created_furniture_box.Controls.Add(this.furnitureList);
            this.created_furniture_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.created_furniture_box.Location = new System.Drawing.Point(3, 267);
            this.created_furniture_box.Name = "created_furniture_box";
            this.created_furniture_box.Size = new System.Drawing.Size(329, 258);
            this.created_furniture_box.TabIndex = 1;
            this.created_furniture_box.TabStop = false;
            this.created_furniture_box.Text = "Created furniture";
            // 
            // furnitureList
            // 
            this.furnitureList.DataSource = this.bindingSource1;
            this.furnitureList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.furnitureList.FormattingEnabled = true;
            this.furnitureList.Location = new System.Drawing.Point(3, 16);
            this.furnitureList.Name = "furnitureList";
            this.furnitureList.Size = new System.Drawing.Size(323, 239);
            this.furnitureList.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BlueprintMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1069, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip";
            // 
            // BlueprintMenuItem
            // 
            this.BlueprintMenuItem.Name = "BlueprintMenuItem";
            this.BlueprintMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.BlueprintMenuItem.Size = new System.Drawing.Size(94, 20);
            this.BlueprintMenuItem.Text = "New blueprint";
            this.BlueprintMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 552);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip1);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Room planner";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.add_furniture_box.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.created_furniture_box.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox add_furniture_box;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox created_furniture_box;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem BlueprintMenuItem;
        private System.Windows.Forms.Button coffee_button;
        private System.Windows.Forms.Button table_button;
        private System.Windows.Forms.Button sofa_button;
        private System.Windows.Forms.Button bed_button;
        private System.Windows.Forms.ListBox furnitureList;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button wall_button;
    }
}

