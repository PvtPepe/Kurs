namespace ClinicAppDAL.Models.ClinicModel
{
    using ClinicAppDAL.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DoctorVisit")]
    public partial class DoctorVisit : EntityBase
    {

        [Column(TypeName = "date")]
        public DateTime VisitDate { get; set; }
        public int DoctorID { get; set; }

        public int PatientID { get; set; }

        public int DiagnosisID { get; set; }

        [ForeignKey(nameof(DiagnosisID))]
        public Diagnosis Diagnosis { get; set; }

        [ForeignKey(nameof(DoctorID))]
        public Doctor Doctor { get; set; }

        [ForeignKey(nameof(PatientID))]
        public Patient Patient { get; set; }

        public override string ToString()
        {
            return VisitDate.Date.ToString() + " " + DoctorID.ToString() + " " + PatientID.ToString() ;
        }
    }
}
