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
    public partial class TrainingBetweenDatesForm : Form
    {
        private UnitOfWork unitOfWork;
        private List<Training> trainings;
        public TrainingBetweenDatesForm(List<Training> trainings)
        {
            unitOfWork = new UnitOfWork();
            this.trainings = trainings;
            InitializeComponent();
        }

        private void TrainingBetweenDatesForm_Load(object sender, EventArgs e)
        {
            foreach (var training in trainings)
            {
                dataGridView1.Rows.Add(training.Id, training.Name);
            }
        }
    }
}
