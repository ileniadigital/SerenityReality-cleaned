import React from 'react';
import { TouchableOpacity, Text, StyleSheet } from 'react-native';
import { ActionButtonProps } from '../../../data/interfaces';

export const ActionButton: React.FC<ActionButtonProps> = ({
    title,
    onPress,
    disabled = false,
    variant = 'primary'
}) => {
    const buttonStyles = [
        styles.actionButton,
        variant === 'login' && styles.loginButton,
        variant === 'signOut' && styles.signOutButton,
        variant === 'link' && styles.linkButton,
    ];

    const textStyles = [
        variant !== 'link' ? styles.buttonText : styles.linkText,
    ];

    return (
        <TouchableOpacity
            style={buttonStyles}
            onPress={onPress}
            disabled={disabled}
        >
            <Text style={textStyles}>{title}</Text>
        </TouchableOpacity>
    );
};

const styles = StyleSheet.create({
    actionButton: {
        backgroundColor: '#B1D699',
        padding: 10,
        borderRadius: 10,
        marginTop: 15,
        alignItems: 'center',
        width: '100%',
        // Shadow
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.5,
        shadowRadius: 5,
        // Elevation for Android
        elevation: 5,
    },
    loginButton: {
        backgroundColor: '#B1D699',
    },
    signOutButton: {
        backgroundColor: '#FF6347',
    },
    linkButton: {
        padding: 10,
        marginTop: 10,
        alignItems: 'center',
        backgroundColor: 'transparent',
        shadowColor: 'transparent',
        shadowOffset: { width: 0, height: 0 },
        shadowOpacity: 0,
        shadowRadius: 0,
        elevation: 0,
    },
    buttonText: {
        color: 'white',
        fontWeight: 'bold',
        fontSize: 16,
    },
    linkText: {
        color: '#4682B4',
        fontSize: 16,
    },
});