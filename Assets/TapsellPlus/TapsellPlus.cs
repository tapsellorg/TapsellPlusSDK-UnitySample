using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
		public string message;
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

		private static Dictionary<string, Action<string>> requestResponsePool = new Dictionary<string, Action<string>> ();
		private static Dictionary<string, Action<TapsellNativeBannerAd>> nativeBannerResponsePool = new Dictionary<string, Action<TapsellNativeBannerAd>> ();
		private static Dictionary<string, Action<TapsellError>> requestErrorPool = new Dictionary<string, Action<TapsellError>> ();

		private static Dictionary<string, Action<string>> openAdPool = new Dictionary<string, Action<string>> ();
		private static Dictionary<string, Action<string>> closeAdPool = new Dictionary<string, Action<string>> ();
		private static Dictionary<string, Action<string>> rewardPool = new Dictionary<string, Action<string>> ();
		private static Dictionary<string, Action<TapsellError>> errorPool = new Dictionary<string, Action<TapsellError>> ();
		
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

		public static void addFacebookTestDevice (string hash) {			
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsellPlus.CallStatic ("addFacebookTestDevice", hash);
			#endif
		}

		private static void setJavaObject () {
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsellPlus = new AndroidJavaClass ("ir.tapsell.plus.TapsellPlusUnity");
			#endif
		}

		public static void setDebugMode (int logLevel) {
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsellPlus.CallStatic ("setDebugMode", logLevel);
			#endif
		}

		public static void requestRewardedVideo (
			string zoneId, Action<string> onRequestResponse, Action<TapsellError> onRequestError) {

			#if UNITY_ANDROID && !UNITY_EDITOR
			if (requestResponsePool.ContainsKey (zoneId)) {
				requestResponsePool.Remove (zoneId);
			}

			if (requestErrorPool.ContainsKey (zoneId)) {
				requestErrorPool.Remove (zoneId);
			}

			requestResponsePool.Add (zoneId, onRequestResponse);
			requestErrorPool.Add (zoneId, onRequestError);

			tapsellPlus.CallStatic ("requestRewardedVideo", zoneId);
			#endif
		}

		public static void requestInterstitial (
			string zoneId, Action<string> onRequestResponse, Action<TapsellError> onRequestError) {
				
			#if UNITY_ANDROID && !UNITY_EDITOR
			if (requestResponsePool.ContainsKey (zoneId)) {
				requestResponsePool.Remove (zoneId);
			}

			if (requestErrorPool.ContainsKey (zoneId)) {
				requestErrorPool.Remove (zoneId);
			}

			requestResponsePool.Add (zoneId, onRequestResponse);
			requestErrorPool.Add (zoneId, onRequestError);

			tapsellPlus.CallStatic ("requestInterstitial", zoneId);
			#endif
		}

		public static void requestNativeBanner (
			MonoBehaviour monoBehaviour, string zoneId, Action<TapsellNativeBannerAd> onRequestResponse, Action<TapsellError> onRequestError) {
				
			#if UNITY_ANDROID && !UNITY_EDITOR
			mMonoBehaviour = monoBehaviour;

			if (nativeBannerResponsePool.ContainsKey (zoneId)) {
				nativeBannerResponsePool.Remove (zoneId);
			}

			if (requestErrorPool.ContainsKey (zoneId)) {
				requestErrorPool.Remove (zoneId);
			}

			nativeBannerResponsePool.Add (zoneId, onRequestResponse);
			requestErrorPool.Add (zoneId, onRequestError);

			tapsellPlus.CallStatic ("requestNativeBanner", zoneId);
			#endif
		}

		public static void onRequestResponse (String zoneId) {
			if (requestResponsePool.ContainsKey (zoneId)) {
				requestResponsePool[zoneId] (zoneId);
			}
		}

		public static void onNativeRequestResponse (TapsellNativeBannerAd result) {
			#if UNITY_ANDROID && !UNITY_EDITOR
			string zoneId = result.zoneId;
			if (result != null) {
				if (mMonoBehaviour != null && mMonoBehaviour.isActiveAndEnabled) {
					mMonoBehaviour.StartCoroutine (loadNativeBannerAdImages (result));
				} else {
					if (requestErrorPool.ContainsKey (zoneId)) {
						TapsellError error = new TapsellError();
						error.zoneId = zoneId;
						error.message = "Invalid MonoBehaviour Object";
						requestErrorPool[zoneId] (error);
					}
				}
			} else {
				if (requestErrorPool.ContainsKey (zoneId)) {
					TapsellError error = new TapsellError();
					error.zoneId = zoneId;
					error.message = "Invalid Result";
					requestErrorPool [zoneId](error);
				}
			}
			#endif
		}

		public static void onRequestError (TapsellError error) {
			if (requestErrorPool.ContainsKey (error.zoneId)) {
				requestErrorPool[error.zoneId] (error);
			}
		}

		public static void showAd (
			string zoneId,
			Action<string> onShowAd,
			Action<string> onCloseAd,
			Action<string> onReward,
			Action<TapsellError> onError) {
				
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

			if (errorPool.ContainsKey (zoneId)) {
				errorPool.Remove (zoneId);
			}

			openAdPool.Add (zoneId, onShowAd);
			closeAdPool.Add (zoneId, onCloseAd);
			rewardPool.Add (zoneId, onReward);
			errorPool.Add (zoneId, onError);

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

		public static void onError (TapsellError error) {
			if (errorPool.ContainsKey (error.zoneId)) {
				errorPool[error.zoneId] (error);
			}
		}

		public static void showBannerAd (
			string zoneId, int bannerType, int horizontalGravity, int verticalGravity, 
				Action<string> onRequestResponse, Action<TapsellError> onRequestError) {

			#if UNITY_ANDROID && !UNITY_EDITOR
			if (requestResponsePool.ContainsKey (zoneId)) {
				requestResponsePool.Remove (zoneId);
			}

			if (requestErrorPool.ContainsKey (zoneId)) {
				requestErrorPool.Remove (zoneId);
			}

			requestResponsePool.Add (zoneId, onRequestResponse);
			requestErrorPool.Add (zoneId, onRequestError);

			tapsellPlus.CallStatic ("showBannerAd", zoneId, bannerType, horizontalGravity, verticalGravity);
			#endif
		}

		public static void hideBanner () {
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsellPlus.CallStatic ("hideBanner");
			#endif
		}

		public static void nativeBannerAdClicked (string zoneId, string adId) {
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsellPlus.CallStatic ("nativeBannerAdClicked", zoneId, adId);
			#endif
		}

		static IEnumerator loadNativeBannerAdImages (TapsellNativeBannerAd result) {
			if (result.iconUrl != null && !result.iconUrl.Equals ("")) {
				UnityWebRequest wwwIcon = UnityWebRequestTexture.GetTexture (result.iconUrl);
				yield return wwwIcon.SendWebRequest ();
				if (wwwIcon.isNetworkError || wwwIcon.isHttpError) {
					Debug.Log (wwwIcon.error);
				} else {
					result.iconImage = ((DownloadHandlerTexture) wwwIcon.downloadHandler).texture;
				}
			}

			if (result.portraitStaticImageUrl != null && !result.portraitStaticImageUrl.Equals ("")) {
				UnityWebRequest wwwPortrait = UnityWebRequestTexture.GetTexture (result.portraitStaticImageUrl);
				yield return wwwPortrait.SendWebRequest ();
				if (wwwPortrait.isNetworkError || wwwPortrait.isHttpError) {
					Debug.Log (wwwPortrait.error);
				} else {
					result.portraitBannerImage = ((DownloadHandlerTexture) wwwPortrait.downloadHandler).texture;
				}
			}

			if (result.landscapeStaticImageUrl != null && !result.landscapeStaticImageUrl.Equals ("")) {
				UnityWebRequest wwwLandscape = UnityWebRequestTexture.GetTexture (result.landscapeStaticImageUrl);
				yield return wwwLandscape.SendWebRequest ();
				if (wwwLandscape.isNetworkError || wwwLandscape.isHttpError) {
					Debug.Log (wwwLandscape.error);
				} else {
					result.landscapeBannerImage = ((DownloadHandlerTexture) wwwLandscape.downloadHandler).texture;
				}
			}

			if (nativeBannerResponsePool.ContainsKey (result.zoneId)) {
				nativeBannerResponsePool[result.zoneId] (result);
			}
		}
	}
}