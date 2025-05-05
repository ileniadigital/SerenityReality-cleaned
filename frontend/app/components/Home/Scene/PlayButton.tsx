// The play button that opens the modal to open the AR scene
import React from 'react';
import { View, StyleSheet, TouchableOpacity } from 'react-native';
import FontAwesome from '@expo/vector-icons/FontAwesome';

export default function PlayButton({ setModalVisible }: { setModalVisible: React.Dispatch<React.SetStateAction<boolean>> }) {
    return (
        <View>
            <TouchableOpacity onPress={() => setModalVisible(true)} style={styles.button}>
                <FontAwesome name="play-circle-o" size={55} color="#B1D699" />
            </TouchableOpacity>
        </View>
    );
}

const styles = StyleSheet.create({
    button: {
        alignItems: "center",
        justifyContent: "center",
    },
});