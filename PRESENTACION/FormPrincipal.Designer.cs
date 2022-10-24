namespace BIPS.PRESENTACION
{
    partial class FormPrincipal
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PanelMenuLateral = new System.Windows.Forms.Panel();
            this.SubPanelConfiguraciones = new System.Windows.Forms.Panel();
            this.Btn = new FontAwesome.Sharp.IconButton();
            this.BtnFel = new FontAwesome.Sharp.IconButton();
            this.BtnEmpresa = new FontAwesome.Sharp.IconButton();
            this.BtnConfiguraciones = new FontAwesome.Sharp.IconButton();
            this.PanelCabeceraMenu = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.PanelMenuLateral.SuspendLayout();
            this.SubPanelConfiguraciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.configuracionesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarToolStripMenuItem});
            this.archivoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.editarToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // configuracionesToolStripMenuItem
            // 
            this.configuracionesToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.configuracionesToolStripMenuItem.Name = "configuracionesToolStripMenuItem";
            this.configuracionesToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.configuracionesToolStripMenuItem.Text = "Configuraciones";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.statusStrip1.Location = new System.Drawing.Point(0, 465);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // PanelMenuLateral
            // 
            this.PanelMenuLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.PanelMenuLateral.Controls.Add(this.SubPanelConfiguraciones);
            this.PanelMenuLateral.Controls.Add(this.BtnConfiguraciones);
            this.PanelMenuLateral.Controls.Add(this.PanelCabeceraMenu);
            this.PanelMenuLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelMenuLateral.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PanelMenuLateral.Location = new System.Drawing.Point(0, 24);
            this.PanelMenuLateral.Name = "PanelMenuLateral";
            this.PanelMenuLateral.Size = new System.Drawing.Size(200, 441);
            this.PanelMenuLateral.TabIndex = 3;
            // 
            // SubPanelConfiguraciones
            // 
            this.SubPanelConfiguraciones.Controls.Add(this.Btn);
            this.SubPanelConfiguraciones.Controls.Add(this.BtnFel);
            this.SubPanelConfiguraciones.Controls.Add(this.BtnEmpresa);
            this.SubPanelConfiguraciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.SubPanelConfiguraciones.Location = new System.Drawing.Point(0, 108);
            this.SubPanelConfiguraciones.Name = "SubPanelConfiguraciones";
            this.SubPanelConfiguraciones.Size = new System.Drawing.Size(200, 115);
            this.SubPanelConfiguraciones.TabIndex = 2;
            // 
            // Btn
            // 
            this.Btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.Btn.FlatAppearance.BorderSize = 0;
            this.Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Btn.ForeColor = System.Drawing.Color.Silver;
            this.Btn.IconChar = FontAwesome.Sharp.IconChar.None;
            this.Btn.IconColor = System.Drawing.Color.Black;
            this.Btn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Btn.Location = new System.Drawing.Point(0, 60);
            this.Btn.Name = "Btn";
            this.Btn.Size = new System.Drawing.Size(200, 30);
            this.Btn.TabIndex = 2;
            this.Btn.Text = "FEL";
            this.Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn.UseVisualStyleBackColor = true;
            // 
            // BtnFel
            // 
            this.BtnFel.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnFel.FlatAppearance.BorderSize = 0;
            this.BtnFel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.BtnFel.ForeColor = System.Drawing.Color.Silver;
            this.BtnFel.IconChar = FontAwesome.Sharp.IconChar.None;
            this.BtnFel.IconColor = System.Drawing.Color.Black;
            this.BtnFel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtnFel.Location = new System.Drawing.Point(0, 30);
            this.BtnFel.Name = "BtnFel";
            this.BtnFel.Size = new System.Drawing.Size(200, 30);
            this.BtnFel.TabIndex = 1;
            this.BtnFel.Text = "FEL";
            this.BtnFel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnFel.UseVisualStyleBackColor = true;
            // 
            // BtnEmpresa
            // 
            this.BtnEmpresa.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnEmpresa.FlatAppearance.BorderSize = 0;
            this.BtnEmpresa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEmpresa.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.BtnEmpresa.ForeColor = System.Drawing.Color.Silver;
            this.BtnEmpresa.IconChar = FontAwesome.Sharp.IconChar.None;
            this.BtnEmpresa.IconColor = System.Drawing.Color.Black;
            this.BtnEmpresa.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtnEmpresa.Location = new System.Drawing.Point(0, 0);
            this.BtnEmpresa.Name = "BtnEmpresa";
            this.BtnEmpresa.Size = new System.Drawing.Size(200, 30);
            this.BtnEmpresa.TabIndex = 0;
            this.BtnEmpresa.Text = "Empresa";
            this.BtnEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnEmpresa.UseVisualStyleBackColor = true;
            // 
            // BtnConfiguraciones
            // 
            this.BtnConfiguraciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnConfiguraciones.FlatAppearance.BorderSize = 0;
            this.BtnConfiguraciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnConfiguraciones.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.BtnConfiguraciones.ForeColor = System.Drawing.Color.White;
            this.BtnConfiguraciones.IconChar = FontAwesome.Sharp.IconChar.Sellsy;
            this.BtnConfiguraciones.IconColor = System.Drawing.Color.White;
            this.BtnConfiguraciones.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtnConfiguraciones.Location = new System.Drawing.Point(0, 63);
            this.BtnConfiguraciones.Name = "BtnConfiguraciones";
            this.BtnConfiguraciones.Size = new System.Drawing.Size(200, 45);
            this.BtnConfiguraciones.TabIndex = 1;
            this.BtnConfiguraciones.Text = "Configuraciones";
            this.BtnConfiguraciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnConfiguraciones.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnConfiguraciones.UseVisualStyleBackColor = true;
            // 
            // PanelCabeceraMenu
            // 
            this.PanelCabeceraMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelCabeceraMenu.Location = new System.Drawing.Point(0, 0);
            this.PanelCabeceraMenu.Name = "PanelCabeceraMenu";
            this.PanelCabeceraMenu.Size = new System.Drawing.Size(200, 63);
            this.PanelCabeceraMenu.TabIndex = 0;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(800, 487);
            this.Controls.Add(this.PanelMenuLateral);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormPrincipal";
            this.Text = "FormPrincipal";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.PanelMenuLateral.ResumeLayout(false);
            this.SubPanelConfiguraciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem configuracionesToolStripMenuItem;
        private StatusStrip statusStrip1;
        private Panel PanelMenuLateral;
        private Panel SubPanelConfiguraciones;
        private FontAwesome.Sharp.IconButton Btn;
        private FontAwesome.Sharp.IconButton BtnFel;
        private FontAwesome.Sharp.IconButton BtnEmpresa;
        private FontAwesome.Sharp.IconButton BtnConfiguraciones;
        private Panel PanelCabeceraMenu;
        private ToolStripMenuItem editarToolStripMenuItem;
    }
}