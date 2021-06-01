import 'package:flutter/material.dart';
import 'package:my_test/flutter_login_signup/welcomePage.dart';
class SplashScreen extends StatefulWidget {
  @override
  _SplashScreenState createState() => _SplashScreenState();
}

class _SplashScreenState extends State<SplashScreen>
    with SingleTickerProviderStateMixin {
  AnimationController _animationController;
  Animation<Offset> _animation;

  void initState() {
    Future.delayed(Duration(seconds: 3), () {
      Navigator.push(
          context, MaterialPageRoute(builder: (context) => WelcomePage()));
    });

    _animationController = AnimationController(
      duration: Duration(seconds: 3),
      vsync: this,
    );
    _animation = Tween<Offset>(
      end: Offset(0.0, 1.0),
      begin: Offset(0.0, 0.0),
    ).animate(CurvedAnimation(
        parent: _animationController,
        curve: Curves.slowMiddle,
        reverseCurve: Curves.easeInOutBack));
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.green,
      body: Stack(
        children: [
          SlideTransition(
            position:
                _animation, //Burda pozisyonumuzu yukardaki _animasyondan alırız.
            child: SizedBox.expand(
              child: Image.asset("images/resim1.png"),
            ),
          ),
          Positioned(
            right: 25,
            bottom: 25,
            child: GestureDetector(
              child: Icon(
                Icons.add,
                color: Colors.white,
              ),
              onTap: () {
                _animationController.forward();
              },
            ),
          ),
          Positioned(
            left: 25,
            bottom: 25,
            child: GestureDetector(
              child: Icon(
                Icons.add,
                color: Colors.white,
              ),
              onTap: () {
                _animationController.reverse();
              },
            ),
          ),
        ],
      ),
    );
  }
}
