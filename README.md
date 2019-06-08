

## <div dir="rtl">آموزش راه اندازی کتاب‌خانه TapsellPlus در Unity</div>

#### <div dir="rtl">برای استفاده از این کتابخانه باید از build system گردل استفاده کنید. همچنین سعی کنید از آخرین نسخه unity استفاده کنید.</div>

### <div dir="rtl">اضافه کردن کتابخانه به پروژه</div>

<div dir="rtl">نسخه 1.0</div>
<div dir="rtl">ابتدا <a href="https://github.com">unity package</a> تپسل پلاس را دانلود و مطابق توضیحات زیر به پروژه اضافه کنید. سپس هر adNetwork که تپسل پشتیبانی میکند و مایل هستید را مطابق توضیحات به پروژه اضافه کنید. در انتها با روش‌های تست مطمعن شوید که adNetwork مورد نظر به درستی کار میکند.<br /> <br /></div>


<div dir="rtl">از player settings قسمت publishing settings تیک custom gradle template رو بزارید.</div>

<div dir="rtl">خطوط زیر را در بخش android فایل mainTemplate.gradle در صورتی که وجود ندارد اضافه کنید.</div>

```gradle
android {
  compileOptions {
    sourceCompatibility JavaVersion.VERSION_1_8
    targetCompatibility JavaVersion.VERSION_1_8
  }
}
```

#### <div dir="rtl">برای اضافه کردن کتابخانه‌های مورد نیاز ۲ روش وجود دارد از هرکدام که مایل هستید استفاده کنید.</div>
#### <div dir="rtl">روش اول استفاده از gradle</div>

<div dir="rtl">خطوط زیر را در بخش dependencies فایل mainTemplate.gradle در مسیر Assets/Plugins/Android اضافه کنید. توجه داشته باشید که ۲ قسمت dependencies وجود دارد، این تغییرات باید در قسمت دوم انجام شود.</div>

```gradle
...
dependencies {
  implementation fileTree(dir: 'libs', include: ['*.jar'])
  implementation 'ir.tapsell.plus:tapsell-plus-sdk-unity:1.0.0'
**DEPS**}
...
```

<div dir="rtl">در نسخه‌های قدیمی یونیتی ممکن هست implementation شناخته نشود در این صورت از compile استفاده کنید.<br /><br /></div>

<div dir="rtl">هر یک از خطوط زیر که در بخش allprojects -> repositories فایل mainTemplate.gradle وجود ندارد اضافه کنید.</div>

```
...
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
...
```

<div dir="rtl">هنگام import یونیتی‌پکیج تپسل پلاس تیک قسمت playServicesResolver و TapsellPlusDependencies.xml را بردارید.</div>

#### <div dir="rtl">روش دوم استفاده از resolver</div>

<div dir="rtl">هنگام import کردن unityPackage تپسل پلاس تیک تمامی قسمت‌ها را بزارید.</div>
<div dir="rtl">در صورتی که تنظیمات ریزالور بر روی حالت auto-resolution میباشد، لایبراری های تپسل پلاس به صورت خودکار اضافه میشود. در غیر اینصورت به صورت دستی Resolve را انجام دهید.</div>
<div dir="rtl">فعال یا غیر فعال کردن auto-resolution از مسیر زیر انجام میشود.</div>

```
Assets > Play Services Resolver > Android Resolver > Settings
```

<div dir="rtl">برای Resolve دستی نیز از این مسیر اقدام نمایید.</div>

```
Assets > Play Services Resolver > Android Resolver > Resolve
Assets > Play Services Resolver > Android Resolver > Force Resolve
```

## <div dir="rtl">اضافه کردن سایر Ad Network ها</div>

#### <div dir="rtl">روش اول استفاده از gradle</div>

<div dir="rtl">در قسمت dependencies فایل mainTemplate.gradle این موارد را اضافه کنید. برای کسب اطلاعات بیشتر در مورد هر ad network میتوانید با همکاران ما در تیم رسانه صحبت کنید.</div>

```gradle
...
dependencies {
  implementation fileTree(dir: 'libs', include: ['*.jar'])
  ...
  //for adMob
  implementation 'com.google.android.gms:play-services-ads:17.2.1'
  implementation 'com.google.android.gms:play-services-basement:16.2.0'
  implementation 'com.google.android.gms:play-services-ads-identifier:16.0.0'
  implementation 'com.google.android.gms:play-services-location:16.0.0' 
  
  //for unityAds
  implementation 'com.unity3d.ads:unity-ads:3.0.0'

  //for chartboost
  implementation 'ir.tapsell.sdk:chartboost-sdk-android:7.3.1'

  //for facebook
  implementation 'com.facebook.android:audience-network-sdk:5.3.0'
**DEPS**}
...
```

#### <div dir="rtl">روش دوم استفاده از resolver</div>

