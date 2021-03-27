import 'package:flutter/material.dart';
import 'package:carousel_slider/carousel_slider.dart';
import 'package:carousel_slider/carousel_controller.dart';
import 'package:geocoding/geocoding.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:my_test/myBottomNavigationBar.dart';
import 'package:my_test/quest/ImageScreen.dart';
import 'package:my_test/quest/infoList.dart';
import 'quest.dart';

class Solution {
  final String title;
  final DateTime date;
  final String sender;
  final Difficulty difficulty;
  final String imageLink;

  Solution(
    this.title,
    this.date,
    this.sender,
    this.difficulty,
    this.imageLink,
  );
}

class QuestEvaluationScreen extends StatefulWidget {
  static const String routeName = "/questEvaluation";

  @override
  _QuestEvaluationScreenState createState() => _QuestEvaluationScreenState();
}

class _QuestEvaluationScreenState extends InfoListState {
  final List<Solution> questList = List<Solution>.generate(
    10,
    (index) => Solution(
      'Gorev 1',
      DateTime.utc(2021, 1, 1),
      "Mete",
      Difficulty.easy,
      "https://mir-s3-cdn-cf.behance.net/project_modules/1400/14331954471197.595cd9574ad45.jpg",
    ),
  );

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(
          "Gorevlerim",
          textAlign: TextAlign.center,
        ),
      ),
      bottomNavigationBar: MyBottomNavigationBar(),
      body: Builder(
        builder: (context) {
          return CarouselSlider(
            options: CarouselOptions(
              height: MediaQuery.of(context).size.height,
              autoPlay: false,
            ),
            items: questList.map((item) => _buildMain(item)).toList(),
          );
        },
      ),
    );
  }

  @override
  Widget _buildMain(Solution solution) {
    DateTime d = solution.date;
    String date = "${d.day}/${d.month}/${d.year}\n"
        "${(d.hour < 10 ? "0" : "") + d.hour.toString()}:"
        "${(d.minute < 10 ? "0" : "") + d.minute.toString()}";

    GestureDetector imageSection = GestureDetector(
      child: Hero(
        child: Image.network(solution.imageLink),
        tag: "evaluationTag${solution.sender}",
      ),
      onTap: () {
        Navigator.push(
          context,
          MaterialPageRoute(
            builder: (_) {
              return ImageScreen(Image.network(solution.imageLink),
                  "evaluationTag${solution.sender}");
            },
          ),
        );
      },
    );

    return Card(
      child: Stack(
        children: [
          Align(
            alignment: Alignment.topCenter,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.start,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Container(
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                    children: [
                      Text(
                        solution.title,
                        style: const TextStyle(
                            fontWeight: FontWeight.bold, fontSize: 20.0),
                      ),
                      buildDifficultyText(solution.difficulty, fontSize: 20.0),
                    ],
                  ),
                  padding: EdgeInsets.all(20),
                ),
                imageSection,
                Container(
                  padding: EdgeInsets.symmetric(horizontal: 10, vertical: 5),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text(date),
                      Text(
                        "Gonderen\n ${solution.sender}",
                        textAlign: TextAlign.center,
                        style: TextStyle(
                            fontSize: 15, fontWeight: FontWeight.bold),
                      ),
                    ],
                  ),
                )
              ],
            ),
          ),
          Align(
            alignment: Alignment.bottomCenter,
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: [
                SizedBox(
                  width: MediaQuery.of(context).size.width / 4,
                  child: ElevatedButton(
                    onPressed: () {},
                    child: Icon(Icons.check),
                  ),
                ),
                SizedBox(
                  width: MediaQuery.of(context).size.width / 4,
                  child: ElevatedButton(
                    onPressed: () {},
                    child: Icon(Icons.close),
                    style: ButtonStyle(
                        backgroundColor: MaterialStateProperty.all(Colors.red)),
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
