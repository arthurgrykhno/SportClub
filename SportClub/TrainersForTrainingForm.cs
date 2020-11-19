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
    public partial class TrainersForTrainingForm : Form
    {
        private List<Trainer> trainers;
        private UnitOfWork unitOfWork;
        public TrainersForTrainingForm(List<Trainer> trainers)
        {
            unitOfWork = new UnitOfWork();
            this.trainers = trainers;
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TrainersForTrainingForm_Load(object sender, EventArgs e)
        {
            foreach (var trainer in trainers)
            {
                dataGridView1.Rows.Add(trainer.Id, trainer.FullName, trainer.Qualification, trainer.Age);
            }
        }
    }
}
