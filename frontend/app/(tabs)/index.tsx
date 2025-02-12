import { Text, View, StyleSheet } from "react-native";
import { Link } from "expo-router";
import Header from "../components/Header";

export default function Index() {
  return (
    <View
      style={styles.container}
    >
      <Text style={styles.text}>This is a home screen</Text>
      <Link href="/about" style={styles.button}> Go to About Screen</Link>
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
