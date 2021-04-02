import 'package:flutter/cupertino.dart';
import 'package:google_mobile_ads/google_mobile_ads.dart';

class MyBannerAd extends StatefulWidget {
  @override
  _MyBannerAdState createState() => _MyBannerAdState();
}

class _MyBannerAdState extends State<MyBannerAd> {
  BannerAd _myBanner;

  _MyBannerAdState();

  @override
  void initState() {
    super.initState();

    final AdListener _listener = AdListener(
      onAdLoaded: (Ad ad) => print('Ad loaded.'),
      onAdFailedToLoad: (Ad ad, LoadAdError error) {
        print('Ad failed to load: $error');
      },
      onAdOpened: (Ad ad) => print('Ad opened.'),
      onAdClosed: (Ad ad) => print('Ad closed.'),
      onApplicationExit: (Ad ad) => print('Left application.'),
    );

    final AdRequest _request = AdRequest();

    _myBanner = BannerAd(
        size: AdSize.banner,
        adUnitId: "ca-app-pub-3940256099942544/6300978111",
        listener: _listener,
        request: _request);

    _myBanner.load();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      alignment: Alignment.center,
      child: AdWidget(ad: _myBanner),
      width: _myBanner.size.width.toDouble(),
      height: _myBanner.size.height.toDouble(),
    );
  }
}
