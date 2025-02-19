import { Text, View, StyleSheet } from "react-native";
import MoodRating from "../components/MoodRating";

export default function Index() {
  return (
    <View
      style={styles.container}
    >
      <MoodRating />
    </View>
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
