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
    public partial class TrainingsForm : Form
    {
        private static UnitOfWork unitOfWork;
        public TrainingsForm()
        {
            unitOfWork = new UnitOfWork();
            InitializeComponent();
        }

        private void TrainingsForm_Load(object sender, EventArgs e)
        {
            DisplayAllAfterUpdate();
        }

        private void DisplayAllAfterUpdate()
        {
            dataGridView1.Rows.Clear();

            var trainings = unitOfWork.Trainings.GetAll();

            foreach (var training in trainings)
            {
                dataGridView1.Rows.Add(training.Id, training.Name);
            }

            dataGridView1.Update();

            textBox1.Text = 0.ToString();
            textBox2.Text = string.Empty;
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
                    if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
                    {
                        throw new FormatException("");
                    }

                    Training training = new Training
                    {
                        Name = textBox2.Text,
                    };

                    if (unitOfWork.Trainings.Create(training))
                    {
                        DisplayAllAfterUpdate();
                    }
                }
                else
                {
                    Training training = new Training
                    {
                        Id = id,
                        Name = textBox2.Text
                    };

                    if (unitOfWork.Trainings.Update(training))
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

                if (unitOfWork.Trainings.Delete(id))
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
            int trainingId = int.Parse(textBox1.Text);

            var trainers = unitOfWork.Trainers.GetAllForTraining(trainingId);

            TrainersForTrainingForm trainersForTrainingForm = new TrainersForTrainingForm(trainers);
            trainersForTrainingForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Parse(dateTimePicker1.Text);
            DateTime finishDate = DateTime.Parse(dateTimePicker2.Text);

            int count = unitOfWork.Trainings.CountOfTrainingsBetweenDates(startDate, finishDate);

            MessageBox.Show($"Count of trainings between these dates are {count}");
        }
    }
}
