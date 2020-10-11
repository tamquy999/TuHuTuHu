namespace TuHuTuHu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Love")]
    public partial class Love
    {
        public int LoveID { get; set; }

        public int UserID { get; set; }

        public int PostID { get; set; }

        public virtual Account Account { get; set; }

        public virtual Post Post { get; set; }
    }
}
