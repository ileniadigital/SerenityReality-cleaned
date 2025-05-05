// Props for the authentication screens and components
// This file contains the type definitions for the authentication screens and components

import { ReactNode } from 'react';
// Sign up props
export interface SignUpProps {
    userData: {
        name: string;
        email: string;
        password: string;
    };
    isLoading: boolean;
    onInputChange: (field: 'name' | 'email' | 'password', value: string) => void;
    onSignUp: () => void;
    onBack: () => void;
    onSwitchToLogin: () => void;
}

// Login props
export interface LoginProps {
    userData: {
        email: string;
        password: string;
    };
    isLoading: boolean;
    onInputChange: (field: 'email' | 'password', value: string) => void;
    onLogin: () => void;
    onBack: () => void;
    onSwitchToSignUp: () => void;
}

// Form field props
export interface FormFieldProps {
    label: string;
    value: string;
    onChangeText: (text: string) => void;
    placeholder?: string;
    secureTextEntry?: boolean;
    keyboardType?: 'default' | 'email-address' | 'numeric' | 'phone-pad';
    autoCapitalize?: 'none' | 'sentences' | 'words' | 'characters';
    editable?: boolean;
}

// Action button props
export interface ActionButtonProps {
    title: string;
    onPress: () => void;
    disabled?: boolean;
    variant?: 'primary' | 'login' | 'signOut' | 'link';
}

// Type definitions
export interface AuthResult {
    success: boolean;
    message: string;
    user?: any;
  }
  
  export interface UserProfile {
    uid: string;
    email: string | null;
    name: string;
    emailVerified: boolean;
  }
  
  export interface ProfileUpdateData {
    name?: string;
    email?: string;
    password?: string | null;
  }

// Create context type
export interface UserContextType {
    user: UserProfile | null;
    isLoggedIn: boolean;
    updateUserContext: () => void;
}

// Provider component
export interface UserProviderProps {
    children: ReactNode;
}

// Welcome screen props
export interface WelcomeScreenProps {
    onSignUpPress: () => void;
    onLoginPress: () => void;
}

// Profile screen props
export interface ProfileProps {
    userData: {
        name: string;
        email: string;
        password: string;
    };
    isLoading: boolean;
    onInputChange: (field: 'name' | 'email' | 'password', value: string) => void;
    onUpdateProfile: () => void;
    onSignOut: () => void;
    onDeleteAccount: () => void;
}