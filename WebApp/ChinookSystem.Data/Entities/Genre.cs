using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace ChinookSystem.Data.Entities
{
    [Table("Genres")]
    class Genre
    {       
        [Key]
        public int GenreID { get; set; }
        public string Name { get; set; }
        
        public virtual Genre Genre { get; set; }
    }
   
}
