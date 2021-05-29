import 'dart:convert';

import 'package:my_test/models/difficultyEnum.dart';
import 'package:my_test/models/solution.dart';
import 'package:http/http.dart' as http;
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class SolutionService {
  final storage = new FlutterSecureStorage();
  //questEvaluationScreen 50. Satir. Map yerine list ile alsin. Builder yerinede FutureBuilder.
  Future<List<Solution>> getSolutionList(String sender) async {
    var token = await storage.read(key: 'jwt');

    Map<String, String> headers = {'Authorization': 'Bearer ${token}'};
    var response = (await http.get(
      Uri.parse(
          "http://projectforschool-001-site1.btempurl.com/api/Evidences/getAllByMissionId/" +
              sender),
      headers: headers,
    ));

    List<dynamic> responseObject = jsonDecode(response.body);
    List<Solution> listOfSolution = [];

    responseObject.forEach((element) {
      Solution solution = Solution(
        element["title"],
        DateTime.parse(element["date"]),
        element["sender"],
        Difficulty.values[element["difficulty"]],
        element["imageLink"],
        id: element["id"],
      );
      listOfSolution.add(solution);
    });

    return listOfSolution;
  }

  //questDetialScreen 192 _getImage methodundan sonra olusturulacak
  void createSolution(Solution solution) {}

  //questEvaluationScreen 154
  void acceptSolution(int id) {}

  //questEvaluationScreen 158
  void declineSolution(int id) {}
}
