#if AndroidPlatformPlatform_yg
using YG.Insides;
using UnityEngine;

namespace YG
{
    public class InterstitialCoroutineRunner : MonoBehaviour { }

    public partial class PlatformYG2 : MonoBehaviour, IPlatformsYG2
    {/* 
        public void LoadInterAdv()
        {
            LoadInterstitial();
        }

        public void OnAdHidden(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            YGInsides.CloseInterAdv();
            LoadInterstitial();
        }

        protected virtual void OnDestroy()
        {
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent -= OnAdHidden;
        }


        public void InterstitialAdvShow()
        {
            Debug.Log($"Попытка показа межстраничной рекламы. Готова: {MaxSdk.IsInterstitialReady(interstitialAdUnitId)}");

            if (MaxSdk.IsInterstitialReady(interstitialAdUnitId))
            {
                MaxSdk.ShowInterstitial(interstitialAdUnitId);
                YGInsides.OpenInterAdv();
            }
            else
            {
                Debug.LogWarning("Межстраничная реклама не готова к показу");
                LoadInterstitial();
                YGInsides.ErrorInterAdv();
                YGInsides.CloseInterAdv();
            }
        }

        public void LoadInterstitial()
        {
            Debug.Log($"Начало загрузки межстраничной рекламы. ID: {interstitialAdUnitId}");
            MaxSdk.LoadInterstitial(interstitialAdUnitId);
        }

        public void FirstInterAdvShow()
        {
            OptionalPlatform.FirstInterAdvShow_RealizationSkip();
        }

        public void OtherInterAdvShow()
        {
            InterstitialAdvShow();
        } */
    }
}
#endif