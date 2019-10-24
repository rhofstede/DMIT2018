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
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {
                //test for valid incoming parameter. needs to be either null or ienumerable collection. .firstordefault returns null if no playlist
                var results = (from x in context.Playlists
                               where x.UserName.Equals(username) &&
                               x.Name.Equals(playlistname)
                               select x).FirstOrDefault();
                if (results == null)
                {
                    return null;
                }
                else
                {
                    var tracks = from x in context.PlaylistTracks
                                 where x.PlaylistId.Equals(results.PlaylistId)
                                 orderby x.TrackNumber
                                 select new UserPlaylistTrack
                                 {
                                     TrackID = x.TrackId,
                                     TrackNumber = x.TrackNumber,
                                     TrackName = x.Track.Name,
                                     Milliseconds = x.Track.Milliseconds,
                                     UnitPrice = x.Track.UnitPrice
                                 };
                    return tracks.ToList();
                }                
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookContext())
            {
                //code to go here
                
             
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
               //code to go here


            }
        }//eom
    }
}
