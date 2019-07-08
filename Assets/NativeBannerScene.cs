using System;
using System.Collections;
using System.Linq;
using ArabicSupport;
using TapsellPlusSDK;
using UnityEngine;
using UnityEngine.UI;

public class NativeBannerScene : MonoBehaviour {
	
	private const string TapsellNativeKey =	"5cfaa9deaede570001d5553a";
//	private const string AdmobNativeKey =	"5d123c9968287d00019e1a94";
//	private const string AdmobNativeVideoKey =	"5d123d6f68287d00019e1a95";
	private readonly string ZONE_ID = TapsellNativeKey;
	public static TapsellPlusNativeBannerAd PlusNativeAd = null;

	public void Request () {
		TapsellPlus.requestNativeBanner (this, ZONE_ID,

			(TapsellPlusNativeBannerAd result) => {
				Debug.Log ("on response");
				PlusNativeAd = result;
			},
			(TapsellError error) => {
				Debug.Log ("Error " + error.message);
			}
		);
	}

	void OnGUI () {
		if (PlusNativeAd != null) {
			GUIStyle titleStyle = new GUIStyle ();
			titleStyle.alignment = TextAnchor.UpperRight;
			titleStyle.fontSize = 32;
			titleStyle.normal.textColor = Color.white;
			GUI.Label (new Rect (50, 500, 600, 50), ArabicFixer.Fix (PlusNativeAd.getTitle (), true), titleStyle);

			GUIStyle descriptionStyle = new GUIStyle ();
			descriptionStyle.richText = true;
			descriptionStyle.alignment = TextAnchor.MiddleRight;
			descriptionStyle.fontSize = 32;
			descriptionStyle.normal.textColor = Color.white;
			GUI.Label (new Rect (50, 550, 600, 50), ArabicFixer.Fix (PlusNativeAd.getDescription (), true), descriptionStyle);

			GUI.DrawTexture (new Rect (660, 500, 100, 100), PlusNativeAd.getIcon ());

			Rect callToActionRect;
			if (PlusNativeAd.getLandscapeBannerImage () != null) {
				GUI.DrawTexture (new Rect (50, 610, 710, 400), PlusNativeAd.getLandscapeBannerImage ());
				callToActionRect = new Rect (50, 1020, 710, 100);
			} else if (PlusNativeAd.getPortraitBannerImage () != null) {
				GUI.DrawTexture (new Rect (50, 300, 500, 280), PlusNativeAd.getPortraitBannerImage ());
				callToActionRect = new Rect (50, 580, 500, 50);
			} else {
				callToActionRect = new Rect (50, 300, 500, 50);
			}

			GUIStyle buttonStyle = new GUIStyle ("button");
			buttonStyle.fontSize = 32;
			if (GUI.Button (callToActionRect, ArabicFixer.Fix (PlusNativeAd.getCallToAction (), true), buttonStyle)) {
				PlusNativeAd.clicked ();
			}
		}
	}
}