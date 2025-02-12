import { View, Text, StyleSheet, SafeAreaView } from 'react-native';
import MaterialIcons from '@expo/vector-icons/MaterialIcons';
import { Link } from 'expo-router';

export default function Header({ title }: { title: string }) {
    return (
        <SafeAreaView style={styles.safeArea}>
            <View style={styles.container}>
                <Text style={styles.title}>{title}</Text>
                <Link href="/account">
                    <MaterialIcons name="account-circle" size={40} color="#F5F5F5" />
                </Link>
            </View>
        </SafeAreaView>
    );
}

const styles = StyleSheet.create({
    safeArea: {
        backgroundColor: '#B1D699',
    },
    container: {
        height: 130,  // Fixed height
        paddingHorizontal: 20,
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
    },
    title: {
        fontSize: 24,
        fontWeight: 'bold',
        color: '#ffffff',
    },
});
