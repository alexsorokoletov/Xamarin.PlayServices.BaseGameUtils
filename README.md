# Xamarin.PlayServices.BaseGameUtils (fork)

Fork of https://github.com/slown1/Xamarin.PlayServices.BaseGameUtils which is a port of https://github.com/playgameservices/android-basic-samples

The example ported as proof of concept is the application [Type A Number](https://github.com/playgameservices/android-basic-samples/tree/master/BasicSamples/TypeANumber).

The only thing you have to set is your own application and game, you can use [this walkthrough](https://developers.google.com/games/services/console/enabling). 

[License Apache Version 2.0](http://www.apache.org/licenses/LICENSE-2.0.html)


### Project structure
BaseGameUtils - initial port of Android helper/classes convinient for Google Play services integration in Android games.

CocosSharpBaseGameUtils - base activity class you want to use with your [CocosSharp](https://github.com/mono/CocosSharp) game to integrate Google Play Services. Right now it's linked to the CocosSharp 1.6.2 (as we use it).

TypeANumber - sample project.

All projects are using Xamarin.GooglePlayServices.* NuGet packages version 29.0.0.2.

