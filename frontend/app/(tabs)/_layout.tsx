import { Tabs } from 'expo-router';
import Header from '../components/Header';
import { MaterialIcons } from '@expo/vector-icons';
import React from 'react';

// Icons
const ICON_SIZE = 30;
const homeIcon = ({ color }: { color: string }) => <MaterialIcons name="home" color={color} size={ICON_SIZE} />;
const aboutIcon = ({ color }: { color: string }) => <MaterialIcons name="info" color={color} size={ICON_SIZE} />;
const accountIcon = ({ color }: { color: string }) => <MaterialIcons name="account-circle" color={color} size={ICON_SIZE} />;

export default function TabLayout() {
    return (
        <>
            <Header title="SerenityReality" />
            <Tabs
                screenOptions={{
                    tabBarActiveTintColor: '#B1D699',
                    tabBarInactiveTintColor: '#000000',
                }}>
                <Tabs.Screen name="index" options={{
                    headerShown: false, tabBarLabel: 'Home', tabBarIcon: homeIcon
                }} />

                <Tabs.Screen name="about" options={{ headerShown: false, tabBarLabel: 'About', tabBarIcon: aboutIcon }} />
                <Tabs.Screen name="account" options={{ headerShown: false, tabBarLabel: ' My Account', tabBarIcon: accountIcon }} />
            </Tabs>
        </>
    );
}

