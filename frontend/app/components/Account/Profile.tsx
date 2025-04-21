import React from 'react';
import { StyleSheet, ScrollView } from 'react-native';
import { FormField } from './FormField';
import { ActionButton } from './ActionButton';
import { ProfileProps } from '../../../data/interfaces';

export const Profile: React.FC<ProfileProps> = ({
    userData,
    isLoading,
    onInputChange,
    onUpdateProfile,
    onSignOut,
    onDeleteAccount,
}) => {
    return (
        <ScrollView contentContainerStyle={styles.scrollContainer}>
            <FormField
                label="Name"
                value={userData.name}
                onChangeText={(text) => onInputChange('name', text)}
                editable={!isLoading}
            />
            <FormField
                label="Email"
                value={userData.email}
                onChangeText={(text) => onInputChange('email', text)}
                keyboardType="email-address"
                autoCapitalize="none"
                editable={!isLoading}
            />
            <FormField
                label="Password"
                value={userData.password}
                onChangeText={(text) => onInputChange('password', text)}
                secureTextEntry
                editable={!isLoading}
            />
            <ActionButton
                title="Update Profile"
                onPress={onUpdateProfile}
                disabled={isLoading}
            />
            <ActionButton
                title="Sign Out"
                onPress={onSignOut}
                disabled={isLoading}
                variant="signOut"
            />
            <ActionButton
                title="Delete Account"
                onPress={onDeleteAccount}
                disabled={isLoading}
                variant="signOut"
            />
        </ScrollView>
    );
};

const styles = StyleSheet.create({
    scrollContainer: {
        flexGrow: 1,
        paddingBottom: 40
    },
    switchContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginBottom: 20,
    },
    label: {
        fontSize: 18,
        fontWeight: 'bold',
    },
});