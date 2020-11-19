using SportClub.Models;
using SportClub.Unit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportClub
{
    public partial class GroupsForm : Form
    {
        private static UnitOfWork unitOfWork;
        public GroupsForm()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
        }

        private void GroupsForm_Load(object sender, EventArgs e)
        {
            DisplayAllAfterUpdate();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            if (dataGridView1.Rows[rowIndex].Cells[0].Value != null)
            {
                textBox1.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            }
            else
            {
                textBox1.Text = 0.ToString();
                textBox2.Text = string.Empty;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox1.Text);

                if (id == 0)
                {
                    if (textBox2.Text == string.Empty)
                    {
                        throw new FormatException("");
                    }

                    Group group = new Group
                    {
                        Name = textBox2.Text
                    };

                    if (unitOfWork.Groups.Create(group))
                    {
                        DisplayAllAfterUpdate();
                    }
                }
                else
                {
                    Group group = new Group
                    {
                        Id = id,
                        Name = textBox2.Text
                    };

                    if (unitOfWork.Groups.Update(group))
                    {
                        DisplayAllAfterUpdate();
                    }
                    
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Check the correctness of entered data", "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox1.Text);

                if (id == 0)
                {
                    throw new FormatException("");
                }

                if (unitOfWork.Groups.Delete(id))
                {
                    DisplayAllAfterUpdate();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Check if all data is correct!", "Error");
            }
        }

        private void DisplayAllAfterUpdate()
        {
            dataGridView1.Rows.Clear();
            comboBox1.Items.Clear();

            var groups = unitOfWork.Groups.GetAll();

            foreach (var group in groups)
            {
                dataGridView1.Rows.Add(group.Id, group.Name);
                comboBox1.Items.Add(group.Name);
            }

            dataGridView1.Update();
            textBox1.Text = 0.ToString();
            textBox2.Text = string.Empty;
            comboBox1.Text = groups[0].Name;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var groupName = comboBox1.Text;
                var groupList = unitOfWork.Subscriptions.GetListOfGroup(groupName);

                ItemsForGroupForm itemsForGroupForm = new ItemsForGroupForm(groupList);
                itemsForGroupForm.Show();
            }
            catch (FormatException)
            {
                MessageBox.Show("Check if all data is correct!", "Error");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var result = unitOfWork.Groups.TrainingsCount();

            string message = "";
            foreach (var item in result)
            {
                string temp = item.ToString().Substring(12, 29) + "\n";
                message += temp;
            }

            MessageBox.Show(message, "Result");
        }
    }
}
