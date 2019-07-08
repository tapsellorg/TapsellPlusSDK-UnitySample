using System.Collections;
using System.Collections.Generic;
using TapsellPlusSDK;
using UnityEngine;

public class RewardedScene : MonoBehaviour {

//	private const string TapsellRewardedVideoKey =	"5cfaa802e8d17f0001ffb28e";
	private const string AdmobRewardedVideoKey =	"5cfaa8aee8d17f0001ffb28f";
//	private const string UnityAdsRewardedVideoKey =	"5cfaa8eae8d17f0001ffb291";
//	private const string ChartboostRewardedVideoKey =	"5cfaa8cee8d17f0001ffb290";
//	private const string FacebookRewardedVideoKey =	"5cfaa838aede570001d55538";
	
	private readonly string ZONE_ID = AdmobRewardedVideoKey;

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