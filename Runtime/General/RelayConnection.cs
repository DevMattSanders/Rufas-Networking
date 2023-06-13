using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;

namespace Rufas.Networking
{
    public class RelayConnection : MonoBehaviour
    {
        [SerializeField] private int maxPlayers;

        public async void StartHostUsingRelay()
        {
            // Create Relay Allocation
            Allocation relayAllocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(relayAllocation.AllocationId);
            RufasDebugger.Log("Relay Join Code:" +  joinCode);

            // Set Unity Transport Relay Data
            UnityTransport unityTransport = GetComponent<UnityTransport>();
            unityTransport.SetHostRelayData(relayAllocation.RelayServer.IpV4, (ushort)relayAllocation.RelayServer.Port, relayAllocation.AllocationIdBytes, relayAllocation.Key, relayAllocation.ConnectionData);

            // Start Host
            NetworkManager.Singleton.StartHost();

            // Update join code text
            NetworkingHUD hud = FindAnyObjectByType<NetworkingHUD>();
            hud.HideHostJoinMenu();
            hud.ActivateGameplayHUD(joinCode, true);
        }

        public async void JoinAsClientUsingRelay()
        {
            string joinCode = FindAnyObjectByType<NetworkingHUD>().GetJoinCodeString();

            RufasDebugger.Log("Starting client on realy with join code: " + joinCode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            UnityTransport unityTransport = GetComponent<UnityTransport>();
            unityTransport.SetClientRelayData(joinAllocation.RelayServer.IpV4, (ushort)joinAllocation.RelayServer.Port, joinAllocation.AllocationIdBytes, joinAllocation.Key, joinAllocation.ConnectionData, joinAllocation.HostConnectionData);

            // Start Client
            NetworkManager.Singleton.StartClient();

            // Update join code text
            NetworkingHUD hud = FindAnyObjectByType<NetworkingHUD>();
            hud.HideHostJoinMenu();
            hud.ActivateGameplayHUD(joinCode, false);
        }

        private void OnApplicationQuit()
        {
            RufasDebugger.Log("Network Manager Shut Down");
            NetworkManager.Singleton.Shutdown();
        }
    }
}
