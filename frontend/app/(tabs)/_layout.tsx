import { Tabs } from 'expo-router';
import Header from '../components/Header';
import { MaterialIcons } from '@expo/vector-icons';
import React from 'react';
import { UserProvider, useUser } from '@/contexts/UserContext';
import MaterialCommunityIcons from '@expo/vector-icons/MaterialCommunityIcons';

// Icons
const ICON_SIZE = 30;
const homeIcon = ({ color }: { color: string }) => <MaterialIcons name="home" color={color} size={ICON_SIZE} />;
const aboutIcon = ({ color }: { color: string }) => <MaterialIcons name="info" color={color} size={ICON_SIZE} />;
const accountIcon = ({ color }: { color: string }) => <MaterialIcons name="account-circle" color={color} size={ICON_SIZE} />;
const settingsIcon = ({ color }: { color: string }) => <MaterialIcons name="settings" color={color} size={ICON_SIZE} />;
const promptIcon = ({ color }: { color: string }) => <MaterialCommunityIcons name="card" color={color} size={ICON_SIZE} />;

export default function TabLayout() {
    const { user } = useUser();

    return (
        <>
            <UserProvider>
                <Header title="SerenityReality" name={user?.name || ''} />
                <Tabs
                    screenOptions={{
                        tabBarActiveTintColor: '#B1D699',
                        tabBarInactiveTintColor: '#000000',
                    }}>
                    <Tabs.Screen name="index" options={{ headerShown: false, tabBarLabel: 'Home', tabBarIcon: homeIcon }} />
                    <Tabs.Screen name="prompts" options={{ headerShown: false, tabBarLabel: 'Prompts', tabBarIcon: promptIcon }} />
                    <Tabs.Screen name="about" options={{ headerShown: false, tabBarLabel: 'About', tabBarIcon: aboutIcon }} />
                    <Tabs.Screen name="account" options={{ headerShown: false, tabBarLabel: ' My Account', tabBarIcon: accountIcon }} />
                    <Tabs.Screen name="settings" options={{ headerShown: false, tabBarLabel: 'Settings', tabBarIcon: settingsIcon }} />

                </Tabs>
            </UserProvider>
        </>
    );
}

