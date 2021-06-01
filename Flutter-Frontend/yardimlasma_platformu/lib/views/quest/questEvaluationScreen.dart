import 'package:flutter/material.dart';
import 'package:carousel_slider/carousel_slider.dart';
import 'package:my_test/models/difficultyEnum.dart';
import 'package:my_test/models/quest.dart';
import 'package:my_test/models/solution.dart';
import 'package:my_test/myBottomNavigationBar.dart';
import 'package:my_test/services/missionService.dart';
import 'package:my_test/services/solutionService.dart';
import 'imageScreen.dart';
import 'infoList.dart';

class QuestEvaluationScreen extends StatefulWidget {
  static const String routeName = "/questEvaluation";

  @override
  _QuestEvaluationScreenState createState() => _QuestEvaluationScreenState();
}

class _QuestEvaluationScreenState extends InfoListState {
  SolutionService _solutionService = SolutionService();
  MissionService _missionService = MissionService();

  // final List<Solution> questList = List<Solution>.generate(
  //   10,
  //   (index) => Solution(
  //     'Gorev 1',
  //     DateTime.utc(2021, 1, 1),
  //     "Mete",
  //     Difficulty.easy,
  //     "https://mir-s3-cdn-cf.behance.net/project_modules/1400/14331954471197.595cd9574ad45.jpg",
  //     id: index,
  //   ),
  // );

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
        // body: FutureBuilder<List<Solution>>(
        //     future: _solutionService.getSolutionList("1"),
        //     builder: (context, AsyncSnapshot<List<Solution>> snapshot) {
        //       if (snapshot.connectionState == ConnectionState.done) {
        //         if (snapshot.hasData) {
        //           var dataWithIndex = snapshot.data.asMap();
        //           return CarouselSlider(
        //               options: CarouselOptions(
        //                 height: MediaQuery.of(context).size.height,
        //                 autoPlay: false,
        //               ),
        //               items: snapshot.data
        //                   .map((item) => _buildMain(item))
        //                   .toList());
        //         } else {
        //           return SizedBox();
        //         }
        //       } else {
        //         return SizedBox();
        //       }
        //     })
        body: FutureBuilder<List<Quest>>(
            future: _missionService.getList(),
            builder: (context, AsyncSnapshot<List<Quest>> snapshot) {
              if (snapshot.connectionState == ConnectionState.done) {
                if (snapshot.hasData) {
                  var dataWithIndex = snapshot.data.asMap();
                  return CarouselSlider(
                      options: CarouselOptions(
                        height: MediaQuery.of(context).size.height,
                        autoPlay: false,
                      ),
                      items: snapshot.data
                          .map((item) => _buildMain(item))
                          .toList());
                } else {
                  return SizedBox();
                }
              } else {
                return SizedBox();
              }
            }));
  }

  @override
  Widget _buildMain(Quest quest) {
    DateTime d = quest.date;
    String date = "${d.day}/${d.month}/${d.year}\n"
        "${(d.hour < 10 ? "0" : "") + d.hour.toString()}:"
        "${(d.minute < 10 ? "0" : "") + d.minute.toString()}";

    GestureDetector imageSection = GestureDetector(
      child: Hero(
        child: Image.network(quest.imageLink),
        tag: "evaluationTag${quest.id}",
      ),
      onTap: () {
        Navigator.push(
          context,
          MaterialPageRoute(
            builder: (_) {
              return ImageScreen(
                  Image.network(quest.imageLink), "evaluationTag${quest.id}");
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
                      Expanded(
                        child: Text(
                          quest.title,
                          style: const TextStyle(
                              fontWeight: FontWeight.bold, fontSize: 20.0),
                        ),
                      ),
                      buildDifficultyText(quest.difficulty, fontSize: 20.0),
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
                        "Gonderen\n ${quest.questGiver}",
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
                    onPressed: () {
                      _acceptSolution(quest.id);
                    },
                    child: Icon(Icons.check),
                  ),
                ),
                SizedBox(
                  width: MediaQuery.of(context).size.width / 4,
                  child: ElevatedButton(
                    onPressed: () {
                      _declineSolution(quest.id);
                    },
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

  void _acceptSolution(int id) {}

  void _declineSolution(int id) {}
}
