using ClinicAppDAL.EF;
using ClinicAppDAL.Models.ClinicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClinicAppDAL.Repos.ClinicRepo
{
    public class DoctorRepo : BaseRepo<Doctor>
    {
        public DoctorRepo(ClinicAppClinicContext context) : base(context)
        {
        }

        public List<Doctor> GetDoctorsBySpeciality(string spec) 
            => GetSome(x => x.Speciality == spec);

        public List<Doctor> GetAllDoctorsBySpeciality()
            => GetAll(x => x.Speciality, true);
        
        public List<Patient> GetPatientsByTime(int docId, DateTime leftBorderDate, DateTime rightBorderDate)
        {
            if (Context is ClinicAppClinicContext c) 
            {
                DoctorVisitRepo doctorVisitRepo = new DoctorVisitRepo(c);
                BaseRepo<Patient> patientRepo = new BaseRepo<Patient>(c);
                List<int> ids = doctorVisitRepo.GetPatientsIdByDate(leftBorderDate, rightBorderDate, docId);
                List<Patient> patients = new List<Patient>();
                foreach(int id in ids)
                {
                    patients.Add(patientRepo.GetOne(id));
                }
                patients = patients.Distinct().ToList();
                return patients;
            }
            return null;
        }
    }
}
