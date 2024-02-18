using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendAssessment.Models
{
    public class FreeLancerEmplist
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Mail { get; set; }
        public int PhoneNumber { get; set; }
        public string SkillSets { get; set; }
        public string Hobby { get; set; } 
    }
}