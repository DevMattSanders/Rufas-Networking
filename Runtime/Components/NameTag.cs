using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace Rufas.Networking
{
    public class NameTag : NetworkBehaviour
    {
        private string userName = "unnamed player";
        [SerializeField] private NetworkVariable<int> playerNumber = new NetworkVariable<int>();
        [SerializeField] private TMP_Text nameTagText;

        private void Start()
        {
            //UpdateNameTag(userName);
        }

        public void UpdateNameTag()
        {
            userName = "Player (" + playerNumber.ToString() + ")";
            nameTagText.SetText(userName);
        }
    }
}
