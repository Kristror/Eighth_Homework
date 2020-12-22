using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrustGame2
{
    public partial class Form1 : Form
    {
        TrueFalse database;
        public Form1()
        {
            InitializeComponent();            
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if(database == null)
            {
                MessageBox.Show("Не выбран файл с вопросами", "Ошибка");
                return;
            }

            textBox.Text = database.Next();
        }
    }
}
