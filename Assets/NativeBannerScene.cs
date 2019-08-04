using System;
using System.Collections;
using System.Linq;
using ArabicSupport;
using TapsellPlusSDK;
using UnityEngine;
using UnityEngine.UI;

public class NativeBannerScene : MonoBehaviour {
	private readonly string ZONE_ID = "5cfaa9deaede570001d5553a";
	public static TapsellPlusNativeBannerAd nativeAd = null;

	public void Request () {
		TapsellPlus.requestNativeBanner (this, ZONE_ID,

			(TapsellPlusNativeBannerAd result) => {
				Debug.Log ("on response");
				nativeAd = result;
			},

			(TapsellError error) => {
				Debug.Log ("Error " + error.message);
			}
		);
	}

	void OnGUI () {
		if (nativeAd != null) {
			GUIStyle titleStyle = new GUIStyle ();
			titleStyle.alignment = TextAnchor.UpperRight;
			titleStyle.fontSize = 32;
			titleStyle.normal.textColor = Color.white;
			GUI.Label (new Rect (50, 500, 600, 50), ArabicFixer.Fix (nativeAd.getTitle (), true), titleStyle);

			GUIStyle descriptionStyle = new GUIStyle ();
			descriptionStyle.richText = true;
			descriptionStyle.alignment = TextAnchor.MiddleRight;
			descriptionStyle.fontSize = 32;
			descriptionStyle.normal.textColor = Color.white;
			GUI.Label (new Rect (50, 550, 600, 50), ArabicFixer.Fix (nativeAd.getDescription (), true), descriptionStyle);

			GUI.DrawTexture (new Rect (660, 500, 100, 100), nativeAd.getIcon ());

			Rect callToActionRect;
			if (nativeAd.getLandscapeBannerImage () != null) {
				GUI.DrawTexture (new Rect (50, 610, 710, 400), nativeAd.getLandscapeBannerImage ());
				callToActionRect = new Rect (50, 1020, 710, 100);
			} else if (nativeAd.getPortraitBannerImage () != null) {
				GUI.DrawTexture (new Rect (50, 300, 500, 280), nativeAd.getPortraitBannerImage ());
				callToActionRect = new Rect (50, 580, 500, 50);
			} else {
				callToActionRect = new Rect (50, 300, 500, 50);
			}

			GUIStyle buttonStyle = new GUIStyle ("button");
			buttonStyle.fontSize = 32;
			if (GUI.Button (callToActionRect, ArabicFixer.Fix (nativeAd.getCallToAction (), true), buttonStyle)) {
				nativeAd.clicked ();
			}
		}
	}
}