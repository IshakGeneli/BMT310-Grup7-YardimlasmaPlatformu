import 'package:flutter/material.dart';
import 'package:my_test/homeScreen.dart';
import 'package:google_mobile_ads/google_mobile_ads.dart';

import 'questScreens/questEvaluationScreen.dart';
import 'questScreens/questFormScreen.dart';
import 'questScreens/questListScreen.dart';

void main() {
  WidgetsFlutterBinding.ensureInitialized();
  runApp(MyApp());
}

class MyApp extends StatefulWidget {
  @override
  MyAppState createState() => MyAppState();
}

class MyAppState extends State<MyApp> {
  static const String _title = "Flutter Layout Demo";

  @override
  void initState() {
    super.initState();

    //Load Ads
    MobileAds.instance.initialize().then((InitializationStatus status) {
      print('Initialization done: ${status.adapterStatuses}');
      MobileAds.instance
          .updateRequestConfiguration(
            RequestConfiguration(
                tagForChildDirectedTreatment:
                    TagForChildDirectedTreatment.unspecified),
          )
          .then((value) {});
    });
  }

  @override
  void dispose() {
    //_interstitialAd?.dispose();
    //_rewardedAd?.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: _title,
      theme: ThemeData(
        brightness: Brightness.dark,
        primarySwatch: Colors.green,
        primaryColor: Colors.lightGreen,
        accentColor: Colors.lightGreenAccent,
        elevatedButtonTheme: ElevatedButtonThemeData(
          style: ElevatedButton.styleFrom(
            onPrimary: Colors.black,
            primary: Colors.lightGreen,
          ),
        ),
      ),
      routes: {
        HomeScreen.routeName: (context) => HomeScreen(),
        QuestListScreen.routeName: (context) => QuestListScreen(),
        QuestFormScreen.routeName: (context) => QuestFormScreen(),
        QuestEvaluationScreen.routeName: (context) => QuestEvaluationScreen(),
      },
    );
  }
}
