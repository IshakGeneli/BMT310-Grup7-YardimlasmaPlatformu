import 'package:flutter/cupertino.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:geocoding/geocoding.dart';

enum Difficulty {
  easy,
  normal,
  hard,
}

class Quest {
  final String title;
  final String description;
  final DateTime date;
  final String questGiver;
  final Difficulty difficulty;
  final LatLng location;
  final String imageLink;
  bool hasFollowed;

  Quest(
      this.title, this.description, this.date, this.questGiver, this.difficulty,
      {this.location, this.imageLink, this.hasFollowed = false});

  Future<String> requestAdress() async {
    if (location != null) {
      try {
        List<Placemark> placemarks = await placemarkFromCoordinates(
          location.latitude,
          location.longitude,
          localeIdentifier: "tr-TR",
        );
        return "${placemarks[0].locality} / ${placemarks[0].country}";
      } catch (e) {
        print(e.toString());
      }
    }
    return null;
  }

  Widget getImage() {
    if (imageLink != null) return Image.network(imageLink);
    return Container();
  }
}
