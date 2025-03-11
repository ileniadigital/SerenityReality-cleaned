import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import FontAwesome from '@expo/vector-icons/FontAwesome';

export default function PlayButton() {
    return (
        <TouchableOpacity onPress={() => { /* Add your play button logic here */ }} style={styles.button}>
            <FontAwesome name="play-circle-o" size={55} color="#B1D699" />
        </TouchableOpacity>
    )
};

const styles = StyleSheet.create({
    button: {
        alignItems: "center",
        justifyContent: "center",
    },
    textContainer: {
        marginLeft: 10,
        alignItems: "center",
    },
});