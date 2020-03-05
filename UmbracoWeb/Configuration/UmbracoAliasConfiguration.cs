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
            public static readonly string PlayerName = "PlayerName";
            public static readonly string PlayerAge = "PlayerAge";
        }

        public static class Team
        {
            public static readonly string Alias = "team";
            public static readonly string Players = "Players";

        }

    }
}