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
                        ClearControls();
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
        protected void ClearControls()
        {
            EditAlbumID.Text = "";
            EditTitle.Text = "";
            EditReleaseYear.Text = "";
            EditReleaseLabel.Text = "";
            EditAlbumArtistList.SelectedIndex = 0;
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string title = EditTitle.Text;
                int year = int.Parse(EditReleaseYear.Text);
                string label = EditReleaseLabel.Text == "" ? null : EditReleaseLabel.Text;
                int artist = int.Parse(EditAlbumArtistList.SelectedValue);

                Album newAlbum = new Album();
                newAlbum.ArtistID = artist;
                newAlbum.Title = title;
                newAlbum.ReleaseYear = year;
                newAlbum.ReleaseLabel = label;

                MessageUserControl.TryRun(() =>
                {
                    AlbumController controller = new AlbumController();
                    int albumID = controller.Album_Add(newAlbum);
                    EditAlbumID.Text = albumID.ToString(); //redoes ODS controls
                    if (AlbumList.Rows.Count > 0)
                    {
                        AlbumList.DataBind();
                    }
                    
                }, "Sucessful!", "Album added to file.");
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string editID = EditAlbumID.Text;
                int albumID = 0;
                if (string.IsNullOrEmpty(editID))
                {
                    MessageUserControl.ShowInfo("Error","Must have album to edit.");
                }
                else if (!int.TryParse(editID, out albumID))
                {
                    MessageUserControl.ShowInfo("Error","Invalid album ID");
                }
                else
                {
                    string title = EditTitle.Text;
                    int year = int.Parse(EditReleaseYear.Text);
                    string label = EditReleaseLabel.Text == "" ? null : EditReleaseLabel.Text;
                    int artist = int.Parse(EditAlbumArtistList.SelectedValue);
                    int editAlbumID = int.Parse(EditAlbumID.Text);

                    Album newAlbum = new Album();
                    newAlbum.AlbumID = editAlbumID;
                    newAlbum.ArtistID = artist;
                    newAlbum.Title = title;
                    newAlbum.ReleaseYear = year;
                    newAlbum.ReleaseLabel = label;

                    MessageUserControl.TryRun(() =>
                    {
                        AlbumController controller = new AlbumController();
                        int rows = controller.Album_Update(newAlbum);

                        if (rows > 0)
                        {
                            AlbumList.DataBind();
                        }
                        else
                        {
                            throw new Exception("No album found. Try again.");
                        }

                    }, "Sucessful!", "Album updated.");
                }                
            }
        }

        protected void Remove_Click(object sender, EventArgs e)
        {
            string editID = EditAlbumID.Text;
            int albumID = 0;
            if (string.IsNullOrEmpty(editID))
            {
                MessageUserControl.ShowInfo("Error", "Must have album to edit.");
            }
            else if (!int.TryParse(editID, out albumID))
            {
                MessageUserControl.ShowInfo("Error", "Invalid album ID");
            }
            else
            {                
                MessageUserControl.TryRun(() =>
                {
                    AlbumController controller = new AlbumController();
                    int rows = controller.Album_Delete(albumID);

                    if (rows > 0)
                    {
                        AlbumList.DataBind();
                        EditAlbumID.Text = "";
                    }
                    else
                    {
                        throw new Exception("No album found. Try again.");
                    }

                }, "Sucessful!", "Album deleted.");
            }
        }
    }
}