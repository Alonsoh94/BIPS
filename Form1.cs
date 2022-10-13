using BIPS.NEGOCIO.PROCESOS.FEL;

namespace BIPS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ImplementacionFEL implementacionFEL = new ImplementacionFEL();
        private async void button1_Click(object sender, EventArgs e)
        {            
           await implementacionFEL.CertificarDTE();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
          if ( await implementacionFEL.AnularDTE() == false)
            {
                MessageBox.Show(implementacionFEL.MensajeResultado());
                
            }
            
        }
    }
}