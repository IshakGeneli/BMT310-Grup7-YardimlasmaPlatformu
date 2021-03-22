import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:geocoding/geocoding.dart';
import 'package:image_picker/image_picker.dart';
import 'package:my_test/myBottomNavigationBar.dart';
import 'quest.dart';
import 'dart:io';

class QuestFormScreen extends StatefulWidget {
  static const String routeName = "/questsForm";

  @override
  _QuestFormScreenState createState() => _QuestFormScreenState();
}

class _QuestFormScreenState extends State {
  static const TextStyle _titleTextStyle =
      const TextStyle(fontWeight: FontWeight.bold, fontSize: 20);
  static const TextStyle _inputTextStyle = const TextStyle(fontSize: 17);

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

    return Scaffold(
      appBar: AppBar(
        title: const Text("Gorev Ver"),
      ),
      bottomNavigationBar: MyBottomNavigationBar(),
      body: ListView(
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
                ),
              ],
            ),
          ),
          Container(
            margin: EdgeInsets.all(20),
            child:
                Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
              const Text(
                "Zorluk",
                style: _titleTextStyle,
              ),
              _createRadioButton("Kolay", Difficulty.easy),
              _createRadioButton("Normal", Difficulty.normal),
              _createRadioButton("Zor", Difficulty.hard),
            ]),
          ),
          Container(
            margin: EdgeInsets.all(20),
            child:
                Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
              Text(
                "*Resim",
                style: _titleTextStyle,
              ),
              _image != null ? Image.file(_image) : Container(),
            ]),
          ),
          Container(
            padding: EdgeInsets.all(20),
            child: ElevatedButton(
              onPressed: _getImage,
              child: Text(
                "Resim Yukle",
                style: _inputTextStyle,
              ),
            ),
          ),
          Container(
            margin: EdgeInsets.all(20),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  "*Adres",
                  style: _titleTextStyle,
                ),
                TextFormField(
                  controller: _addressController,
                  autocorrect: false,
                  style: _inputTextStyle,
                  inputFormatters: [
                    LengthLimitingTextInputFormatter(200),
                  ],
                  onChanged: (text) {
                    setState(() {
                      _showGoogleMap = _addressController.text.isNotEmpty;
                    });
                  },
                ),
              ],
            ),
          ),
          _showGoogleMap
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

                    return snapshot.hasData ? map : Container();
                  })
              : Container(),
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
    );
  }

  Future<LatLng> _getCoordinates() async {
    Location location = (await locationFromAddress(_addressController.text,
        localeIdentifier: "tr-TR"))[0];
    return LatLng(location.latitude, location.longitude);
  }

  RadioListTile _createRadioButton(String label, Difficulty difficulty) {
    return RadioListTile<Difficulty>(
        title: Text(label),
        value: difficulty,
        groupValue: _difficulty,
        onChanged: (Difficulty value) {
          setState(() {
            _difficulty = value;
          });
        });
  }

  _getImage() async {
    print("mete");

    final pickedFile = await _imagePicker.getImage(source: ImageSource.camera);
    setState(() {
      if (pickedFile != null) {
        _image = File(pickedFile.path);
      } else {
        print('No image selected.');
      }
    });
  }

  void _submit() {}
}
