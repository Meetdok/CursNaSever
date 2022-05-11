using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cursach.DTO;
using Cursach.Tools;

namespace Cursach.Данные
{
    [Table("department")]
    public class Department : BaseDTO
    {
        [Column("title")]
        public string Title { get; set; }
        
    }
}
