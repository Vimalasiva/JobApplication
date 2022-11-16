using JobApplication.Core.IRepository;
using JobApplication.Core.Model;
using JobApplication.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JobApplication.Repository
{
    public class JobRepository : IJobRepository
    {
        #region Creating new applicant detail
        public void CreateForm(JobModel jobModel)
        {
            if (jobModel != null)
            {
                using (Job_ApplicationContext entity = new Job_ApplicationContext())
                {
                    if (jobModel.ID == 0)
                    {
                        JobApplicationTable jobApplicationTable = new JobApplicationTable();
                        jobApplicationTable.Applicant_Name = jobModel.Name;
                        jobApplicationTable.Applicant_Gender = jobModel.Gender;
                        jobApplicationTable.Applicant_Age = jobModel.Age;
                        jobApplicationTable.Applicant_Email = jobModel.Email;
                        jobApplicationTable.Applicant_Location = jobModel.Location;
                        jobApplicationTable.Applicant_Resume= jobModel.Resume;
                        jobApplicationTable.Applicant_FullName = jobModel.FullName;
                        jobApplicationTable.Applicant_Xth_Mark = int.Parse(jobModel.Xth_Mark);
                        jobApplicationTable.Applicant_XIIth_Mark = int.Parse(jobModel.XIIth_Mark);
                        jobApplicationTable.Applicant_CGPA = int.Parse(jobModel.CGPA);
                        jobApplicationTable.Applicant_Interest = jobModel.Interest;
                        jobApplicationTable.Applicant_Skills = jobModel.Skills;
                        entity.Add(jobApplicationTable);
                        entity.SaveChanges();
                    }
                }
            }
        }
        #endregion

        #region Listing the details of applicant
        public List<JobModel> ListDetail()
        {
            List<JobModel> Joblist = new List<JobModel>();
            using (Job_ApplicationContext entity = new Job_ApplicationContext())
            {
                var List = entity.JobApplicationTable.Where(list => list.Is_Deleted == false).ToList();
                if (List != null)
                {
                    foreach (var detail in List)
                    {
                        JobModel jobModel = new JobModel();
                        jobModel.Name = detail.Applicant_Name;
                        jobModel.Email = detail.Applicant_Email;    
                        jobModel.Location = detail.Applicant_Location;
                        jobModel.Resume = detail.Applicant_Resume.ToString();
                        jobModel.ID = detail.Applicant_ID;
                        Joblist.Add(jobModel);
                    }
                    
                }
                return Joblist;
            }
        }
        #endregion

        #region Viewing details in excel by partial view(details in excel)
        public JobModel Save(int id)
        {

            using (Job_ApplicationContext entity = new Job_ApplicationContext())
            {
                JobModel jobModel = new JobModel();
                var save = entity.JobApplicationTable.Where(m => m.Applicant_ID == id).SingleOrDefault();
                if (save != null)
                {
                    jobModel.FullName = save.Applicant_FullName;
                    jobModel.Xth_Mark = save.Applicant_Xth_Mark.ToString();
                    jobModel.XIIth_Mark = save.Applicant_XIIth_Mark.ToString();
                    jobModel.CGPA = save.Applicant_CGPA.ToString();
                    jobModel.Interest = save.Applicant_Interest;
                    jobModel.Skills = save.Applicant_Skills;
                }
                return jobModel;
            }

        }
        #endregion
    }
}
