using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    public static NetworkPlayerTools networkTools = new NetworkPlayerTools();

    [SerializeField,ReadOnly]
    private NetworkPlayerTools localNetworkPlayerTools;

    public static PlayerTools instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Singleton not so single! - PlayerTools duplicated on: " + gameObject.name);
            GameObject.Destroy(gameObject);
        }          
    }

    private void Start()
    {
        NetworkPlayerTools.onNewPlayerToolsCreated.AddListener(NewNetorkPlayerToolsCreated);    
    }

    private void OnDestroy()
    {
        NetworkPlayerTools.onNewPlayerToolsCreated.RemoveListener(NewNetorkPlayerToolsCreated);
    }


    public void OnBeganJoinSession()
    {
        //Remove all of my objects ahead of joining
    }

    public void OnHostedSession()
    {
        //Make all my objects spawn on network.
    }

    public void OnLeftSession()
    {

    }

    private void NewNetorkPlayerToolsCreated(NetworkPlayerTools newNetworkPlayer)
    {
        foreach(NetworkPlayerTools next in NetworkPlayerTools.allNetworkPlayerTools)
        {
            if (next.IsOwner)
            {
                if(localNetworkPlayerTools != null && localNetworkPlayerTools != next)
                {
                    Debug.LogError("Multiple network player tools found with IsOwner enabled!");
                }

                localNetworkPlayerTools = next;
                networkTools = next;
                break;
            }
        }
    }

    public List<GameObject> spawnedOffOfServer = new List<GameObject>();


  //  [Button]
   // public void SpawnFromInstanced(GameObject gameObject)
   // {
      //  networkTools.Spawn(gameObject.GetComponent<NetworkObject>()) ;
   // }


    [Button]
    public void SpawnObject(GameObject prefab, Vector3 pos, Quaternion rot, bool setClientAsOwner = false)
    {
        //GameObject returnedGameobject = null;

        if (localNetworkPlayerTools)
        {
            localNetworkPlayerTools.SpawnObject(prefab,pos,rot, setClientAsOwner);
        }
        else
        {
            //Create gameobject and spawn on server later once we host.
           
            //Make sure to spawn these items into a list for this reason!
        }

        

    }


    public void DespawnObject(GameObject objectToDespawn)
    {

    }
}
