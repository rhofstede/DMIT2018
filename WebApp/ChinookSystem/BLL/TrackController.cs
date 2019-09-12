﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region
using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
#endregion

namespace ChinookSystem.BLL
{
    class TrackController
    {
        public List<Track> Track_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.ToList();
            }
        }

        public Track GetTrack(int trackID)
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.Find(trackID);
            }
        }
    }
}
