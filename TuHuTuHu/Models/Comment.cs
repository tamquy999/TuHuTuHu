namespace TuHuTuHu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        public int CmtID { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string Content { get; set; }

        public int UserID { get; set; }

        public int PostID { get; set; }

        public virtual Account Account { get; set; }

        public virtual Post Post { get; set; }
    }
}
