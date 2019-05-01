
Adding TapsellPlus to your Unity Project
----

#### Integrating TapsellPlus plugin, you must use Gradle build system for Unity.

### Import TapsellPlus SDK

Version 0.2.2.0

First of all, download [TapsellPlus Unity Package](https://storage.backtory.com/tapsell-sdk-private/plus-unity/tapsellplus-v0.2.2.0.unitypackage) and add this to your project.

In `Player Settings` of your project, select `Custom Gradle Template` from `Publishing Settings`.
Add the following lines to dependencies of `mainTemplate.gradle` file in `Assets/Plugins/Android` directory of your project.

```gradle
dependencies {
  implementation fileTree(dir: 'libs', include: ['*.jar'])

  implementation 'com.google.code.gson:gson:2.8.5'
  implementation 'com.squareup.retrofit2:retrofit:2.5.0'
  implementation 'com.squareup.retrofit2:converter-gson:2.5.0'
  implementation 'com.squareup.okhttp3:logging-interceptor:3.12.1'
  implementation 'ir.tapsell.sdk:tapsell-sdk-android:4.2.4'

  implementation 'com.unity3d.ads:unity-ads:3.0.0'
  implementation 'com.google.android.gms:play-services-ads:17.1.3'
  implementation 'com.google.android.gms:play-services-basement:16.2.0'
  implementation 'com.google.android.gms:play-services-ads-identifier:16.0.0'
  implementation 'com.google.android.gms:play-services-location:16.0.0'
}
```

Add the following lines to `allprojects` section of the `mainTemplate.gradle` file.


```
allprojects {
    repositories {
        google()
        jcenter()
        flatDir {
            dirs 'libs'
        }

        maven {
            url 'https://dl.bintray.com/tapsellorg/maven'
        }
    }
}
```

Add the following lines to `android` section of the `mainTemplate.gradle` file.


```gradle
android {
  compileOptions {
    sourceCompatibility JavaVersion.VERSION_1_8
    targetCompatibility JavaVersion.VERSION_1_8
  }
}
```

### Proguard Configuration

Get `proguard.properties` file from [this link](https://github.com/tapsellorg/TapsellPlusSDK-AndroidSample/blob/master/app/proguard-rules.pro) and add it to proguard properties of your app.

### Initialize TapsellPlus SDK

Get your app-key from [Tapsell Dashboard](https://dashboard.tapsell.ir/) and Initialize the SDK in a script when app starts.

```cs
void Start () {
  TapsellPlus.initialize (TAPSELL_KEY);
}
```

Where `TAPSELL_KEY` is the app-key copied from your tapsell dashboard.

### Implementing Rewarded Video Ads

First of all, you must create a new rewarded video ad-zone in your application dashboard and use the generated `zoneId` to show rewarded video ads.

Use the following code to request a new rewarde video ad using the TapsellPlus SDK:

```cs
public void Request () {
  TapsellPlus.requestRewardedVideo (ZONE_ID,
    (string zoneId) => {
      Debug.Log ("on response " + zoneId);
    },
    (TapsellError error) => {
      Debug.Log ("Error " + error.message);
    }
  );
}
```

When `response` action is called, the ad is ready to be shown. You can start showing the video using the `showAd` method and the `zoneId` from you dashboard:

```cs
public void Show () {
  TapsellPlus.showAd (ZONE_ID,
    (string zoneId) => {
      Debug.Log ("onOpenAd " + zoneId);
    },
    (string zoneId) => {
      Debug.Log ("onCloseAd " + zoneId);
    },
    (string zoneId) => {
      Debug.Log ("onReward " + zoneId);
    },
    (TapsellError error) => {
      Debug.Log ("onError " + error.message);
    }
  );
}
```

### Implementing Interstitial Ads

To implement interstitial ads in your application, follow the procedure describe in implementing rewarded ads but use `TapsellPlus.requestInterstitial` method instead of `requestRewardedVideo` method.
The `zoneId` used in this method must have interstitial type in your dashboard.


### Implementing Standard Banner Ads

To show a standard banner, use the `showBannerAd` method from `TapsellPlus` class as shown in the following code block.

```cs
TapsellPlus.showBannerAd (ZONE_ID, BANNER_TYPE, VERTICAL_GRAVITY, HORIZONTAL_GRAVITY,
  (string zoneId) => {
    Debug.Log ("on response " + zoneId);
  },
  (TapsellError error) => {
    Debug.Log ("Error " + error.message);
  });
```

`BANNER_TYPE` parameter indicates the size of banner and can have any of the values given in the following table.


|     keyword    |   size  |
|:--------------:|:-------:|
|  BANNER_320x50 |  320x50 |
| BANNER_320x100 | 320x100 |
| BANNER_250x250 | 250x250 |
| BANNER_300x250 | 300x250 |
|  BANNER_468x60 |  468x60 |
|  BANNER_728x90 |  728x90 |


`VERTICAL_GrAVITY` and `HORIZONTAL_GRAVITY` indicate the vertical and horizontal position of banner on the screen. You can use `Gravity.TOP`, `Gravity.BOTTOM` or `Gravity.CENTER` for vertical gravity and `Gravity.LEFT`, `Gravity.RIGHT` or `Gravity.CENTER` for horizontal gravity.

For example, if you want to show a banner at the bottom of the screen with 320x50 size, you can use the following code:

```cs
TapsellPlus.showBannerAd (ZONE_ID, BannerType.BANNER_320x50, Gravity.BOTTOM, Gravity.CENTER,
  (string zoneId) => {
    Debug.Log ("on response " + zoneId);
  },
  (TapsellError error) => {
    Debug.Log ("Error " + error.message);
  });
```

To hide the banner, use `hideBanner` function:

```cs
TapsellPlus.hideBanner ();
```

### Implementing Native Banner Ads

You need to create a native banner ad-zone in your dashboard to use the generated `zoneId` for showing native banner ads.

To request a native banner ad, use the following code sample:

```cs
public void Request () {
  TapsellPlus.requestNativeBanner (this, ZONE_ID,
    (TapsellNativeBannerAd result) => {
      Debug.Log ("on response");
      //show ad
    },
    (TapsellError error) => {
      Debug.Log ("Error " + error.message);
    }
  );
}
```

The result delivered to `onResponse` action includes the contents and creatives of the advertisement which can be used to show the ad to the user.
`TapsellNativeBannerAd` class has a few functions to get this content. These functions are introduced in the following table.

|           function          |      usage      |
|:---------------------------:|:---------------:|
|         getTitle  ()        |     title       |
|      getDescription  ()     |   description   |
|         getIcon  ()         |      icon       |
| getLandscapeBannerImage  () | landscape image |
|  getPortraitBannerImage  () |  portrait image |
|     getCallToAction  (),    | call to action  |

To open the advertisement when user clicks on the call to action button, use `clicked` function from `TapsellNativeBannerAd` instance.

```cs
nativeAd.clicked ();
```

A sample project is available on this github repository.
