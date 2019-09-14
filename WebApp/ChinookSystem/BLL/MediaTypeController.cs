using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region
using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    class MediaTypeController
    {
        public List<MediaType> MediaType_List()
        {
            using (var context = new ChinookContext())
            {
                return context.MediaTypes.ToList();
            }
        }

        public MediaType GetMediaType(int mediaTypeID)
        {
            using (var context = new ChinookContext())
            {
                return context.MediaTypes.Find(mediaTypeID);
            }
        }
    }
}
