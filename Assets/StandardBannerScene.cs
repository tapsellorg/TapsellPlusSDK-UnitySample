using System.Collections;
using System.Collections.Generic;
using TapsellPlusSDK;
using UnityEngine;

public class StandardBannerScene : MonoBehaviour {
    private readonly string ZONE_ID = "5cfaaa30e8d17f0001ffb294";

    public void Show () {
        TapsellPlus.showBannerAd (ZONE_ID, BannerType.BANNER_320x50, Gravity.BOTTOM, Gravity.CENTER,

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

    public void display () {
        TapsellPlus.displayBanner ();
    }
}