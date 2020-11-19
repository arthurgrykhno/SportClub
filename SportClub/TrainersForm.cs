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
    public partial class TrainersForm : Form
    {
        private static UnitOfWork unitOfWork;
        private static bool isAscending = false;
        public TrainersForm()
        {
            unitOfWork = new UnitOfWork();
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TrainersForm_Load(object sender, EventArgs e)
        {
            DisplayAllAfterUpdate();
        }

        private void DisplayAllAfterUpdate()
        {
            dataGridView1.Rows.Clear();

            var trainers = unitOfWork.Trainers.GetAll();

            foreach (var trainer in trainers)
            {
                dataGridView1.Rows.Add(trainer.Id, trainer.FullName,
                    trainer.Qualification, trainer.Age);
            }

            dataGridView1.Update();


            textBox1.Text = 0.ToString();
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            numericUpDown1.Value = 18;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;

            if (dataGridView1.Rows[rowIndex].Cells[0].Value != null)
            {
                textBox1.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
                numericUpDown1.Value = int.Parse(dataGridView1.Rows[rowIndex].Cells[3].Value.ToString());
            }
            else
            {
                textBox1.Text = 0.ToString();
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                numericUpDown1.Value = 18;
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
                        textBox2.Text == string.Empty ||
                        textBox3.Text == string.Empty)
                    {
                        throw new FormatException("");
                    }

                    Trainer trainer = new Trainer
                    {
                        FullName = textBox2.Text,
                        Qualification = textBox3.Text,
                        Age = int.Parse(numericUpDown1.Value.ToString())
                    };

                    if (unitOfWork.Trainers.Create(trainer))
                    {
                        DisplayAllAfterUpdate();
                    }
                }
                else
                {
                    Trainer trainer = new Trainer
                    {
                        Id = id,
                        FullName = textBox2.Text,
                        Qualification = textBox3.Text,
                        Age = int.Parse(numericUpDown1.Value.ToString())
                    };

                    if (unitOfWork.Trainers.Update(trainer))
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

                if (unitOfWork.Trainers.Delete(id))
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

            var trainers = unitOfWork.Trainers.GetAllOrderedByQualification(isAscending);

            foreach (var trainer in trainers)
            {
                dataGridView1.Rows.Add(trainer.Id, trainer.FullName,
                    trainer.Qualification, trainer.Age);
            }

            dataGridView1.Update();


            textBox1.Text = 0.ToString();
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            numericUpDown1.Value = 18;

            isAscending = !isAscending;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DisplayAllAfterUpdate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int trainerId = int.Parse(textBox1.Text);

            SchedulesForTrainerForm schedulesForTrainerForm = new SchedulesForTrainerForm(trainerId);
            schedulesForTrainerForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int count = unitOfWork.Trainers.GetCountOfTrainers();
            MessageBox.Show($"Total count of trainers: {count}", "Result");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);

            int count = unitOfWork.Trainers.GetCountOfGroupsOfCurrentTrainer(id);

            MessageBox.Show($"Current trainer has {count} groups");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var result = unitOfWork.Trainers.GetQualificationCount();

            MessageBox.Show(result[0].ToString().Substring(12, 34));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int count = int.Parse(numericUpDown2.Value.ToString());

            var trainers = unitOfWork.Trainers.GetAllTrainingsMoreThan(count);

            TrainingsMoreThanCountForm trainingsMoreThanCountForm = new TrainingsMoreThanCountForm(trainers);
            trainingsMoreThanCountForm.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var trainers = unitOfWork.Trainers.GetEldest();

            string result = "";
            foreach (var trainer in trainers)
            {
                string temp = trainer.FullName + " " + trainer.Age + "\n";
                result += temp;
            }

            MessageBox.Show(result, "Eldest");
        }
    }
}
