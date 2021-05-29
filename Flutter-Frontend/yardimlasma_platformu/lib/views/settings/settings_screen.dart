import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:my_test/flutter_login_signup/loginPage.dart';
import 'package:settings_ui/settings_ui.dart';

class SettingsScreen extends StatelessWidget {
  static String routeName = "/settings";

  @override
  Widget build(BuildContext context) {
    return Scaffold(appBar: AppBar(title: Text("Settings")), body: Body());
  }
}

class Body extends StatelessWidget {
  final storage = new FlutterSecureStorage();

  Body({
    Key key,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return SettingsList(
      contentPadding: EdgeInsets.all(16.0),
      backgroundColor: Colors.black12,
      sections: [
        SettingsSection(title: 'Common', tiles: [
          SettingsTile(
              title: 'Language',
              subtitle: 'English',
              leading: Icon(Icons.language),
              onPressed: (BuildContext context) {})
        ]),
        SettingsSection(title: 'Account', tiles: [
          SettingsTile(
              title: 'Phone number',
              leading: Icon(Icons.phone),
              onPressed: (BuildContext context) {}),
          SettingsTile(
              title: 'Email',
              leading: Icon(Icons.email),
              onPressed: (BuildContext context) {}),
          SettingsTile(
              title: 'Sign out',
              leading: Icon(Icons.logout),
              onPressed: (BuildContext context) async {
                Navigator.push(context,
                    MaterialPageRoute(builder: (context) => LoginPage()));
                await storage.delete(key: "jwt");
              })
        ]),
        SettingsSection(title: 'Security', tiles: [
          SettingsTile.switchTile(
              title: 'Use fingerprint',
              leading: Icon(Icons.fingerprint),
              switchValue: true,
              onToggle: (bool value) {})
        ]),
        SettingsSection(title: 'Misc', tiles: [
          SettingsTile(
              title: 'Terms of Service',
              leading: Icon(Icons.notes),
              onPressed: (BuildContext context) {})
        ])
      ],
    );
  }
}
