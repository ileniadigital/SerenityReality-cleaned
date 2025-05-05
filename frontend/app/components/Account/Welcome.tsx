// Welcome component for unauthenticated users
import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { ActionButton } from './ActionButton';
import { WelcomeScreenProps } from '../../../data/interfaces';

export const Welcome: React.FC<WelcomeScreenProps> = ({
    // Props
    onSignUpPress,
    onLoginPress
}) => {
    return (
        <View style={styles.welcomeContainer}>
            <Text style={styles.welcomeText}>Sign up or log in for a free account</Text>
            {/* Sign up button */}
            <ActionButton
                title="Sign Up"
                onPress={onSignUpPress}
            />
            {/* Log in button */}
            <ActionButton
                title="Log In"
                onPress={onLoginPress}
                variant="login"
            />
        </View>
    );
};

const styles = StyleSheet.create({
    welcomeContainer: {
        alignItems: 'center',
        justifyContent: 'center',
        marginTop: 40,
    },
    welcomeText: {
        fontSize: 22,
        textAlign: 'center',
        marginBottom: 30,
    },
});