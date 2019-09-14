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
    public class GenreController
    {
        public List<Genre> Genre_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Genres.ToList();
            }
        }

        public Genre GetGenre(int genreID)
        {
            using (var context = new ChinookContext())
            {
                return context.Genres.Find(genreID);
            }
        }
    }
}
