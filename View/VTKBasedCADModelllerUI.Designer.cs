namespace WindowsFormsApp1
{
    partial class VTKBasedCADModelllerUI
    {
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

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sketchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sketchPanel = new System.Windows.Forms.Panel();
            this.planesGroupBox = new System.Windows.Forms.GroupBox();
            this.xyButton = new System.Windows.Forms.Button();
            this.yzButton = new System.Windows.Forms.Button();
            this.zxButton = new System.Windows.Forms.Button();
            this.shapesGroupBox = new System.Windows.Forms.GroupBox();
            this.circleButton = new System.Windows.Forms.Button();
            this.lineButton = new System.Windows.Forms.Button();
            this.pointButton = new System.Windows.Forms.Button();
            this.arcButton = new System.Windows.Forms.Button();
            this.ellipseButton = new System.Windows.Forms.Button();
            this.plainPanel = new System.Windows.Forms.Panel();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.viewButton = new System.Windows.Forms.Button();
            this.savePanel = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.deletePanel = new System.Windows.Forms.Panel();
            this.deleteButton = new System.Windows.Forms.Button();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.propertiesLabel = new System.Windows.Forms.Label();
            this.propertiesTextBox = new System.Windows.Forms.TextBox();
            this.constraintTextBox = new System.Windows.Forms.TextBox();
            this.renderWindowControl1 = new Kitware.VTK.RenderWindowControl();
            this.menuStrip1.SuspendLayout();
            this.sketchPanel.SuspendLayout();
            this.planesGroupBox.SuspendLayout();
            this.shapesGroupBox.SuspendLayout();
            this.viewPanel.SuspendLayout();
            this.savePanel.SuspendLayout();
            this.deletePanel.SuspendLayout();
            this.propertiesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sketchToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1696, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sketchToolStripMenuItem
            // 
            this.sketchToolStripMenuItem.Name = "sketchToolStripMenuItem";
            this.sketchToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.sketchToolStripMenuItem.Text = "Sketch";
            this.sketchToolStripMenuItem.Click += new System.EventHandler(this.SketchToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // plainToolStripMenuItem
            // 
            this.plainToolStripMenuItem.Name = "plainToolStripMenuItem";
            this.plainToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // sketchPanel
            // 
            this.sketchPanel.Controls.Add(this.planesGroupBox);
            this.sketchPanel.Controls.Add(this.shapesGroupBox);
            this.sketchPanel.Location = new System.Drawing.Point(12, 27);
            this.sketchPanel.Name = "sketchPanel";
            this.sketchPanel.Size = new System.Drawing.Size(1280, 80); // Adjusted height to accommodate both groups
            this.sketchPanel.TabIndex = 1;
            this.sketchPanel.Visible = false;
            // 
            // planesGroupBox
            // 
            this.planesGroupBox.Controls.Add(this.xyButton);
            this.planesGroupBox.Controls.Add(this.yzButton);
            this.planesGroupBox.Controls.Add(this.zxButton);
            this.planesGroupBox.Location = new System.Drawing.Point(10, 10);
            this.planesGroupBox.Name = "planesGroupBox";
            this.planesGroupBox.Size = new System.Drawing.Size(350, 60); // Adjusted size for more vertical space
            this.planesGroupBox.TabIndex = 1;
            this.planesGroupBox.TabStop = false;
            this.planesGroupBox.Text = "Planes";
            // 
            // xyButton
            // 
            this.xyButton.Location = new System.Drawing.Point(10, 20); // Adjusted to add space from the top
            this.xyButton.Name = "xyButton";
            this.xyButton.Size = new System.Drawing.Size(100, 30);
            this.xyButton.TabIndex = 0;
            this.xyButton.Text = "XY";
            this.xyButton.UseVisualStyleBackColor = true;
            this.xyButton.Click += new System.EventHandler(this.XYButton_Click);
            // 
            // yzButton
            // 
            this.yzButton.Location = new System.Drawing.Point(120, 20); // Adjusted to add space from the top
            this.yzButton.Name = "yzButton";
            this.yzButton.Size = new System.Drawing.Size(100, 30);
            this.yzButton.TabIndex = 1;
            this.yzButton.Text = "YZ";
            this.yzButton.UseVisualStyleBackColor = true;
            this.yzButton.Click += new System.EventHandler(this.YZButton_Click);
            // 
            // zxButton
            // 
            this.zxButton.Location = new System.Drawing.Point(230, 20); // Adjusted to add space from the top
            this.zxButton.Name = "zxButton";
            this.zxButton.Size = new System.Drawing.Size(100, 30);
            this.zxButton.TabIndex = 2;
            this.zxButton.Text = "ZX";
            this.zxButton.UseVisualStyleBackColor = true;
            this.zxButton.Click += new System.EventHandler(this.ZXButton_Click);
            // 
            // shapesGroupBox
            // 
            this.shapesGroupBox.Controls.Add(this.circleButton);
            this.shapesGroupBox.Controls.Add(this.lineButton);
            this.shapesGroupBox.Controls.Add(this.pointButton);
            this.shapesGroupBox.Controls.Add(this.arcButton);
            this.shapesGroupBox.Controls.Add(this.ellipseButton);
            this.shapesGroupBox.Location = new System.Drawing.Point(370, 10); // Positioned next to planesGroupBox
            this.shapesGroupBox.Name = "shapesGroupBox";
            this.shapesGroupBox.Size = new System.Drawing.Size(600, 60); // Adjusted size for more vertical space
            this.shapesGroupBox.TabIndex = 0;
            this.shapesGroupBox.TabStop = false;
            this.shapesGroupBox.Text = "Shapes";
            // 
            // circleButton
            // 
            this.circleButton.Location = new System.Drawing.Point(10, 20); // Adjusted to add space from the top
            this.circleButton.Name = "circleButton";
            this.circleButton.Size = new System.Drawing.Size(100, 30);
            this.circleButton.TabIndex = 0;
            this.circleButton.Text = "Circle";
            this.circleButton.UseVisualStyleBackColor = true;
            this.circleButton.Click += new System.EventHandler(this.CircleButton_Click);
            // 
            // lineButton
            // 
            this.lineButton.Location = new System.Drawing.Point(120, 20); // Adjusted to add space from the top
            this.lineButton.Name = "lineButton";
            this.lineButton.Size = new System.Drawing.Size(100, 30);
            this.lineButton.TabIndex = 1;
            this.lineButton.Text = "Line";
            this.lineButton.UseVisualStyleBackColor = true;
            this.lineButton.Click += new System.EventHandler(this.LineButton_Click);
            // 
            // pointButton
            // 
            this.pointButton.Location = new System.Drawing.Point(230, 20); // Adjusted to add space from the top
            this.pointButton.Name = "pointButton";
            this.pointButton.Size = new System.Drawing.Size(100, 30);
            this.pointButton.TabIndex = 2;
            this.pointButton.Text = "Point";
            this.pointButton.UseVisualStyleBackColor = true;
            this.pointButton.Click += new System.EventHandler(this.PointButton_Click);
            // 
            // arcButton
            // 
            this.arcButton.Location = new System.Drawing.Point(340, 20); // Adjusted to add space from the top
            this.arcButton.Name = "arcButton";
            this.arcButton.Size = new System.Drawing.Size(100, 30);
            this.arcButton.TabIndex = 3;
            this.arcButton.Text = "Arc";
            this.arcButton.UseVisualStyleBackColor = true;
            this.arcButton.Click += new System.EventHandler(this.ArcButton_Click);
            // 
            // ellipseButton
            // 
            this.ellipseButton.Location = new System.Drawing.Point(450, 20); // Adjusted to add space from the top
            this.ellipseButton.Name = "ellipseButton";
            this.ellipseButton.Size = new System.Drawing.Size(100, 30);
            this.ellipseButton.TabIndex = 4;
            this.ellipseButton.Text = "Ellipse";
            this.ellipseButton.UseVisualStyleBackColor = true;
            this.ellipseButton.Click += new System.EventHandler(this.EllipseButton_Click);

            // 
            // plainPanel
            // 
            this.plainPanel.Location = new System.Drawing.Point(12, 27);
            this.plainPanel.Name = "plainPanel";
            this.plainPanel.Size = new System.Drawing.Size(1280, 40);
            this.plainPanel.TabIndex = 2;
            this.plainPanel.Visible = false;
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.viewButton);
            this.viewPanel.Location = new System.Drawing.Point(12, 27);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(1280, 40);
            this.viewPanel.TabIndex = 3;
            this.viewPanel.Visible = false;
            // 
            // viewButton
            // 
            this.viewButton.Location = new System.Drawing.Point(10, 10);
            this.viewButton.Name = "viewButton";
            this.viewButton.Size = new System.Drawing.Size(100, 30);
            this.viewButton.TabIndex = 0;
            this.viewButton.Text = "View";
            this.viewButton.UseVisualStyleBackColor = true;
            this.viewButton.Click += new System.EventHandler(this.ViewButton_Click);
            // 
            // savePanel
            // 
            this.savePanel.Controls.Add(this.saveButton);
            this.savePanel.Location = new System.Drawing.Point(12, 27);
            this.savePanel.Name = "savePanel";
            this.savePanel.Size = new System.Drawing.Size(1280, 40);
            this.savePanel.TabIndex = 4;
            this.savePanel.Visible = false;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(10, 10);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 30);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // deletePanel
            // 
            this.deletePanel.Controls.Add(this.deleteButton);
            this.deletePanel.Location = new System.Drawing.Point(12, 27);
            this.deletePanel.Name = "deletePanel";
            this.deletePanel.Size = new System.Drawing.Size(1671, 55);
            this.deletePanel.TabIndex = 5;
            this.deletePanel.Visible = false;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(10, 10);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 30);
            this.deleteButton.TabIndex = 0;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.Controls.Add(this.propertiesLabel);
            this.propertiesPanel.Controls.Add(this.propertiesTextBox);
            this.propertiesPanel.Location = new System.Drawing.Point(906, 230);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(217, 40);
            this.propertiesPanel.TabIndex = 6;
            // 
            // propertiesLabel
            // 
            this.propertiesLabel.Location = new System.Drawing.Point(0, 0);
            this.propertiesLabel.Name = "propertiesLabel";
            this.propertiesLabel.Size = new System.Drawing.Size(100, 23);
            this.propertiesLabel.TabIndex = 0;
            // 
            // propertiesTextBox
            // 
            this.propertiesTextBox.Location = new System.Drawing.Point(0, 0);
            this.propertiesTextBox.Name = "propertiesTextBox";
            this.propertiesTextBox.Size = new System.Drawing.Size(100, 22);
            this.propertiesTextBox.TabIndex = 1;
            // 
            // constraintTextBox
            // 
            this.constraintTextBox.Location = new System.Drawing.Point(0, 0);
            this.constraintTextBox.Name = "constraintTextBox";
            this.constraintTextBox.Size = new System.Drawing.Size(100, 22);
            this.constraintTextBox.TabIndex = 0;
            // 
            // renderWindowControl1
            // 
            this.renderWindowControl1.AddTestActors = false;
            this.renderWindowControl1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.renderWindowControl1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.renderWindowControl1.Location = new System.Drawing.Point(12, 134);
            this.renderWindowControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.renderWindowControl1.Name = "renderWindowControl1";
            this.renderWindowControl1.Size = new System.Drawing.Size(1671, 762);
            this.renderWindowControl1.TabIndex = 6;
            this.renderWindowControl1.TestText = null;
            this.renderWindowControl1.Load += new System.EventHandler(this.RenderWindowControl1_Load);
            // 
            // VTKBasedCADModelllerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1696, 901);
            this.Controls.Add(this.renderWindowControl1);
            this.Controls.Add(this.propertiesPanel);
            this.Controls.Add(this.deletePanel);
            this.Controls.Add(this.savePanel);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.plainPanel);
            this.Controls.Add(this.sketchPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "VTKBasedCADModelllerUI";
            this.Text = "VTK Based CAD Modeller";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.sketchPanel.ResumeLayout(false);
            this.planesGroupBox.ResumeLayout(false);
            this.shapesGroupBox.ResumeLayout(false);
            this.viewPanel.ResumeLayout(false);
            this.savePanel.ResumeLayout(false);
            this.deletePanel.ResumeLayout(false);
            this.propertiesPanel.ResumeLayout(false);
            this.propertiesPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sketchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Panel sketchPanel;
        private System.Windows.Forms.Button circleButton;
        private System.Windows.Forms.Button lineButton;
        private System.Windows.Forms.Button pointButton;
        private System.Windows.Forms.Button arcButton;
        private System.Windows.Forms.Button ellipseButton;
        private System.Windows.Forms.Panel plainPanel;
        private System.Windows.Forms.Button xyButton;
        private System.Windows.Forms.Button yzButton;
        private System.Windows.Forms.Button zxButton;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.Button viewButton;
        private System.Windows.Forms.Panel savePanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Panel deletePanel;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Panel propertiesPanel;
        private System.Windows.Forms.Label propertiesLabel;
        private System.Windows.Forms.TextBox propertiesTextBox;
        private System.Windows.Forms.TextBox constraintTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox constraintXTextBox;
        private System.Windows.Forms.TextBox constraintYTextBox;
        private System.Windows.Forms.GroupBox shapesGroupBox;
        private System.Windows.Forms.GroupBox planesGroupBox;
        private Kitware.VTK.RenderWindowControl renderWindowControl1;
    }
}
