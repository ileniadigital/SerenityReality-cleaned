import React from 'react';
import { View, Text, StyleSheet, Modal, TouchableOpacity, Alert } from 'react-native';
import FontAwesome from '@expo/vector-icons/FontAwesome';
// import SendIntentAndroid from 'react-native-send-intent';
import * as IntentLauncher from 'expo-intent-launcher';


interface AnxietyRatingPopUpProps {
    modalVisible: boolean,
    setModalVisible: React.Dispatch<React.SetStateAction<boolean>>,
    packageName: string
}
export default function AnxietyRatingPopUp({ modalVisible, setModalVisible, packageName }: { modalVisible: boolean, setModalVisible: React.Dispatch<React.SetStateAction<boolean>>, packageName: string }) {

    const launchUnityScene = async () => {
        setModalVisible(false);
        try {
            await IntentLauncher.startActivityAsync('android.intent.action.MAIN', {
                packageName: packageName,
                className: 'com.unity3d.player.UnityPlayerActivity',
            });
        } catch (error) {
            Alert.alert(
                'Error',
                'Could not launch the AR experience. Please make sure the app is installed.',
                [{ text: 'OK' }]
            );
        }
    };

    return (
        <Modal
            animationType="slide"
            transparent={true}
            visible={modalVisible}
            onRequestClose={() => {
                setModalVisible(!modalVisible);
            }}
        >
            <View style={styles.modalContainer}>
                <View style={styles.modalView}>
                    <TouchableOpacity style={styles.closeButton} onPress={() => setModalVisible(!modalVisible)}>
                        <FontAwesome name="close" size={24} color="black" />
                    </TouchableOpacity>
                    <Text style={styles.modalText}>How anxious or stressed are you feeling right now?</Text>
                    {/* Buttons from 1 to 10 */}
                    <View style={styles.buttonContainer}>
                        {[...Array(5)].map((_, index) => (
                            <TouchableOpacity
                                key={index + 1}
                                onPress={() => console.log(`Button ${index + 1} pressed`)}
                                style={styles.button}
                            >
                                <Text style={styles.buttonText}>{index + 1}</Text>
                            </TouchableOpacity>
                        ))}
                    </View>
                    {/* Button to play scene */}
                    <View style={styles.playContainer}>
                        <TouchableOpacity style={styles.playButton} onPress={launchUnityScene}>
                            <FontAwesome name="play-circle-o" size={55} color="#f5f5f5" />
                            <Text style={styles.buttonText}>Start Scene</Text>
                        </TouchableOpacity>
                    </View>
                </View >
            </View >
        </Modal >
    );
}

const styles = StyleSheet.create({
    modalContainer: {
        flex: 1,
        justifyContent: "center",
        alignItems: "center",
        backgroundColor: 'rgba(0, 0, 0, 0.5)',
    },
    modalView: {
        margin: 20,
        backgroundColor: "#B1D699",
        borderRadius: 20,
        padding: 35,
        alignItems: "center",
        width: "90%",
        // Shadows
        shadowColor: "#000",
        shadowOffset: {
            width: 0,
            height: 2,
        },
        shadowOpacity: 0.25,
        shadowRadius: 4,
        elevation: 5,
    },
    closeButton: {
        position: 'absolute',
        top: 10,
        right: 10,
    },
    modalText: {
        marginBottom: 15,
        textAlign: "center",
        fontSize: 20,
    },
    buttonContainer: {
        flexDirection: "row",
        justifyContent: "center",
        alignSelf: "center",
        marginHorizontal: 20,
    },
    button: {
        padding: 10,
        margin: 5,
        borderRadius: 5,
    },
    buttonText: {
        fontSize: 18,
        marginLeft: 10,
    },
    playContainer: {
        alignItems: "center",
        justifyContent: "center",
        marginTop: 20,
        flexDirection: "row",
    },
    playButton: {
        flexDirection: "row",
        alignItems: "center",
    },
});