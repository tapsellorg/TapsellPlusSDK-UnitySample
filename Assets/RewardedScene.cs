using System.Collections;
using System.Collections.Generic;
using TapsellPlusSDK;
using UnityEngine;

public class RewardedScene : MonoBehaviour {
	private readonly string ZONE_ID = "5cfaa802e8d17f0001ffb28e";

	public void Request () {
		TapsellPlus.requestRewardedVideo (ZONE_ID,

			(string zoneId) => {
				Debug.Log ("on response " + zoneId);
			},
			(TapsellError error) => {
				Debug.Log ("Error " + error.message);
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
			},
			(TapsellError error) => {
				Debug.Log ("onError " + error.message);
			}
		);
	}
}