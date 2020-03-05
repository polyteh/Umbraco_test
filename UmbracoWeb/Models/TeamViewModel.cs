using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoWeb.Models
{
    /// <summary>
    /// team model
    /// </summary>
    public class TeamViewModel
    {
        /// <summary>
        /// team name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// team stadium name
        /// </summary>
        public string Stadium { get; set; }
        /// <summary>
        /// list of team players
        /// </summary>
        public List<PlayerViewModel> Players { get; set; }
    }
}