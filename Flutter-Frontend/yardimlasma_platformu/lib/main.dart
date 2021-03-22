import 'package:flutter/material.dart';
import 'package:my_test/homeScreen.dart';
import 'package:my_test/questFormScreen.dart';
import 'package:my_test/takePictureScreen.dart';
import 'questListScreen.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  static const String _title = "Flutter Layout Demo";

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: _title,
      theme: ThemeData(
        brightness: Brightness.dark,
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
        TakePictureScreen.routeName: (context) => TakePictureScreen(),
      },
    );
  }
}
