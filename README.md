
# SafeOrizer
A Xamarin.Forms app that encrypts media and documents in a SQLite db locally or in the cloud.

## Introduction

This app is the first Xamarin.Forms (XF) app that I want to bring in a state that is ready for public release. 

## Roadmap

### Target Platforms
- UWP
- iOS 
- Android

### Planned Features
- Add or create Images, Videos and Documents 
- AES Encryption and Decryption
- SQLite database
- Storage Options: Local device or shared with OneDrive
- Custom Renderers on all platforms for usable UIs


## Progress
- Started implementing [MediaPlugin](https://github.com/jamesmontemagno/MediaPlugin) for Camera and Gallery access
- Started implementing [SQLite](https://github.com/praeclarum/sqlite-net) as data backendxamarin, following [this guide](https://developer.xamarin.com/guides/xamarin-forms/working-with/databases/)
- Implemented [Vibration](https://github.com/jamesmontemagno/VibratePlugin) so that the device vibrates when you enter a wrong passcode
- Implemented [Permissions](https://github.com/jamesmontemagno/PermissionsPlugin)
- Started testing [PCLCrypto](https://github.com/AArnott/PCLCrypto). I want to use it for the AES encryption and decryption but first I need to learn how to implement it correctly

## Screenshots (nothing here is final yet)
<a href="url"><img src="http://fs5.directupload.net/images/170104/w9g6lr72.png" align="left" width="500" ></a>


