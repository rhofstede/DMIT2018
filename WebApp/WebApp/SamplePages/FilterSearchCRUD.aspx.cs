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

        
    }
}