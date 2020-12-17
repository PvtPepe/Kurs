namespace ClinicAppDAL.Models.ClinicModel
{
    using ClinicAppDAL.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Patient")]
    public partial class Patient : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string MidName { get; set; }

        [Column(TypeName = "date")]
        public DateTime PatientBirthdate { get; set; }

        [Required]
        [StringLength(50)]
        public string PatientAdress { get; set; }

        [Required]
        [StringLength(50)]
        public string PatientNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<DoctorVisit> DoctorVisits { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {MidName} {LastName}";

        public override string ToString()
        {
            return FullName;
        }
    }
}
