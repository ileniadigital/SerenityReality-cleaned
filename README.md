# SerenityReality

_SerenityReality_ is a wellbeing and anxiety self-management AR-based application, developed as part of my Final Year Project.
The application features:

- 3 AR scenes developed using Unity to support with anxiety self-management
- A main application used to access the scenes and other (incoming) features, developed in React Native and Firebase

# Drive link with downloadable files

The application is **Android Only**
Follow this link to download the files for _SerenityReality_ (instructions included): (https://drive.google.com/drive/folders/1cr-xBpXs-ncdICy6gDSFiLrf8jnGlCHo?usp=sharing)

# Running instructions (if modified and not installing)

If the app is not being installed but modified and it needs to be tested.

**Note**: this application was only tested on an Android device, so the iOS version was not tested. The process may vary.

## AR scenes

1. Install Unity, the 2022.3.551f versions from the Unity Archive, alongside with all the AR plug-ins.
2. Open the _ar_ folder in Unity and start the project.
3. Select the scene to test from the _Scenes_ folder
4. Select the _Build Settings_ folder and switch platform to either _Android_ or _iOS_
5. Ensure the package name is in the form _com.SerenityReality.SCENE_NAME_, changed in the build settings. e.g. for the _Sea Breathing_ scene, the package name should be _com.SerenityReality.SeaBreathing_.
6. Select _add open scenes_ and select the scene to install
7. Connect a device via USB.
8. Select _Build and Run_ and wait for the scene to be installed on the device to be tested.
9. Once you are finished testing, ensure to close the installed scene completely on your phone before re-building.

## Main application

In terminal:

1. Navigate to the _frontend_ folder.
2. _npm i_ to install all the packages necessary.
3. Install the **Expo Go** application on an Android device
4. run _npx expo start_ to start the server
5. Scan the generated QR code using the **Expo Go** app and wait for it to load
6. Press the option _a_ for Android, and then press _r_ if the **Expo Go** app returns a time-out error.
7. The application will load and update after every change. If it is not, then press _r_ in the terminal to reload.
8. To stop the server, press _Ctrl+C_ (Windows) and then type _yes_ to close the connection.

**Note**: the AR scenes must be installed on the target device to test the connection to the scenes.

# Previous repository for history and progress tracking

During development, the Git history got corrupted during a pushing operation, so I had to clean the history and upload the project to a new repository.
Link to the old repository: (https://github.com/ileniadigital/SerenityReality)

# Structure

## ar

This folder contains the Unity project used to develop the AR scenees.

### Assets

This folder contains the assets used to develop the AR scenes:

- _Assets_SleepMeditation_: the assets for the _Sleep Meditation_ scene.
- _Audio_ contains the ocean sounds used for the _Sea Breathing_ scene.
- _FlexibleColorPicker_ is the asset used for the _Visualisation_ scene, sourced from the [Unity Asset Store](https://assetstore.unity.com/packages/tools/gui/flexible-color-picker-150497?srsltid=AfmBOooOdklFTfNXVRE_iprGy1NbqoTimW5INuR30kbkvPXkeI4v4MWh)

* _Materials_ contains any materials created and used for the assets across the scenes.
* _Prefabs_ contains the prefabs created and used for the scenes.
* _Scenes_ contains the the files for the individual scenes created.
* _Scripts_ contains the scripts used for each scene, divided in the respective folders.
* _SimpleOcean_ contains the asset used for the water in the _Sea Breathing_ scene, sourced from the [Unity Asset Store](https://www.youtube.com/watch?v=mWg4CE6ybKE)
* _Splash_Screen_ contains the image displayed on the splash screen before loading the scene. It contains the disclaimer.
* _Stylized Oasis_ contains the assets used for the sand in the _Sea Breathing_ scene, sourced from the [Unity Asset Store](https://assetstore.unity.com/packages/2d/textures-materials/4k-tiled-ground-textures-part-1-269480)

The other folders are default folders generated for the project.

## apks

This folder contains the exported _.apk_ files and the set up for the export of the Unity scenes. The actual _.apk_ files are on the Drive folder linked below, as they are greater than 100MB and cannot be pushed on GitHub.

## frontend

### android & ios

These folders contain the build files for Android and iOS. The iOS has the basic set up but it has nto been tested as this application was developed and exported on a Windows laptop.

### apks folder

This folder is similar to the main _apks_ folder and contains the set up for the exported AR scenes.

### app

This folder contains the screens, the routing and the component of each screen of the application.

### assets & patches

These folders are created by default using Expo, and were not modified for this project.

### contexts

This folder contains the context to manage the user authentication state and share it across the application. It refreshes the user's authentication status throughout the application if there are any changes.

### data

This folder contains the non-sensitive data used in the application:

- _intefaces.ts_ stores the details about the data types for each structure.
- _prompts.ts_ stores the text used for the prompts in the **Prompts** screen.
- _scenes.ts_ stores the details about the AR scenes, thier descriptions and their package names to be called when selected.

## services

This folder includes any services used for the application, mainly the _Firebase_ services:

- _auth.ts_ handles the authentication functions used throughout the application.
- _firebase.js_ handles the configuration of the _Firebase_ app and services for this applicaiton.
- _useAuth.ts_ handles the changes from the user authentication and each of the account functions.
