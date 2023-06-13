using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;

namespace Rufas.Networking.Examples
{
    public class NetworkMovement : NetworkBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;

        void Update()
        {
            // Do nothing if this instance is not owned by the current client
            if (!IsOwner) { return; }

            Vector2 moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (moveDir.magnitude > 0.1f) 
            { transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime); }
        }
    }
}
