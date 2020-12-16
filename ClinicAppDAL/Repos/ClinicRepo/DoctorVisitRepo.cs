using ClinicAppDAL.EF;
using ClinicAppDAL.Models.ClinicModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppDAL.Repos.ClinicRepo
{
    public class DoctorVisitRepo : BaseRepo<DoctorVisit>
    {
        public DoctorVisitRepo(ClinicAppClinicContext context) : base(context)
        {
        }

        public bool CheckDate(DateTime date, int docId)
            => GetSome(x => (x.VisitDate == date && x.DoctorID == docId)).Count != 0; 

        public int GetDoctorVisitCount()
            => GetAll().Count;

        public int GetDoctorVisitCountByDiagnosis(int id)
            => GetSome(x => x.DiagnosisID == id).Count;

        public List<int> GetPatientsIdByDate(DateTime leftBorderDate, DateTime rightBorderDate, int docId) 
            => GetSome(x =>
            (DateTime.Compare(leftBorderDate, x.VisitDate) <= 0 && 
            DateTime.Compare(rightBorderDate, x.VisitDate) >= 0 && 
            x.DoctorID == docId)).ConvertAll(Converter);

        private int Converter(DoctorVisit doctorVisit) 
            => doctorVisit.PatientID;
    }
}
