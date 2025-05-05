// This component handles the log in page and functions
import React from 'react';
import { ScrollView } from 'react-native';
import { FormField } from './FormField';
import { ActionButton } from './ActionButton';
import { LoginProps } from '../../../data/interfaces';

export const Login: React.FC<LoginProps> = ({
    // Props
    userData,
    isLoading,
    onInputChange,
    onLogin,
    onBack,
    onSwitchToSignUp
}) => {
    return (
        // Scrollable view for the login form
        <ScrollView>
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
            {/* Log in button*/}
            <ActionButton
                title="Log In"
                onPress={onLogin}
                disabled={isLoading}
                variant="login"
            />
            {/* Back button */}
            <ActionButton
                title="Back"
                onPress={onBack}
                disabled={isLoading}
                variant="link"
            />
            {/* Sign up button */}
            <ActionButton
                title="Need an account? Sign up"
                onPress={onSwitchToSignUp}
                disabled={isLoading}
                variant="link"
            />
        </ScrollView>
    );
};