<div dir="rtl">خطوط مربوط به هر adNetwork که مایل هستید را به فایل  TapsellPlusDependencies.xml اضافه کنید.</div>

```xml
<dependencies>
  <androidPackages>

    <repositories>
      <repository>https://dl.bintray.com/tapsellorg/maven</repository>
    </repositories>

    <androidPackage spec="ir.tapsell.plus:tapsell-plus-sdk-unity:1.0.0"/>
    ......

    <!--for admob-->
    <androidPackage spec="com.google.android.gms:play-services-ads:17.2.1"/>
    <androidPackage spec="com.google.android.gms:play-services-basement:16.2.0"/>
    <androidPackage spec="com.google.android.gms:play-services-ads-identifier:16.0.0"/>
    <androidPackage spec="com.google.android.gms:play-services-location:16.0.0"/>

    <!--for Chartboost-->
    <androidPackage spec="ir.tapsell.sdk:chartboost-sdk-android:7.3.1"/>

    <!--for unityads-->
    <androidPackage spec="com.unity3d.ads:unity-ads:3.0.0"/>

    <!--for facebook-->
    <androidPackage spec="com.facebook.android:audience-network-sdk:5.3.0"/>

    ......

  </androidPackages>
</dependencies>
```

## <div dir="rtl">تنظیمات proguard</div>

<div dir="rtl">تنظیمات پروگوارد را از  <a href="https://github.com/tapsellorg/TapsellPlusSDK-AndroidSample/blob/master/app/proguard-rules.pro">این فایل</a> دریافت کنید.</div>



## <div dir="rtl">راه اندازی تپسل پلاس</div>

<div dir="rtl">کلید تپسل را از <a href="https://dashboard.tapsell.ir/">پنل</a> دریافت کنید.</div>

<div dir="rtl">تابع زیر را در یکی از اسکریپت‌های ابتدایی برنامه بزارید.</div>


```cs
void Start () {
  TapsellPlus.initialize (TAPSELL_KEY);
}
```

## <div dir="rtl">پیاده سازی تبلیغات ویدیو جایزه‌ای</div>

<div dir="rtl">ابتدا از پنل یک تبلیغگاه (zone) ویدیو جایزه‌ای بسازید و zoneId رو زمان درخواست و نمایش تبلیغ استفاده کنید.</div>

<div dir="rtl">سپس مطابق کد زیر درخواست تبلیغ دهید.</div>

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

<div dir="rtl">بعد از اجرای متد response تبلیغ آماده نمایش است و میتوانید مطابق روش زیر نمایش دهید.</div>

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

## <div dir="rtl">پیاده سازی تبلیغات آنی</div>

<div dir="rtl">مطابق تبلیغات جایزه‌ای پیش برید فقط زمان درخواست تبلیغ از متد TapsellPlus.requestInterstitial استفاده کنید.</div>


## <div dir="rtl">آموزش تبلیغات بنر استاندارد</div>

<div dir="rtl">مطابق کد زیر میتونید بنر استاندارد نمایش دهید.</div>

```cs
TapsellPlus.showBannerAd (ZONE_ID, BANNER_TYPE, VERTICAL_GRAVITY, HORIZONTAL_GRAVITY,
  (string zoneId) => {
    Debug.Log ("on response " + zoneId);
  },
  (TapsellError error) => {
    Debug.Log ("Error " + error.message);
  });
```

<div dir="rtl">BANNER_TYPE سایز نمایش بنر هست و میتواند مقادیر زیر باشد.</div>

|     keyword    |   size  |
|:--------------:|:-------:|
|  BANNER_320x50 |  320x50 |
| BANNER_320x100 | 320x100 |
| BANNER_250x250 | 250x250 |
| BANNER_300x250 | 300x250 |
|  BANNER_468x60 |  468x60 |
|  BANNER_728x90 |  728x90 |


<div dir="rtl">VERTICAL_GRAVITY و HORIZONTAL_GRAVITY موقعیت قرار گیری بنر در صفحه هست و میتواند مقادیر زیر باشد.</div>

```
Gravity.TOP - Gravity.BOTTOM - Gravity.LEFT - Gravity.RIGHT - Gravity.CENTER
```

<div dir="rtl">به عنوان مثال میتوانید به این شکل درخواست تبلیغ دهید.</div>

```cs
TapsellPlus.showBannerAd (ZONE_ID, BannerType.BANNER_300x250, Gravity.BOTTOM, Gravity.CENTER,
  (string zoneId) => {
    Debug.Log ("on response " + zoneId);
  },
  (TapsellError error) => {
    Debug.Log ("Error " + error.message);
  });
```

<div dir="rtl">با این کد میتوانید تبلیغ بنر استاندارد را مخفی کنید.</div>

