using BIPS.NEGOCIO;

namespace BIPS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImplementacionFEL implementacionFEL = new ImplementacionFEL();
            implementacionFEL.ProbarImplementacion();
        }
    }
}