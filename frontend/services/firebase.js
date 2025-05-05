/**
 * This module initializes and exports Firebase authentication and Firestore services
 * It uses the `@react-native-firebase/auth` 
 * and `@react-native-firebase/firestore` libraries to interact with Firebase.
 * 
 * The `firebaseConfig` object contains the Firebase configuration details: 
 * API key, project ID, and other identifiers, to connect the app to Firebase.
 * 
 * If the Firebase app is not already initialized, the `initializeApp` method is called
 * with the provided configuration to set up the Firebase services.
 * 
 * Exports:
 * - `auth`: Firebase Authentication service for managing user authentication.
 * - `firestore`: Firebase Firestore service for interacting with the NoSQL database.
 */

import auth from '@react-native-firebase/auth';
import firestore from '@react-native-firebase/firestore';

// Android config (from your google-services.json)
const firebaseConfig = {
  apiKey: "AIzaSyB_sqh1SlmBhKEKoCeadBO07pHfpRK3zw8",
  authDomain: "serenityreality-af160.firebaseapp.com",
  projectId: "serenityreality-af160",
  storageBucket: "serenityreality-af160.firebasestorage.app",
  messagingSenderId: "412124305994",
  appId: "1:412124305994:android:5a50cf3907013fd33fa4b3",
};

if (!auth().app) {
  auth().initializeApp(firebaseConfig);
}

export { auth, firestore };