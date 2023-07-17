using Sirenix.OdinInspector;
using Rufas;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPrefabsListCollection : MonoBehaviour
{
    public static NetworkPrefabsListCollection instance;

    

   public List<GameObject> networkPrefabs = new List<GameObject>();

    [SerializeField,ReadOnly]
    private List<NetworkPrefabsList> networkPrefabLists = new List<NetworkPrefabsList>();


     public List<GameObject> registeredPrefabs = new List<GameObject>();

    [Button]
    private void GetPrefabLists()
    {
#if UNITY_EDITOR
        networkPrefabLists = RufasStatic.GetAllScriptables_ToList<NetworkPrefabsList>();//.FindAllScriptableObjectsOfType<NetworkPrefabsList>();
#endif

    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Other Network Manager Bridge found");
        }
    }

    public void Start()
    {
        NetworkManager manager = GetComponent<NetworkManager>();

        //NetworkManager.Singleton.SpawnManager.

        //Generate true list of all network prefabs
        registeredPrefabs.Clear();
        registeredPrefabs.AddRange(networkPrefabs);

        foreach (GameObject prefabToAdd in networkPrefabs)
        {
            foreach (NetworkPrefabsList nextList in networkPrefabLists)
            {
                foreach (NetworkPrefab nextNetworkPrefab in nextList.PrefabList)
                {
                    if (!registeredPrefabs.Contains(nextNetworkPrefab.Prefab))
                    {
                        registeredPrefabs.Add(nextNetworkPrefab.Prefab);
                    }
                }
            }
        }

        foreach (GameObject prefabToAdd in networkPrefabs)
        {
            foreach (NetworkPrefabsList nextList in networkPrefabLists)
            {
                bool shouldSkipThisPrefab = false;

                foreach (NetworkPrefab nextNetworkPrefab in nextList.PrefabList)
                {


                    if (nextNetworkPrefab.Prefab == prefabToAdd)
                    {
                        shouldSkipThisPrefab = true;
                        break;
                    }
                }

                if (shouldSkipThisPrefab)
                {
                    continue;
                }
                else
                {
                    manager.AddNetworkPrefab(prefabToAdd);
                }
            }


        }
    }
}
