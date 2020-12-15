namespace ClinicAppDAL.Models.ClinicModel
{
    using ClinicAppDAL.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Diagnosis : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string DiagnosisName { get; set; }

        [Required]
        [StringLength(50)]
        public string DiagnosisTreatment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<DoctorVisit> DoctorVisits { get; set; }

        public override string ToString()
        {
            return DiagnosisName;
        }
    }
}
