import React from 'react';
import { View, Text, StyleSheet, Button } from 'react-native';

const AccountPage: React.FC = () => {
    return (
        <View style={styles.container}>
            <Text style={styles.title}>Account</Text>
            <View style={styles.infoContainer}>
                <Text style={styles.label}>Username:</Text>
                <Text style={styles.value}>JohnDoe</Text>
            </View>
            <View style={styles.infoContainer}>
                <Text style={styles.label}>Email:</Text>
                <Text style={styles.value}>johndoe@example.com</Text>
            </View>
            <Button title="Edit Profile" onPress={() => { }} />
            <Button title="Logout" onPress={() => { }} />
        </View>
    );
};

export default AccountPage;

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        backgroundColor: '#fff',
    },
    title: {
        fontSize: 24,
        fontWeight: 'bold',
        marginBottom: 20,
    },
    infoContainer: {
        flexDirection: 'row',
        marginBottom: 10,
    },
    label: {
        fontWeight: 'bold',
        marginRight: 10,
    },
    value: {
        color: '#555',
    },
});