using JobApplication.Core.IService;
using JobApplication.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Core.IRepository
{
    public interface IJobRepository
    {
         void CreateForm(JobModel jobModel);
           List<JobModel> ListDetail();
        public JobModel Save(int id);
    }
}
