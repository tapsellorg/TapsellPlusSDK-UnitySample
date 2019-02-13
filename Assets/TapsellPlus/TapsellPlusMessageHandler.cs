using System;
using TapsellPlusSDK;
using UnityEngine;

public class TapsellPlusMessageHandler : MonoBehaviour {

	public void notifyRequestResponse (String zoneId) {
		Debug.Log ("notifyRequestResponse:" + zoneId);
		TapsellPlus.onRequestResponse (zoneId);
	}

	public void notifyNativeRequestResponse (String body) {
		TapsellNativeBannerAd result = new TapsellNativeBannerAd ();
		result = JsonUtility.FromJson<TapsellNativeBannerAd> (body);
		Debug.Log ("notifyNativeRequestResponse:" + result.zoneId);
		TapsellPlus.onNativeRequestResponse (result);
	}

	public void notifyRequestError (String body) {
		TapsellError error = new TapsellError ();
		error = JsonUtility.FromJson<TapsellError> (body);
		Debug.Log ("notifyRequestError:" + error.zoneId);
		TapsellPlus.onRequestError (error);
	}

	public void notifyAdOpened (String zoneId) {
		Debug.Log ("notifyAdOpened:" + zoneId);
		TapsellPlus.onOpenAd (zoneId);
	}

	public void notifyAdClosed (String zoneId) {
		Debug.Log ("notifyAdClosed:" + zoneId);
		TapsellPlus.onCloseAd (zoneId);
	}

	public void onReward (String zoneId) {
		Debug.Log ("notifyReward:" + zoneId);
		TapsellPlus.onReward (zoneId);
	}
}