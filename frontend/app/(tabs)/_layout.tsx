import { Tabs } from 'expo-router';
import Header from '../components/Header';

export default function TabLayout() {
    return (
        <>
            <Header title="SerenityReality" />
            <Tabs>
                <Tabs.Screen name="index" options={{ headerShown: false }} />
                <Tabs.Screen name="about" options={{ headerShown: false }} />
                <Tabs.Screen name="account" options={{ headerShown: false }} />
            </Tabs>
        </>
    );
}

