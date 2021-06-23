namespace Poliklinika_kurs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HistoryDes
    {
        public int id { get; set; }

        [StringLength(50)]
        public string date { get; set; }

        [StringLength(50)]
        public string doctor { get; set; }

        [StringLength(50)]
        public string pacient { get; set; }

        public int? doctor_id { get; set; }

        public int? pacient_id { get; set; }

        [StringLength(50)]
        public string verdict { get; set; }
    }
}