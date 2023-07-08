## Space Arcade

C# Monogame Android simple game project

### Required to build

- [Dotnet SDK 6.0 or higher](https://dotnet.microsoft.com/en-us/download) 
- [OpenJDK 11](https://learn.microsoft.com/en-us/java/openjdk/download#openjdk-11)
- [Android SDK 31](https://developer.android.com/studio) and  build tools
android phone with usb cable to run app

### to build and run
- build
```bash
dotnet msbuild /p:AndroidSdkDirectory=<path_to_AndroidSDK> SpaceArcade.csproj /verbosity:normal /t:Rebuild /t:PackageForAndroid /t:SignAndroidPackage /p:Configuration=Release
```
- run
plug a phone to pc with usb and go to `bin\Release\net<dotnet-sdk-version>-android` 
```bash
adb install -r SpaceArcade.SpaceArcade-Signed.apk
```
- new app should appear on your phone
