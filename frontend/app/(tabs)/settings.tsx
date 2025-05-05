// This is the settings page for the notifications (currently not used) and the dark mode toggle (not implemented)
import React from 'react';
import { View, Text, StyleSheet, Switch, ScrollView } from 'react-native';
import { ActionButton } from '../components/Account/ActionButton';


const Settings: React.FC = () => {
    const [notificationsEnabled, setNotificationsEnabled] = React.useState(false);

    const toggleNotifications = () => {
        setNotificationsEnabled((prev) => !prev);
    };

    return (
        <ScrollView style={styles.container}>
            <Text style={styles.title}>Settings</Text>

            {/* Notifications Toggle */}
            <View style={styles.settingItem}>
                <Text style={styles.label}>Enable Notifications</Text>
                <Switch
                    value={notificationsEnabled}
                    onValueChange={toggleNotifications}
                />
            </View>

            {/* Dark mode*/}
            {/* <View style={styles.settingItem}>
                <Text style={styles.label}>Dark Mode</Text>
                <Switch
                    value={false} // Replace with state if needed
                    onValueChange={() => { }}
                />
            </View> */}

            {/* Save Button */}
            <ActionButton
                title="Save Changes"
                onPress={() => {
                    // Handle save logic
                    alert('Settings saved!');
                }}
            />
        </ScrollView>
    );
};

export default Settings;
const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        backgroundColor: '#fff',
    },
    title: {
        fontSize: 30,
        fontWeight: 'bold',
        marginBottom: 20,
    },
    settingItem: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginBottom: 20,
    },
    label: {
        fontSize: 18,
        fontWeight: 'bold',
    },
});