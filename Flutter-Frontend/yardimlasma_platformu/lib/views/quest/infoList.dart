import 'package:flutter/material.dart';
import 'package:my_test/models/difficultyEnum.dart';

abstract class InfoListState extends State {
  @protected
  TextStyle bottomTextStyle({bool isBold = false}) {
    return TextStyle(
        color: Colors.white60,
        fontWeight: isBold ? FontWeight.bold : FontWeight.normal,
        fontSize: isBold ? 14 : 13);
  }

  @protected
  Text buildDifficultyText(Difficulty difficulty, {fontSize}) {
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
      style: TextStyle(
          color: color, fontWeight: FontWeight.bold, fontSize: fontSize),
    );
  }

  @protected
  FutureBuilder<String> buildLocationText(Function() requestAddress) {
    return FutureBuilder<String>(
      future: requestAddress(),
      builder: (context, snapshot) {
        String address = "Not Found";
        if (snapshot.hasData)
          address = snapshot.data;
        else if (snapshot.hasError) address = "A problem occured";

        return SizedBox(
            width: MediaQuery.of(context).size.width / 3,
            child: Text(
              address,
              softWrap: true,
            ));
      },
    );
  }
}
