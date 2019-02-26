using System.Collections;
using System.Collections.Generic;
using TapsellPlusSDK;
using UnityEngine;

public class StandardBannerScene : MonoBehaviour {

  private readonly string ZONE_ID = "5c7125a3c16a660001bd3c67";

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
}