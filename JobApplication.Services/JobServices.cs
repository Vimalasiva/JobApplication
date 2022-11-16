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
        #region Declaration
        private readonly IJobRepository jobRepository;
        #endregion

        #region Constructor
        public JobServices(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }
        #endregion

        #region Creating new applicant detail
        public void CreateForm(JobModel jobModel)
        {
            jobRepository.CreateForm(jobModel);
        }
        #endregion

        #region Listing the applicant details
        public List<JobModel> ListDetail()
        {
            return jobRepository.ListDetail();
        }
        #endregion

        #region Viewing details in partial view from database
        public JobModel Save(int ID)
        {
            return jobRepository.Save(ID);
        }
        #endregion
    }
}
