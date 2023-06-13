//using Autohand;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class NetworkGrabber : MonoBehaviour
{
    /*
    [SerializeField,HideInEditorMode] private Hand hand;

    [SerializeField, ReadOnly] private Grabbable targetGrabbable;
    [SerializeField, ReadOnly] private NetworkObject targetNetworkObject;

    private void Start()
    {
        hand = GetComponent<Hand>();

        hand.OnGrabbed += OnGrabGrabbable;
        hand.OnForcedRelease += OnGrabReleased;
        hand.OnReleased += OnGrabReleased;
    }

    private void OnDestroy()
    {
        hand.OnGrabbed -= OnGrabGrabbable;
        hand.OnForcedRelease -= OnGrabReleased;
        hand.OnReleased -= OnGrabReleased;
    }

    public void OnGrabGrabbable(Hand _hand,Grabbable _grabbable)
    {
        //We are not in a networked session so continue grabbing
        if (PlayerTools.networkTools == null) return;

        

        targetGrabbable = _grabbable;
        targetNetworkObject = _grabbable.GetComponent<NetworkObject>();

        //Not a network grabbable, this only exists on our end so can be grabbed!
        if (targetNetworkObject == null) return;


        //Owned by server so can attempt to own it ourselves
        if (targetNetworkObject.IsOwnedByServer)
        {
            if (RequestOwnershipRoutine != null) StopCoroutine(RequestOwnershipRoutine);

            RequestOwnershipRoutine = RequestOwnership();

            SetOwnershipToMe(targetNetworkObject);
            StartCoroutine(RequestOwnershipRoutine);
        }
        else
        {
            //This is not ours to grab!!! Let go
            hand.ForceReleaseGrab();
        }

        //If the owner clientId is null, attempt to set myself as the owner.
        //Start a timer of 0.5 seconds. If after the timer I'm still not the owner, release the hand
    }

    public void OnGrabReleased(Hand _hand, Grabbable _grabbable)
    {
        //We are not in a networked session so ignore this
        if (PlayerTools.networkTools == null) return;

        if (_grabbable == null) return;

        if(_grabbable.GetComponent<NetworkObject>() == targetNetworkObject)
        {
            RemoveAllOwnership(targetNetworkObject);

            targetGrabbable = null;
            targetNetworkObject = null;
        }
    }


    private IEnumerator RequestOwnershipRoutine;

    
    private IEnumerator RequestOwnership()
    {
        if (targetGrabbable == null || targetNetworkObject == null) yield break;

        Debug.Log(targetNetworkObject.name + " -- 1");

        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log(targetNetworkObject.name + " -- 2");
        if (targetNetworkObject.IsOwner == false)
        {
            if (targetGrabbable == hand.GetHeldGrabbable())
            {
                hand.ForceReleaseGrab();
            }
        }
    }

    [Button]
    public void SetOwnershipToMe(NetworkObject target)
    {
        PlayerTools.networkTools.RequestNetoworkObjectOwnershipServerRpc(PlayerTools.networkTools.OwnerClientId, target);       
    }
    [Button]
    public void RemoveAllOwnership(NetworkObject target)
    {
        PlayerTools.networkTools.RemoveNetworkObjectOwnershipServerRpc(target);
    }
    */
}
