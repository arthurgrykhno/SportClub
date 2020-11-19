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
    public partial class SchedulesForm : Form
    {
        private static UnitOfWork unitOfWork;
        public SchedulesForm()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
        }

        private void SchedulesForm_Load(object sender, EventArgs e)
        {
            DisplayAllAfterUpdate();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;

            if (dataGridView1.Rows[rowIndex].Cells[0].Value != null)
            {
                textBox1.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();
                comboBox3.Text = dataGridView1.Rows[rowIndex].Cells[5].Value.ToString();
            }
            else
            {
                textBox1.Text = 0.ToString();
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                comboBox1.Text = string.Empty;
                comboBox2.Text = string.Empty;
                comboBox3.Text = string.Empty;
            }
        }

        private void domainUpDown3_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void DisplayAllAfterUpdate()
        {
            dataGridView1.Rows.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            var schedules = unitOfWork.Schedules.GetAll();

            foreach (var schedule in schedules)
            {
                dataGridView1.Rows.Add(schedule.Id, schedule.Date.ToShortDateString(), schedule.Time,
                    schedule.TrainerId, schedule.TrainingId, schedule.GroupId);
            }

            var trainers = unitOfWork.Trainers.GetAll();
            foreach (var trainer in trainers)
            {
                comboBox1.Items.Add(trainer.Id);
            }

            var trainings = unitOfWork.Trainings.GetAll();
            foreach (var training in trainings)
            {
                comboBox2.Items.Add(training.Id);
            }

            var groups = unitOfWork.Groups.GetAll();
            foreach (var group in groups)
            {
                comboBox3.Items.Add(group.Id);
            }

            dataGridView1.Update();

            textBox1.Text = 0.ToString();
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            comboBox1.Text = trainers[0].Id.ToString();
            comboBox2.Text = trainings[0].Id.ToString();
            comboBox3.Text = groups[0].Id.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox1.Text);

                if (id == 0)
                {
                    if (textBox1.Text == string.Empty ||
                        textBox2.Text == string.Empty ||
                        textBox3.Text == string.Empty)
                    {
                        throw new FormatException("");
                    }

                    Schedule schedule = new Schedule
                    {
                        Date = DateTime.Parse(textBox2.Text),
                        Time = TimeSpan.Parse(textBox3.Text),
                        TrainerId = int.Parse(comboBox1.Text),
                        TrainingId = int.Parse(comboBox2.Text),
                        GroupId = int.Parse(comboBox3.Text)
                    };

                    if (unitOfWork.Schedules.Create(schedule))
                    {
                        DisplayAllAfterUpdate();
                    }
                }
                else
                {
                    Schedule schedule = new Schedule
                    {
                        Id = id,
                        Date = DateTime.Parse(textBox2.Text),
                        Time = TimeSpan.Parse(textBox3.Text),
                        TrainerId = int.Parse(comboBox1.Text),
                        TrainingId = int.Parse(comboBox2.Text),
                        GroupId = int.Parse(comboBox3.Text)
                    };

                    if (unitOfWork.Schedules.Update(schedule))
                    {
                        DisplayAllAfterUpdate();

                        textBox1.Text = 0.ToString();
                        textBox2.Text = string.Empty;
                        textBox3.Text = string.Empty;
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

                if (unitOfWork.Schedules.Delete(id))
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
            int groupId = int.Parse(comboBox3.Text);

            int count = unitOfWork.Schedules.GetPopulationAtCurrentTraining(groupId);

            MessageBox.Show($"At the current training were present {count} people", "Result");
        }
    }
}
