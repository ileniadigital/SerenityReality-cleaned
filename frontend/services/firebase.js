// import { initializeApp } from '@react-native-firebase/app';
// import { getAuth } from 'firebase/auth';
// import { getFirestore } from 'firebase/firestore';
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