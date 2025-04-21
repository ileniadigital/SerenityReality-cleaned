import { useState, useEffect } from 'react';
import { Alert } from 'react-native';
import { useUser} from '../contexts/UserContext';
import {
  signUpWithEmail,
  signInWithEmail,
  updateUserProfile,
  signOut,
  deleteAccount
} from './auth';

// Define auth modes
export enum AuthMode {
  WELCOME = 'welcome',
  SIGNUP = 'signup',
  LOGIN = 'login',
  PROFILE = 'profile',
  REAUTHENTICATE = 'reauthenticate',
  DELETE = 'delete'
}

export const useAccount = () => {
  const { updateUserContext, user: contextUser, isLoggedIn: contextIsLoggedIn } = useUser();
  const [authMode, setAuthMode] = useState<AuthMode>(AuthMode.WELCOME);
  const [isLoading, setIsLoading] = useState(false);
  const [userData, setUserData] = useState({
    name: '',
    email: '',
    password: ''
  });
  const [currentPassword, setCurrentPassword] = useState('');
  const [confirmationPassword, setConfirmationPassword] = useState('');
  const [pendingUpdate, setPendingUpdate] = useState<any>(null);

  // Update local state when context changes
  useEffect(() => {
    if (contextIsLoggedIn && contextUser) {
      setAuthMode(AuthMode.PROFILE);
      setUserData({
        name: contextUser.name || '',
        email: contextUser.email || '',
        password: '********' // For security, never display the actual password
      });
    } else {
      // Only reset to welcome if we're on the profile screen
      if (authMode === AuthMode.PROFILE) {
        setAuthMode(AuthMode.WELCOME);
      }
    }
  }, [contextIsLoggedIn, contextUser]);

  // Update local state based on input changes
  const handleInputChange = (field: keyof typeof userData, value: string) => {
    setUserData(prev => ({
      ...prev,
      [field]: value
    }));
  };

  // Handle sign up 
  const handleSignUp = async () => {
    if (!userData.email || !userData.password) {
      Alert.alert('Error', 'Please enter both email and password');
      return;
    }

    setIsLoading(true);
    const result = await signUpWithEmail(userData.email, userData.password, userData.name);
    setIsLoading(false);

    if (result.success) {
      Alert.alert('Success', result.message);
      updateUserContext(); // Refresh the global user context
    } else {
      Alert.alert('Error', result.message);
    }
  };

  // Handle login 
  const handleLogin = async () => {
    if (!userData.email || !userData.password) {
      Alert.alert('Error', 'Please enter both email and password');
      return;
    }

    setIsLoading(true);
    const result = await signInWithEmail(userData.email, userData.password);
    setIsLoading(false);

    if (result.success) {
      Alert.alert('Success', result.message);
      updateUserContext(); // Refresh the global user context
    } else {
      Alert.alert('Error', result.message);
    }
  };

  // Request reauthentication when needed
  const requestReauthentication = (pendingUpdateData: any) => {
    setPendingUpdate(pendingUpdateData);
    setCurrentPassword('');
    setAuthMode(AuthMode.REAUTHENTICATE);
  };

  // Handle update account with possible reauthentication
  const handleUpdateAccount = async () => {
    // Check if sensitive data is being changed (email or password)
    const needsReauthentication =
      (userData.email !== contextUser?.email) ||
      (userData.password !== '********');

    if (needsReauthentication) {
      // Save the update data and request reauthentication
      requestReauthentication({
        name: userData.name,
        email: userData.email,
        password: userData.password !== '********' ? userData.password : null
      });
      return;
    }

    // No reauthentication needed, proceed with update
    setIsLoading(true);
    const result = await updateUserProfile({
      name: userData.name,
      email: userData.email,
      password: userData.password !== '********' ? userData.password : null
    });
    setIsLoading(false);

    if (result.success) {
      Alert.alert('Success', result.message);
      updateUserContext(); // Refresh the global user context
    } else {
      Alert.alert('Error', result.message);
    }
  };

  // Handle reauthentication and complete the update
  const handleReauthenticate = async () => {
    if (!currentPassword) {
      Alert.alert('Error', 'Please enter your current password');
      return;
    }

    setIsLoading(true);
    const result = await updateUserProfile(pendingUpdate, currentPassword);
    setIsLoading(false);

    if (result.success) {
      Alert.alert('Success', result.message);
      updateUserContext(); // Refresh the global user context
      setAuthMode(AuthMode.PROFILE); // Return to profile
      setPendingUpdate(null); // Clear pending update
    } else {
      Alert.alert('Error', result.message);
    }
  };

  // Handle delete account with confirmation
  const handleDeleteAccount = () => {
    // Show confirmation dialog first
    Alert.alert(
        'Delete Account',
        'Are you sure you want to delete your account? This action cannot be undone.',
        [
            {
                text: 'Cancel',
                style: 'cancel',
            },
            {
                text: 'Yes, Delete',
                onPress: async () => {
                    setIsLoading(true);
                    const result = await deleteAccount();
                    setIsLoading(false);

                    if (result.success) {
                        Alert.alert('Account Deleted', result.message);
                        updateUserContext(); // Reset the app since the user is deleted
                        setAuthMode(AuthMode.WELCOME); // Navigate back to the welcome screen
                    } else {
                        Alert.alert('Error', result.message);
                    }
                },
                style: 'destructive',
            },
        ]
    );
};

  // Process actual account deletion
  const confirmDeleteAccount = async () => {
    if (!confirmationPassword) {
      Alert.alert('Error', 'Please enter your password to confirm deletion');
      return;
    }

    setIsLoading(true);
    const result = await deleteAccount();
    setIsLoading(false);

    if (result.success) {
      Alert.alert('Account Deleted', result.message);
      updateUserContext(); // Reset the app since user is deleted
      setAuthMode(AuthMode.WELCOME);
    } else {
      Alert.alert('Error', result.message);
    }
  };

  // Handle sign out
  const handleSignOut = async () => {
    setIsLoading(true);
    const result = await signOut();
    setIsLoading(false);

    if (result.success) {
      Alert.alert('Success', result.message);
      updateUserContext(); // Refresh the global user context
    } else {
      Alert.alert('Error', result.message);
    }
  };

  // Render reauthentication screen
  const renderReauthenticationScreen = () => {
    return {
      title: 'Confirm Password',
      message: 'To update your email or password, please confirm your current password for security.',
      currentPassword,
      setCurrentPassword,
      handleConfirm: handleReauthenticate,
      handleCancel: () => setAuthMode(AuthMode.PROFILE)
    };
  };

  // Render delete confirmation screen
  const renderDeleteConfirmationScreen = () => {
    return {
      title: 'Delete Account',
      message: 'You are about to permanently delete your account. This action cannot be undone.',
      confirmationPassword,
      setConfirmationPassword,
      handleConfirm: confirmDeleteAccount,
      handleCancel: () => setAuthMode(AuthMode.PROFILE)
    };
  };

  // Navigation helpers
  const navigateToWelcome = () => setAuthMode(AuthMode.WELCOME);
  const navigateToSignUp = () => setAuthMode(AuthMode.SIGNUP);
  const navigateToLogin = () => setAuthMode(AuthMode.LOGIN);
  const navigateToProfile = () => setAuthMode(AuthMode.PROFILE);

  return {
    // State
    authMode,
    isLoading,
    userData,
    currentPassword,
    confirmationPassword,
    
    // State setters
    setAuthMode,
    setCurrentPassword,
    setConfirmationPassword,
    
    // Input handlers
    handleInputChange,
    
    // Auth actions
    handleSignUp,
    handleLogin,
    handleUpdateAccount,
    handleReauthenticate,
    handleDeleteAccount,
    confirmDeleteAccount,
    handleSignOut,
    
    // Screen helpers
    renderReauthenticationScreen,
    renderDeleteConfirmationScreen,
    
    // Navigation
    navigateToWelcome,
    navigateToSignUp,
    navigateToLogin,
    navigateToProfile
  };
};