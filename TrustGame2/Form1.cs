using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrueFalseLib;

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
            bool anwser = rbtTrue.Checked ? true : false;
            
            database.Check(anwser);
            string text = database.Next();
            if (text == "Конец")
            {
                var res = MessageBox.Show($@"Вы набрали {database.rightAns} баллов.
Хотите начать сначала?",text, MessageBoxButtons.YesNo);
                if(res == DialogResult.Yes)
                {
                    database.Restart();
                    textBox.Text = database.Next();
                }
                return;
            }
            textBox.Text = text ;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML files (*.xml)|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(ofd.FileName);
                database.Load();
                textBox.Text = database.Next();
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            database.Restart();
            textBox.Text = database.Next();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
