import 'dart:ui';

import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'dart:async';
import 'myBottomNavigationBar.dart';

import 'quest.dart';

class DetailScreen extends StatefulWidget {
  final Quest _quest;

  DetailScreen(this._quest);

  @override
  State createState() => DetailScreenState(_quest);
}

class DetailScreenState extends State {
  final Quest _quest;
  Completer<GoogleMapController> _controller = Completer();

  DetailScreenState(this._quest);

  @override
  Widget build(BuildContext context) {
    Container locationSection = _quest.location != null
        ? Container(
            margin: EdgeInsets.fromLTRB(0, 20, 0, 0),
            child: Column(
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
            ),
          )
        : Container();

    Container buttonSection = Container(
      margin: EdgeInsets.all(20),
      child: Expanded(
          child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: [
          _buildIconButton(Icons.search, "Kanit Yukle", () {
            Navigator.pushNamed(context, "/camera");
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

    Container infoSection = Container(
      child: Column(children: [
        _quest.getImage(),
        Container(
          margin: EdgeInsets.all(20),
          child: Text(
            _quest.title,
            style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
          ),
        ),
        Container(
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

  void _follow(){
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
}
