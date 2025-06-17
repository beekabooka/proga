namespace DemoGKV44
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelMaterials = new System.Windows.Forms.Panel();
            this.btnAddMaterial = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelMaterials
            // 
            this.panelMaterials.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMaterials.AutoScroll = true;
            this.panelMaterials.BackColor = System.Drawing.SystemColors.Window;
            this.panelMaterials.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMaterials.Location = new System.Drawing.Point(12, 50);
            this.panelMaterials.Name = "panelMaterials";
            this.panelMaterials.Size = new System.Drawing.Size(560, 400);
            this.panelMaterials.TabIndex = 0;
            // 
            // btnAddMaterial
            // 
            this.btnAddMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddMaterial.BackColor = System.Drawing.Color.SteelBlue;
            this.btnAddMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddMaterial.ForeColor = System.Drawing.Color.White;
            this.btnAddMaterial.Location = new System.Drawing.Point(450, 12);
            this.btnAddMaterial.Name = "btnAddMaterial";
            this.btnAddMaterial.Size = new System.Drawing.Size(122, 32);
            this.btnAddMaterial.TabIndex = 1;
            this.btnAddMaterial.Text = "Добавить";
            this.btnAddMaterial.UseVisualStyleBackColor = false;
            this.btnAddMaterial.Click += new System.EventHandler(this.btnAddMaterial_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.Location = new System.Drawing.Point(12, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(171, 24);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Учет материалов";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnAddMaterial);
            this.Controls.Add(this.panelMaterials);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Система учета материалов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panelMaterials;
        private System.Windows.Forms.Button btnAddMaterial;
        private System.Windows.Forms.Label lblTitle;
    }
}

