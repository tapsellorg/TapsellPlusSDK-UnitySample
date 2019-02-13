

## <div dir="rtl">آموزش راه اندازی کتاب‌خانه TapsellPlus در Unity</div>

#### <div dir="rtl">برای استفاده از این کتابخانه باید از build system گردل استفاده کنید. همچنین سعی کنید از آخرین نسخه unity استفاده کنید.</div>

### <div dir="rtl">اضافه کردن کتابخانه به پروژه</div>

<div dir="rtl">ابتدا <a href="https://dashboard.tapsell.ir/">unity package</a> تپسل را دانلود کنید و به پروژه اضافه کنید.</div>


<div dir="rtl">از player settings قسمت publishing settings تیک custom gradle template رو بزارید.</div>
<div dir="rtl">خطوط زیر را در بخش dependencies فایل mainTemplate.gradle در مسیر Assets/Plugins/Android اضافه کنید.</div>

```gradle
dependencies {
    implementation fileTree(dir: 'libs', include: ['*.jar'])

    implementation 'com.google.code.gson:gson:2.8.5'
    implementation 'com.squareup.retrofit2:retrofit:2.5.0'
    implementation 'com.squareup.retrofit2:converter-gson:2.5.0'
    implementation 'com.squareup.okhttp3:logging-interceptor:3.12.1'
    implementation 'ir.tapsell.sdk:tapsell-sdk-android:4.1.4'

    implementation 'com.google.android.gms:play-services-ads:15.0.1'
    implementation 'com.google.android.gms:play-services-basement:15.0.1'
    implementation 'com.google.android.gms:play-services-ads-identifier:15.0.1'
    implementation 'com.google.android.gms:play-services-location:15.0.1'
    implementation 'com.unity3d.ads:unity-ads:3.0.0'
}
```

<div dir="rtl">تابع زیر را در یکی از اسکریپت‌های ابتدایی برنامه بزارید.</div>

```cs
void Start () {
  TapsellPlus.initialize (TAPSELL_KEY);
}
```

## <div dir="rtl">آموزش تبلیغات ویدیو جایزه‌ای</div>

<div dir="rtl">ابتدا از پنل یک تبلیغگاه (zone) ویدیو جایزه‌ای بسازید و zoneId رو زمان درخواست و نمایش تبلیغ استفاده کنید</div>

<div dir="rtl">مطابق کد زیر درخواست تبلیغ دهید</div>

```cs
public void Request () {
  TapsellPlus.requestRewardedVideo (ZONE_ID,
    (string zoneId) => {
      Debug.Log ("on response " + zoneId);
    },
    (TapsellError error) => {
      Debug.Log ("Error " + error.error);
    }
  );
}
```

<div dir="rtl">بعد از اجرای متد response تبلیغ آماده نمایش است و میتوانید مطابق روش زیر نمایش دهید</div>

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
    }
  );
}
```
