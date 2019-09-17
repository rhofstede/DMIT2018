using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region
using ChinookSystem.BLL;
using ChinookSystem.Data.Entities;
#endregion

namespace WebApp.SamplePages
{
    public partial class FilterSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindArtistList();
                //max val for editreleaseyear validation control
                RangeEditReleaseYear.MaximumValue = DateTime.Today.Year.ToString();
            }
        }

        protected void BindArtistList()
        {
            ArtistController controller = new ArtistController();
            List<Artist> artistList = controller.Artist_List();
            artistList.Sort((x, y) => x.Name.CompareTo(y.Name));

            ArtistList.DataSource = artistList;
            ArtistList.DataTextField = nameof(Artist.Name);
            ArtistList.DataValueField = nameof(Artist.ArtistID);
            ArtistList.DataBind();
            //ArtistList.Items.Insert(0, "Select");
        }

        //method called by ods
        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = AlbumList.Rows[AlbumList.SelectedIndex];
            string albumID = (row.FindControl("AlbumID") as Label).Text;
            MessageUserControl.TryRun(
                () =>
                {
                    AlbumController controller = new AlbumController();
                    Album album = controller.GetAlbum(int.Parse(albumID));                    
                    if (album == null)
                    {
                        //ClearControls();
                        throw new Exception("Record no longer exists on file.");
                    }
                    else
                    {
                        EditAlbumID.Text = album.AlbumID.ToString();
                        EditTitle.Text = album.Title;
                        EditAlbumArtistList.SelectedValue = album.ArtistID.ToString();
                        EditReleaseYear.Text = album.ReleaseYear.ToString();
                        EditReleaseLabel.Text = album.ReleaseLabel ?? "";
                        //EditReleaseLabel.Text = album.ReleaseLabel == null ? "" : album.ReleaseLabel;
                    }
                },"Find Album", "Album found" //success messages
            );    
        }
    }
}