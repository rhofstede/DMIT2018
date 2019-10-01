using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region
using ChinookSystem.Data.POCOs;
#endregion

namespace ChinookSystem.Data.DTOs
{
    public class ArtistAlbum
    {
        public string AlbumTitle { get; set; }
        public string ArtistName { get; set; }
        public int TrackCount { get; set; }
        public int AlbumPlayTime { get; set; }
        public List<AlbumTrack> AlbumTracks { get; set; }
    }
}
