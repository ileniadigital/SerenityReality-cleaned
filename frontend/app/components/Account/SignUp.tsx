// This component handles the sign up page and functions
import React from 'react';
import { View, ScrollView } from 'react-native';
import { FormField } from './FormField';
import { ActionButton } from './ActionButton';
import { SignUpProps } from '../../../data/interfaces';

export const SignUp: React.FC<SignUpProps> = ({
    // Props
    userData,
    isLoading,
    onInputChange,
    onSignUp,
    onBack,
    onSwitchToLogin
}) => {
    return (
        // Scrollable view for the sign up form
        <ScrollView>
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
            {/* Create account */}
            <ActionButton
                title="Create Account"
                onPress={onSignUp}
                disabled={isLoading}
            />
            {/* Back button */}
            <ActionButton
                title="Back"
                onPress={onBack}
                disabled={isLoading}
                variant="link"
            />
            {/* Log in button */}
            <ActionButton
                title="Already have an account? Log in"
                onPress={onSwitchToLogin}
                disabled={isLoading}
                variant="link"
            />
        </ScrollView>
    );
};