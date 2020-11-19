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
    public partial class SubscriptionsForm : Form
    {
        private static UnitOfWork unitOfWork;
        public SubscriptionsForm()
        {
            unitOfWork = new UnitOfWork();
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SubscriptionsForm_Load(object sender, EventArgs e)
        {
            DisplayAllAfterUpdate();
        }

        private void DisplayAllAfterUpdate()
        {
            dataGridView1.Rows.Clear();
            comboBox1.Items.Clear();

            var subscriptions = unitOfWork.Subscriptions.GetAll();

            foreach (var subscription in subscriptions)
            {
                dataGridView1.Rows.Add(subscription.Id, subscription.FullName,
                    subscription.GroupId, subscription.Attends);
            }

            var groups = unitOfWork.Groups.GetAll();
            foreach (var group in groups)
            {
                comboBox1.Items.Add(group.Id);
            }

            dataGridView1.Update();


            textBox1.Text = 0.ToString();
            textBox2.Text = string.Empty;
            textBox4.Text = string.Empty;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;

            if (dataGridView1.Rows[rowIndex].Cells[0].Value != null)
            {
                textBox1.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
            }
            else
            {
                textBox1.Text = 0.ToString();
                textBox2.Text = string.Empty;
                comboBox1.Text = string.Empty;
                textBox4.Text = string.Empty;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox1.Text);

                if (id == 0)
                {
                    if (textBox1.Text == string.Empty ||
                        textBox2.Text == string.Empty)
                    {
                        throw new FormatException("");
                    }

                    Subscription subscription = new Subscription
                    {
                        FullName = textBox2.Text,
                        GroupId = int.Parse(comboBox1.Text),
                        Attends = int.Parse(textBox4.Text)
                    };

                    if (unitOfWork.Subscriptions.Create(subscription))
                    {
                        DisplayAllAfterUpdate();
                    }
                }
                else
                {
                    Subscription subscription = new Subscription
                    {
                        Id = id,
                        FullName = textBox2.Text,
                        GroupId = int.Parse(comboBox1.Text),
                        Attends = int.Parse(textBox4.Text)
                    };

                    if (unitOfWork.Subscriptions.Update(subscription))
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

                if (unitOfWork.Subscriptions.Delete(id))
                {
                    DisplayAllAfterUpdate();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Check if all data is correct!", "Error");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            int attends = int.Parse(numericUpDown1.Value.ToString());

            var subscriptions = unitOfWork.Subscriptions.GetAllWithAttendsSorting(attends);

            foreach (var subscription in subscriptions)
            {
                dataGridView1.Rows.Add(subscription.Id, subscription.FullName,
                    subscription.GroupId, subscription.Attends);
            }

            dataGridView1.Update();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DisplayAllAfterUpdate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text.ToString());
            DateTime startDate = DateTime.Parse(dateTimePicker1.Text);
            DateTime finishDate = DateTime.Parse(dateTimePicker2.Text);

            var trainings = unitOfWork.Trainings.GetTrainingsBetweenDateSet(id, startDate, finishDate);

            TrainingBetweenDatesForm trainingBetweenDatesForm = new TrainingBetweenDatesForm(trainings);
            trainingBetweenDatesForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int countOfSubscription = unitOfWork.Subscriptions.GetCountOfSubscriptions();

            MessageBox.Show($"Total current of subscriptions is {countOfSubscription}");
        }
    }
}
