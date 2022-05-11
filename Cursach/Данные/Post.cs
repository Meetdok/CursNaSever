using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cursach.DTO;
using Cursach.Tools;

namespace Cursach.Данные
{
    [Table("post")]
    public class Post : BaseDTO
    {

        [Column("name")]
        public string Name { get; set; }

        [Column("department_id")]
        public int DepartmentId { get; set; }
    }
}
