#if AndroidPlatformPlatform_yg 
using YG.Insides;
using UnityEngine;

namespace YG
{
    public partial class PlatformYG2 : IPlatformsYG2
    {/* 
        private bool isRewardedAdReady = false;
        private System.Action onRewardedAdDisplayFailed;
        private System.Action onRewardedAdHidden;
        private System.Action onRewardedAdReceivedReward;

        public void RewardedAdvShow(string id)
        {
            Debug.Log("Показ рекламы на андроиде");
            if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
            {
                MaxSdk.ShowRewardedAd(rewardedAdUnitId);
                Debug.Log("Показ рекламы");
                YGInsides.OpenRewardedAdv();
            }
            else
            {
                Debug.Log("Реклама не готова");
                LoadRewardedAd();
                YGInsides.ErrorRewardedAdv();
                YGInsides.CloseRewardedAdv();
            }  
        }

        private void LoadRewardedAd()
        {
            MaxSdk.LoadRewardedAd(rewardedAdUnitId);
        }

        public void InitializeRewardedAds()
        {
            Debug.Log("Инициализация рекламы Rewarded на андроиде");
            // Подписываемся на событие успешной загрузки
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += (_, _) =>
            {
                isRewardedAdReady = true;
                Debug.Log("Реклама загружена");
            }; 

            // Подписываемся на события один раз при инициализации
            onRewardedAdDisplayFailed = () =>
            {
                YGInsides.ErrorRewardedAdv();
                YGInsides.CloseRewardedAdv();
                LoadRewardedAd();
                Debug.Log("Ошибка рекламы");
            };

            onRewardedAdHidden = () =>
            {
                YGInsides.CloseRewardedAdv();
                LoadRewardedAd();
                Debug.Log("Закрытие рекламы");
            };

            onRewardedAdReceivedReward = () =>
            {
                YGInsides.RewardAdv(rewardedAdUnitId);
                Debug.Log("Награда рекламы");
            };

            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += (string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo) => onRewardedAdDisplayFailed();
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += (string adUnitId, MaxSdkBase.AdInfo adInfo) => onRewardedAdHidden();
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += (string adUnitId, MaxSdkBase.Reward reward, MaxSdkBase.AdInfo adInfo) => onRewardedAdReceivedReward();

            // Первая загрузка рекламы
            LoadRewardedAd(); 
        } */
    }
}
#endif