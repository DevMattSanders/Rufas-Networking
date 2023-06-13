using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode.Components;

namespace Rufas.Networking
{
    public class ClientNetworkAnimator : NetworkAnimator
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}
