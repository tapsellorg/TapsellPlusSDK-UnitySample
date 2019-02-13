using System;
using System.Collections;
using System.Linq;
using ArabicSupport;
using TapsellPlusSDK;
using UnityEngine;
using UnityEngine.UI;

public class NativeBannerScene : MonoBehaviour {

	private readonly string ZONE_ID = "5bb3bb6557b2f0000120ecc2";
	public static TapsellNativeBannerAd nativeAd = null;

	public void Request () {
		TapsellPlus.requestNativeBanner (this, ZONE_ID,

			(TapsellNativeBannerAd result) => {
				Debug.Log ("on response");
				NativeBannerScene.nativeAd = result;
			},
			(TapsellError error) => {
				Debug.Log ("Error " + error.zoneId);
			}
		);
	}

	void OnGUI () {
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (NativeBannerScene.nativeAd != null) {
			GUIStyle titleStyle = new GUIStyle ();
			titleStyle.alignment = TextAnchor.UpperRight;
			titleStyle.fontSize = 32;
			titleStyle.normal.textColor = Color.white;
			GUI.Label (new Rect (50, 500, 600, 50), ArabicFixer.Fix (NativeBannerScene.nativeAd.getTitle (), true), titleStyle);

			GUIStyle descriptionStyle = new GUIStyle ();
			descriptionStyle.richText = true;
			descriptionStyle.alignment = TextAnchor.MiddleRight;
			descriptionStyle.fontSize = 32;
			descriptionStyle.normal.textColor = Color.white;
			GUI.Label (new Rect (50, 550, 600, 50), ArabicFixer.Fix (NativeBannerScene.nativeAd.getDescription (), true), descriptionStyle);

			GUI.DrawTexture (new Rect (660, 500, 100, 100), NativeBannerScene.nativeAd.getIcon ());

			Rect callToActionRect;
			if (NativeBannerScene.nativeAd.getLandscapeBannerImage () != null) {
				GUI.DrawTexture (new Rect (50, 610, 710, 400), NativeBannerScene.nativeAd.getLandscapeBannerImage ());
				callToActionRect = new Rect (50, 1020, 710, 100);
			} else if (NativeBannerScene.nativeAd.getPortraitBannerImage () != null) {
				GUI.DrawTexture (new Rect (50, 300, 500, 280), NativeBannerScene.nativeAd.getPortraitBannerImage ());
				callToActionRect = new Rect (50, 580, 500, 50);
			} else {
				callToActionRect = new Rect (50, 300, 500, 50);
			}

			GUIStyle buttonStyle = new GUIStyle ("button");
			buttonStyle.fontSize = 32;
			if (GUI.Button (callToActionRect, ArabicFixer.Fix (NativeBannerScene.nativeAd.getCallToAction (), true), buttonStyle)) {
				NativeBannerScene.nativeAd.clicked ();
			}
		}
		#endif

	}
}