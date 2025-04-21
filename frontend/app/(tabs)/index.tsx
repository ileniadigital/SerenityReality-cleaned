import { Text, View, StyleSheet, FlatList } from "react-native";
import MoodRating from "../components/Home/MoodRating";
import Scene from "../components/Home/Scene/Scene";
import scenes from "../../data/scenes";

export default function Index() {
  return (
    <View style={styles.container} >
      <MoodRating />
      <FlatList
        data={scenes}
        renderItem={({ item }) => <Scene title={item.title} description={item.description} packageName={item.packageName} />}
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
