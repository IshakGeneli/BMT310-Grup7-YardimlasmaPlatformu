import 'package:flutter/material.dart';
import 'package:material_design_icons_flutter/material_design_icons_flutter.dart';

class MyBottomNavigationBar extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BottomAppBar(
      child: IconTheme(
        data: IconThemeData(color: Theme.of(context).colorScheme.onPrimary),
        child: Row(mainAxisAlignment: MainAxisAlignment.spaceEvenly, children: [
          IconButton(
              onPressed: () {
                Navigator.popUntil(context, ModalRoute.withName('/'));
              },
              icon: const Icon(Icons.home)),
          IconButton(
              onPressed: () {
                Navigator.popAndPushNamed(context, '/questsList');
              },
              icon: const Icon(Icons.assignment)),
          IconButton(onPressed: null, icon: const Icon(MdiIcons.accountCircle)),
          IconButton(onPressed: null, icon: const Icon(Icons.settings)),
        ]),
      ),
    );
  }
}
