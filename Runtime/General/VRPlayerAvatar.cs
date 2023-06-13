using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;

namespace Rufas.Networking
{
    public class VRPlayerAvatar : NetworkBehaviour
    {
        [Header("Avatar Transforms")]
        public Transform avatarHeadTransform;
        public Transform avatarLeftHandTransform;
        public Transform avatarRightHandTransform;

        [Header("VR Rig Transforms")]
        public Transform vrHeadTransform;
        public Transform vrLeftHandTransform;
        public Transform vrRightHandTransform;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner)
            {
                // -- No reference to VRRigTransforms in RUFAS. Was this something from SlotCar?

              //  VRRigTransforms vRRigTransforms = FindAnyObjectByType<VRRigTransforms>();
              //  if (vRRigTransforms == null) { Debug.LogError("Cannot find VR Rig Transforms class"); }

              //  AssignRigToAvatar(vRRigTransforms.head, vRRigTransforms.rightHand, vRRigTransforms.leftHand);

                MeshRenderer[] renders = GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer renderer in renders)
                {
                    renderer.enabled = false;
                }
            }
        }

        private void LateUpdate()
        {
            if (!IsOwner) { return; }

            if (vrHeadTransform != null && vrLeftHandTransform != null && vrRightHandTransform != null) 
            {
                avatarHeadTransform.position = vrHeadTransform.position;
                avatarHeadTransform.rotation = vrHeadTransform.rotation;

                avatarLeftHandTransform.position = vrLeftHandTransform.position;
                avatarLeftHandTransform.rotation = vrLeftHandTransform.rotation;

                avatarRightHandTransform.position = vrRightHandTransform.position;
                avatarRightHandTransform.rotation= vrRightHandTransform.rotation;
            }   
        }

        public void AssignRigToAvatar(Transform vrHead, Transform vrRightHand, Transform vrLeftHand)
        {
            vrHeadTransform = vrHead;
            vrLeftHandTransform = vrLeftHand;
            vrRightHandTransform = vrRightHand;
        }
    }
}