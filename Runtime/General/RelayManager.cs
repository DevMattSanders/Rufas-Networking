using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;

using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq;

namespace Rufas.Networking
{
    [RequireComponent(typeof(NetworkManager))]
    public class RelayManager : MonoBehaviour
    {
        public static RelayManager Instance;
        public static string HostJoinCode;
        [ReadOnly] public string currentSessionJoinCode;

        [SerializeField] private NetworkPrefabsList networkPrefabsList;

        [Header("Session Details")]
        public int maxPlayers;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("Two Relay Managers Found", this.gameObject);
                Destroy(this);
            }
        }

        public UnityEvent OnRelayServerStartedDelegate;
        private void OnEnable()
        {
            GetComponent<NetworkManager>().OnServerStarted += OnRelayStarted;
        }

        private void OnDisable()
        {
            GetComponent<NetworkManager>().OnServerStarted -= OnRelayStarted;                 
        }

        private void OnRelayStarted()
        {
            OnRelayServerStartedDelegate.Invoke();
        }

        [Button()]
        public async void HostNewSessionViaRelay()
        {
            // Create Relay Allocation
            Allocation relayAllocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
            HostJoinCode = await RelayService.Instance.GetJoinCodeAsync(relayAllocation.AllocationId);
            RufasDebugger.Log("Relay Join Code:" + HostJoinCode);

            // Set Unity Transport Relay Data
            UnityTransport unityTransport = GetComponent<UnityTransport>();
            unityTransport.SetHostRelayData(relayAllocation.RelayServer.IpV4, (ushort)relayAllocation.RelayServer.Port, relayAllocation.AllocationIdBytes, relayAllocation.Key, relayAllocation.ConnectionData);

            currentSessionJoinCode = HostJoinCode;

            // Start Host
            NetworkManager.Singleton.StartHost();
        }

        [Button()]
        public async void JoinSessionViaRelay(string joinCode)
        {
            RufasDebugger.Log("Starting client on realy with join code: " + joinCode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            UnityTransport unityTransport = GetComponent<UnityTransport>();
            unityTransport.SetClientRelayData(joinAllocation.RelayServer.IpV4, (ushort)joinAllocation.RelayServer.Port, joinAllocation.AllocationIdBytes, joinAllocation.Key, joinAllocation.ConnectionData, joinAllocation.HostConnectionData);
            currentSessionJoinCode = joinCode;

            // Start Client
            NetworkManager.Singleton.StartClient();
        }

        private void OnApplicationQuit()
        {
            QuitMultiplayerSession();
        }

        public void QuitMultiplayerSession()
        {
            RufasDebugger.Log("Network Manager Shut Down");
            currentSessionJoinCode = string.Empty;
            NetworkManager.Singleton.Shutdown();
        }
    }
}
