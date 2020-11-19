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
    public partial class SchedulesForTrainerForm : Form
    {
        private static UnitOfWork unitOfWork;

        private int trainerId;
        private bool isAscending = false;
        public SchedulesForTrainerForm(int trainerId)
        {
            unitOfWork = new UnitOfWork();
            this.trainerId = trainerId;
            InitializeComponent();
        }

        private void SchedulesForTrainerForm_Load(object sender, EventArgs e)
        {
            DisplayAllAfterUpdate();
        }

        private void DisplayAllAfterUpdate()
        {
            dataGridView1.Rows.Clear();

            var schedules = unitOfWork.Schedules.GetScheduleForTrainer(trainerId, isAscending);
            isAscending = !isAscending;

            foreach (var schedule in schedules)
            {
                dataGridView1.Rows.Add(schedule.Id, schedule.Date, schedule.Time, schedule.TrainingId, schedule.GroupId);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayAllAfterUpdate();
        }
    }
}
