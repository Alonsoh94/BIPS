using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIPS.PRESENTACION
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }


        #region Diseño de Menú Superior
        private void configuracionesToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            
            configuracionesToolStripMenuItem.BackColor = Color.Transparent;
            configuracionesToolStripMenuItem.ForeColor = Color.Black;

        }
        private void configuracionesToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            configuracionesToolStripMenuItem.BackColor = Color.Transparent;
            configuracionesToolStripMenuItem.ForeColor = Color.White;
        }

        private void archivoToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            archivoToolStripMenuItem.BackColor = Color.Transparent;
            archivoToolStripMenuItem.ForeColor = Color.Black;
        }

        private void archivoToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            archivoToolStripMenuItem.BackColor = Color.Transparent;
            archivoToolStripMenuItem.ForeColor = Color.White;
        }


        #endregion

        #region Estructura de Colores
        
        private struct ColeccionColoresRGB        {
            public static System.Drawing.Color BipsColor1 = System.Drawing.Color.FromArgb(88, 255, 43);
            public static System.Drawing.Color BipsColor2 = System.Drawing.Color.FromArgb(255, 150, 94);
            public static System.Drawing.Color BipsColor3 = System.Drawing.Color.FromArgb(171, 59, 255);
            public static System.Drawing.Color BipsColor4 = System.Drawing.Color.FromArgb(32, 179, 91);
            public static System.Drawing.Color BipsColor5 = System.Drawing.Color.FromArgb(28, 241, 255);
            public static System.Drawing.Color BipsColor6 = System.Drawing.Color.FromArgb(255, 53, 179);
            public static System.Drawing.Color BipsColor7 = System.Drawing.Color.FromArgb(255, 231, 79);
            public static System.Drawing.Color BipsColor8 = System.Drawing.Color.FromArgb(255, 77, 54);
            public static System.Drawing.Color BipsColor9 = System.Drawing.Color.FromArgb(255, 209, 54);

            public static System.Drawing.Color BipsColorButtonRed = System.Drawing.Color.FromArgb(179, 2, 2);
            public static System.Drawing.Color BipsColorButtonSkyBlue = System.Drawing.Color.FromArgb(0, 109, 179);
            public static System.Drawing.Color BipsColorButtonGreen = System.Drawing.Color.FromArgb(21, 97, 17);
            public static System.Drawing.Color BipsColorButtonTeal = System.Drawing.Color.FromArgb(50, 179, 134);
        }

        #endregion

    }
}
