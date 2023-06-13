using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.Netcode;

namespace Rufas.Networking
{
    [CreateAssetMenu(menuName = "Rufas/Network")]
    public class NetworkManagerScriptable : SuperScriptable
    {
        [Button]
        public void Host()
        {
            NetworkManager.Singleton.StartHost();
        }

    }
}
