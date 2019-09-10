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
    [Table("Albums")]
    public class Album
    {
        [Key]
        public int AlbumID { get; set; }
        public string Title { get; set; }
        public int ArtistID { get; set; }
        public int ReleaseYear { get; set; }
        private string _releaseLabel;
        public string ReleaseLabel
        {
            get
            {
                return _releaseLabel;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _releaseLabel = null;
                    _releaseLabel = null;
                }
                else
                {
                    _releaseLabel = value;
                }
            }
        }
        public virtual Artist Artist { get; set; }
    }
}
