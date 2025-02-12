import { Tabs } from 'expo-router';
import Header from '../components/Header';

export default function TabLayout() {
    return (
        <>
            <Header title="My App" />
            <Tabs>
                <Tabs.Screen name="index" options={{ title: 'Home' }} />
                <Tabs.Screen name="about" options={{ title: 'About' }} />
            </Tabs>
        </>
    );
}

