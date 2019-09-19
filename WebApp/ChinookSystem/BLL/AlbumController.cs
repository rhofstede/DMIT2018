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
        #region Queries
        [DataObjectMethod(DataObjectMethodType.Select,false)]
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
        #endregion

        #region CRUD
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Album_Add(Album album)
        {
            using(var context = new ChinookContext())
            {
                context.Albums.Add(album);  //staged
                context.SaveChanges();      //committed
                return album.AlbumID;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Album_Update(Album album)
        {
            using (var context = new ChinookContext())
            {
                context.Entry(album).State = System.Data.Entity.EntityState.Modified;   //staged
                return context.SaveChanges();                                           //committed
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int Album_Delete(Album album)
        {
            return Album_Delete(album.AlbumID);
        }
        public int Album_Delete(int albumID)
        {
            using (var context = new ChinookContext())
            {
                var existing = context.Albums.Find(albumID);
                if (existing == null)
                {
                    throw new Exception("Album not on file. Delete failed.");
                }
                else
                {
                    context.Albums.Remove(existing);                //staged
                    return context.SaveChanges();                   //committed
                }                
            }
        }
        #endregion
    }
}
