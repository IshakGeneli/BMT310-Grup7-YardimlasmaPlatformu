import 'package:flutter/material.dart';
import 'myBottomNavigationBar.dart';

enum Difficulty {
  easy,
  normal,
  hard,
  hell,
}

class Quest {
  final String title;
  final String description;
  final DateTime date;
  final String questGiver;
  final Difficulty difficulty;
  final String location;

  Quest(this.title, this.description, this.date, this.questGiver,
      this.difficulty, this.location);
}

class DetailScreen extends StatelessWidget {
  final Quest quest;

  DetailScreen(this.quest);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(quest.title),
      ),
      body: Padding(
        padding: EdgeInsets.all(16.0),
        child: Text(quest.description),
      ),
      bottomNavigationBar: MyBottomNavigationBar(),
    );
  }
}
