import "package:flutter/material.dart";
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:my_test/models/difficultyEnum.dart';
import 'package:my_test/myBottomNavigationBar.dart';
import 'infoList.dart';
import 'questDetailScreen.dart';
import '../../models/quest.dart';

class QuestListScreen extends StatefulWidget {
  static const String routeName = "/questsList";

  @override
  _QuestListScreenState createState() => _QuestListScreenState();
}

class _QuestListScreenState extends InfoListState {
  final _quests = List<Quest>.generate(
    30,
    (index) => Quest(
      'Gorev ${index + 1}',
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ornare convallis orci, at consequat risus porttitor in. Etiam eu ex non mauris maximus elementum. Nulla sed eros mauris. Nam quis dapibus tellus. Vivamus vitae quam et magna condimentum pharetra id ut leo. Ut libero ante, vulputate non mauris non, venenatis vehicula mi. Suspendisse nec orci urna. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nam interdum dictum accumsan. Aliquam porttitor ultrices augue, non accumsan lorem egestas eu.',
      DateTime.utc(2021, 1, 1),
      "Mete",
      Difficulty.easy,
      id: index,
      location: LatLng(37.42796133580664, -122.085749655962),
      imageLink:
          "https://mir-s3-cdn-cf.behance.net/project_modules/1400/14331954471197.595cd9574ad45.jpg",
      hasFollowed: false,
    ),
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
                title: const Text("Quest Board"),
                background: FlutterLogo(),
              ),
            ),
            SliverList(
              delegate: SliverChildBuilderDelegate(
                (context, index) {
                  Quest q = _quests[index];
                  return _buildListTile(context, q, index);
                },
                childCount: _quests.length,
              ),
            ),
          ],
        ),
        bottomNavigationBar: MyBottomNavigationBar());
  }

  _buildListTile(BuildContext context, Quest q, int index) {
    return ListTile(
      tileColor:
          index % 2 == 1 ? Theme.of(context).backgroundColor : Colors.grey[800],
      minVerticalPadding: 10.0,
      title: Container(
        decoration: BoxDecoration(
            border: Border(
                bottom: BorderSide(color: Theme.of(context).primaryColor))),
        padding: EdgeInsets.fromLTRB(0, 10.0, 0, 25.0),
        child:
            Row(mainAxisAlignment: MainAxisAlignment.spaceBetween, children: [
          Text(
            q.title,
            style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 20.0),
          ),
          Row(children: [
            buildDifficultyText(q.difficulty),
            Icon(q.hasFollowed ? Icons.location_on : Icons.location_on_outlined)
          ]),
        ]),
      ),
      subtitle: Container(
        margin: EdgeInsets.fromLTRB(0, 3.0, 0, 0),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          children: [
            q.location != null
                ? buildLocationText(q.requestAdress)
                : Container(),
            Column(crossAxisAlignment: CrossAxisAlignment.end, children: [
              Text(
                "Gonderen: ${q.questGiver}",
                style: bottomTextStyle(isBold: true),
              ),
              Text(
                "${q.date.day}/${q.date.month}/${q.date.year}",
                style: bottomTextStyle(),
              ),
            ]),
          ],
        ),
      ),
      onTap: () {
        Navigator.push(
            context,
            MaterialPageRoute(
                settings: RouteSettings(
                    name: "${QuestListScreen.routeName}/${q.title}"),
                builder: (context) => QuestDetailScreen(q)));
      },
    );
  }
}
