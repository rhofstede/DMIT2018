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
    public class ArtistController
    {
        #region Queries
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Artist> Artist_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Artists.ToList();
            }
        }

        public Artist GetArtist(int artistID)
        {
            using (var context = new ChinookContext())
            {
                return context.Artists.Find(artistID);
            }
        }
        #endregion

        #region CRUD
        public int Artist_Add(Artist artist)
        {
            using (var context = new ChinookContext())
            {
                context.Artists.Add(artist);  //staged
                context.SaveChanges();      //committed
                return artist.ArtistID;
            }
        }

        public int Artist_Update(Artist artist)
        {
            using (var context = new ChinookContext())
            {
                context.Entry(artist).State = System.Data.Entity.EntityState.Modified;   //staged
                return context.SaveChanges();                                           //committed
            }
        }

        public int Artist_Delete(int artistID)
        {
            using (var context = new ChinookContext())
            {
                var existing = context.Artists.Find(artistID);
                if (existing == null)
                {
                    throw new Exception("Album not on file. Delete failed.");
                }
                else
                {
                    context.Artists.Remove(existing);                //staged
                    return context.SaveChanges();                   //committed
                }
            }
        }
        #endregion        
    }
}
