﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.ENTITIES;
using ChinookSystem.DAL;
using ChinookSystem.VIEWMODELS;
using System.Data.Entity;
using System.Data.SqlClient;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
	[DataObject]
    public class PlayListController
	{
        #region Repeater Demo
        [DataObjectMethod(DataObjectMethodType.Select, false)]
		public List<PlayListItem> PlayList_GetPlayListOfSize(int lowestplaylistsize)
		{
			using (var context = new ChinookSystemContext())
			{
				var results = from x in context.Playlists
							  where x.PlaylistTracks.Count() >= lowestplaylistsize
							  orderby x.UserName
							  select new PlayListItem
							  {
								  Name = x.Name,
								  TrackCount = x.PlaylistTracks.Count(),
								  UserName = x.UserName,
								  Songs = from y in x.PlaylistTracks
										  orderby y.Track.Genre.Name, y.Track.Name
										  select new PlayListSong
										  {
											  Song = y.Track.Name,
											  GenreName = y.Track.Genre.Name
										  }
							  };
				return results.ToList();
			}
		}
        #endregion
    }
}
