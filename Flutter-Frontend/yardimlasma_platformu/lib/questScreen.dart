import "package:flutter/material.dart";
import 'package:my_test/myBottomNavigationBar.dart';
import 'detailsScreen.dart';

class QuestScreen extends StatelessWidget {
  static const String routeName = "/";

  final _quests = List<Quest>.generate(
    30,
    (index) => Quest(
        'Gorev ${index + 1}',
        'A description of what needs to be done for Gorev ${index + 1}',
        DateTime.utc(2021, 1, 1),
        "Mete",
        Difficulty.easy,
        'Ankara/Kizilay'),
  );

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        body: CustomScrollView(
          slivers: [
            SliverAppBar(
              pinned: true,
              snap: false,
              floating: false,
              expandedHeight: 160.0,
              flexibleSpace: const FlexibleSpaceBar(
                title: Text("Quest Board"),
                background: FlutterLogo(),
              ),
            ),
            SliverList(
              delegate: SliverChildBuilderDelegate(
                (context, index) {
                  Quest q = _quests[index];

                  return ListTile(
                    tileColor: index % 2 == 1
                        ? Theme.of(context).backgroundColor
                        : Colors.grey[800],
                    minVerticalPadding: 10.0,
                    title: Container(
                      decoration: BoxDecoration(
                          border: Border(
                              bottom: BorderSide(
                                  color: Theme.of(context).primaryColor))),
                      padding: EdgeInsets.fromLTRB(0, 10.0, 0, 25.0),
                      child: Row(
                          mainAxisAlignment: MainAxisAlignment.spaceBetween,
                          children: [
                            Text(
                              q.title,
                              style: TextStyle(
                                  fontWeight: FontWeight.bold, fontSize: 20.0),
                            ),
                            _buildDifficultyText(q.difficulty),
                          ]),
                    ),
                    subtitle: Container(
                      padding: EdgeInsets.fromLTRB(0, 3.0, 0, 0),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.spaceAround,
                        children: [
                          Text(
                            "Konum: ${q.location}",
                            style: _bottomTextStyle(isBold: true),
                          ),
                          Column(
                              crossAxisAlignment: CrossAxisAlignment.end,
                              children: [
                                Text(
                                  "Gonderen: ${q.questGiver}",
                                  style: _bottomTextStyle(isBold: true),
                                ),
                                Text(
                                  "${q.date.day}/${q.date.month}/${q.date.year}",
                                  style: _bottomTextStyle(),
                                ),
                              ]),
                        ],
                      ),
                    ),
                    onTap: () {
                      Navigator.push(
                          context,
                          MaterialPageRoute(
                              settings:
                                  RouteSettings(name: "$routeName/${q.title}"),
                              builder: (context) => DetailScreen(q)));
                    },
                  );
                },
                childCount: _quests.length,
              ),
            ),
          ],
        ),
        bottomNavigationBar: MyBottomNavigationBar());
  }

  TextStyle _bottomTextStyle({bool isBold = false}) {
    return TextStyle(
        color: Colors.white60,
        fontWeight: isBold ? FontWeight.bold : FontWeight.normal,
        fontSize: isBold ? 14 : 13);
  }

  Text _buildDifficultyText(Difficulty difficulty) {
    Color color;
    String text;

    switch (difficulty) {
      case Difficulty.easy:
        color = Colors.lightGreen;
        text = "Kolay";
        break;
      case Difficulty.normal:
        color = Colors.lightBlue;
        text = "Normal";
        break;
      case Difficulty.hard:
        color = Colors.deepOrange[900];
        text = "Zor";
        break;
    }

    return Text(
      text,
      style: TextStyle(color: color, fontWeight: FontWeight.bold),
    );
  }
}
