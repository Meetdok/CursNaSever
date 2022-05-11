using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cursach.DTO;
using Cursach.Tools;

namespace Cursach.Данные
{
    [Table("employer")]
    public class Employ : BaseDTO
    {
        [Column("firstname")]
        public string FirstName { get; set; }

        [Column("lastname")]
        public string LastName { get; set; }

        [Column("ochestvo")]
        public string Ochestvo { get; set; }

        [Column("adress")]
        public string Adress { get; set; }

        [Column("post_id")]
        public int PostId { get; set; }

        [Column("department_id")]
        public int DepartmentId { get; set; }

        
    }
}
