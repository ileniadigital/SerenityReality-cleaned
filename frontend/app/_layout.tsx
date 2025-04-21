import { UserProvider } from '@/contexts/UserContext';
import { BackHandler, Alert } from 'react-native';
import { Stack } from 'expo-router';
import React, { useEffect } from 'react';
import { useRouter } from 'expo-router';

export default function RootLayout() {
  // useEffect(() => {
  //   const backAction = () => {
  //     Alert.alert('Hold on!', 'Are you sure you want to exit the app?', [
  //       {
  //         text: 'Cancel',
  //         onPress: () => null,
  //         style: 'cancel',
  //       },
  //       { text: 'YES', onPress: () => BackHandler.exitApp() },
  //     ]);
  //     return true; // Prevent default back button behavior
  //   };

  //   const backHandler = BackHandler.addEventListener(
  //     'hardwareBackPress',
  //     backAction
  //   );

  //   return () => backHandler.remove(); // Cleanup the event listener
  // }, []);

  const router = useRouter();

  const backAction = () => {
    Alert.alert('Hold on!', 'Are you sure you want to exit the app?', [
      {
        text: 'Cancel',
        onPress: () => null,
        style: 'cancel',
      },
      { text: 'YES', onPress: () => BackHandler.exitApp() },
    ]);
    return true;
  };

  useEffect(() => {
    const backHandler = BackHandler.addEventListener(
      'hardwareBackPress',
      backAction
    );

    return () => backHandler.remove();
  }, []);
  return (
    <UserProvider>
      <Stack>
        <Stack.Screen name="index" options={{ headerShown: false }} />
        <Stack.Screen name="about" options={{ headerShown: false }} />
        <Stack.Screen name="(tabs)" options={{ headerShown: false }} />
        <Stack.Screen name="account" options={{ headerShown: false }} />
        <Stack.Screen name="promptst" options={{ headerShown: false }} />
      </Stack>
    </UserProvider>
  );
}
