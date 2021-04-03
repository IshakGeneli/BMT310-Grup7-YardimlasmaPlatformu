import 'dart:ui';
import 'dart:io';

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:image_picker/image_picker.dart';
import 'dart:async';

import '../../models/quest.dart';
import '../../myBottomNavigationBar.dart';
import 'imageScreen.dart';

class QuestDetailScreen extends StatefulWidget {
  final Quest _quest;

  QuestDetailScreen(this._quest);

  @override
  State createState() => _QuestDetailScreenState(_quest);
}

class _QuestDetailScreenState extends State {
  final Quest _quest;
  File _image;
  final ImagePicker _imagePicker = ImagePicker();

  Completer<GoogleMapController> _controller = Completer();

  _QuestDetailScreenState(this._quest);

  @override
  Widget build(BuildContext context) {
    Container infoSection = Container(
      child: Column(children: [
        GestureDetector(
          child: Hero(
            child: Image.network(_quest.imageLink),
            tag: "questTag${_quest.title}",
          ),
          onTap: () {
            Navigator.push(
              context,
              MaterialPageRoute(
                builder: (_) {
                  return ImageScreen(
                      Image.network(_quest.imageLink), "questTag${_quest.id}");
                },
              ),
            );
          },
        ),
        Container(
          margin: EdgeInsets.all(20),
          child: Text(
            _quest.title,
            style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
          ),
        ),
        Container(
          padding: EdgeInsets.all(10),
          child: Text(
            _quest.description,
            style: TextStyle(fontSize: 17),
          ),
        ),
        Container(
          margin: EdgeInsets.fromLTRB(20, 20, 20, 0),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.end,
            children: [
              Column(crossAxisAlignment: CrossAxisAlignment.end, children: [
                Text(
                  "Gonderen: ${_quest.questGiver}",
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 17),
                ),
                Text(
                  "${_quest.date.day}/${_quest.date.month}/${_quest.date.year}",
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 17),
                ),
              ]),
            ],
          ),
        ),
      ]),
    );
    Container buttonSection = Container(
      margin: EdgeInsets.all(20),
      child: Expanded(
          child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: [
          _buildIconButton(Icons.search, "Kanit Yukle", () {
            _getImage();
          }),
          _buildIconButton(
              _quest.hasFollowed
                  ? Icons.location_on
                  : Icons.location_on_outlined,
              _quest.hasFollowed ? "Takip Ediliyor" : "Takip Et", () {
            setState(() {
              _follow();
            });
          }),
          _buildIconButton(Icons.message, "Iletisime Gec", null),
        ],
      )),
    );
    Container locationSection = Container(
      margin: EdgeInsets.fromLTRB(0, 20, 0, 0),
      child: _quest.location != null
          ? Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Container(
                  margin: EdgeInsets.fromLTRB(5, 0, 5, 20),
                  child: FutureBuilder<String>(
                      future: _quest.requestAdress(),
                      builder: (context, snapshot) {
                        if (snapshot.hasError)
                          return Text("Error: " + snapshot.error);

                        return snapshot.hasData
                            ? Text(
                                "Konum: ${snapshot.data}",
                                style: TextStyle(
                                    fontSize: 20, fontWeight: FontWeight.bold),
                              )
                            : Container();
                      }),
                ),
                SizedBox(
                  width: MediaQuery.of(context).size.width,
                  height: MediaQuery.of(context).size.height / 3,
                  child: GoogleMap(
                    mapType: MapType.hybrid,
                    initialCameraPosition: CameraPosition(
                      target: _quest.location,
                      zoom: 14.4746,
                    ),
                    onMapCreated: (GoogleMapController controller) {
                      _controller.complete(controller);
                    },
                    markers: Set.from([
                      Marker(markerId: MarkerId(""), position: _quest.location)
                    ]),
                  ),
                ),
              ],
            )
          : null,
    );

    return Scaffold(
        appBar: AppBar(
          title: Text(
            _quest.title,
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
        ),
        body: ListView(children: [
          infoSection,
          buttonSection,
          locationSection,
        ]),
        bottomNavigationBar: MyBottomNavigationBar());
  }

  void _follow() {
    _quest.hasFollowed = !_quest.hasFollowed;
  }

  Column _buildIconButton(IconData icon, String text, Function onPressed) {
    return Column(children: [
      IconButton(
        onPressed: onPressed,
        icon: Icon(icon),
        iconSize: 40,
      ),
      Text(
        text,
        style: TextStyle(
          fontWeight: FontWeight.bold,
          fontSize: 17,
        ),
        overflow: TextOverflow.visible,
        textAlign: TextAlign.center,
      ),
    ]);
  }

  _getImage() async {
    final pickedFile = await _imagePicker.getImage(source: ImageSource.camera);
    setState(() {
      if (pickedFile != null) {
        _image = File(pickedFile.path);
      } else {
        print('No image selected.');
      }
    });
  }
}
