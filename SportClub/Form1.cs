using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SportClub.Unit;

namespace SportClub
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GroupsForm groupsForm = new GroupsForm();
            groupsForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SchedulesForm schedulesForm = new SchedulesForm();
            schedulesForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SubscriptionsForm subscriptionsForm = new SubscriptionsForm();
            subscriptionsForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TrainersForm trainersForm = new TrainersForm();
            trainersForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TrainingsForm trainingsForm = new TrainingsForm();
            trainingsForm.Show();
        }
    }
}
