import { Text, View, StyleSheet, FlatList } from "react-native";
import MoodRating from "../components/Home/MoodRating";
import Scene from "../components/Home/Scene";

const scenes = [
  { id: '1', title: 'Sea Breathing', description: 'Description for Scene 1' },
  { id: '2', title: 'Visualise your anxiety', description: 'Description for Scene 2' },
  { id: '3', title: 'Sleep', description: 'Description for Scene 3' },
  // { id: '4', title: 'Sea Breathing', description: 'Description for Scene 1' },
  // { id: '5', title: 'Visualise your anxiety', description: 'Description for Scene 2' },
  // { id: '6', title: 'Sleep', description: 'Description for Scene 3' },
]
export default function Index() {
  return (
    <View style={styles.container} >
      <MoodRating />
      <FlatList
        data={scenes}
        renderItem={({ item }) => <Scene title={item.title} description={item.description} />}
        keyExtractor={item => item.id}
      />
    </View >
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#F5F5F5",
    justifyContent: "center",
    alignItems: "center",
  },
  text: {
    color: "#000000",
  },
  button: {
    fontSize: 20,
    textDecorationLine: "underline",
    color: "#000000",
  }
});
