import React, { useRef } from 'react';
import { View, Text, StyleSheet, FlatList, } from 'react-native';
import Card from '../components/Prompts/Card';
import prompts from '../../data/prompts'

const Settings: React.FC = () => {
    const flatListRef = useRef<FlatList>(null);

    const scrollToIndex = (index: number) => {
        flatListRef.current?.scrollToIndex({ animated: true, index });
    };
    return (
        <View style={styles.container}>
            <Text style={styles.title}>Prompts</Text>
            <Text style={styles.text}>Scroll through for some positive reminders!</Text>

            {/* Gallery of prompt cards */}
            <FlatList
                data={prompts}
                renderItem={({ item }) => (
                    <Card text={item.text} />
                )}
                keyExtractor={(item, index) => index.toString()}
                horizontal
                pagingEnabled
                showsHorizontalScrollIndicator={true}
                contentContainerStyle={styles.gallery}
                ItemSeparatorComponent={() => <View style={{ width: 30 }} />} // Add space between cards
            />
        </View >
    );
};

export default Settings;
const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        backgroundColor: '#fff',
        justifyContent: 'center',
        alignItems: 'center',
    },
    title: {
        fontSize: 30,
        fontWeight: 'bold',
        marginBottom: 20,
        textAlign: 'center',
    },
    text: {
        fontSize: 20,
        color: '#00000',
        textAlign: 'justify',
        flexWrap: 'wrap',
    },
    gallery: {
        paddingHorizontal: 20,
        justifyContent: 'center',
        alignItems: 'center',
    }
});