import React from 'react';
import { ScrollView } from 'react-native';
import { FormField } from './FormField';
import { ActionButton } from './ActionButton';
import { LoginProps } from '../../../data/interfaces';

export const Login: React.FC<LoginProps> = ({
    userData,
    isLoading,
    onInputChange,
    onLogin,
    onBack,
    onSwitchToSignUp
}) => {
    return (
        <ScrollView>
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
                title="Log In"
                onPress={onLogin}
                disabled={isLoading}
                variant="login"
            />
            <ActionButton
                title="Back"
                onPress={onBack}
                disabled={isLoading}
                variant="link"
            />
            <ActionButton
                title="Need an account? Sign up"
                onPress={onSwitchToSignUp}
                disabled={isLoading}
                variant="link"
            />
        </ScrollView>
    );
};