namespace ClinicAppDAL.Models.AuthModel
{
    using ClinicAppDAL.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public int Role { get; set; }

        public bool Access { get; set; }

        public override string ToString()
        {
            return Login;
        }
    }
}
