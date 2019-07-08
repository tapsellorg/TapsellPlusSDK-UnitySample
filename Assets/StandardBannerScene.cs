using System.Collections;
using System.Collections.Generic;
using TapsellPlusSDK;
using UnityEngine;

public class StandardBannerScene : MonoBehaviour {

//    private const string TapsellStandardBannerKey =	"5cfaaa30e8d17f0001ffb294";
    private const string AdmobStandardBannerKey =	"5cfaaa4ae8d17f0001ffb295";
    private readonly string ZONE_ID = AdmobStandardBannerKey;

    public void Show () {
        TapsellPlus.showBannerAd (ZONE_ID, BannerType.BANNER_320x50, Gravity.CENTER, Gravity.BOTTOM,

            (string zoneId) => {
                Debug.Log ("on response " + zoneId);
            },
            (TapsellError error) => {
                Debug.Log ("Error " + error.message);
            });
    }

    public void hide () {
        TapsellPlus.hideBanner ();
    }
}