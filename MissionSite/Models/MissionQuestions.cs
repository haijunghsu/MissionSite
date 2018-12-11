using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MissionSite.Models
{
    [Table("MissionQuestions")]
    public class MissionQuestions
    {
        [Key]
        public int MissionQuestionID { get; set; }
        public int MissionID { get; set; }
        public int UserID { get; set; }
        public string MissionQuestion { get; set; }
        public string MissionAnswer { get; set; }

        public virtual Missions Missions { get; set; }
        public virtual Users Users { get; set; }
    }
}