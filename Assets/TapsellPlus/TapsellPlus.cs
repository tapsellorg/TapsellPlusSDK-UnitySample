using System;
using System.Collections;
using System.Collections.Generic;
using AOT;
using UnityEngine;
using UnityEngine.Networking;

namespace TapsellPlusSDK {

	public struct Gravity {
		public const int TOP = 1;
		public const int BOTTOM = 2;
		public const int LEFT = 3;
		public const int RIGHT = 4;
		public const int CENTER = 5;
	}

	public struct BannerType {
		public const int BANNER_320x50 = 1;
		public const int BANNER_320x100 = 2;
		public const int BANNER_250x250 = 3;
		public const int BANNER_300x250 = 4;
		public const int BANNER_468x60 = 5;
		public const int BANNER_728x90 = 6;
	}

	[Serializable]
	public class TapsellError {
		public string zoneId;
		public string message;
		public TapsellError () { }

		public TapsellError (string zoneId, string message) {
			this.zoneId = zoneId;
			this.message = message;
		}
	}

	[Serializable]
	public class TapsellPlusNativeBannerAd {
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
			TapsellPlus.nativeBannerAdClicked (zoneId, adId);
		}
	}

	public class TapsellPlus {
		private static readonly Dictionary<string, Action<string>> requestResponsePool = new Dictionary<string, Action<string>> ();
		private static readonly Dictionary<string, Action<TapsellPlusNativeBannerAd>> nativeBannerResponsePool = new Dictionary<string, Action<TapsellPlusNativeBannerAd>> ();
		private static readonly Dictionary<string, Action<TapsellError>> requestErrorPool = new Dictionary<string, Action<TapsellError>> ();

		private static readonly Dictionary<string, Action<string>> openAdPool = new Dictionary<string, Action<string>> ();
		private static readonly Dictionary<string, Action<string>> closeAdPool = new Dictionary<string, Action<string>> ();
		private static readonly Dictionary<string, Action<string>> rewardPool = new Dictionary<string, Action<string>> ();
		private static readonly Dictionary<string, Action<TapsellError>> errorPool = new Dictionary<string, Action<TapsellError>> ();

		private static MonoBehaviour mMonoBehaviour = null;
		private static GameObject tapsellPlusManager = null;
		private static TapsellPlusPlugin plugin = null;

		public static void initialize (string key) {
			if (tapsellPlusManager == null) {
				tapsellPlusManager = new GameObject ("TapsellPlusManager");
				UnityEngine.Object.DontDestroyOnLoad (tapsellPlusManager);
				tapsellPlusManager.AddComponent<TapsellPlusMessageHandler> ();

				plugin = new TapsellPlusPlugin ();
				#if UNITY_ANDROID && !UNITY_EDITOR
				plugin = new TapsellPlusAndroidPlugin ();
				#endif
				
				#if UNITY_IOS && !UNITY_EDITOR
				plugin = new TapsellPlusIOSPlugin ();
				#endif

				plugin.initialize (key);
			}
		}

		internal static GameObject getTapsellPlusManager () {
			// Used in iOS Plugin
			return tapsellPlusManager;
		}

		private static void AddToPool<T> (IDictionary<string, Action<T>> pool, string zoneId, Action<T> item) {
			if (pool.ContainsKey (zoneId)) {
				pool.Remove (zoneId);
			}
			pool.Add (zoneId, item);
		}

		private static void CallIfAvailable<T> (IDictionary<string, Action<T>> pool, string zoneId, T input) {
			if (pool != null && pool.ContainsKey (zoneId)) {
				pool[zoneId] (input);
			}
		}

		public static void addFacebookTestDevice (string hash) {
			plugin.addFacebookTestDevice (hash);
		}

		public static void setDebugMode (int logLevel) {
			plugin.setDebugMode (logLevel);
		}

		public static void requestRewardedVideo (
			string zoneId, Action<string> onRequestResponse, Action<TapsellError> onRequestError) {
			AddToPool (requestResponsePool, zoneId, onRequestResponse);
			AddToPool (requestErrorPool, zoneId, onRequestError);

			plugin.requestRewardedVideo (zoneId);
		}

		public static void requestInterstitial (
			string zoneId, Action<string> onRequestResponse, Action<TapsellError> onRequestError) {
			AddToPool (requestResponsePool, zoneId, onRequestResponse);
			AddToPool (requestErrorPool, zoneId, onRequestError);

			plugin.requestInterstitial (zoneId);
		}

		public static void showAd (
			string zoneId, Action<string> onShowAd, Action<string> onCloseAd,
			Action<string> onReward, Action<TapsellError> onError) {

			AddToPool (openAdPool, zoneId, onShowAd);
			AddToPool (closeAdPool, zoneId, onCloseAd);
			AddToPool (rewardPool, zoneId, onReward);
			AddToPool (errorPool, zoneId, onError);

			plugin.showAd (zoneId);
		}

		public static void showBannerAd (
			string zoneId, int bannerType, int horizontalGravity, int verticalGravity,
			Action<string> onRequestResponse, Action<TapsellError> onRequestError) {

			AddToPool (requestResponsePool, zoneId, onRequestResponse);
			AddToPool (requestErrorPool, zoneId, onRequestError);

			plugin.showBannerAd (zoneId, bannerType, horizontalGravity, verticalGravity);
		}

		public static void hideBanner () {
			plugin.hideBanner ();
		}
		public static void displayBanner () {
			plugin.displayBanner ();
		}

		public static void requestNativeBanner (
			MonoBehaviour monoBehaviour, string zoneId, Action<TapsellPlusNativeBannerAd> onRequestResponse, Action<TapsellError> onRequestError) {
			AddToPool (nativeBannerResponsePool, zoneId, onRequestResponse);
			AddToPool (requestErrorPool, zoneId, onRequestError);
			mMonoBehaviour = monoBehaviour;

			plugin.requestNativeBanner (zoneId);
		}

		public static void nativeBannerAdClicked (string zoneId, string adId) {
			plugin.nativeBannerAdClicked (zoneId, adId);
		}

		internal static void onNativeRequestResponse (TapsellPlusNativeBannerAd result) {
			if (result != null) {
				if (mMonoBehaviour != null && mMonoBehaviour.isActiveAndEnabled) {
					mMonoBehaviour.StartCoroutine (loadNativeBannerAdImages (result));
				} else {
					Debug.Log ("Invalid MonoBehaviour Object");
					onRequestError (new TapsellError (result.zoneId, "Invalid MonoBehaviour Object"));
				}
				
			} else {
				Debug.Log ("Invalid Native Banner Ad Result");
				onRequestError (new TapsellError (result.zoneId, "Invalid Native Banner Ad Result"));
			}
		}

		internal static IEnumerator loadNativeBannerAdImages (TapsellPlusNativeBannerAd result) {
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

			CallIfAvailable (nativeBannerResponsePool, result.zoneId, result);
		}

		internal static void onRequestResponse (String zoneId) {
			CallIfAvailable (requestResponsePool, zoneId, zoneId);
		}

		internal static void onRequestError (TapsellError error) {
			CallIfAvailable (requestErrorPool, error.zoneId, error);
		}

		internal static void onOpenAd (String zoneId) {
			CallIfAvailable (openAdPool, zoneId, zoneId);
		}

		internal static void onCloseAd (String zoneId) {
			CallIfAvailable (closeAdPool, zoneId, zoneId);
		}

		internal static void onReward (String zoneId) {
			CallIfAvailable (rewardPool, zoneId, zoneId);
		}

		internal static void onError (TapsellError error) {
			CallIfAvailable (errorPool, error.zoneId, error);
		}
	}
}