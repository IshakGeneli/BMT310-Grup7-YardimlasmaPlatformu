import 'package:flutter/material.dart';
import 'package:my_test/flutter_login_signup/loginPage.dart';
import 'package:my_test/homeScreen.dart';
import 'package:google_mobile_ads/google_mobile_ads.dart';
import 'package:my_test/views/profile/profile_screen.dart';
import 'views/quest/questEvaluationScreen.dart';
import 'views/quest/questFormScreen.dart';
import 'views/quest/questListScreen.dart';
import 'views/settings/settings_screen.dart';

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
        LoginPage.routeName: (context) => LoginPage(),
        HomeScreen.routeName: (context) => HomeScreen(),
        QuestListScreen.routeName: (context) => QuestListScreen(),
        QuestFormScreen.routeName: (context) => QuestFormScreen(),
        QuestEvaluationScreen.routeName: (context) => QuestEvaluationScreen(),
        ProfileScreen.routeName: (context) => ProfileScreen(),
        SettingsScreen.routeName: (context) => SettingsScreen()
      },
    
    );
  }
}
