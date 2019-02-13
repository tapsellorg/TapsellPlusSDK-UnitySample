using System.Collections;
using System.Collections.Generic;
using TapsellPlusSDK;
using UnityEngine;

public class RewardedScene : MonoBehaviour {

	private readonly string ZONE_ID = "5b9e3885e82f6f000153de3c";

	public void Request () {
		TapsellPlus.requestRewardedVideo (ZONE_ID,

			(string zoneId) => {
				Debug.Log ("on response " + zoneId);
			},
			(TapsellError error) => {
				Debug.Log ("Error " + error.zoneId);
			}
		);
	}

	public void Show () {
		TapsellPlus.showAd (ZONE_ID,

			(string zoneId) => {
				Debug.Log ("onOpenAd " + zoneId);
			},
			(string zoneId) => {
				Debug.Log ("onCloseAd " + zoneId);
			},
			(string zoneId) => {
				Debug.Log ("onReward " + zoneId);
			});
	}
}