﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.POCOs;
using DMIT2018Common.UserControls;
//using WebApp.Security;
#endregion

namespace Jan2018DemoWebsite.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                MessageUserControl.ShowInfo("Missing data","Enter all or part of an artist name. ");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    SearchArg.Text = ArtistName.Text;
                    TracksBy.Text = "Artist";
                    TracksSelectionList.DataBind(); //executes ods
                },"Track Search","Select songs to add to playlist.");
            }       
        }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
                {
                    SearchArg.Text = MediaTypeDDL.SelectedValue;
                    TracksBy.Text = "MediaType";
                    TracksSelectionList.DataBind(); //executes ods
                }, "Track Search", "Select songs to add to playlist.");
        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                SearchArg.Text = GenreDDL.SelectedValue;
                TracksBy.Text = "Genre";
                TracksSelectionList.DataBind(); //executes ods
            }, "Track Search", "Select songs to add to playlist.");
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                MessageUserControl.ShowInfo("Missing data", "Enter all or part of an album title. ");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    SearchArg.Text = AlbumTitle.Text;
                    TracksBy.Text = "Album";
                    TracksSelectionList.DataBind(); //executes ods
                }, "Track Search", "Select songs to add to playlist.");
            }
        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing data", "Enter a playlist name. ");
            }
            else
            {
                string playlistname = PlaylistName.Text;
                string username = "HansenB"; //will change when we add security
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController controller = new PlaylistTracksController();
                    List<UserPlaylistTrack> info = controller.List_TracksForPlaylist(playlistname, username);
                    PlayList.DataSource = info;
                    PlayList.DataBind();
                }, "Playlist Search", "Current Playlist Tracks");
            }
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            List<string> reasons = new List<string>();
            int trackID = 0;
            int trackNumber = 0;
            int rowSelected = 0;
            CheckBox playlistSelection = null;

            //check for playlist
            if (PlayList.Rows.Count == 0)
            {
                reasons.Add("No playlist selected. Fetch a playlist first.");
            }

            //check for playlist name
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                reasons.Add("No playlist name found. Enter a playlist name first.");
            }

            //check playlist for number of selected row(s)
            for (int rowIndex = 0; rowIndex < PlayList.Rows.Count; rowIndex-= -1)
            {
                //set playlistSelection to the checkbox control on the gridview row
                playlistSelection = PlayList.Rows[rowIndex].FindControl("Selected") as CheckBox;
                if (playlistSelection.Checked)
                {
                    rowSelected -= -1;
                    trackID = int.Parse((PlayList.Rows[rowIndex].FindControl("TrackID") as Label).Text);
                    trackNumber = int.Parse((PlayList.Rows[rowIndex].FindControl("TrackNumber") as Label).Text);
                }
            }

            if (rowSelected != 1)
            {
                reasons.Add("Only one track can be moved at a time.");
            }

            //check if selected is last track
            if (trackNumber == PlayList.Rows.Count)
            {
                reasons.Add("Track is already last; cannot move further down.");
            }

            //move track or show errors
            if (reasons.Count == 0)
            {
                MoveTrack(trackID, trackNumber, "down");
            }
            else
            {
                MessageUserControl.TryRun(() => {
                    throw new BusinessRuleException("Unable to move track: ", reasons);
                });
            }
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            List<string> reasons = new List<string>();
            int trackID = 0;
            int trackNumber = 0;
            int rowSelected = 0;
            CheckBox playlistSelection = null;

            //check for playlist
            if (PlayList.Rows.Count == 0)
            {
                reasons.Add("No playlist selected. Fetch a playlist first.");
            }

            //check for playlist name
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                reasons.Add("No playlist name found. Enter a playlist name first.");
            }

            //check playlist for number of selected row(s)
            for (int rowIndex = 0; rowIndex < PlayList.Rows.Count; rowIndex -= -1)
            {
                //set playlistSelection to the checkbox control on the gridview row
                playlistSelection = PlayList.Rows[rowIndex].FindControl("Selected") as CheckBox;
                if (playlistSelection.Checked)
                {
                    rowSelected -= -1;
                    trackID = int.Parse((PlayList.Rows[rowIndex].FindControl("TrackID") as Label).Text);
                    trackNumber = int.Parse((PlayList.Rows[rowIndex].FindControl("TrackNumber") as Label).Text);
                }
            }

            if (rowSelected != 1)
            {
                reasons.Add("Only one track can be moved at a time.");
            }

            //check if selected is first track
            if (trackNumber == 1)
            {
                reasons.Add("Track is first; cannot move higher.");
            }

            //move track or show errors
            if (reasons.Count == 0)
            {
                MoveTrack(trackID, trackNumber, "up");
            }
            else
            {
                MessageUserControl.TryRun(() => {
                    throw new BusinessRuleException("Unable to move track: ", reasons);
                });
            }

        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            MessageUserControl.TryRun(()=>
            {
                PlaylistTracksController controller = new PlaylistTracksController();
                controller.MoveTrack("HansenB", PlaylistName.Text, trackid, tracknumber, direction);
                List<UserPlaylistTrack> info = controller.List_TracksForPlaylist(PlaylistName.Text, "HansenB");
                PlayList.DataSource = info;
                PlayList.DataBind();
            }, "Move successful","Track has been moved."
            );
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            //validate incoming parameters
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required Data ", "Adding a track requires a playlist name.");
            }
            else
            {
                string playlistName = PlaylistName.Text;
                //username eventually will come from security. Currently hardcoded 
                string username = "HansenB";
                //track id comes from the list view command arguments
                int trackID = int.Parse(e.CommandArgument.ToString()); //the command argument must be cast to a string, then parsed to an int.
                MessageUserControl.TryRun(() => 
                {
                    PlaylistTracksController controller = new PlaylistTracksController();
                    controller.Add_TrackToPLaylist(playlistName, username, trackID); //add data here - there can only be one call to the database
                    List<UserPlaylistTrack> info = controller.List_TracksForPlaylist(playlistName, username); //read data back to user
                    PlayList.DataSource = info;
                    PlayList.DataBind();
                },"Added track", "Track has been added to playlist.");
            }            
        }

    }
}