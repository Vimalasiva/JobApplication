using JobApplication.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Core.IService
{
    public interface IJobService
    {
         void CreateForm(JobModel jobModel);
      
        List<JobModel> ListDetail();
        public JobModel Save(int id);
    }
}
