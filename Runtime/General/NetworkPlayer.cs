using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Rufas.Networking
{
    public class NetworkPlayer : NetworkBehaviour
    {
        public string playerUsername;

        public override void OnNetworkSpawn()
        {
            
            base.OnNetworkSpawn();
        }
    }
}
