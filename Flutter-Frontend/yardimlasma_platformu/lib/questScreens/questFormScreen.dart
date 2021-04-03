import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:geocoding/geocoding.dart';
import 'package:image_picker/image_picker.dart';
import 'package:my_test/models/difficultyEnum.dart';
import 'package:my_test/myBottomNavigationBar.dart';
import 'package:smart_select/smart_select.dart';
import '../models/quest.dart';
import 'dart:io';

class QuestFormScreen extends StatefulWidget {
  static const String routeName = "/questsForm";

  @override
  _QuestFormScreenState createState() => _QuestFormScreenState();
}

class _QuestFormScreenState extends State {
  static const TextStyle _titleTextStyle =
      const TextStyle(fontWeight: FontWeight.bold, fontSize: 20);
  static const TextStyle _inputTextStyle = const TextStyle(
    fontSize: 17,
  );

  final TextEditingController _titleController = TextEditingController();
  final TextEditingController _descriptionController = TextEditingController();
  final TextEditingController _addressController = TextEditingController();
  GoogleMapController _mapController;

  final ImagePicker _imagePicker = ImagePicker();
  File _image;

  Difficulty _difficulty = Difficulty.easy;
  LatLng _coordinates = LatLng(39.9334, 32.8597);


  bool _showGoogleMap = false;

  @override
  Widget build(BuildContext context) {
    SizedBox map = SizedBox(
      width: MediaQuery.of(context).size.width,
      height: MediaQuery.of(context).size.height / 3,
      child: GoogleMap(
        onMapCreated: (controller) {
          _mapController = controller;
        },
        initialCameraPosition: CameraPosition(target: _coordinates, zoom: 50),
        markers:
            Set.from([Marker(markerId: MarkerId("0"), position: _coordinates)]),
      ),
    );

    List<S2Choice<Difficulty>> difficultyLevels = [
      S2Choice<Difficulty>(value: Difficulty.easy, title: "Kolay"),
      S2Choice<Difficulty>(value: Difficulty.normal, title: "Normal"),
      S2Choice<Difficulty>(value: Difficulty.hard, title: "Zor"),
    ];

    Container textSection = Container(
      child: Column(
        children: [
          Container(
            margin: EdgeInsets.all(20),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  "Gorev Adi",
                  style: _titleTextStyle,
                ),
                TextFormField(
                  controller: _titleController,
                  autocorrect: false,
                  style: _inputTextStyle,
                  inputFormatters: [LengthLimitingTextInputFormatter(40)],
                  decoration: InputDecoration(hintText: "Gorev Adi"),
                ),
              ],
            ),
          ),
          Container(
            margin: EdgeInsets.all(20),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  "Gorev Aciklamasi",
                  style: _titleTextStyle,
                ),
                TextFormField(
                  keyboardType: TextInputType.multiline,
                  maxLines: null,
                  controller: _descriptionController,
                  autocorrect: false,
                  style: _inputTextStyle,
                  inputFormatters: [LengthLimitingTextInputFormatter(400)],
                  decoration: InputDecoration(hintText: "Gorev Aciklamasi"),
                ),
              ],
            ),
          ),
        ],
      ),
    );
    Container difficultyPickerSection = Container(
      margin: EdgeInsets.all(10),
      child: SmartSelect<Difficulty>.single(
        title: "Zorluk Sec",
        value: _difficulty,
        choiceItems: difficultyLevels,
        onChange: (state) => setState(() => _difficulty = state.value),
        modalType: S2ModalType.bottomSheet,
        tileBuilder: (context, state) {
          return S2Tile.fromState(
            state,
            title: Text(
              state.title,
              style: _titleTextStyle,
            ),
            trailing: Icon(Icons.arrow_forward_ios),
          );
        },
      ),
    );
    Container imageSection = Container(
      margin: EdgeInsets.all(20),
      child: Column(
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Text(
                "*Resim",
                style: _titleTextStyle,
              ),
              Container(
                child: ElevatedButton(
                  onPressed: _getImage,
                  child: Text(
                    "Resim Yukle",
                    style: _inputTextStyle,
                  ),
                ),
              ),
            ],
          ),
          _image != null
              ? SizedBox(
                  width: MediaQuery.of(context).size.width,
                  child: Image.file(_image))
              : Container(),
        ],
      ),
    );
    Container addressSection = Container(
      child: Column(
        children: [
          Container(
            margin: EdgeInsets.all(20),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      const Text(
                        "*Adres",
                        style: _titleTextStyle,
                      ),
                      ElevatedButton(
                          onPressed: () {
                            setState(() {
                              _showGoogleMap =
                                  _addressController.text.isNotEmpty;
                            });
                          },
                          child: Text("Ara", style: _inputTextStyle,))
                    ]),
                TextFormField(
                  controller: _addressController,
                  autocorrect: false,
                  style: _inputTextStyle,
                  inputFormatters: [
                    LengthLimitingTextInputFormatter(200),
                  ],
                  decoration: InputDecoration(hintText: "Adres"),
                ),
              ],
            ),
          ),
          Container(
            child: _showGoogleMap
                ? FutureBuilder<LatLng>(
                    future: _getCoordinates(),
                    builder: (context, snapshot) {
                      if (snapshot.hasData) _coordinates = snapshot.data;

                      if (_coordinates != null && _mapController != null)
                        _mapController.moveCamera(
                          CameraUpdate.newCameraPosition(
                            CameraPosition(target: _coordinates, zoom: 50),
                          ),
                        );

                      return snapshot.hasData
                          ? map
                          : Text(
                              "Adres Bulunamadi",
                              style: TextStyle(color: Colors.red),
                            );
                    })
                : null,
          )
        ],
      ),
    );

    return Scaffold(
      appBar: AppBar(
        title: const Text("Gorev Ver"),
      ),
      bottomNavigationBar: MyBottomNavigationBar(),
      body: Form(
        child: ListView(
          children: [
            textSection,
            difficultyPickerSection,
            imageSection,
            addressSection,
            Container(
                margin: EdgeInsets.all(20),
                child: ElevatedButton(
                  child: Text(
                    "Olustur",
                    style: _inputTextStyle,
                  ),
                  onPressed: _submit,
                )),
          ],
        ),
      ),
    );
  }

  Future<LatLng> _getCoordinates() async {
    Location location = (await locationFromAddress(_addressController.text,
        localeIdentifier: "tr-TR"))[0];
    return LatLng(location.latitude, location.longitude);
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

  void _submit() {
    Quest quest = Quest(_titleController.text, _descriptionController.text, DateTime.now(), "userName", _difficulty);

  }
}
