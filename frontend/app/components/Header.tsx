// Main header component for the app, with welcome and name if user is authenticated
import { View, Text, StyleSheet, SafeAreaView } from 'react-native';
import MaterialIcons from '@expo/vector-icons/MaterialIcons';
import { Link } from 'expo-router';
import { UserProvider } from '@/contexts/UserContext';

export default function Header({ title, name }: { title: string; name: string }) {
    return (
        <UserProvider>
            <SafeAreaView style={styles.safeArea}>
                <View style={styles.container}>
                    {/* Text */}
                    <Text style={styles.title}>{title}</Text>
                    <Link href="/account">
                        {/* Accoun page link */}
                        <MaterialIcons name="account-circle" size={40} color="#000000" />
                    </Link>
                </View>
                {/* Greeting message */}
                <View style={styles.greeting}>
                    <Text style={styles.greetingText}>Hi{name ? `, ${name}!` : ' there!'}</Text>
                    <Text style={styles.greetingText}>How are you feeling today?</Text>
                </View>
            </SafeAreaView>
        </UserProvider>
    );
}

const styles = StyleSheet.create({
    safeArea: {
        backgroundColor: '#B1D699',
        paddingTop: 0,
        borderBottomLeftRadius: 30,
        borderBottomRightRadius: 30,
    },
    container: {
        height: 130,  // Fixed height
        paddingHorizontal: 20,
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
    },
    title: {
        fontSize: 20,
        fontWeight: 'bold',
        color: '#000000',
    },
    greeting: {
        alignItems: 'center',
        marginTop: -30,
        marginBottom: 20,
    },
    greetingText: {
        fontSize: 25,
        color: '#000000',
        fontWeight: 'bold',
    },
});
