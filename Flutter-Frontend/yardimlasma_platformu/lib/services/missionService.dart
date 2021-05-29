import 'dart:convert';

import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:http/http.dart' as http;
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:my_test/models/difficultyEnum.dart';
import 'package:my_test/models/quest.dart';
import 'package:my_test/shared/constants.dart';

class MissionService {
  final storage = new FlutterSecureStorage();
  final _base_path = Constants.base_path;

  Future<List<Quest>> getList() async {
    var token = await storage.read(key: 'jwt');

    Map<String, String> headers = {'Authorization': 'Bearer ${token}'};
    var response = (await http.get(
        Uri.parse("$_base_path/Missions/getAllWithEvidences"),
        headers: headers));

    List<dynamic> responseObject = jsonDecode(response.body);
    List<Quest> listOfQuest = [];

    responseObject.forEach((element) {
      Quest quest = Quest(
          element["title"],
          element["content"],
          DateTime.parse(element["createdDate"]),
          element["user"]["userName"],
          Difficulty.values[element["difficulty"]],
          id: element["id"],
          location: LatLng(element["latitude"], element["longitude"]),
          imageLink: element["missionImages"][0]["image"]["url"]);

      listOfQuest.add(quest);
    });

    return listOfQuest;
  }

  void createMission(Quest quest) async {
    var request = http.MultipartRequest(
        'POST', Uri.parse("$_base_path/Missions/createMission"));

    request.fields['title'] = quest.title;
    request.fields['content'] = quest.description;
    request.fields['createdDate'] = quest.date.toString();
    // request.fields['difficulty'] = "1";
    request.fields['latitude'] = quest.location.latitude.toString();
    request.fields['longitude'] = quest.location.longitude.toString();
    request.files
        .add(await http.MultipartFile.fromPath('picture', quest.imageLink));

    await request.send();
  }
}
