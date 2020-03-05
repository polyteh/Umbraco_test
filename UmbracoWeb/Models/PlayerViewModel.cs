using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace UmbracoWeb.Models
{
    /// <summary>
    /// player model
    /// </summary>
    public class PlayerViewModel
    {
        /// <summary>
        /// player name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// player age
        /// </summary>
        public int Age { get; set; }
    }
}