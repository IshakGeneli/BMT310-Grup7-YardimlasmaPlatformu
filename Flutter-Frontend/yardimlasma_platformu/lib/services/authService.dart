import 'package:my_test/models/loginUser.dart';
import 'package:my_test/models/registerUser.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:my_test/shared/constants.dart';

class AuthService {
  final storage = new FlutterSecureStorage();
  final _base_path = Constants.base_path;

  void register(RegisterUser registerUser) async {
    var request =
        http.MultipartRequest('POST', Uri.parse("$_base_path/Auth/register"));

    request.fields['userName'] = registerUser.userName;
    request.fields['email'] = registerUser.email;
    request.fields['password'] = registerUser.password;

    request.files.add(
        await http.MultipartFile.fromPath('picture', registerUser.filePath));
    await request.send();
  }

  Future<bool> login(LoginUser loginUser) async {
    Map<String, String> headers = {'Content-Type': 'application/json'};
    var response = await http.post(Uri.parse("$_base_path/Auth/login"),
        headers: headers, body: jsonEncode(loginUser.toJson()));

    if (response.statusCode == 200) {
      await storage.write(key: 'jwt', value: response.body);
      return true;
    } else {
      return false;
    }
  }
}
