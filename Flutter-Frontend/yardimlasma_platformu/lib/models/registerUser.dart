class RegisterUser {
  String userName;
  String email;
  String password;
  String filePath;

  RegisterUser({this.userName, this.email, this.password, this.filePath});

  RegisterUser.fromJson(Map<String, dynamic> json) {
    userName = json['userName'];
    email = json['email'];
    password = json['password'];
    filePath = json['filePath'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['userName'] = this.userName;
    data['email'] = this.email;
    data['password'] = this.password;
    data['filePath'] = this.filePath;
    return data;
  }
}
