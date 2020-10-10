namespace TuHuTuHu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Follow")]
    public partial class Follow
    {
        public int FollowID { get; set; }

        public int UserID { get; set; }

        public int FollowerID { get; set; }

        public virtual Account Account { get; set; }

        public virtual Account Account1 { get; set; }
    }
}
