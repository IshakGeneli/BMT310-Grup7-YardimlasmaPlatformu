import 'package:flutter/material.dart';

class ImageScreen extends StatelessWidget {
  final Image _image;
  final String _tag;

  ImageScreen(this._image, this._tag);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: GestureDetector(
        onTap: () {
          Navigator.pop(context);
        },
        child: Center(child: Hero(tag: _tag, child: _image)),
      ),
    );
  }
}
