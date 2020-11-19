using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClub.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int GroupId { get; set; }

        public int Attends { get; set; }
    }
}
