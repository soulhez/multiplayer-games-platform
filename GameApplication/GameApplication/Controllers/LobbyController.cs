﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GameApplication.Exceptions;
using Microsoft.AspNetCore.Mvc;
using GameApplication.Models.Games;
using GameApplication.Services;

namespace GameApplication.Controllers
{
    public class LobbyController : Controller
    {
        private readonly LobbyService _lobbyService;

        public LobbyController(LobbyService lobbyService)
        {
            this._lobbyService = lobbyService;
        }

        public IActionResult Index(string gameName)
        {
            ViewData["gameName"] = gameName;
            var lobbies = _lobbyService.FindAllByGameName(gameName);
            return View(lobbies);
        }

        public IActionResult Create(string gameName)
        {
            var lobby = _lobbyService.Create(gameName, new Player(User));
            return RedirectToAction("Join", new { lobbyId = lobby.Id, gameName = gameName});
        }

        public IActionResult Join(long lobbyId, string gameName)
        {
            var lobby = _lobbyService.FindByIdAndGameName(lobbyId, gameName);
            ViewData["loggedUser"] = User.Identity.Name;
            return View("SingleLobby", lobby);
        }

        public ActionResult RefreshLobbiesList(string gameName)
        {
            var lobbies = _lobbyService.FindAllByGameName(gameName);

            return Json(LobbyResponse.Create(lobbies));
        }

        private class LobbyResponse
        {
            public long Id;
            public string Owner;
            public int NumberOfConnectedPlayers;

            private LobbyResponse(Lobby lobby)
            {
                Id = lobby.Id;
                Owner = lobby.Owner.GetName();
                NumberOfConnectedPlayers = lobby.GetNumberOfConnectedPlayers();
            }

            public static List<LobbyResponse> Create(IEnumerable<Lobby> lobbies)
            {
                return lobbies.Select(lobby => new LobbyResponse(lobby)).ToList();
            }

        }
    }
}