import 'difficultyEnum.dart';

class Solution {
  int id;
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
     {this.id}
      );
}