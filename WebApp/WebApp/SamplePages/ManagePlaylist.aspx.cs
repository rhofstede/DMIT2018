using System;
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
            //code to go here
 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
 
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            //code to go here
            
        }

    }
}