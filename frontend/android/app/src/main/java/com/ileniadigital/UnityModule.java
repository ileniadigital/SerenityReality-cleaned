// Launch Unity and communicate with React
package  com.ileniadigital.serenityreality;

import android.app.Activity;
import android.content.Intent;

import com.facebook.react.bridge.ReactApplicationContext;
import com.facebook.react.bridge.ReactContextBaseJavaModule;
import com.facebook.react.bridge.ReactMethod;

public class UnityModule extends ReactContextBaseJavaModule {
    private final ReactApplicationContext reactContext;

    public UnityModule(ReactApplicationContext context) {
        super(context);
        this.reactContext = context;
    }

    @Override
    public String getName() {
        return "UnityLauncher"; // Name in React Native

    @ReactMethod
    public void launchUnityScene(String sceneName) {
        Activity activity = getCurrentActivity();
        if (activity != null) {
            Intent intent = new Intent(activity, UnityLauncherActivity.class);
            intent.putExtra("scene", sceneName);
            activity.startActivity(intent);
        }
    }
}
