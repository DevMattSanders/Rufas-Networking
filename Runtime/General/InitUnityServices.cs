using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unity Gaming Services
using Unity.Services.Core;
using Unity.Services.Authentication;

using UnityEngine.Events;
using Unity.Netcode;
using Sirenix.OdinInspector;

namespace Rufas.Networking
{
    public class InitUnityServices : MonoBehaviour
    {
        public static InitUnityServices i;


        public UnityEvent onInitUnityServices;


        private bool unityServicesInitialized = false;
        [ShowInInspector,ReadOnly]
        public bool UnityServicesInitialized
        {
            get
            {
                return unityServicesInitialized;
            }
        }

        private void Awake()
        {
            if (i != null)
            {
                Debug.LogError("Multiple InitUnityServices exist!");

                return;
            }

            i = this;


        }

        public async void Start()
        {
            unityServicesInitialized = false;
            // Init Unity Services
            RufasDebugger.Log("Initializing Unity Services");
            await UnityServices.InitializeAsync();

            // Anon Signed In On Init (Progress Linked to their Machine)
            if (UnityServices.State == ServicesInitializationState.Initialized)
            {
                RufasDebugger.Log("Unity Services Initialized. Signing In Anonymously");
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

                if (AuthenticationService.Instance.IsSignedIn)
                {
                    RufasDebugger.Log("Signed in to Unity.Services.Authentication");
                    string userName = PlayerPrefs.GetString(key: "username");
                    if (userName == "")
                    {
                        userName = "Guest Player";
                        PlayerPrefs.SetString("username", userName);
                    }

                    unityServicesInitialized = true;
                    onInitUnityServices.Invoke();
                }
            }
        }
    }
}