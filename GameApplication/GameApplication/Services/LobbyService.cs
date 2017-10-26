﻿using GameApplication.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameApplication.Services
{
    public class LobbyService
    {
        private readonly GameService _gameService;
        private readonly Dictionary<string, List<Lobby>> _lobbies;

        public LobbyService(GameService gameService)
        {
            _gameService = gameService;
            _lobbies = new Dictionary<string, List<Lobby>>();
            var gameNames = gameService.FindAllGamesNames();
            foreach (var gameName in gameNames)
            {
                _lobbies.Add(gameName, new List<Lobby>());
            }

        }

        public List<Lobby> FindAllByGameName(string gameName)
        {
            return _lobbies[gameName];
        }

        public Lobby Create(string gameName, Player creator)
        {
            var game = _gameService.FindByGameName(gameName);
            var lobby = new Lobby(game, creator);
            _lobbies[gameName].Add(lobby);
            return lobby;
        }

        public void Join(long lobbyId, string gameName, Player mockedPlayer)
        {
            var lobby = FindByIdAndGameName(lobbyId, gameName);
            lobby.AddPlayer(mockedPlayer);
        }


        public Lobby FindByIdAndGameName(long lobbyId, string gameName)
        {
            var lobb = _lobbies[gameName].Find(lobby => lobby.Id == lobbyId);
            return lobb;
        }

    }
}
