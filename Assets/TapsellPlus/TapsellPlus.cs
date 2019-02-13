using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TapsellPlusSDK {
	
	public class Gravity {
		public static int TOP = 1;
		public static int BOTTOM = 2;
		public static int LEFT = 3;
		public static int RIGHT = 4;
		public static int CENTER = 5;
	}

	public class BannerType {
		public static int BANNER_320x50 = 1;
		public static int BANNER_320x100 = 2;
		public static int BANNER_250x250 = 3;
		public static int BANNER_300x250 = 4;
		public static int BANNER_468x60 = 5;
		public static int BANNER_728x90 = 6;
	}
	
	[Serializable]
	public class TapsellError {
		public string error;
		public string zoneId;
	}

	[Serializable]
	public class TapsellNativeBannerAd {
		public string zoneId;
		public string adId;
		public string title;
		public string description;
		public string iconUrl;
		public string callToActionText;
		public string portraitStaticImageUrl;
		public string landscapeStaticImageUrl;

		public Texture2D portraitBannerImage;
		public Texture2D landscapeBannerImage;
		public Texture2D iconImage;

		public string getTitle () {
			return title;
		}

		public string getDescription () {
			return description;
		}

		public string getCallToAction () {
			return callToActionText;
		}

		public Texture2D getPortraitBannerImage () {
			return portraitBannerImage;
		}

		public Texture2D getLandscapeBannerImage () {
			return landscapeBannerImage;
		}

		public Texture2D getIcon () {
			return iconImage;
		}

		public void clicked () {
			TapsellPlus.nativeBannerAdClicked (this.zoneId, this.adId);
		}
	}

	public class TapsellPlus {
		#if UNITY_ANDROID && !UNITY_EDITOR
		private static AndroidJavaClass tapsellPlus;
		#endif

		private static Dictionary<string, Action<string>> responsePool = new Dictionary<string, Action<string>> ();
		private static Dictionary<string, Action<TapsellNativeBannerAd>> nativeBannerResponsePool = new Dictionary<string, Action<TapsellNativeBannerAd>> ();
		private static Dictionary<string, Action<TapsellError>> errorPool = new Dictionary<string, Action<TapsellError>> ();

		private static Dictionary<string, Action<string>> openAdPool = new Dictionary<string, Action<string>> ();
		private static Dictionary<string, Action<string>> closeAdPool = new Dictionary<string, Action<string>> ();
		private static Dictionary<string, Action<string>> rewardPool = new Dictionary<string, Action<string>> ();
		
		#if UNITY_ANDROID && !UNITY_EDITOR
		private static MonoBehaviour mMonoBehaviour;
		#endif

		private static GameObject tapsellPlusManager = null;

		public static void initialize (string key) {
			if (tapsellPlusManager == null) {
				tapsellPlusManager = new GameObject ("TapsellPlusManager");
				UnityEngine.Object.DontDestroyOnLoad (tapsellPlusManager);
				tapsellPlusManager.AddComponent<TapsellPlusMessageHandler> ();
			}
			
			#if UNITY_ANDROID && !UNITY_EDITOR
			setJavaObject ();
			tapsellPlus.CallStatic ("initialize", key);
			#endif
		}

		private static void setJavaObject () {
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsellPlus = new AndroidJavaClass ("ir.tapsell.plus.TapsellPlusUnity");
			#endif
		}

		public static void requestRewardedVideo (
			string zoneId, Action<string> onRequestResponse, Action<TapsellError> onRequestError) {

			#if UNITY_ANDROID && !UNITY_EDITOR
			if (responsePool.ContainsKey (zoneId)) {
				responsePool.Remove (zoneId);
			}

			if (errorPool.ContainsKey (zoneId)) {
				errorPool.Remove (zoneId);
			}

			responsePool.Add (zoneId, onRequestResponse);
			errorPool.Add (zoneId, onRequestError);

			tapsellPlus.CallStatic ("requestRewardedVideo", zoneId);
			#endif
		}

		public static void requestInterstitial (
			string zoneId, Action<string> onRequestResponse, Action<TapsellError> onRequestError) {
				
			#if UNITY_ANDROID && !UNITY_EDITOR
			if (responsePool.ContainsKey (zoneId)) {
				responsePool.Remove (zoneId);
			}

			if (errorPool.ContainsKey (zoneId)) {
				errorPool.Remove (zoneId);
			}

			responsePool.Add (zoneId, onRequestResponse);
			errorPool.Add (zoneId, onRequestError);

			tapsellPlus.CallStatic ("requestInterstitial", zoneId);
			#endif
		}

		public static void requestNativeBanner (
			MonoBehaviour monoBehaviour, string zoneId, Action<TapsellNativeBannerAd> onRequestResponse, Action<TapsellError> onRequestError) {
				
			#if UNITY_ANDROID && !UNITY_EDITOR
			mMonoBehaviour = monoBehaviour;

			if (responsePool.ContainsKey (zoneId)) {
				responsePool.Remove (zoneId);
			}

			if (errorPool.ContainsKey (zoneId)) {
				errorPool.Remove (zoneId);
			}

			nativeBannerResponsePool.Add (zoneId, onRequestResponse);
			errorPool.Add (zoneId, onRequestError);

			tapsellPlus.CallStatic ("requestNativeBanner", zoneId);
			#endif
		}

		public static void onRequestResponse (String zoneId) {
			if (responsePool.ContainsKey (zoneId)) {
				responsePool[zoneId] (zoneId);
			}
		}

		public static void onNativeRequestResponse (TapsellNativeBannerAd result) {
			#if UNITY_ANDROID && !UNITY_EDITOR
			string zone = result.zoneId;
			if (result != null) {
				if (mMonoBehaviour != null && mMonoBehaviour.isActiveAndEnabled) {
					mMonoBehaviour.StartCoroutine (loadNativeBannerAdImages (result));
				} else {
					// if (errorPool.ContainsKey (zoneId)) {
						// TapsellError error = new TapsellError();
						// error.zoneId = zone;
						// error.error = "Invalid MonoBehaviour Object";
						// requestNativeBannerErrorPool [zone](error);
						// errorPool[zoneId] (zoneId);
					// }
				}
			} else {
				if (errorPool.ContainsKey (zone)) {
					// TapsellError error = new TapsellError();
					// error.zoneId = zone;
					// error.error = "Invalid Result";
					// requestNativeBannerErrorPool [zone](error);
				}

				// errorPool[zoneId] (zoneId);
			}
			#endif
		}

		public static void onRequestError (TapsellError error) {
			if (errorPool.ContainsKey (error.zoneId)) {
				errorPool[error.zoneId] (error);
			}
		}

		public static void showAd (
			string zoneId,
			Action<string> onShowAd,
			Action<string> onCloseAd,
			Action<string> onReward) {
				
			#if UNITY_ANDROID && !UNITY_EDITOR
			if (openAdPool.ContainsKey (zoneId)) {
				openAdPool.Remove (zoneId);
			}

			if (closeAdPool.ContainsKey (zoneId)) {
				closeAdPool.Remove (zoneId);
			}

			if (rewardPool.ContainsKey (zoneId)) {
				rewardPool.Remove (zoneId);
			}

			openAdPool.Add (zoneId, onShowAd);
			closeAdPool.Add (zoneId, onCloseAd);
			rewardPool.Add (zoneId, onReward);

			tapsellPlus.CallStatic ("showAd", zoneId);
			#endif
		}

		public static void onOpenAd (String zoneId) {
			if (openAdPool.ContainsKey (zoneId)) {
				openAdPool[zoneId] (zoneId);
			}
		}

		public static void onCloseAd (String zoneId) {
			if (closeAdPool.ContainsKey (zoneId)) {
				closeAdPool[zoneId] (zoneId);
			}
		}

		public static void onReward (String zoneId) {
			if (rewardPool.ContainsKey (zoneId)) {
				rewardPool[zoneId] (zoneId);
			}
		}

		public static void showBannerAd (string zoneId, int bannerType, int horizontalGravity, int verticalGravity) {
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsellPlus.CallStatic ("showBannerAd", zoneId, bannerType, horizontalGravity, verticalGravity);
			#endif
		}

		public static void nativeBannerAdClicked (string zoneId, string adId) {
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsellPlus.CallStatic ("nativeBannerAdClicked", zoneId, adId);
			#endif
		}

		static IEnumerator loadNativeBannerAdImages (TapsellNativeBannerAd result) {
			if (result.iconUrl != null && !result.iconUrl.Equals ("")) {
				WWW wwwIcon = new WWW (result.iconUrl);
				yield return wwwIcon;
				if (wwwIcon.texture != null) {
					result.iconImage = wwwIcon.texture;
				}
			}

			if (result.portraitStaticImageUrl != null && !result.portraitStaticImageUrl.Equals ("")) {
				WWW wwwPortrait = new WWW (result.portraitStaticImageUrl);
				yield return wwwPortrait;
				if (wwwPortrait.texture != null) {
					result.portraitBannerImage = wwwPortrait.texture;
				}
			}
			if (result.landscapeStaticImageUrl != null && !result.landscapeStaticImageUrl.Equals ("")) {
				WWW wwwLandscape = new WWW (result.landscapeStaticImageUrl);
				yield return wwwLandscape;
				if (wwwLandscape.texture != null) {
					result.landscapeBannerImage = wwwLandscape.texture;
				}
			}

			if (nativeBannerResponsePool.ContainsKey (result.zoneId)) {
				nativeBannerResponsePool[result.zoneId] (result);
			}
		}
	}
}