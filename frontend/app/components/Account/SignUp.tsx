import React from 'react';
import { View, ScrollView } from 'react-native';
import { FormField } from './FormField';
import { ActionButton } from './ActionButton';
import { SignUpProps } from '../../../data/interfaces';

export const SignUp: React.FC<SignUpProps> = ({
    userData,
    isLoading,
    onInputChange,
    onSignUp,
    onBack,
    onSwitchToLogin
}) => {
    return (
        <ScrollView>
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
                title="Create Account"
                onPress={onSignUp}
                disabled={isLoading}
            />
            <ActionButton
                title="Back"
                onPress={onBack}
                disabled={isLoading}
                variant="link"
            />
            <ActionButton
                title="Already have an account? Log in"
                onPress={onSwitchToLogin}
                disabled={isLoading}
                variant="link"
            />
        </ScrollView>
    );
};