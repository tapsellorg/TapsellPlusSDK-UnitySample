using System;
using TapsellPlusSDK;
using UnityEngine;

namespace TapsellPlusSDK {
	public class TapsellPlusMessageHandler : MonoBehaviour {

		public void notifyRequestResponse (String zoneId) {
			Debug.Log ("notifyRequestResponse:" + zoneId);
			TapsellPlus.onRequestResponse (zoneId);
		}

		public void notifyNativeRequestResponse (String body) {
			TapsellPlusNativeBannerAd result = JsonUtility.FromJson<TapsellPlusNativeBannerAd> (body);
			Debug.Log ("notifyNativeRequestResponse:" + result.zoneId);
			TapsellPlus.onNativeRequestResponse (result);
		}

		public void notifyRequestError (String body) {
			TapsellError error = JsonUtility.FromJson<TapsellError> (body);
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

		public void notifyReward (String zoneId) {
			Debug.Log ("notifyReward:" + zoneId);
			TapsellPlus.onReward (zoneId);
		}

		public void notifyError (String body) {
			TapsellError error = JsonUtility.FromJson<TapsellError> (body);
			Debug.Log ("notifyError:" + error.zoneId);
			TapsellPlus.onError (error);
		}
	}
}