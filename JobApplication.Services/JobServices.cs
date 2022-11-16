using JobApplication.Core.IRepository;
using JobApplication.Core.IService;
using JobApplication.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Services
{
    public  class JobServices:IJobService
    {
        private readonly IJobRepository jobRepository;
        public JobServices(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }
        public void CreateForm(JobModel jobModel)
        {
            jobRepository.CreateForm(jobModel);
        }
        public List<JobModel> ListDetail()
        {
            return jobRepository.ListDetail();
        }
        public JobModel Save(int ID)
        {
            return jobRepository.Save(ID);
        }
    }
}
