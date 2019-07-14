using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace TapsellPlusSDK {
	public class TapsellPlusIOSPlugin : TapsellPlusPlugin {
		#if UNITY_IOS && !UNITY_EDITOR

		private static TapsellPlusMessageHandler messageHandler;

		[DllImport ("__Internal")]
		private static extern void _tsp_initialize (string apiKey);
		[DllImport ("__Internal")]
		private static extern void _tsp_setDebugMode (int logLevel);
		[DllImport ("__Internal")]
		private static extern void _tsp_requestInterstitialAd (string zoneId);
		[DllImport ("__Internal")]
		private static extern void _tsp_requestRewardedVideoAd (string zoneId);
		[DllImport ("__Internal")]
		private static extern void _tsp_showAd (string zoneId);
		[DllImport ("__Internal")]
		private static extern void _tsp_requestBannerAd (string zoneId, int bannerType, int horizontalGravity, int verticalGravity);
		[DllImport ("__Internal")]
		private static extern void _tsp_requestNativeBannerAd (string zoneId);
		[DllImport ("__Internal")]
		private static extern void _tsp_nativeBannerAdClicked (string zoneId, string adId);

		[DllImport ("__Internal")]
		private static extern void _tsp_setRequestNotifier (ReceiveMessageFunction f);
		[DllImport ("__Internal")]
		private static extern void _tsp_setRequestErrorNotifier (ReceiveMessageFunction f);
		[DllImport ("__Internal")]
		private static extern void _tsp_setNativeRequestNotifier (ReceiveMessageFunction f);
		[DllImport ("__Internal")]
		private static extern void _tsp_setAdOpenedNotifier (ReceiveMessageFunction f);
		[DllImport ("__Internal")]
		private static extern void _tsp_setAdClosedNotifier (ReceiveMessageFunction f);
		[DllImport ("__Internal")]
		private static extern void _tsp_setErrorNotifier (ReceiveMessageFunction f);
		[DllImport ("__Internal")]
		private static extern void _tsp_setRewardNotifier (ReceiveMessageFunction f);

		private delegate void ReceiveMessageFunction (string message);

		[MonoPInvokeCallback (typeof (ReceiveMessageFunction))]
		private static void RequestNotifReceived (string message) {
			messageHandler.notifyRequestResponse (message);
		}

		[MonoPInvokeCallback (typeof (ReceiveMessageFunction))]
		private static void RequestErrorNotifReceived (string message) {
			messageHandler.notifyRequestError (message);
		}

		[MonoPInvokeCallback (typeof (ReceiveMessageFunction))]
		private static void NativeRequestNotifReceived (string message) {
			messageHandler.notifyNativeRequestResponse (message);
		}

		[MonoPInvokeCallback (typeof (ReceiveMessageFunction))]
		private static void AdOpenedNotifReceived (string message) {
			messageHandler.notifyAdOpened (message);
		}

		[MonoPInvokeCallback (typeof (ReceiveMessageFunction))]
		private static void AdClosedNotifReceived (string message) {
			messageHandler.notifyAdClosed (message);
		}

		[MonoPInvokeCallback (typeof (ReceiveMessageFunction))]
		private static void ErrorNotifReceived (string message) {
			messageHandler.notifyError (message);
		}

		[MonoPInvokeCallback (typeof (ReceiveMessageFunction))]
		private static void RewardNotifReceived (string message) {
			messageHandler.notifyReward (message);
		}

		private void setNotifier () {
			messageHandler = TapsellPlus.getTapsellPlusManager ().GetComponent<TapsellPlusMessageHandler> ();
			_tsp_setRequestNotifier (RequestNotifReceived);
			_tsp_setRequestErrorNotifier (RequestErrorNotifReceived);
			_tsp_setNativeRequestNotifier (NativeRequestNotifReceived);
			_tsp_setAdOpenedNotifier (AdOpenedNotifReceived);
			_tsp_setAdClosedNotifier (AdClosedNotifReceived);
			_tsp_setErrorNotifier (ErrorNotifReceived);
			_tsp_setRewardNotifier (RewardNotifReceived);
		}

		private void initializeSDK (string apiKey) {
			_tsp_initialize (apiKey);
		}

		public override void initialize (string key) {
			setNotifier ();
			initializeSDK (key);
		}

		public override void setDebugMode (int logLevel) {
			_tsp_setDebugMode (logLevel);
		}

		public override void requestInterstitial (string zoneId) {
			_tsp_requestInterstitialAd (zoneId);
		}

		public override void requestRewardedVideo (string zoneId) {
			_tsp_requestRewardedVideoAd (zoneId);
		}

		public override void showAd (string zoneId) {
			_tsp_showAd (zoneId);
		}

		public override void showBannerAd (string zoneId, int bannerType, int horizontalGravity, int verticalGravity) {
			_tsp_requestBannerAd (zoneId, bannerType, horizontalGravity, verticalGravity);
		}

		public override void requestNativeBanner (string zoneId) {
			_tsp_requestNativeBannerAd (zoneId);
		}

		public override void nativeBannerAdClicked (string zoneId, string adId) {
			_tsp_nativeBannerAdClicked (zoneId, adId);
		}
		#endif
	}
}