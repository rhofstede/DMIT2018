namespace ChinookSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            Tracks = new HashSet<Track>();
        }

        public int AlbumId { get; set; }

        [Required(ErrorMessage ="Album title required.")]
        [StringLength(160, ErrorMessage ="Album title limited to 160 characters.")]
        public string Title { get; set; }

        public int ArtistId { get; set; }
        //range validation annotation can check numeric values for ranges of values. Min and max values must be constants. can't use it for a changing year, in this case
        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage ="Release label limited to 50 characters.")]
        public string ReleaseLabel { get; set; }

        public virtual Artist Artist { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
