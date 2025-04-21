import { Text, View, StyleSheet, ScrollView } from 'react-native';

export default function AboutScreen() {
    return (
        <View style={styles.container}>
            <ScrollView contentContainerStyle={styles.scrollContainer}>
                {/* Title */}
                <Text style={styles.title}>Important Disclaimer</Text>
                {/* Main info */}
                <Text style={styles.text}>
                    SerenityReality is designed as a self-help tool to assist with anxiety management techniques.
                    It does not replace professional medical advice, diagnosis, or treatment.
                    The contents and guidance of this application are not medical or clinical advice,
                    have not been evaluated by medical authorities, and should not be used as a substitute for professional healthcare.
                </Text>
                <Text style={styles.text}>
                    If you are experiencing symptoms of anxiety, panic, or other mental health concerns,
                    please contact a qualified healthcare professional immediately.
                    You can make a request through the NHS and NHS Talking Therapies.
                </Text>
                <Text style={styles.text}>
                    If you or someone you know is experiencing a mental health emergency, please call 999, go to your nearest A&E,
                    or call a crisis helpline.
                </Text>
                {/* Credits */}
                <Text style={styles.footer}>Developed by Ilenia Maietta © 2025</Text>
            </ScrollView>

        </View>
    );
};
const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#f5f5f5',
        justifyContent: 'center',
        alignItems: 'center',
    },
    scrollContainer: {
        padding: 20,
        alignItems: 'center',
    },
    title: {
        color: '#000000',
        fontSize: 30,
        fontWeight: 'bold',
        textAlign: 'center',
        margin: 20,
        marginTop: 0,
    },
    text: {
        color: '#000000',
        fontSize: 18,
        textAlign: 'justify',
        margin: 20,

    },
    footer: {
        color: '#000000',
        fontSize: 16,
        fontWeight: 'bold',
        textAlign: 'center',
        position: 'absolute',
        bottom: 0,
        marginTop: 20,
    }
});
