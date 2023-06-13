//using Autohand;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class NetworkPlayerTools : NetworkBehaviour
{
    public static UnityEvent<NetworkPlayerTools> onNewPlayerToolsCreated = new UnityEvent<NetworkPlayerTools>();
    public static List<NetworkPlayerTools> allNetworkPlayerTools = new List<NetworkPlayerTools>();

    public List<NetworkObject> spawnableNetworkObjects = new List<NetworkObject>();

    [SerializeField] private int indexToBuild;

    public ulong clientId;

    private void Start()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        allNetworkPlayerTools.Add(this);
        onNewPlayerToolsCreated.Invoke(this);

        if (IsClient)
        {
            clientId = NetworkObject.OwnerClientId;

            spawnableNetworkObjects.Clear();
            
            foreach (GameObject next in NetworkPrefabsListCollection.instance.registeredPrefabs)
            {
                spawnableNetworkObjects.Add(next.GetComponent<NetworkObject>());
            }
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        allNetworkPlayerTools.Remove(this);
    }

    private NetworkObject toSpawn;

    

    public void SpawnObject(GameObject prefab, Vector3 pos, Quaternion rot, bool setClientAsOwner = false)
    {

        //GameObject returnedGameObject = null;

        indexToBuild = spawnableNetworkObjects.IndexOf(prefab.GetComponent<NetworkObject>());

        //
        //returnAs = //.GetComponent<NetworkObject>();

        

        SpawnObjectServerRpc(indexToBuild, pos, rot, setClientAsOwner);

        //returnAs = prefabInstance.gameObject;
    }

    public GameObject spawnedOnNetwork;


    [ServerRpc(RequireOwnership = false)]
    public void SpawnObjectServerRpc(int indexOf, Vector3 pos, Quaternion rot, bool setClientAsOwner = false)
    {
        Debug.Log(indexOf + " " + spawnableNetworkObjects.Count + " " + NetworkPrefabsListCollection.instance.networkPrefabs.Count);
        NetworkObject prefabInstance = Instantiate(spawnableNetworkObjects[indexOf], pos, rot);

        if (setClientAsOwner)
        {
            prefabInstance.SpawnWithOwnership(clientId);
        }
        else
        {
            prefabInstance.Spawn();
        }

        spawnedOnNetwork = prefabInstance.gameObject;

        //return prefabInstance.gameObject;
    }

    [ServerRpc]
    public void RequestNetoworkObjectOwnershipServerRpc(ulong newOwnerClientId, NetworkObjectReference networkObjectReference)
    {
        if (networkObjectReference.TryGet(out NetworkObject networkObject))
        {
            networkObject.ChangeOwnership(newOwnerClientId);
        }
        else
        {
            Debug.LogWarning("Unable to change ownership for clientId {newOwnerClientId}");
        }
    }

    [ServerRpc]
    public void RemoveNetworkObjectOwnershipServerRpc(NetworkObjectReference networkObjectReference)
    {
        if (networkObjectReference.TryGet(out NetworkObject networkObject))
        {
            networkObject.RemoveOwnership();
        }
    }
}