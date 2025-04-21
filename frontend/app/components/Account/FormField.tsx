import React from 'react';
import { View, Text, TextInput, StyleSheet } from 'react-native';
import { FormFieldProps } from '../../../data/interfaces';

export const FormField: React.FC<FormFieldProps> = ({
    label,
    value,
    onChangeText,
    placeholder,
    secureTextEntry = false,
    keyboardType = 'default',
    autoCapitalize = 'sentences',
    editable = true
}) => {
    return (
        <View style={styles.infoContainer}>
            <Text style={styles.label}>{label}</Text>
            <TextInput
                style={styles.value}
                placeholder={placeholder || `Enter your ${label.toLowerCase()}`}
                value={value}
                onChangeText={onChangeText}
                secureTextEntry={secureTextEntry}
                keyboardType={keyboardType}
                autoCapitalize={autoCapitalize}
                editable={editable}
            />
        </View>
    );
};

const styles = StyleSheet.create({
    infoContainer: {
        flexDirection: 'column',
        marginBottom: 15,
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
});