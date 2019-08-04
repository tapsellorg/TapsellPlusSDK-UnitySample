using UnityEngine;

namespace TapsellPlusSDK {
	public class TapsellPlusAndroidPlugin : TapsellPlusPlugin {
		#if UNITY_ANDROID && !UNITY_EDITOR
		private static AndroidJavaClass tapsellPlus;

		private void setJavaObject () {
			tapsellPlus = new AndroidJavaClass ("ir.tapsell.plus.TapsellPlusUnity");
		}

		private void initializeSDK (string key) {
			tapsellPlus?.CallStatic ("initialize", key);
		}

		public override void initialize (string key) {
			setJavaObject ();
			initializeSDK (key);
		}

		public override void setDebugMode (int logLevel) {
			tapsellPlus?.CallStatic ("setDebugMode", logLevel);
		}

		public override void addFacebookTestDevice (string hash) {
			tapsellPlus?.CallStatic ("addFacebookTestDevice", hash);
		}

		public override void requestRewardedVideo (string zoneId) {
			tapsellPlus?.CallStatic ("requestRewardedVideo", zoneId);
		}

		public override void requestInterstitial (string zoneId) {
			tapsellPlus?.CallStatic ("requestInterstitial", zoneId);
		}

		public override void requestNativeBanner (string zoneId) {
			tapsellPlus?.CallStatic ("requestNativeBanner", zoneId);
		}

		public override void showAd (string zoneId) {
			tapsellPlus?.CallStatic ("showAd", zoneId);
		}

		public override void showBannerAd (string zoneId, int bannerType, int horizontalGravity, int verticalGravity) {
			tapsellPlus?.CallStatic ("showBannerAd", zoneId, bannerType, horizontalGravity, verticalGravity);
		}

		public override void hideBanner () {
			tapsellPlus?.CallStatic ("hideBanner");
		}
		
		public override void displayBanner () {
			tapsellPlus?.CallStatic ("displayBanner");
		}

		public override void nativeBannerAdClicked (string zoneId, string adId) {
			tapsellPlus?.CallStatic ("nativeBannerAdClicked", zoneId, adId);
		}
		#endif
	}
}