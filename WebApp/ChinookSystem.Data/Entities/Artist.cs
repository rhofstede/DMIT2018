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
    [Table("Artists")]

    public class Artist
    {
        //[Key, Column(Order = n)] - for compound key
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)] - for non-identity PK
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)] - for info computed from other fields
        [Key]
        public int ArtistID { get; set; }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _name = null;
                }
                else
                {
                    _name = value;
                }
            }
        }
        public virtual ICollection<Album> Albums { get; set; }
    }
}
