using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region
using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
using System.ComponentModel;
using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {
        #region Class Variables
        private List<string> reasons = new List<string>();
        #endregion
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
                var results = from x in context.Albums where x.ArtistId == artistID select x;                
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
                if (CheckReleaseYear(album))
                {
                    context.Albums.Add(album);  //staged
                    context.SaveChanges();      //committed
                    return album.AlbumId;
                }
                else
                {
                    throw new BusinessRuleException("Validation error: ", reasons);
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Album_Update(Album album)
        {
            using (var context = new ChinookContext())
            {
                if (CheckReleaseYear(album))
                {
                    context.Entry(album).State = System.Data.Entity.EntityState.Modified;   //staged
                    return context.SaveChanges();                                           //committed
                }
                else
                {
                    throw new BusinessRuleException("Validation error: ", reasons);
                }
                                                        
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int Album_Delete(Album album)
        {
            return Album_Delete(album.AlbumId);
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
        #region Support Methods
        private bool CheckReleaseYear(Album album)
        {
            bool valid = true;
            int releaseYear;
            if (string.IsNullOrEmpty(album.ReleaseYear.ToString()))
            {
                valid = false;
                reasons.Add("Release year is required.");
            }
            else
            {
                if (!int.TryParse(album.ReleaseYear.ToString(), out releaseYear))
                {
                    valid = false;
                    reasons.Add("Release year must be a number.");
                }
                else
                {
                    if (album.ReleaseYear < 1950 || album.ReleaseYear > DateTime.Today.Year)
                    {
                        valid = false;
                        reasons.Add(string.Format($"Album release year of {album.ReleaseYear} invalid. Must be between 1950 and this year."));
                    }
                }                
            }          
            return valid;
        }
        #endregion

        #region

        #endregion
    }
}
