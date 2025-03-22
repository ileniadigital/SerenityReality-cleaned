// Launches the Unity library
package  com.ileniadigital.serenityreality;

import android.content.Intent;
import android.os.Bundle;
import com.unity3d.player.UnityPlayerActivity;

public class UnityLauncher extends UnityPlayerActivity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    public String getIntentExtra(String key) {
        Intent intent = getIntent();
        return intent.getStringExtra(key);
    }
}
