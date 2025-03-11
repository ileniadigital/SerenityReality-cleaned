import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import FontAwesome from '@expo/vector-icons/FontAwesome';

import PlayButton from './PlayButton';

export default function Scene({ title, description }: { title: string, description: string }) {
    return (
        <View style={styles.container}>
            <PlayButton />
            <View style={styles.textContainer}>
                <Text style={styles.title}>{title}</Text>
                <Text style={styles.description}>{description}</Text>
            </View>
        </View >
    )
};

const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
        backgroundColor: "#F5F5F5",
        justifyContent: "flex-start",
        alignItems: "flex-start",
        padding: 15,
        margin: 10,
        borderRadius: 10,
        // Shadow
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.5,
        shadowRadius: 5,
        // Elevation for Android
        elevation: 5,
    },
    button: {
        alignItems: "center",
        justifyContent: "center",
    },
    textContainer: {
        marginLeft: 10,
        alignItems: "center",
    },
    title: {
        fontSize: 20,
        fontWeight: "bold",
        color: "#000000",
    },
    description: {
        fontSize: 16,
        color: "#000000",
        textAlign: "center",
    }
});