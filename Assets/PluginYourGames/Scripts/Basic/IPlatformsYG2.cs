using UnityEngine;

namespace YG
{
    public partial interface IPlatformsYG2
    {
        void InitAwake()
        {
            if (YG2.infoYG.Basic.syncInitSDK)
                YG2.SyncInitialization();
        }
        void InitStart() { }
        void InitComplete() { }
        void Message(string message) => Debug.Log(message);
    }
    public partial class PlatformYG2 : IPlatformsYG2
    {
#if YandexGamesPlatform_yg
        public void InitializeRewardedAds()
        {
            
        }
#endif
#if AndroidPlatformPlatform_yg
        public PlatformYG2()
        {
        }
#endif
#if UNITY_SERVER
        public void InitializeRewardedAds()
        {
            throw new System.NotImplementedException();
        }
#endif
    }
    public partial class PlatformYG2NoRealization : IPlatformsYG2
    {



        public void InitializeRewardedAds()
        {

        }
    }
}