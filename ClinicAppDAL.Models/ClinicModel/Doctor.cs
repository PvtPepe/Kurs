namespace ClinicAppDAL.Models.ClinicModel
{
    using ClinicAppDAL.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Doctor")]
    public partial class Doctor : EntityBase
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

        [Required]
        [StringLength(50)]
        public string Speciality { get; set; }

        [Required]
        [StringLength(50)]
        public string LengthOfService { get; set; }

        [Required]
        [StringLength(50)]
        public string DocNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<DoctorVisit> DoctorVisits { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {MidName} {LastName}";
    }
}
