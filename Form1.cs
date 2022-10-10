using BIPS.NEGOCIO;

namespace BIPS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ImplementacionFEL implementacionFEL = new ImplementacionFEL();
        private void button1_Click(object sender, EventArgs e)
        {            
            implementacionFEL.ProbarImplementacion();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            implementacionFEL.ProbarAnulacion();
        }
    }
}