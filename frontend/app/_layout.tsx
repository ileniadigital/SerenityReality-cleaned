// Main layout to handle the navigation and user context
import { UserProvider } from '@/contexts/UserContext';
import { BackHandler, Alert } from 'react-native';
import { Stack } from 'expo-router';
import React, { useEffect } from 'react';
import { useRouter } from 'expo-router';

export default function RootLayout() {

  const router = useRouter();

  // Function to handle back button press
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

  // Add event listener for back button press
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
        {/* Screens */}
        <Stack.Screen name="index" options={{ headerShown: false }} />
        <Stack.Screen name="about" options={{ headerShown: false }} />
        <Stack.Screen name="(tabs)" options={{ headerShown: false }} />
        <Stack.Screen name="account" options={{ headerShown: false }} />
        <Stack.Screen name="promptst" options={{ headerShown: false }} />
      </Stack>
    </UserProvider>
  );
}
