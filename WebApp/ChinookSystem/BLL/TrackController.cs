using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.DTOs;
using ChinookSystem.DAL;
using System.ComponentModel;
using ChinookSystem.Data.POCOs;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Track_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track Track_Find(int trackid)
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.Find(trackid);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Track_GetByAlbumId(int albumid)
        {
            using (var context = new ChinookContext())
            {
                var results = from aRowOn in context.Tracks
                              where aRowOn.AlbumId.HasValue
                              && aRowOn.AlbumId == albumid
                              select aRowOn;
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<TrackList> List_TracksForPlaylistSelection(string tracksby, string arg)
        {
            using (var context = new ChinookContext())
            {
                //verify incoming parameters; set to default if necessary
                if (string.IsNullOrEmpty(tracksby))
                {
                    tracksby = "";
                }
                if (string.IsNullOrEmpty(arg))
                {
                    arg = "";
                }

                //local variables for arg as int and string
                int argid = 0;
                string argstring = "jksdljksdjf";

                //set argument to int or string
                if (tracksby.Equals("Genre")||tracksby.Equals("MediaType"))
                {
                    argid = int.Parse(arg);
                }
                else
                {
                    argstring = arg.Trim();
                }

                var results = (from x in context.Tracks
                               where (x.GenreId == argid &&  tracksby.Equals("Genre") ||
                               (x.MediaTypeId == argid && tracksby.Equals("MediaType")))
                               select new TrackList
                               {
                                   TrackID = x.TrackId,
                                   Name = x.Name,
                                   Title = x.Album.Title,
                                   ArtistName = x.Album.Artist.Name,
                                   MediaName = x.MediaType.Name,
                                   GenreName = x.Genre.Name,
                                   Composer = x.Composer,
                                   Milliseconds = x.Milliseconds, //BLL doesn't format things. formatting happens on the webpage
                                   Bytes = x.Bytes,
                                   UnitPrice = x.UnitPrice
                               }).Union(
                                from x in context.Tracks
                                where   tracksby.Equals("Artist") ? x.Album.Artist.Name.Contains(argstring) : 
                                        tracksby.Equals("Album") ? x.Album.Title.Contains(argstring) : false
                                select new TrackList
                                {
                                    TrackID = x.TrackId,
                                    Name = x.Name,
                                    Title = x.Album.Title,
                                    ArtistName = x.Album.Artist.Name,
                                    MediaName = x.MediaType.Name,
                                    GenreName = x.Genre.Name,
                                    Composer = x.Composer,
                                    Milliseconds = x.Milliseconds,
                                    Bytes = x.Bytes,
                                    UnitPrice = x.UnitPrice
                                });

                return results.ToList();
            }
        }//eom

       
    }//eoc
}
