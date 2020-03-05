using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Composing;
using Umbraco.Web.WebApi;
using UmbracoWeb.Configuration;
using UmbracoWeb.Models;

namespace UmbracoWeb.Controllers
{
    [RoutePrefix("api/footballmanager/team")]
    public class TeamController : UmbracoApiController
    {
        private readonly IContentService _contentService;
        public TeamController()
        {
            _contentService = Services.ContentService;
        }

        [HttpGet]
        [Route("test/{p}")]
        public string Test(int p)
        {
            int k = p + 1;
            return k.ToString();
        }

        /// <summary>
        /// Get all players from DB
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns>List of players</returns>
        [HttpGet]
        [Route("GetAllPlayers/{nodeId}")]
        public IEnumerable<PlayerViewModel> GetAllPlayers(int nodeId)
        {
            //int nodeID = 2077; //Players content


            if (!IsNodeIdCorrect(nodeId))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var playersContentById = Umbraco.Content(nodeId);
            if (!IsNodeExists(playersContentById))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            //var contentService = Services.ContentService;
            //var obj = contentService.GetById(nodeID);

            IEnumerable<IPublishedContent> playersListContent = GetChildrensByAlias(playersContentById, UmbracoAliasConfiguration.Player.Alias);

            if (playersListContent == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            List<PlayerViewModel> allPlayers = new List<PlayerViewModel>();
            foreach (var player in playersListContent)
            {
                allPlayers.Add(new PlayerViewModel()
                {
                    Name = player.Value(UmbracoAliasConfiguration.Player.PlayerName).ToString(),
                    Age = Int32.Parse(player.Value(UmbracoAliasConfiguration.Player.PlayerAge).ToString())
                });
                //Debug.WriteLine("New player");
                //Debug.WriteLine(item.Value("PlayerName"));
            }

            return allPlayers;
        }

        /// <summary>
        /// Get Player By ID from all players
        /// </summary>
        /// <exception cref="HttpResponseException">Not Found</exception>
        /// <param name="nodeId">Player ID</param>
        /// <returns>Player Model</returns>
        [HttpGet]
        [Route("GetPlayerById/{nodeId}")]
        public PlayerViewModel GetPlayerById(int nodeId)
        {
            //int nodeId = 2084; //Varan
            if (!IsNodeIdCorrect(nodeId))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var playerContent = Umbraco.Content(nodeId);
            if (!IsNodeExists(playerContent))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var player = new PlayerViewModel()
            {
                Name = playerContent.Value(UmbracoAliasConfiguration.Player.PlayerName).ToString(),
                Age = Int32.Parse(playerContent.Value(UmbracoAliasConfiguration.Player.PlayerAge).ToString())
            };

            return player;
        }
        /// <summary>
        /// Add new player
        /// </summary>
        /// <param name="newPlayer"></param>
        /// <returns>Player model, which was created</returns>
        [HttpPost]
        [Route("AddNewPlayer/")]
        public PlayerViewModel AddNewPlayer(PlayerViewModel newPlayer)
        {
            int nodeID = 2077;

            //get node info to add in
            var playersNodeName = Umbraco.Content(nodeID).Name;
            var playersNodeGuid = Umbraco.Content(nodeID).Key;
            GuidUdi currentPageUdi = new GuidUdi(playersNodeName, playersNodeGuid);

            //create content node
            var data = _contentService.CreateContent(playersNodeName, currentPageUdi, UmbracoAliasConfiguration.Player.Alias, 0);

            //define properties
            data.Name = newPlayer.Name;
            data.SetValue(UmbracoAliasConfiguration.Player.PlayerName, newPlayer.Name);
            data.SetValue(UmbracoAliasConfiguration.Player.PlayerAge, newPlayer.Age);

            _contentService.SaveAndPublish(data);

            return newPlayer;
        }

        /// <summary>
        /// Get all players from the team
        /// </summary>
        /// <param name="nodeId"></param>
        /// <exception cref="HttpResponseException">Not Found</exception>
        /// <returns>List of team players</returns>
        [HttpGet]
        [Route("GetTeamPlayers/{nodeId}")]
        public IEnumerable<PlayerViewModel> GetTeamPlayers(int nodeId)
        {
            //int nodeID = 2068; //Barcelona content

            if (!IsNodeIdCorrect(nodeId))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var teamPlayersContentById = Umbraco.Content(nodeId);
            if (!IsNodeExists(teamPlayersContentById))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var teamPlayersContent = teamPlayersContentById.Value<IEnumerable<IPublishedContent>>(UmbracoAliasConfiguration.Team.Players).ToList();

            if (teamPlayersContent == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            List<PlayerViewModel> teamPlayersList = new List<PlayerViewModel>();
            foreach (var player in teamPlayersContent)
            {
                teamPlayersList.Add(new PlayerViewModel()
                {
                    Name = player.Value(UmbracoAliasConfiguration.Player.PlayerName).ToString(),
                    Age = Int32.Parse(player.Value(UmbracoAliasConfiguration.Player.PlayerAge).ToString())
                });
            }
            return teamPlayersList;
        }

        /// <summary>
        /// check, if nodeId is above zero
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        private bool IsNodeIdCorrect(int nodeId)
        {
            return nodeId > 0 ? true : false;
        }

        /// <summary>
        /// check, if content with specified node exists
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private bool IsNodeExists(IPublishedContent content)
        {
            return content != null ? true : false;
        }

        /// <summary>
        /// select children from parent by alias
        /// </summary>
        /// <param name="parentContent"></param>
        /// <param name="childrenAlias"></param>
        /// <returns></returns>
        private IEnumerable<IPublishedContent> GetChildrensByAlias(IPublishedContent parentContent, string childrenAlias)
        {
            IEnumerable<IPublishedContent> contentList = parentContent.Children;
            if (contentList.Count() != 0)
            {
                return contentList.Where(x => x.IsDocumentType(childrenAlias));
            }
            return null;
        }


    }
}
