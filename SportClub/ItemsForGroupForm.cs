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
    public partial class ItemsForGroupForm : Form
    {
        private List<Subscription> subscriptions;
        public ItemsForGroupForm(List<Subscription> subscriptions)
        {
            InitializeComponent();
            this.subscriptions = subscriptions;
        }

        private void ItemsForGroupForm_Load(object sender, EventArgs e)
        {
            foreach (var subscription in subscriptions)
            {
                dataGridView1.Rows.Add(subscription.Id, subscription.FullName, subscription.Attends);
            }

            dataGridView1.Update();
        }
    }
}
