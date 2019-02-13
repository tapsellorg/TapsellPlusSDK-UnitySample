using System.Collections;
using System.Collections.Generic;
using TapsellPlusSDK;
using UnityEngine;

public class InterstititalScene : MonoBehaviour {

	private readonly string ZONE_ID = "5bfc08d06ac0bb0001b7df7d";

	public void Request () {
		TapsellPlus.requestInterstitial (ZONE_ID,

			(string zoneId) => {
				Debug.Log ("on response" + zoneId);
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