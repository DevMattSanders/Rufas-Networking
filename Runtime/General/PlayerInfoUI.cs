using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace Rufas.Networking
{
    public class PlayerInfoUI : MonoBehaviour
    {
        public TMP_Text playerUsernameText;
        public TMP_Text playerPingText;

        public void UpdatePlayerInfoText(string username, int ping)
        {
            playerUsernameText.SetText(username);
            playerPingText.SetText(ping.ToString() + " ms");
        }
    }
}
