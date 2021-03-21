import 'package:flutter/material.dart';
import 'package:material_design_icons_flutter/material_design_icons_flutter.dart';
import 'package:my_test/questScreen.dart';

class MyBottomNavigationBar extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BottomAppBar(
      child: IconTheme(
        data: IconThemeData(color: Theme.of(context).colorScheme.onPrimary),
        child: Row(mainAxisAlignment: MainAxisAlignment.spaceEvenly, children: [
          IconButton(
              onPressed: () {
                Navigator.popUntil(
                  context,
                  ModalRoute.withName(QuestScreen.routeName)
                );
              },
              icon: const Icon(Icons.home)),
          IconButton(onPressed: null, icon: const Icon(MdiIcons.trophy)),
          IconButton(onPressed: null, icon: const Icon(Icons.person)),
          IconButton(onPressed: null, icon: const Icon(Icons.settings)),
        ]),
      ),
    );
  }
}
