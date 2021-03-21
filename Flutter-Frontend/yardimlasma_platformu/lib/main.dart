import 'package:flutter/material.dart';
import 'questScreen.dart';

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
        QuestScreen.routeName: (context) => QuestScreen(),
      },
    );
  }
}
