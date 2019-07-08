using System.Collections;
using System.Collections.Generic;
using TapsellPlusSDK;
using UnityEngine;

public class InterstititalScene : MonoBehaviour {

//	private const string TapsellInterstitialKey =	"5cfaa942e8d17f0001ffb292";
	private const string AdmobInterstitialKey =	"5cfaa9b0e8d17f0001ffb293";
//	private const string FacebookInterstitialKey =	"5cfaa975aede570001d55539";
	
	private readonly string ZONE_ID = AdmobInterstitialKey;

	public void Request () {
		TapsellPlus.requestInterstitial (ZONE_ID,

			(string zoneId) => {
				Debug.Log ("on response" + zoneId);
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
			});
	}
}