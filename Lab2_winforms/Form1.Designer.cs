using Lab2_winforms.Properties;

namespace Lab2_winforms
{
    partial class RoomPlanner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomPlanner));
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
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newBlueprintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polskiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            resources.ApplyResources(this.splitContainer.Panel1, "splitContainer.Panel1");
            this.splitContainer.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanel1);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Name = "panel1";
            this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseWheel);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.add_furniture_box, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.created_furniture_box, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // add_furniture_box
            // 
            this.add_furniture_box.Controls.Add(this.flowLayoutPanel1);
            resources.ApplyResources(this.add_furniture_box, "add_furniture_box");
            this.add_furniture_box.Name = "add_furniture_box";
            this.add_furniture_box.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.coffee_button);
            this.flowLayoutPanel1.Controls.Add(this.table_button);
            this.flowLayoutPanel1.Controls.Add(this.sofa_button);
            this.flowLayoutPanel1.Controls.Add(this.bed_button);
            this.flowLayoutPanel1.Controls.Add(this.wall_button);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // coffee_button
            // 
            this.coffee_button.BackColor = System.Drawing.Color.White;
            this.coffee_button.BackgroundImage = global::Lab2_winforms.Properties.Resources.coffee_table;
            resources.ApplyResources(this.coffee_button, "coffee_button");
            this.coffee_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.coffee_button.Name = "coffee_button";
            this.coffee_button.Tag = global::Lab2_winforms.Properties.Resources.coffee_table;
            this.coffee_button.UseVisualStyleBackColor = false;
            this.coffee_button.Click += new System.EventHandler(this.Furniture_Button_Click);
            // 
            // table_button
            // 
            this.table_button.BackColor = System.Drawing.Color.White;
            this.table_button.BackgroundImage = global::Lab2_winforms.Properties.Resources.table;
            resources.ApplyResources(this.table_button, "table_button");
            this.table_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.table_button.Name = "table_button";
            this.table_button.Tag = global::Lab2_winforms.Properties.Resources.table;
            this.table_button.UseVisualStyleBackColor = false;
            this.table_button.Click += new System.EventHandler(this.Furniture_Button_Click);
            // 
            // sofa_button
            // 
            this.sofa_button.BackColor = System.Drawing.Color.White;
            this.sofa_button.BackgroundImage = global::Lab2_winforms.Properties.Resources.sofa;
            resources.ApplyResources(this.sofa_button, "sofa_button");
            this.sofa_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sofa_button.Name = "sofa_button";
            this.sofa_button.Tag = global::Lab2_winforms.Properties.Resources.sofa;
            this.sofa_button.UseVisualStyleBackColor = false;
            this.sofa_button.Click += new System.EventHandler(this.Furniture_Button_Click);
            // 
            // bed_button
            // 
            this.bed_button.BackColor = System.Drawing.Color.White;
            this.bed_button.BackgroundImage = global::Lab2_winforms.Properties.Resources.double_bed;
            resources.ApplyResources(this.bed_button, "bed_button");
            this.bed_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bed_button.Name = "bed_button";
            this.bed_button.Tag = global::Lab2_winforms.Properties.Resources.double_bed;
            this.bed_button.UseVisualStyleBackColor = false;
            this.bed_button.Click += new System.EventHandler(this.Furniture_Button_Click);
            // 
            // wall_button
            // 
            this.wall_button.BackColor = System.Drawing.Color.White;
            this.wall_button.BackgroundImage = global::Lab2_winforms.Properties.Resources.wall;
            resources.ApplyResources(this.wall_button, "wall_button");
            this.wall_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.wall_button.Name = "wall_button";
            this.wall_button.Tag = global::Lab2_winforms.Properties.Resources.wall;
            this.wall_button.UseVisualStyleBackColor = false;
            this.wall_button.Click += new System.EventHandler(this.Furniture_Button_Click);
            // 
            // created_furniture_box
            // 
            this.created_furniture_box.Controls.Add(this.furnitureList);
            resources.ApplyResources(this.created_furniture_box, "created_furniture_box");
            this.created_furniture_box.Name = "created_furniture_box";
            this.created_furniture_box.TabStop = false;
            // 
            // furnitureList
            // 
            this.furnitureList.DataSource = this.bindingSource1;
            resources.ApplyResources(this.furnitureList, "furnitureList");
            this.furnitureList.FormattingEnabled = true;
            this.furnitureList.Name = "furnitureList";
            this.furnitureList.SelectedIndexChanged += new System.EventHandler(this.furnitureList_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBlueprintToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.languageToolStripMenuItem});
            this.FileMenu.Name = "FileMenu";
            resources.ApplyResources(this.FileMenu, "FileMenu");
            // 
            // newBlueprintToolStripMenuItem
            // 
            this.newBlueprintToolStripMenuItem.Name = "newBlueprintToolStripMenuItem";
            resources.ApplyResources(this.newBlueprintToolStripMenuItem, "newBlueprintToolStripMenuItem");
            this.newBlueprintToolStripMenuItem.Click += new System.EventHandler(this.newBitmap_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.Open_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.Save_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.polskiToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // polskiToolStripMenuItem
            // 
            this.polskiToolStripMenuItem.Name = "polskiToolStripMenuItem";
            resources.ApplyResources(this.polskiToolStripMenuItem, "polskiToolStripMenuItem");
            this.polskiToolStripMenuItem.Click += new System.EventHandler(this.polskiToolStripMenuItem_Click);
            // 
            // RoomPlanner
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.Name = "RoomPlanner";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
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
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.Button coffee_button;
        private System.Windows.Forms.Button table_button;
        private System.Windows.Forms.Button sofa_button;
        private System.Windows.Forms.Button bed_button;
        private System.Windows.Forms.ListBox furnitureList;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button wall_button;
        private System.Windows.Forms.ToolStripMenuItem newBlueprintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polskiToolStripMenuItem;
    }
}

