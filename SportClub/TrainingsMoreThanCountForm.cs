using SportClub.Models;
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
    public partial class TrainingsMoreThanCountForm : Form
    {
        List<Trainer> trainers;
        public TrainingsMoreThanCountForm(List<Trainer> trainers)
        {
            this.trainers = trainers;
            InitializeComponent();
        }

        private void TrainingsMoreThanCountForm_Load(object sender, EventArgs e)
        {
            foreach (var trainer in trainers)
            {
                dataGridView1.Rows.Add(trainer.Id, trainer.FullName, trainer.Qualification, trainer.Age);
            }
        }
    }
}
