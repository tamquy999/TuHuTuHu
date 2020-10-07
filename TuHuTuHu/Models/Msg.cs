namespace TuHuTuHu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Msg")]
    public partial class Msg
    {
        public int MsgID { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string Content { get; set; }

        public int SenderID { get; set; }

        public int ReceiverID { get; set; }

        public virtual Account Account { get; set; }

        public virtual Account Account1 { get; set; }
    }
}
