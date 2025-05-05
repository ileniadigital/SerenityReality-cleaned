/**
 * Provides authentication services using Firebase.
 * Includes methods for user sign-up, sign-in, reauthentication, profile updates,
 * password reset, account deletion, and authentication state management.
 *
 * Functions:
 * - `signUpWithEmail`: Registers a new user with email, password, and optional display name.
 * - `signInWithEmail`: Logs in an existing user with email and password.
 * - `reauthenticateWithPassword`: Reauthenticates the current user using their password.
 * - `updateUserProfile`: Updates the current user's profile, including name, email, and password.
 * - `signOut`: Logs out the current user.
 * - `getCurrentUser`: Retrieves the currently logged-in user's profile information.
 * - `subscribeToAuthChanges`: Sets up a listener for authentication state changes.
 * - `isUserLoggedIn`: Checks if a user is currently logged in.
 * - `sendPasswordResetEmail`: Sends a password reset email to the specified address.
 * - `deleteAccount`: Deletes the currently logged-in user's account.
 *
 * Each function returns a Promise or value indicating the success or failure of the operation,
 * with the user data, or an error message if something fails.
 */

import auth, { FirebaseAuthTypes } from '@react-native-firebase/auth';
import { UserProfile, AuthResult, ProfileUpdateData } from '../data/interfaces';

//Sign up a new user with email and password
export const signUpWithEmail = async (
  email: string,
  password: string,
  name: string = ''
): Promise<AuthResult> => {
  try {
    // Create user with email and password
    const userCredential = await auth().createUserWithEmailAndPassword(email, password);

    // Update user account with name if provided
    if (name) {
      await userCredential.user.updateProfile({
        displayName: name
      });
    }
    return {
      success: true,
      user: userCredential.user,
      message: 'Your account has been created successfully!'
    };
  } catch (error: any) {
    let errorMessage = 'An error occurred during sign up';

    // Handle existing email, invalid email, and weak password errors
    switch (error.code) {
      case 'auth/email-already-in-use':
        errorMessage = 'That email address is already in use!';
        break;
      case 'auth/invalid-email':
        errorMessage = 'That email address is invalid!';
        break;
      case 'auth/weak-password':
        errorMessage = 'Password should be at least 6 characters';
        break;
      default:
        errorMessage = error.message;
    }
    return {
      success: false,
      message: errorMessage
    };
  }
};

//Sign in existing user with email and password
export const signInWithEmail = async (
  email: string,
  password: string
): Promise<AuthResult> => {
  // Check if email and password are provided
  try {
    const userCredential = await auth().signInWithEmailAndPassword(email, password);
    return {
      success: true,
      user: userCredential.user,
      message: 'You have successfully logged in!'
    };
  } catch (error: any) {
    let errorMessage = 'Failed to login';

    // Handle invalid email, user not found, and wrong password errors
    switch (error.code) {
      case 'auth/invalid-email':
        errorMessage = 'That email address is invalid!';
        break;
      case 'auth/user-disabled':
        errorMessage = 'This user has been disabled.';
        break;
      case 'auth/user-not-found':
        errorMessage = 'No user found with that email address';
        break;
      case 'auth/wrong-password':
        errorMessage = 'Invalid email or password';
        break;
      default:
        errorMessage = error.message;
    }
    return {
      success: false,
      message: errorMessage
    };
  }
};

// Reauthenticate user with email and password
export const reauthenticateWithPassword = async (password: string): Promise<AuthResult> => {
  try {
    const user = auth().currentUser;

    if (!user || !user.email) {
      return {
        success: false,
        message: 'Current user or email not found'
      };
    }

    // Create credentials with current email and provided password
    const credential = auth.EmailAuthProvider.credential(user.email, password);

    // Reauthenticate
    await user.reauthenticateWithCredential(credential);

    return {
      success: true,
      message: 'Reauthentication successful'
    };
  } catch (error: any) {
    return {
      success: false,
      message: 'Reauthentication failed: ' + (error.message || 'Unknown error')
    };
  }
};

