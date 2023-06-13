using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;

namespace Rufas.Networking
{
    public class SessionManager : NetworkBehaviour
    {
        public static SessionManager Instance;

        [Header("Network Prefabs")]
        public NetworkPrefabsList networkPrefabsList;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("I am a second Session Manager!", gameObject);
            }
        }

        [ServerRpc(RequireOwnership = false)] public void SpawnPrefabOverTheNetwork_ServerRPC(ulong prefabNetworkId, Vector3 spawnPosition)
        {
            Debug.Log("RPC Called");
            if (IsServer) { Debug.Log("I am the server");  }

            foreach(NetworkPrefab networkPrefab in networkPrefabsList.PrefabList)
            {
                if (networkPrefab.Prefab.GetComponent<NetworkObject>().PrefabIdHash == prefabNetworkId)
                {
                    NetworkObject networkObject = Instantiate(networkPrefab.Prefab).GetComponent<NetworkObject>();
                    networkObject.transform.position = spawnPosition;
                    networkObject.Spawn();
                }
            }
        }
    }
}
