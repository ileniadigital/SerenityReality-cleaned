// This page displays the account and handles authentication, log in, sign up and account delettion
import React from 'react';
import { ScrollView, Text, StyleSheet, View } from 'react-native';
import { Link } from 'expo-router';
import { useAccount, AuthMode } from '../../services/useAuth';
// Import components
import { Welcome } from '../components/Account/Welcome';
import { SignUp } from '../components/Account/SignUp';
import { Login } from '../components/Account/Login';
import { Profile } from '../components/Account/Profile';
import { FormField } from '../components/Account/FormField';
import { ActionButton } from '../components/Account/ActionButton';
import { MaterialIcons } from '@expo/vector-icons';

const Account = () => {
    const {
        // States
        authMode,
        isLoading,
        userData,

        // Navigation functions
        navigateToWelcome,
        navigateToSignUp,
        navigateToLogin,

        // Handlers
        handleInputChange,
        handleSignUp,
        handleLogin,
        handleUpdateAccount,
        handleSignOut,
        handleDeleteAccount,

        // Screens
        renderReauthenticationScreen
    } = useAccount();

    // Get reauthentication data if needed
    const reauthData = authMode === AuthMode.REAUTHENTICATE ? renderReauthenticationScreen() : null;

    return (
        <ScrollView style={styles.container}>
            {/* Header with Settings Icon */}
            <View style={styles.header}>
                <Text style={styles.title}>Account</Text>
                <Link href="/settings">
                    <MaterialIcons name="settings" size={30} color="#000" />
                </Link>
            </View>


            {/* Welcome screen for when not logged in*/}
            {authMode === AuthMode.WELCOME && (
                <Welcome
                    onSignUpPress={navigateToSignUp}
                    onLoginPress={navigateToLogin}
                />
            )}

            {/* Sign up button */}
            {authMode === AuthMode.SIGNUP && (
                <SignUp
                    userData={userData}
                    isLoading={isLoading}
                    onInputChange={handleInputChange}
                    onSignUp={handleSignUp}
                    onBack={navigateToWelcome}
                    onSwitchToLogin={navigateToLogin}
                />
            )}


            {/* Login button */}
            {authMode === AuthMode.LOGIN && (
                <Login
                    userData={userData}
                    isLoading={isLoading}
                    onInputChange={handleInputChange}
                    onLogin={handleLogin}
                    onBack={navigateToWelcome}
                    onSwitchToSignUp={navigateToSignUp}
                />
            )}

            {/* Profile screen */}
            {authMode === AuthMode.PROFILE && (
                <Profile
                    userData={userData}
                    isLoading={isLoading}
                    onInputChange={handleInputChange}
                    onUpdateProfile={handleUpdateAccount}
                    onSignOut={handleSignOut}
                    onDeleteAccount={handleDeleteAccount}
                />

            )}

            {/* Reauthentication screen */}
            {authMode === AuthMode.REAUTHENTICATE && reauthData && (
                <ScrollView>
                    <Text style={styles.infoText}>
                        {reauthData.message}
                    </Text>
                    <FormField
                        label="Current Password"
                        value={reauthData.currentPassword}
                        onChangeText={reauthData.setCurrentPassword}
                        secureTextEntry
                        editable={!isLoading}
                    />
                    <ActionButton
                        title="Confirm"
                        onPress={reauthData.handleConfirm}
                        disabled={isLoading}
                    />
                    <ActionButton
                        title="Cancel"
                        onPress={reauthData.handleCancel}
                        disabled={isLoading}
                        variant="link"
                    />
                </ScrollView>
            )}

            {/* Loading indicator */}
            {isLoading && <Text style={styles.loadingText}>Processing...</Text>}
        </ScrollView>
    );
};

export default Account;

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        backgroundColor: '#fff',
    },
    header: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginBottom: 20,
    },
    title: {
        fontSize: 30,
        fontWeight: 'bold',
        marginBottom: 20,
    },
    loadingText: {
        marginTop: 15,
        textAlign: 'center',
        fontStyle: 'italic',
        color: '#888',
    },
    infoText: {
        fontSize: 16,
        marginBottom: 20,
        textAlign: 'center',
        color: '#555',
    }
});