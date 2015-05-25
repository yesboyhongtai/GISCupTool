namespace PolygonDemo
{
    partial class ShpDemo
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
            this.btnPan = new System.Windows.Forms.Button();
            this.btnZoomWin = new System.Windows.Forms.Button();
            this.pMain = new System.Windows.Forms.Panel();
            this.mbMap = new SharpMap.Forms.MapBox();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnRuler = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbX = new System.Windows.Forms.Label();
            this.lbY = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btn_loadPoints = new System.Windows.Forms.Button();
            this.btn_loadLines = new System.Windows.Forms.Button();
            this.btn_loadSimpLines = new System.Windows.Forms.Button();
            this.btn_clearsimplines = new System.Windows.Forms.Button();
            this.btn_clrChoosed = new System.Windows.Forms.Button();
            this.pMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPan
            // 
            this.btnPan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPan.Location = new System.Drawing.Point(814, 22);
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(82, 23);
            this.btnPan.TabIndex = 3;
            this.btnPan.Text = "Pan";
            this.btnPan.UseVisualStyleBackColor = true;
            this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
            // 
            // btnZoomWin
            // 
            this.btnZoomWin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomWin.Location = new System.Drawing.Point(814, 63);
            this.btnZoomWin.Name = "btnZoomWin";
            this.btnZoomWin.Size = new System.Drawing.Size(82, 23);
            this.btnZoomWin.TabIndex = 4;
            this.btnZoomWin.Text = "ZoomWin";
            this.btnZoomWin.UseVisualStyleBackColor = true;
            this.btnZoomWin.Click += new System.EventHandler(this.btnZoomWin_Click);
            // 
            // pMain
            // 
            this.pMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pMain.Controls.Add(this.mbMap);
            this.pMain.Location = new System.Drawing.Point(12, 12);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(784, 557);
            this.pMain.TabIndex = 5;
            // 
            // mbMap
            // 
            this.mbMap.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            this.mbMap.BackColor = System.Drawing.Color.White;
            this.mbMap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mbMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbMap.FineZoomFactor = 10D;
            this.mbMap.Location = new System.Drawing.Point(0, 0);
            this.mbMap.Name = "mbMap";
            this.mbMap.QueryGrowFactor = 5F;
            this.mbMap.QueryLayerIndex = 0;
            this.mbMap.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mbMap.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mbMap.ShowProgressUpdate = false;
            this.mbMap.Size = new System.Drawing.Size(782, 555);
            this.mbMap.TabIndex = 2;
            this.mbMap.Text = "mapBox1";
            this.mbMap.WheelZoomMagnitude = -3D;
            this.mbMap.MouseMove += new SharpMap.Forms.MapBox.MouseEventHandler(this.mbMap_MouseMove);
            this.mbMap.GeometryDefined += new SharpMap.Forms.MapBox.GeometryDefinedHandler(this.mbMap_GeometryDefined);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomOut.Location = new System.Drawing.Point(814, 104);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(82, 23);
            this.btnZoomOut.TabIndex = 7;
            this.btnZoomOut.Text = "ZoomOut";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnRuler
            // 
            this.btnRuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRuler.Location = new System.Drawing.Point(814, 145);
            this.btnRuler.Name = "btnRuler";
            this.btnRuler.Size = new System.Drawing.Size(82, 23);
            this.btnRuler.TabIndex = 8;
            this.btnRuler.Text = "Ruler";
            this.btnRuler.UseVisualStyleBackColor = true;
            this.btnRuler.Click += new System.EventHandler(this.btnRuler_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(810, 514);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(810, 540);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Y:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbX
            // 
            this.lbX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbX.AutoSize = true;
            this.lbX.Location = new System.Drawing.Point(833, 514);
            this.lbX.Name = "lbX";
            this.lbX.Size = new System.Drawing.Size(0, 12);
            this.lbX.TabIndex = 10;
            // 
            // lbY
            // 
            this.lbY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbY.AutoSize = true;
            this.lbY.Location = new System.Drawing.Point(833, 540);
            this.lbY.Name = "lbY";
            this.lbY.Size = new System.Drawing.Size(0, 12);
            this.lbY.TabIndex = 10;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(803, 354);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(93, 23);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "ClearAll";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btn_loadPoints
            // 
            this.btn_loadPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_loadPoints.Location = new System.Drawing.Point(803, 231);
            this.btn_loadPoints.Name = "btn_loadPoints";
            this.btn_loadPoints.Size = new System.Drawing.Size(93, 23);
            this.btn_loadPoints.TabIndex = 10;
            this.btn_loadPoints.Text = "LoadPoints";
            this.btn_loadPoints.UseVisualStyleBackColor = true;
            this.btn_loadPoints.Click += new System.EventHandler(this.btn_loadPoints_Click);
            // 
            // btn_loadLines
            // 
            this.btn_loadLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_loadLines.Location = new System.Drawing.Point(803, 273);
            this.btn_loadLines.Name = "btn_loadLines";
            this.btn_loadLines.Size = new System.Drawing.Size(93, 23);
            this.btn_loadLines.TabIndex = 12;
            this.btn_loadLines.Text = "LoadLines";
            this.btn_loadLines.UseVisualStyleBackColor = true;
            this.btn_loadLines.Click += new System.EventHandler(this.btn_loadLines_Click);
            // 
            // btn_loadSimpLines
            // 
            this.btn_loadSimpLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_loadSimpLines.Location = new System.Drawing.Point(802, 312);
            this.btn_loadSimpLines.Name = "btn_loadSimpLines";
            this.btn_loadSimpLines.Size = new System.Drawing.Size(94, 23);
            this.btn_loadSimpLines.TabIndex = 13;
            this.btn_loadSimpLines.Text = "LoadSimpLines";
            this.btn_loadSimpLines.UseVisualStyleBackColor = true;
            this.btn_loadSimpLines.Click += new System.EventHandler(this.btn_loadSimpLines_Click);
            // 
            // btn_clearsimplines
            // 
            this.btn_clearsimplines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_clearsimplines.Location = new System.Drawing.Point(803, 397);
            this.btn_clearsimplines.Name = "btn_clearsimplines";
            this.btn_clearsimplines.Size = new System.Drawing.Size(93, 23);
            this.btn_clearsimplines.TabIndex = 14;
            this.btn_clearsimplines.Text = "ClrSimpLines";
            this.btn_clearsimplines.UseVisualStyleBackColor = true;
            this.btn_clearsimplines.Click += new System.EventHandler(this.btn_clearsimplines_Click);
            // 
            // btn_clrChoosed
            // 
            this.btn_clrChoosed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_clrChoosed.Location = new System.Drawing.Point(803, 440);
            this.btn_clrChoosed.Name = "btn_clrChoosed";
            this.btn_clrChoosed.Size = new System.Drawing.Size(93, 23);
            this.btn_clrChoosed.TabIndex = 15;
            this.btn_clrChoosed.Text = "ClrChoosed";
            this.btn_clrChoosed.UseVisualStyleBackColor = true;
            this.btn_clrChoosed.Click += new System.EventHandler(this.btn_clrChoosed_Click);
            // 
            // ShpDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 581);
            this.Controls.Add(this.btn_clrChoosed);
            this.Controls.Add(this.btn_clearsimplines);
            this.Controls.Add(this.btn_loadSimpLines);
            this.Controls.Add(this.btn_loadLines);
            this.Controls.Add(this.btn_loadPoints);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lbY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRuler);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.btnZoomWin);
            this.Controls.Add(this.btnPan);
            this.Name = "ShpDemo";
            this.Text = "GISCupDemo";
            this.Load += new System.EventHandler(this.ShpDemo_Load);
            this.SizeChanged += new System.EventHandler(this.ShpDemo_SizeChanged);
            this.pMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPan;
        private System.Windows.Forms.Button btnZoomWin;
        private System.Windows.Forms.Panel pMain;
        private SharpMap.Forms.MapBox mbMap;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnRuler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbX;
        private System.Windows.Forms.Label lbY;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btn_loadPoints;
        private System.Windows.Forms.Button btn_loadLines;
        private System.Windows.Forms.Button btn_loadSimpLines;
        private System.Windows.Forms.Button btn_clearsimplines;
        private System.Windows.Forms.Button btn_clrChoosed;
    }
}