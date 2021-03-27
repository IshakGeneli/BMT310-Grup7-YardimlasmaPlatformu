import 'package:flutter/material.dart';
import 'package:material_design_icons_flutter/material_design_icons_flutter.dart';
import 'ads/myBannerAd.dart';

class HomeScreen extends StatelessWidget {
  static const String routeName = "/";

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Stack(
          children: [
            Column(
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
                      _buildButton(
                          context, MdiIcons.exclamationThick, "Gorev Ver", () {
                        Navigator.pushNamed(context, "/questsForm");
                      }),
                      _buildButton(context, Icons.location_on, "Gorevlerim",
                          () {
                        Navigator.pushNamed(context, "/questEvaluation");
                      }),
                    ],
                  ),
                ),
                Container(
                  padding: EdgeInsets.all(40),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                    children: [
                      _buildButton(
                          context, MdiIcons.accountCircle, "Profil", null),
                      _buildButton(context, Icons.message, "Mesajlar", null),
                      _buildButton(context, Icons.settings, "Ayarlar", null),
                    ],
                  ),
                ),
              ],
            ),
            Align(
                alignment: FractionalOffset.bottomCenter, child: MyBannerAd()),
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
          SizedBox(
            width: 70,
            child: Text(
              label,
              textAlign: TextAlign.center,
              overflow: TextOverflow.visible,
            ),
          ),
        ],
      ),
      padding: EdgeInsets.all(15),
      shape: CircleBorder(),
    );
  }
}
