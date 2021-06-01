import 'package:flutter/material.dart';
import 'package:settings_ui/settings_ui.dart';

class SettingsScreen extends StatelessWidget {
  static String routeName = "/settings";

  @override
  Widget build(BuildContext context) {
    return Scaffold(appBar: AppBar(title: Text("Ayarlar")), body: Body());
  }
}

class Body extends StatelessWidget {
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
              title: 'Dil',
              subtitle: 'Türkçe',
              leading: Icon(Icons.language),
              onPressed: (BuildContext context) {})
        ]),
        SettingsSection(title: 'Hesabım', tiles: [
          SettingsTile(
              title: 'Telefon Numarası',
              leading: Icon(Icons.phone),
              onPressed: (BuildContext context) {}),
          SettingsTile(
              title: 'E-Posta',
              leading: Icon(Icons.email),
              onPressed: (BuildContext context) {})
        ]),
        SettingsSection(title: 'Güvenlik', tiles: [
          SettingsTile.switchTile(
              title: 'Use fingerprint',
              leading: Icon(Icons.fingerprint),
              switchValue: true,
              onToggle: (bool value) {})
        ]),
        SettingsSection(title: 'Çeşitli', tiles: [
          SettingsTile(
              title: 'Terms of Service',
              leading: Icon(Icons.notes),
              onPressed: (BuildContext context) {})
        ])
      ],
    );
  }
}
