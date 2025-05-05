// Draft for launching Unity AR app from React Native
// Just a test, the actual working one is in the AnxietyRatingPopup.tsx file
import React from 'react';
import { View, Button } from 'react-native';
import SendIntentAndroid from 'react-native-send-intent';

export default function LaunchUnityAR() {
    const openUnityApp = () => {
        (SendIntentAndroid as any).openApp('com.SerenityReality.SleepMeditation');
    };

    return (
        <View style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
            <Button title="Launch Sea Breathing AR" onPress={openUnityApp} />
        </View>
    );
}
