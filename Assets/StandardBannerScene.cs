using System.Collections;
using System.Collections.Generic;
using TapsellPlusSDK;
using UnityEngine;

public class StandardBannerScene : MonoBehaviour {

  private readonly string ZONE_ID = "5bb557316528ee00019a2ed9";

  public void Show () {
    TapsellPlus.showBannerAd (ZONE_ID, BannerType.BANNER_320x50, Gravity.BOTTOM, Gravity.CENTER);
  }
}