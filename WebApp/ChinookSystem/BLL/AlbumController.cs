using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region
using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {
        public List<Album> Album_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.ToList();
            }
        }

        public Album GetAlbum(int albumID)
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.Find(albumID);
            }
        }
        
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Album_GetByArtist(int artistID)
        {
            using(var context = new ChinookContext())
            {
                var results = from x in context.Albums where x.ArtistID == artistID select x;                
                return results.ToList();
            }
        }
    }
}
