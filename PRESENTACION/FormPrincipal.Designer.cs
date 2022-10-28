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
            this.datosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PanelMenuLateral = new System.Windows.Forms.Panel();
            this.SubPanelConfiguraciones = new System.Windows.Forms.Panel();
            this.Btn = new FontAwesome.Sharp.IconButton();
            this.BtnFel = new FontAwesome.Sharp.IconButton();
            this.BtnEmpresa = new FontAwesome.Sharp.IconButton();
            this.BtnConfiguraciones = new FontAwesome.Sharp.IconButton();
            this.PanelCabeceraMenu = new System.Windows.Forms.Panel();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.iconButton3 = new FontAwesome.Sharp.IconButton();
            this.iconButton4 = new FontAwesome.Sharp.IconButton();
            this.menuStrip1.SuspendLayout();
            this.PanelMenuLateral.SuspendLayout();
            this.SubPanelConfiguraciones.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(13)))), ((int)(((byte)(21)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.configuracionesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(1, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(798, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.Checked = true;
            this.archivoToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.archivoToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarToolStripMenuItem});
            this.archivoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            this.archivoToolStripMenuItem.DropDownClosed += new System.EventHandler(this.archivoToolStripMenuItem_DropDownClosed);
            this.archivoToolStripMenuItem.DropDownOpened += new System.EventHandler(this.archivoToolStripMenuItem_DropDownOpened);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.editarToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // configuracionesToolStripMenuItem
            // 
            this.configuracionesToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.configuracionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datosToolStripMenuItem});
            this.configuracionesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.configuracionesToolStripMenuItem.Name = "configuracionesToolStripMenuItem";
            this.configuracionesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.configuracionesToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.configuracionesToolStripMenuItem.Text = "Configuraciones";
            this.configuracionesToolStripMenuItem.DropDownClosed += new System.EventHandler(this.configuracionesToolStripMenuItem_DropDownClosed);
            this.configuracionesToolStripMenuItem.DropDownOpened += new System.EventHandler(this.configuracionesToolStripMenuItem_DropDownOpened);
            // 
            // datosToolStripMenuItem
            // 
            this.datosToolStripMenuItem.Name = "datosToolStripMenuItem";
            this.datosToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.datosToolStripMenuItem.Text = "Datos";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(13)))), ((int)(((byte)(21)))));
            this.statusStrip1.Location = new System.Drawing.Point(1, 464);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(798, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // PanelMenuLateral
            // 
            this.PanelMenuLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(13)))), ((int)(((byte)(21)))));
            this.PanelMenuLateral.Controls.Add(this.panel1);
            this.PanelMenuLateral.Controls.Add(this.iconButton1);
            this.PanelMenuLateral.Controls.Add(this.SubPanelConfiguraciones);
            this.PanelMenuLateral.Controls.Add(this.BtnConfiguraciones);
            this.PanelMenuLateral.Controls.Add(this.PanelCabeceraMenu);
            this.PanelMenuLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelMenuLateral.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PanelMenuLateral.Location = new System.Drawing.Point(1, 24);
            this.PanelMenuLateral.Name = "PanelMenuLateral";
            this.PanelMenuLateral.Size = new System.Drawing.Size(200, 440);
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
            this.Btn.Text = "Empresa";
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
            this.BtnFel.Text = "Localidades";
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
            this.BtnEmpresa.Text = "General";
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
            this.BtnConfiguraciones.IconChar = FontAwesome.Sharp.IconChar.Server;
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
            // iconButton1
            // 
            this.iconButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.ChartGantt;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.Location = new System.Drawing.Point(0, 223);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(200, 45);
            this.iconButton1.TabIndex = 3;
            this.iconButton1.Text = "Ventas                  ";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.iconButton2);
            this.panel1.Controls.Add(this.iconButton3);
            this.panel1.Controls.Add(this.iconButton4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 268);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 115);
            this.panel1.TabIndex = 4;
            // 
            // iconButton2
            // 
            this.iconButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(13)))), ((int)(((byte)(21)))));
            this.iconButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.iconButton2.ForeColor = System.Drawing.Color.Silver;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton2.IconColor = System.Drawing.Color.Black;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.Location = new System.Drawing.Point(0, 60);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Size = new System.Drawing.Size(200, 30);
            this.iconButton2.TabIndex = 2;
            this.iconButton2.Text = "Devoluciones";
            this.iconButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.UseVisualStyleBackColor = false;
            // 
            // iconButton3
            // 
            this.iconButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(13)))), ((int)(((byte)(21)))));
            this.iconButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton3.FlatAppearance.BorderSize = 0;
            this.iconButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.iconButton3.ForeColor = System.Drawing.Color.Silver;
            this.iconButton3.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton3.IconColor = System.Drawing.Color.Black;
            this.iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton3.Location = new System.Drawing.Point(0, 30);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.Size = new System.Drawing.Size(200, 30);
            this.iconButton3.TabIndex = 1;
            this.iconButton3.Text = "Cotizaciones";
            this.iconButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton3.UseVisualStyleBackColor = false;
            // 
            // iconButton4
            // 
            this.iconButton4.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton4.FlatAppearance.BorderSize = 0;
            this.iconButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.iconButton4.ForeColor = System.Drawing.Color.Silver;
            this.iconButton4.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton4.IconColor = System.Drawing.Color.Black;
            this.iconButton4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton4.Location = new System.Drawing.Point(0, 0);
            this.iconButton4.Name = "iconButton4";
            this.iconButton4.Size = new System.Drawing.Size(200, 30);
            this.iconButton4.TabIndex = 0;
            this.iconButton4.Text = "Pedidos del Cliente";
            this.iconButton4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton4.UseVisualStyleBackColor = true;
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
            this.Padding = new System.Windows.Forms.Padding(1, 0, 1, 1);
            this.Text = "FormPrincipal";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.PanelMenuLateral.ResumeLayout(false);
            this.SubPanelConfiguraciones.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
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
        private ToolStripMenuItem datosToolStripMenuItem;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton iconButton4;
        private FontAwesome.Sharp.IconButton iconButton1;
    }
}