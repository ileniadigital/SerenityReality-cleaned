import React from 'react';
import { View, Text, StyleSheet, Dimensions } from 'react-native';


// Get screen dimensions
const { width } = Dimensions.get('window');

export default function Card({ text }: { text: string }) {
    return (
        <View style={styles.card}>
            <Text style={styles.text}>{text}</Text>
        </View>
    );
}

const styles = StyleSheet.create({
    card: {
        width: width * 0.8, // 80% of screen width
        height: 150, // Fixed height for the card
        alignSelf: 'center',
        justifyContent: 'center',
        backgroundColor: '#d4f5bf',
        borderRadius: 10,
        padding: 20,
        // marginVertical: 10,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.2,
        shadowRadius: 4,
        elevation: 5,
    },
    text: {
        fontSize: 20,
        color: '#00000',
        textAlign: 'center',
        fontWeight: 'bold',
        flexWrap: 'wrap',
    },
});