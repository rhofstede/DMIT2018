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
using DMIT2018Common.UserControls;
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
                //throw errors with BusinessRuleException
                List<string> reasons = new List<string>();
                PlaylistTrack newTrack = null;
                int trackNumber = 0;

                //find if playlist exists using .IfFirstOrDefault
                Playlist exists = context.Playlists
                                    .Where(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                                    && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase))
                                    .Select(x => x)
                                    .FirstOrDefault();
                ////or in query syntax:
                //(from x in context.Playlists
                //  where(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                //                  && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase))
                //  select x).FirstOrDefault();
                if (exists == null)
                {
                    //create new playlist
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    exists = context.Playlists.Add(exists); //staged
                    trackNumber = 1;
                }
                else
                {
                    //find new track number
                    newTrack = exists.PlaylistTracks.SingleOrDefault(x => x.TrackId == trackid); //null if doesn't exist
                    if (newTrack == null)
                    {
                        trackNumber = exists.PlaylistTracks.Count() + 1;
                    }
                    else
                    {
                        reasons.Add("Track already on playlist.");
                    }
                }

                //create playlist tracks after checking for errors in creation
                if (reasons.Count() > 0)
                {
                    throw new BusinessRuleException("Adding track to playlist", reasons);
                }
                else
                {
                    //navigate through Playlist to PlaylistTracks
                    newTrack = new PlaylistTrack();
                    newTrack.TrackId = trackid;
                    newTrack.TrackNumber = trackNumber;
                    //if PK for Playlist doesn't exist, going through navigational properties on Playlist and using the Hashset will find the correct ID and add to any child records. exists.PlaylistID has a bad PK; don't use it.
                    exists.PlaylistTracks.Add(newTrack); //stages playlist track

                    context.SaveChanges(); //commits the playlist and all tracks
                }
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //get playlist id
                var exists = (from x in context.Playlists
                              where x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase)
                              select x).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("Playlist does not exist.");
                }
                else
                {
                    //get track id
                    PlaylistTrack moveTrack = (from x in exists.PlaylistTracks
                                               where x.TrackId == trackid
                                               select x).FirstOrDefault();
                    if (moveTrack == null)
                    {
                        throw new Exception("Track does not exist on playlist.");
                    }
                    else
                    {
                        PlaylistTrack otherTrack = null;
                        //check direction
                        if (direction.Equals("up"))
                        {
                            if (tracknumber == 1)
                            {
                                throw new Exception("Track 1 cannot be moved higher.");
                            }
                            else
                            {
                                otherTrack = (from x in exists.PlaylistTracks
                                                where x.TrackNumber == moveTrack.TrackNumber - 1
                                                select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("Cannot move these tracks; playlist has been corrupted. Try fetching playlist again.");
                                }
                                else
                                {
                                    moveTrack.TrackNumber -= 1;
                                    otherTrack.TrackNumber += 1;
                                }
                            }
                        }
                        else
                        {
                            if (tracknumber == exists.PlaylistTracks.Count())
                            {
                                throw new Exception("Track is already at the bottom.");
                            }
                            else
                            {
                                otherTrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == moveTrack.TrackNumber + 1
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("Cannot move these tracks; playlist has been corrupted. Try fetching playlist again.");
                                }
                                else
                                {
                                    moveTrack.TrackNumber += 1;
                                    otherTrack.TrackNumber -= 1;
                                }
                            }
                        }//end of direction code

                        context.Entry(moveTrack).Property(y => y.TrackNumber).IsModified = true;    
                        context.Entry(otherTrack).Property(y => y.TrackNumber).IsModified = true;   //staged changes
                        context.SaveChanges();                                                      //committed
                    }
                }
            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
                


            }
        }//eom
    }
}
