namespace Poliklinika_kurs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("checkTime")]
    public partial class checkTime
    {
        public int id { get; set; }

        [StringLength(50)]
        public string date { get; set; }

        [StringLength(50)]
        public string time { get; set; }

        public int? inuse { get; set; }
    }
}
