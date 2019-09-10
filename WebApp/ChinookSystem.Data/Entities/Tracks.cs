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
    [Table("Tracks")]
    class Tracks
    {      
        [Key]
        public int TrackId { get; set; }
        public string Name { get; set; }
        public int AlbumID { get; set; }
        public int MediaTypeID { get; set; }
        public int GenreID { get; set; }
        public int ReleaseYear { get; set; }
        private string _composer;
        public string Composer
        {
            get
            {
                return _composer;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _composer = null;
                    _composer = null;
                }
                else
                {
                    _composer = value;
                }
            }
        }
        public int Milliseconds { get; set; }     
        public int? Bytes { get; set; }
        
        public int UnitPrice { get; set; }
        public virtual Artist Artist { get; set; }
       
    }
}
