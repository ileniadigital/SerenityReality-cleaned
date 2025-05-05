// This displays the profile component using the FormField and ActionButton components
import React from 'react';
import { StyleSheet, ScrollView } from 'react-native';
import { FormField } from './FormField';
import { ActionButton } from './ActionButton';
import { ProfileProps } from '../../../data/interfaces';

export const Profile: React.FC<ProfileProps> = ({
    // Props
    userData,
    isLoading,
    onInputChange,
    onUpdateProfile,
    onSignOut,
    onDeleteAccount,
}) => {
    return (
        // Scrollable view for the profile form
        <ScrollView contentContainerStyle={styles.scrollContainer}>
            {/* Name */}
            <FormField
                label="Name"
                value={userData.name}
                onChangeText={(text) => onInputChange('name', text)}
                editable={!isLoading}
            />
            {/* Email */}
            <FormField
                label="Email"
                value={userData.email}
                onChangeText={(text) => onInputChange('email', text)}
                keyboardType="email-address"
                autoCapitalize="none"
                editable={!isLoading}
            />
            {/* Password */}
            <FormField
                label="Password"
                value={userData.password}
                onChangeText={(text) => onInputChange('password', text)}
                secureTextEntry
                editable={!isLoading}
            />
            {/* Update profile button */}
            <ActionButton
                title="Update Profile"
                onPress={onUpdateProfile}
                disabled={isLoading}
            />
            {/* Sign out button */}
            <ActionButton
                title="Sign Out"
                onPress={onSignOut}
                disabled={isLoading}
                variant="signOut"
            />
            {/* Delete account button */}
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