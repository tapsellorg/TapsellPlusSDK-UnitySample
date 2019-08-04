using UnityEngine;

namespace TapsellPlusSDK {
    public class TapsellPlusPlugin {
        public virtual void initialize (string key) { }

        public virtual void setDebugMode (int logLevel) { }

        public virtual void addFacebookTestDevice (string hash) { }

        public virtual void requestRewardedVideo (string zoneId) { }
        public virtual void requestInterstitial (string zoneId) { }
        public virtual void showAd (string zoneId) { }

        public virtual void showBannerAd (string zoneId, int bannerType, int horizontalGravity, int verticalGravity) { }
        public virtual void hideBanner () { }
        public virtual void displayBanner () { }

        public virtual void requestNativeBanner (string zoneId) { }
        public virtual void nativeBannerAdClicked (string zoneId, string adId) { }
    }
}