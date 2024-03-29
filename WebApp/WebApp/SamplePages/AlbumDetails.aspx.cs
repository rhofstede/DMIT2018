﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class AlbumDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void AlbumTracks_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            // ListViewCommandEventArgs e contains the
            // value that was attach to the link on the
            // listview row
            // The property that you need to access is
            //  CommandArgument
            // It is NOT a string
            CommandArgID.Text = e.CommandArgument.ToString();

            //extract a value from a column on the listview item (row)
            ColumnID.Text = (e.Item.FindControl("TrackIdLabel") as Label).Text;
        }

        protected void Totals_Click(object sender, EventArgs e)
        {
            double time = 0;
            double size = 0;

            //use foreach to cycle through the listview
            foreach (ListViewItem item in this.AlbumTracks.Items)
            {
                time += double.Parse((item.FindControl("MillisecondsLabel") as Label).Text);
                size += double.Parse((item.FindControl("BytesLabel") as Label).Text);
            }

            TracksTime.Text = time.ToString();
            TracksSize.Text = size.ToString();
        }
    }
}