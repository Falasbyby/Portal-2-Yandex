#if AndroidPlatformPlatform_yg
using YG.Insides;
using UnityEngine;

namespace YG
{
    public partial class PlatformYG2 : MonoBehaviour, IPlatformsYG2
    {
        /* private string interstitialAdUnitId = "3ca5e2d0d2818754";
        private string rewardedAdUnitId = "b788e634f8455835";
        public void InitAwake()
        {

            Debug.Log("Инициализация MaxSdk");
            MaxSdk.SetHasUserConsent(true);
            MaxSdk.SetUserId("USER_ID");

            MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
            {
                Debug.Log("MaxSdk инициализирован");
                LoadInterstitial();
                InitializeRewardedAds();
                MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnAdHidden;
                YG2.SyncInitialization();
            };

            MaxSdk.InitializeSdk();

        } */
    }
}
#endif