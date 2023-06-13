using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
//using Unity.Services.Lobbies;
using UnityEngine;

namespace Rufas.Networking
{
    public class LobbyTesting : MonoBehaviour
    {
        public string lobbyName;
        public int maxPlayers;

        private async void Start()
        {
            await UnityServices.InitializeAsync();

            AuthenticationService.Instance.SignedIn += () =>
            {
                Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
            };
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        private void CreateLobby()
        {
     //       LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);
        }
    }
}