// Update user profile and reuathenticate if necessary
export const updateUserProfile = async (
  updateData: ProfileUpdateData,
  currentPassword?: string
): Promise<AuthResult> => {
  try {
    const user = auth().currentUser;
    // Check if user is logged in
    if (!user) {
      return {
        success: false,
        message: 'You must be logged in to update your profile'
      };
    }
    const { name, email, password } = updateData;
    let profileUpdated = false;
    let requiresReauthentication = false;

    // Check if operation requires reauthentication
    if ((email && user.email !== email) || (password && password !== '********')) {
      requiresReauthentication = true;
    }

    // Reauthenticate if needed
    if (requiresReauthentication) {
      if (!currentPassword) {
        return {
          success: false,
          message: 'Current password is required to update email or password'
        };
      }

      const reauth = await reauthenticateWithPassword(currentPassword);
      if (!reauth.success) {
        return reauth; // Return the failed reauthentication result
      }
    }

    // Update display name if provided
    if (name && user.displayName !== name) {
      await user.updateProfile({
        displayName: name
      });
      profileUpdated = true;
    }

    // Update email if provided and different
    if (email && user.email !== email) {
      // Email verification is handled after reauthentication
      try {
        await user.updateEmail(email);
        // Send verification for new email
        await user.sendEmailVerification();
        profileUpdated = true;
      } catch (error: any) {
        return {
          success: false,
          message: `Email update failed: ${error.message}`
        };
      }
    }

    // Update password if provided and not the placeholder
    if (password && password !== '********') {
      try {
        await user.updatePassword(password);
        profileUpdated = true;
      } catch (error: any) {
        return {
          success: false,
          message: `Password update failed: ${error.message}`
        };
      }
    }

    if (profileUpdated) {
      await user.reload(); // Reload user to get updated data
    }

    // Force a refresh of auth state
    const eventEmitter = new (require('events').EventEmitter)();
    eventEmitter.emit('auth-state-changed', user);

    return {
      success: true,
      message: 'Your profile has been updated!'
    };
  } catch (error: any) {
    return {
      success: false,
      message: error.message
    };
  }
};


//Sign out the current user
export const signOut = async (): Promise<AuthResult> => {
  try {
    await auth().signOut();
    return {
      success: true,
      message: 'You have been signed out'
    };
  } catch (error: any) {
    return {
      success: false,
      message: error.message
    };
  }
};

//Get current user data
export const getCurrentUser = (): UserProfile | null => {
  const user = auth().currentUser;

  if (!user) return null;

  return {
    uid: user.uid,
    email: user.email,
    name: user.displayName || '',
    emailVerified: user.emailVerified
  };
};

//Setup an auth state change listener
export const subscribeToAuthChanges = (
  callback: (isAuthenticated: boolean, user: UserProfile | null) => void
): (() => void) => {
  return auth().onAuthStateChanged(async (user) => {
    if (user) {
      await user.reload();
      const userData: UserProfile = {
        uid: user.uid,
        email: user.email,
        name: user.displayName || '',
        emailVerified: user.emailVerified
      };
      callback(true, userData);
    } else {
      callback(false, null);
    }
  });
};

//Check if user is currently logged in
export const isUserLoggedIn = (): boolean => {
  return !!auth().currentUser;
};

//Send password reset email
export const sendPasswordResetEmail = async (email: string): Promise<AuthResult> => {
  try {
    await auth().sendPasswordResetEmail(email);
    return {
      success: true,
      message: 'Password reset email sent!'
    };
  } catch (error: any) {
    let errorMessage = 'Failed to send password reset email';

    if (error.code === 'auth/invalid-email') {
      errorMessage = 'That email address is invalid!';
    } else if (error.code === 'auth/user-not-found') {
      errorMessage = 'No user found with that email address';
    }

    return {
      success: false,
      message: errorMessage
    };
  }
};

// Delete account
export const deleteAccount = async (): Promise<AuthResult> => {
  try {
    const user = auth().currentUser;
    if (!user) {
      return {
        success: false,
        message: 'No user is currently logged in'
      };
    }

    await user.delete();
    return {
      success: true,
      message: 'Your account has been deleted successfully!'
    };
  } catch (error: any) {
    return {
      success: false,
      message: error.message
    };
  }
};