import 'package:flutter/material.dart';
import 'package:material_design_icons_flutter/material_design_icons_flutter.dart';
import 'myBannerAd.dart';

class HomeScreen extends StatelessWidget {
  static const String routeName = "/";

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Container(
              padding: EdgeInsets.all(40),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: [
                  _buildButton(context, Icons.assignment, "Gorev Al", () {
                    Navigator.pushNamed(context, "/questsList");
                  }),
                  _buildButton(context, MdiIcons.exclamationThick, "Gorev Ver",
                      () {
                    Navigator.pushNamed(context, "/questsForm");
                  }),
                  _buildButton(context, Icons.location_on, "Gorevlerim", null),
                ],
              ),
            ),
            Container(
              padding: EdgeInsets.all(40),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: [
                  _buildButton(context, MdiIcons.accountCircle, "Profil", null),
                  _buildButton(context, Icons.message, "Mesajlar", null),
                  _buildButton(context, Icons.settings, "Ayarlar", null),
                ],
              ),
            ),
            MyBannerAd(),
          ],
        ),
      ),
    );
  }

  RawMaterialButton _buildButton(
      BuildContext context, IconData icon, String label, Function onPressed) {
    return RawMaterialButton(
      onPressed: onPressed,
      elevation: 2.0,
      fillColor: Theme.of(context).primaryColor,
      child: Column(
        children: [
          Icon(
            icon,
            size: 40,
          ),
          Text(
            label,
            textAlign: TextAlign.center,
          ),
        ],
      ),
      padding: EdgeInsets.all(MediaQuery.of(context).size.width / 20),
      shape: CircleBorder(),
    );
  }
}
