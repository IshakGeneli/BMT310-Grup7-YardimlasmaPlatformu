import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:my_test/shared/constants.dart';

class UserService {
  final storage = new FlutterSecureStorage();
  final _base_path = Constants.base_path;

  Future<String> getUserPhotoById(String userId) async {
    var token = await storage.read(key: 'jwt');

    Map<String, String> headers = {'Authorization': 'Bearer ${token}'};
    var response = (await http.get(
        Uri.parse("$_base_path/Users/getUserById/${userId}"),
        headers: headers));

    dynamic responseObject = jsonDecode(response.body);

    return responseObject['userImages'][0]["image"]["url"];
  }
}
