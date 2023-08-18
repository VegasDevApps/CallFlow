namespace CallFlow
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ClientDataInput
    {
        [Required]
        [StringLength(9)]
        public string CitizenID { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> BitrthDate { get; set; }
        [StringLength(10)]
        public string Notes { get; set; }
    }
}