```cs
TapsellPlus.hideBanner ();
```

## <div dir="rtl">پیاده سازی تبلیغات همنما بنری</div>

<div dir="rtl">مطابق کد زیر درخواست تبلیغ دهید.</div>

```cs
public void Request () {
  TapsellPlus.requestNativeBanner (this, ZONE_ID,
    (TapsellNativeBannerAd result) => {
      Debug.Log ("on response");
      //showing ad
    },
    (TapsellError error) => {
      Debug.Log ("Error " + error.message);
    }
  );
}
```

<div dir="rtl">متغیر برگردانده شده در on response محتویات تبلیغ هست و برای نمایش تبلیغ باید مطابق جدول زیر ازش استفاده کنید.</div>

|           function          |     usage     |
|:---------------------------:|:-------------:|
|         getTitle  ()        |     عنوان     |
|      getDescription  ()     |    توضیحات    |
|         getIcon  ()         |      آیکن     |
| getLandscapeBannerImage  () |   تصویر افقی  |
|  getPortraitBannerImage  () |  تصویر عمودی  |
|     getCallToAction  (),    | متن دکمه کلیک |

<div dir="rtl">برای باز کردن تبلیغ زمان کلیک کاربر میتوانید از این متد استفاده کنید.</div>

```cs
nativeAd.clicked ();
```

<div dir="rtl">برای دیدن یک نمونه پیاده سازی شده میتوانید همین پروژه در گیت‌هاب را بررسی کنید.</div>



## <div dir="rtl">تست Ad Network ها</div>

<div dir="rtl">برای اطمینان از صحت عملکرد adNetwrok هایی که اضافه کردید از zoneId مربوط به هرکدام استفاده کنید. هر zoneId مربوط به یک adNetwork و یک نوع تبلیغ هست و تبلیغ حالت تست نمایش داده میشود.</div>

<div dir="rtl">* توجه داشته باشید در حالت تست باید از appId تست استفاده کنید.</div>
<div dir="rtl">* هنگام تست باید از ip خارج ایران (فیلتر شکن) استفاده کنید.</div>
<div dir="rtl">* برای عملکرد صحیح حالت تست باید یکبار برنامه باز و بسته شود. همچنین در دومین درخواست، تبلیغ  adNetwork مورد نظر نمایش داده میشود.</div>
<div dir="rtl">* برای تست facebook باید hash دستگاهی که بر روی آن تست انجام میشود طبق روش گفته شده به sdk داده شود.</div>
<div dir="rtl">* تست را در حالت build release هم انجام دهید.</div>

<div dir="rtl"><br /></div>

<div dir="rtl">از این appId برای تست استفاده کنید.</div>

```cs
TapsellPlus.initialize("alsoatsrtrotpqacegkehkaiieckldhrgsbspqtgqnbrrfccrtbdomgjtahflchkqtqosa");
```

<div dir="rtl">برای هر ادنتورک و هر تبلیغ از zoneId های زیر برای درخواست و نمایش تبلیغ استفاده کنید. در حال حاضر فقط adType/adNetwork های زیر قابل استفاده هستند.</div>

|        Ad Network      |              Ad Type              |ZoneId
|:------------:|:----------------------------:|:----------------------------:|
|     Tapsell     |     Rewarded Video    | 5cfaa802e8d17f0001ffb28e|
|     Tapsell    |    Interstitial    |5cfaa942e8d17f0001ffb292|
| Tapsell |  Native  |5cfaa9deaede570001d5553a|
|  Tapsell | Standard |5cfaaa30e8d17f0001ffb294|
|    Admob    |    Rewarded Video   |5cfaa8aee8d17f0001ffb28f|
|    Admob    |     Interstitial     |5cfaa9b0e8d17f0001ffb293|
|    Admob    |     Standard     |5cfaaa4ae8d17f0001ffb295|
|    Unity Ads    |     Rewarded Video     |5cfaa8eae8d17f0001ffb291|
|    Chartboost    |     Rewarded Video     |5cfaa8cee8d17f0001ffb290|
|    Facebook    |     Rewarded Video     |5cfaa838aede570001d55538|
|    Facebook    |     Interstitial     |5cfaa975aede570001d55539|


<div dir="rtl">زمانی که از facebook استفاده میکنید متنی مشابه زیر در logcat پرینت میشود.</div>

```
When testing your app with Facebook's ad units you must specify the device hashed ID to ensure the delivery of test ads, add the following code before loading an ad: AdSettings.addTestDevice("YOUR_DEVICE_HASH");
```


<div dir="rtl">برای دیدن تبلیغات تستی فیسبوک مقدار hash دستگاه خود را از طریق متد زیر به کتابخانه تپسل بدهید.</div>

```cs
TapsellPlus.addFacebookTestDevice("YOUR_DEVICE_HASH");
```

