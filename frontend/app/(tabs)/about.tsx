import { Text, View, StyleSheet } from 'react-native';

export default function AboutScreen() {
    return (
        <View style={styles.container}>
            {/* Title */}
            <Text style={styles.title}>About SerenityReality</Text>
            {/* Main info */}
            <Text style={styles.text}>Stress and anxiety affect everyone, but when they start to affect us every day, they negatively impact our wellbeing. Especially university students who are under a lot of stress and pressure, causing strong anxiety feelings. SerenityReality aims to help university students manage these anxious feelings, encouraging mindfulness and stillness in the mind and the body. Augmented Reality offers that additional opportunity of engangement and interactivity a lot of self-help lacks, to help you feel more connected to the actions you are doing to take care of yourself!</Text>
            {/* Credits */}
            <Text style={styles.footer}>Developed by Ilenia Maietta © 2025</Text>

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
        margin: 30,

    },
    footer: {
        color: '#000000',
        fontSize: 16,
        textAlign: 'center',
        position: 'absolute',
        bottom: 0,
        marginBottom: 20,
    }
});
