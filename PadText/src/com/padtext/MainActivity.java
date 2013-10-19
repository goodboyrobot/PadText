package com.padtext;

import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.telephony.SmsManager;
import android.view.Menu;
import android.view.View;
import android.widget.EditText;

public class MainActivity extends Activity {
	
	SmsManager textManager;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
    	textManager = SmsManager.getDefault();
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.activity_main, menu);
        return true;
    }
    
    public void sendText(View view)
    {
    	EditText phoneNum = (EditText) findViewById(R.id.editPhoneNum);
    	EditText msgContent = (EditText) findViewById(R.id.editMessegeContents);

    	textManager.sendTextMessage(phoneNum.getText().toString(), "4063965236",msgContent.getText().toString(), null, null);
 	
    	
    }
}
