import React, { createContext, useState, useContext, useEffect } from 'react';
import { subscribeToAuthChanges } from '../services/auth';
import { UserProfile } from '../data/interfaces';
import { UserContextType, UserProviderProps } from '../data/interfaces';

// Create the context with a default value
const UserContext = createContext<UserContextType>({
    user: null,
    isLoggedIn: false,
    updateUserContext: () => { }
});

export const useUser = () => useContext(UserContext);

export const UserProvider: React.FC<UserProviderProps> = ({ children }) => {
    const [user, setUser] = useState<UserProfile | null>(null);
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
    const [refreshTrigger, setRefreshTrigger] = useState<number>(0);

    // Force refresh the user context
    const updateUserContext = () => {
        setRefreshTrigger(prev => prev + 1);
    };

    useEffect(() => {
        // Subscribe to auth changes
        const unsubscribe = subscribeToAuthChanges((isAuthenticated, userData) => {
            setIsLoggedIn(isAuthenticated);
            setUser(userData);
        });

        return unsubscribe;
    }, [refreshTrigger]); // Re-run effect when refreshTrigger changes

    const contextValue = {
        user,
        isLoggedIn,
        updateUserContext
    };

    return (
        <UserContext.Provider value={contextValue}>
            {children}
        </UserContext.Provider>
    );
};