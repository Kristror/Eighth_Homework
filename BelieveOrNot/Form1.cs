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

namespace BelieveOrNot
{
    public partial class Form1 : Form
    {
        /// 
        /// Домашняя работа Безукладникова Даниила
        /// 1.  а) Создать приложение, показанное на уроке,
        ///  добавив в него защиту от возможных ошибок 
        ///  (не создана база данных, 
        ///  обращение к несуществующему вопросу,
        ///  открытие слишком большого файла и т.д.).
        ///     б) Изменить интерфейс программы, увеличив шрифт,
        /// поменяв цвет элементов и добавив другие «косметические» улучшения на свое усмотрение.
        ///     в) Добавить в приложение меню «О программе» с информацией о программе (автор, версия,авторские права и др.).
        ///     г) Добавить в приложение сообщение с предупреждением при попытке удалить вопрос.
        ///     д) Добавить пункт меню Save As, в котором можно выбрать имя для сохранения базы данных (элемент SaveFileDialog).
        /// 
        TrueFalse database;
        public Form1()
        {
            InitializeComponent();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Questions";
            sfd.DefaultExt = "xml";
            sfd.Filter = ".xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(sfd.FileName);
                numericUpDown.Minimum = 1;
                numericUpDown.Maximum = 1;
                numericUpDown.Value = 1;
                textBox.ReadOnly = false;
            };
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML files (*.xml)|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(ofd.FileName);
                database.Load();
                numericUpDown.Minimum = 1;
                numericUpDown.Maximum = database.Count;
                numericUpDown.Value = 1;
                textBox.ReadOnly = false;
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (database != null)
            {
                database.Save();
                MessageBox.Show("База данных сохранена", "Успех");
            }
            else MessageBox.Show("База данных не создана", "Ошибка");
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (database != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "Questions";
                sfd.DefaultExt = "xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    database.SaveAs(sfd.FileName);
                    MessageBox.Show("База данных сохранена", "Успех");
                }
            }
            else MessageBox.Show("База данных не создана", "Ошибка");

        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Создана 21.12.2020 Даниилом Безукладниковым
Версия 1.0
Все авторские права принадлежат автору", "О программе");
        }
        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (database == null)
            {
                MessageBox.Show("Создайте новую или откройте базу данных", "Ошибка");
                return;
            }

            int number = (int)numericUpDown.Value - 1;
            if (number < database.Count)
            {
                textBox.Text = database[number].text;
                TrueCheckBox.Checked = database[number].trueFalse;
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (database == null)
            {
                MessageBox.Show("Создайте новую или откройте базу данных", "Ошибка");
                return;
            }
            database.Add((database.Count + 1).ToString(), true);
            numericUpDown.Maximum = database.Count;
            numericUpDown.Value = database.Count;
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (database == null)
            {
                MessageBox.Show("Создайте новую или откройте базу данных", "Ошибка");
                return;
            }
            if (numericUpDown.Maximum == 1)
            {
                MessageBox.Show("Невозможно удалить последний элемент из базу данных", "Ошибка");
                return;
            }
            var res = MessageBox.Show("Вы действительно хотите удалить этот элемент?", "Внимание", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                database.Remove((int)numericUpDown.Value);
                numericUpDown.Maximum--;
                if (numericUpDown.Value > 1) numericUpDown.Value = numericUpDown.Value-1;
            }
           
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (database == null)
            {
                MessageBox.Show("Создайте новую или откройте базу данных", "Ошибка");
                return;
            }
            database[(int)numericUpDown.Value - 1].text = textBox.Text;
            database[(int)numericUpDown.Value - 1].trueFalse = TrueCheckBox.Checked;
        }
    }
}
