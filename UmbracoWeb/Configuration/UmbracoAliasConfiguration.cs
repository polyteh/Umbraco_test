using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoWeb.Configuration
{
    public static class UmbracoAliasConfiguration
    {
        public static class Player
        {
            public static readonly string Alias = "player";
            public static readonly string PlayerName = "playerName";
            public static readonly string PlayerAge = "playerAge";
        }

        public static class Team
        {
            public static readonly string Alias = "team";
            public static readonly string TeamName = "teamName";
            public static readonly string TeamStadium = "stadiumName";
            public static readonly string Players = "Players";
        }

    }
}