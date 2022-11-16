using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Core.Model
{
    public  class JobModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public string Resume { get; set; }
        public string FullName { get; set; }
        public string Xth_Mark { get; set; }
        public string XIIth_Mark { get; set; }
        public string CGPA { get; set; }
        public string Interest { get; set; }
        public string Skills { get; set; }

    }
}
