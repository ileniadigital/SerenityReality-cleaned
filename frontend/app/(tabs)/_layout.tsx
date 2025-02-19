import { Tabs } from 'expo-router';
import Header from '../components/Header';
import { MaterialIcons } from '@expo/vector-icons';
import React from 'react';

const homeIcon = ({ color, size }: { color: string; size: number }) => <MaterialIcons name="home" color={color} size={size} />;
const aboutIcon = ({ color, size }: { color: string; size: number }) => <MaterialIcons name="info" color={color} size={size} />;
const accountIcon = ({ color, size }: { color: string; size: number }) => <MaterialIcons name="account-circle" color={color} size={size} />;

export default function TabLayout() {
    return (
        <>
            <Header title="SerenityReality" />
            <Tabs>
                <Tabs.Screen name="index" options={{
                    headerShown: false, tabBarLabel: 'Home', tabBarIcon: homeIcon
                }} />

                <Tabs.Screen name="about" options={{ headerShown: false, tabBarLabel: 'About', tabBarIcon: aboutIcon, }} />
                <Tabs.Screen name="account" options={{ headerShown: false, tabBarLabel: ' My Account', tabBarIcon: accountIcon, }} />
            </Tabs>
        </>
    );
}

