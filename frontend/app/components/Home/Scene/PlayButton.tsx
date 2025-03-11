import React, { useState } from 'react';
import { View, StyleSheet, TouchableOpacity } from 'react-native';
import FontAwesome from '@expo/vector-icons/FontAwesome';
import AnxietyRatingPopUp from './AnxietyRatingPopUp';

export default function PlayButton() {
    const [modalVisible, setModalVisible] = useState(false);
    return (
        <View>
            <TouchableOpacity onPress={() => playButtonPress(setModalVisible)} style={styles.button}>
                <FontAwesome name="play-circle-o" size={55} color="#B1D699" />
            </TouchableOpacity>

            <AnxietyRatingPopUp modalVisible={modalVisible} setModalVisible={setModalVisible} />
        </View>
    )
};

// Playbutton functionality
const playButtonPress = (setModalVisible: React.Dispatch<React.SetStateAction<boolean>>) => {
    // Open Pop up before scene
    setModalVisible(true);
    // Save state

    //Load scene
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