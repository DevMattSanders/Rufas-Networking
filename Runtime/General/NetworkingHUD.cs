using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

namespace Rufas.Networking
{
    public class NetworkingHUD : MonoBehaviour
    {
        [Header("Host & Join")]
        [SerializeField] private GameObject connectionMenu;
        [SerializeField] private Button joinButton;
        [SerializeField] private TMP_Text joinCodeInputField;

        [Header("Gameplay")]
        [SerializeField] private GameObject gameplayMenu;
        [SerializeField] private TMP_Text joinCodeReferenceText;
        [SerializeField] private TMP_Text hostClientReferenceText;
        [SerializeField] private TMP_Text userNameInputField;

        #region Host & Join
        public string GetJoinCodeString()
        {
            string joinCode = joinCodeInputField.text;
            joinCode = joinCode.Substring(0, joinCode.Length - 1);
            if (joinCode == "")
            {
                RufasDebugger.Error("No join code found");
                return null;
            }

            return joinCode;
        }

        public void OnChangedJoinCodeInput()
        {
            Debug.Log("Changed");
            string joinCode = joinCodeInputField.text;
            joinCode = joinCode.Substring(0, joinCode.Length - 1);
            if (string.IsNullOrEmpty(joinCode))
            {
                joinButton.interactable = false;
            }
            else
            {
                joinButton.interactable = true;
            }
        }

        #endregion

        #region Gameplay
        public void HideHostJoinMenu()
        {
            connectionMenu.SetActive(false);
        }

        public void SubmitUserName()
        {
            NameTag[] nameTags = FindObjectsByType<NameTag>(FindObjectsSortMode.InstanceID);
            foreach (NameTag item in nameTags)
            {
                if (item.IsOwner)
                {
                    //item.UpdateNameTag(GetUserNameString());
                    break;
                }
            }

        }

        public string GetUserNameString()
        {
            string userName = userNameInputField.text;
            userName = userName.Substring(0, userName.Length - 1);
            if (userName == "")
            {
                RufasDebugger.Error("No join code found");
                return null;
            }

            return userName;
        }

        public void ActivateGameplayHUD(string joinCode, bool isHost)
        {
            gameplayMenu.SetActive(true);

            // Join Code
            joinCodeReferenceText.SetText("Join Code: " + joinCode);
            //joinCodeReferenceText.gameObject.SetActive(true);

            // Client Host Text
            if (isHost) { hostClientReferenceText.SetText("Host"); }
            else { hostClientReferenceText.SetText("Client"); }
            //hostClientReferenceText.gameObject.SetActive(true);
        }
        #endregion
    }
}
