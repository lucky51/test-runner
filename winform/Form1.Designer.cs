namespace TestRunner
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TestTree = new TreeView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            runToolStripMenuItem = new ToolStripMenuItem();
            collapseAllToolStripMenuItem = new ToolStripMenuItem();
            expandAllToolStripMenuItem1 = new ToolStripMenuItem();
            richTextBox1 = new RichTextBox();
            splitContainer1 = new SplitContainer();
            comboBox1 = new ComboBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            folderToolStripMenuItem = new ToolStripMenuItem();
            buildToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStripRoot = new ContextMenuStrip(components);
            refreshToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            expandAllToolStripMenuItem = new ToolStripMenuItem();
            folderBrowserDialog1 = new FolderBrowserDialog();
            timer1 = new System.Windows.Forms.Timer(components);
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            contextMenuStripRoot.SuspendLayout();
            SuspendLayout();
            // 
            // TestTree
            // 
            TestTree.ContextMenuStrip = contextMenuStrip1;
            TestTree.Dock = DockStyle.Fill;
            TestTree.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            TestTree.Location = new Point(0, 0);
            TestTree.Name = "TestTree";
            TestTree.Size = new Size(1272, 791);
            TestTree.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { runToolStripMenuItem, collapseAllToolStripMenuItem, expandAllToolStripMenuItem1 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(137, 70);
            // 
            // runToolStripMenuItem
            // 
            runToolStripMenuItem.Name = "runToolStripMenuItem";
            runToolStripMenuItem.Size = new Size(136, 22);
            runToolStripMenuItem.Text = "Run";
            runToolStripMenuItem.Click += runToolStripMenuItem_Click;
            // 
            // collapseAllToolStripMenuItem
            // 
            collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            collapseAllToolStripMenuItem.Size = new Size(136, 22);
            collapseAllToolStripMenuItem.Text = "Collapse All";
            collapseAllToolStripMenuItem.Click += collapseAllToolStripMenuItem_Click;
            // 
            // expandAllToolStripMenuItem1
            // 
            expandAllToolStripMenuItem1.Name = "expandAllToolStripMenuItem1";
            expandAllToolStripMenuItem1.Size = new Size(136, 22);
            expandAllToolStripMenuItem1.Text = "Expand  All";
            expandAllToolStripMenuItem1.Click += expandAllToolStripMenuItem1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.InactiveCaption;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox1.ForeColor = SystemColors.InfoText;
            richTextBox1.Location = new Point(0, 0);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(1272, 791);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(comboBox1);
            splitContainer1.Panel1.Controls.Add(menuStrip1);
            splitContainer1.Size = new Size(1272, 791);
            splitContainer1.SplitterDistance = 612;
            splitContainer1.TabIndex = 4;
            // 
            // comboBox1
            // 
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox1.BackColor = SystemColors.InactiveCaption;
            comboBox1.Dock = DockStyle.Right;
            comboBox1.Enabled = false;
            comboBox1.Font = new Font("Segoe UI", 11F, FontStyle.Italic, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.ItemHeight = 20;
            comboBox1.Location = new Point(285, 0);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(327, 28);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox1.SelectionChangeCommitted += comboBox1_SelectionChangeCommitted;
            comboBox1.SelectedValueChanged += comboBox1_SelectedValueChanged;
            comboBox1.TextChanged += comboBox1_TextChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.Dock = DockStyle.Bottom;
            menuStrip1.GripStyle = ToolStripGripStyle.Visible;
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, buildToolStripMenuItem });
            menuStrip1.Location = new Point(0, 742);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(612, 49);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.BackColor = SystemColors.Control;
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { folderToolStripMenuItem });
            fileToolStripMenuItem.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            fileToolStripMenuItem.Margin = new Padding(10);
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(60, 25);
            fileToolStripMenuItem.Text = "Open";
            // 
            // folderToolStripMenuItem
            // 
            folderToolStripMenuItem.Name = "folderToolStripMenuItem";
            folderToolStripMenuItem.Size = new Size(133, 26);
            folderToolStripMenuItem.Text = "Folder...";
            folderToolStripMenuItem.Click += folderToolStripMenuItem_Click;
            // 
            // buildToolStripMenuItem
            // 
            buildToolStripMenuItem.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            buildToolStripMenuItem.Margin = new Padding(10);
            buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            buildToolStripMenuItem.Size = new Size(57, 25);
            buildToolStripMenuItem.Text = "Build";
            buildToolStripMenuItem.Click += buildToolStripMenuItem_Click;
            // 
            // contextMenuStripRoot
            // 
            contextMenuStripRoot.Items.AddRange(new ToolStripItem[] { refreshToolStripMenuItem, toolStripMenuItem1, expandAllToolStripMenuItem });
            contextMenuStripRoot.Name = "contextMenuStripRoot";
            contextMenuStripRoot.Size = new Size(137, 70);
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Size = new Size(136, 22);
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(136, 22);
            toolStripMenuItem1.Text = "Collapse All";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // expandAllToolStripMenuItem
            // 
            expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            expandAllToolStripMenuItem.Size = new Size(136, 22);
            expandAllToolStripMenuItem.Text = "Expand All";
            expandAllToolStripMenuItem.Click += expandAllToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1272, 791);
            Controls.Add(splitContainer1);
            Controls.Add(richTextBox1);
            Controls.Add(TestTree);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "TestRunner";
            contextMenuStrip1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            contextMenuStripRoot.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TreeView TestTree;
        private ContextMenuStrip contextMenuStrip1;

        private RichTextBox richTextBox1;

        private SplitContainer splitContainer1;
        private ToolStripMenuItem runToolStripMenuItem;
        private ContextMenuStrip contextMenuStripRoot;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem expandAllToolStripMenuItem;
        private ToolStripMenuItem collapseAllToolStripMenuItem;
        private ToolStripMenuItem expandAllToolStripMenuItem1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem folderToolStripMenuItem;
        private FolderBrowserDialog folderBrowserDialog1;
        private ToolStripMenuItem buildToolStripMenuItem;
        private ComboBox comboBox1;
        private System.Windows.Forms.Timer timer1;
    }
}