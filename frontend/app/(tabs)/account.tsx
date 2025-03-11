import { usePathname } from 'expo-router';
import React from 'react';
import { View, Text, StyleSheet, Button, TextInput, TouchableOpacity } from 'react-native';

const AccountPage: React.FC = () => {
    return (
        <View style={styles.container}>
            <Text style={styles.title}>Account</Text>

            {/* Name field */}
            <View style={styles.infoContainer}>
                <Text style={styles.label}>Name</Text>
                <TextInput style={styles.value}>JohnDoe</TextInput>
            </View>

            {/* Email field */}
            <View style={styles.infoContainer}>
                <Text style={styles.label}>Email</Text>
                <TextInput style={styles.value}>johndoe@example.com</TextInput>
            </View>

            {/* Password field */}
            <View style={styles.infoContainer}>
                <Text style={styles.label}>Password</Text>
                <TextInput style={styles.value} secureTextEntry={true}>123</TextInput>
            </View>

            {/* Update Profile Button */}
            <TouchableOpacity style={styles.updateButton} onPress={() => { }}>
                <Text style={styles.label}>Update Profile</Text>
            </TouchableOpacity>
            {/* <Button title="Logout" onPress={() => { }} /> */}
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
        fontSize: 30,
        fontWeight: 'bold',
        marginBottom: 20,
    },
    infoContainer: {
        flexDirection: 'column',
        gap: 7,
        marginBottom: 10,
    },
    label: {
        fontWeight: 'bold',
        marginRight: 10,
        fontSize: 20,
    },
    value: {
        color: '#555',
        fontSize: 20,
        backgroundColor: '#D9D9D9',
        margin: 3,
        padding: 7,
    },
    updateButton: {
        backgroundColor: '#B1D699',
        padding: 10,
        borderRadius: 10,
        marginTop: 20,
        width: '50%',
        alignItems: 'center',
        marginLeft: '25%',
        // Shadow
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.5,
        shadowRadius: 5,
        // Elevation for Android
        elevation: 5,
    },
});