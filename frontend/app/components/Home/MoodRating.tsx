// Mood rating component for the home screen of the app
// Only for visual representation, no functionality
import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import MaterialIcons from '@expo/vector-icons/MaterialIcons';

export default function MoodRating() {
    return (
        <View style={styles.container}>
            {/* Great icon */}
            <View style={styles.iconContainer}>
                <MaterialIcons name="sentiment-very-satisfied" size={80} color="#08842D" />
                <Text style={[styles.iconText, { color: '#08842D' }]}>Great</Text>
            </View>
            {/* Okay icon */}
            <View style={styles.iconContainer}>
                <MaterialIcons name="sentiment-neutral" size={80} color="#ECB30A" />
                <Text style={[styles.iconText, { color: '#ECB30A' }]}>Okay</Text>
            </View>
            {/* Not great icon */}
            <View style={styles.iconContainer}>
                <MaterialIcons name="sentiment-very-dissatisfied" size={80} color="#9A0707" />
                <Text style={[styles.iconText, { color: '#9A0707' }]}>Not great</Text>
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
        justifyContent: 'space-around',
        alignItems: 'center',
        padding: 10,
    },
    iconContainer: {
        alignItems: 'center',
    },
    iconText: {
        marginTop: 5,
        fontSize: 16,
    },
});