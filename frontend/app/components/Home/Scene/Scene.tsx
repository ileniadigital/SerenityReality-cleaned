import React, { useState } from 'react';
import { View, Text, StyleSheet, TouchableOpacity, Dimensions } from 'react-native';
import PlayButton from './PlayButton';
import AnxietyRatingPopUp from './AnxietyRatingPopUp';

// Get screen dimensions for width
const { width } = Dimensions.get('window');

// Data for scenes
interface Scene {
    title: string,
    description: string,
    packageName: string
}

export default function Scene({ title, description, packageName }: { title: string, description: string, packageName: string }) {
    const [modalVisible, setModalVisible] = useState(false);
    // const [selectedRating, setSelectedRating] = useState<number | null>(null);

    return (
        <View style={styles.container}>
            <PlayButton setModalVisible={setModalVisible} />
            <TouchableOpacity style={styles.textContainer} onPress={() => setModalVisible(true)}>
                <Text style={styles.title}>{title}</Text>
                <Text style={styles.description}>{description}</Text>
            </TouchableOpacity>
            <AnxietyRatingPopUp modalVisible={modalVisible} setModalVisible={setModalVisible} packageName={packageName} />
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        width: width * 0.9, // 90% of screen width
        flexDirection: 'row',
        backgroundColor: "#F5F5F5",
        justifyContent: "center",
        alignItems: "center",
        padding: 15,
        margin: 10,
        borderRadius: 10,
        overflow: "hidden",
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.5,
        shadowRadius: 5,
        // Elevation for Android
        elevation: 5,
    },
    textContainer: {
        width: "80%",
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
        flexWrap: "wrap",
    }
});