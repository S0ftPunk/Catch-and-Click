using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public YandexSDK sdk;
    // Start is called before the first frame update
    void Start()
    {
        sdk = YandexSDK.instance;
        sdk.onInterstitialShown += SDKNull;
        sdk.onInterstitialFailed += SDKNull;
        sdk.onInterstitialShowing += PausedForShow;
    }
    void SDKNull(string s)
    {

    }
    void SDKNull()
    {
        Pause(false);
    }
    public void PausedForShow()
    {
       Pause(true);
    }
    public void Pause(bool pause)
    {
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void AddShow()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        sdk.ShowInterstitial();
#endif
    }
    public void ScoresSet(int scores)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        sdk.SaveToLeaderBoard(scores);
#endif
    }
}
