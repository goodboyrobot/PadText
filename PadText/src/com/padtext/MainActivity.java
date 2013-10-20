package com.padtext;

import java.util.Random;

import android.nfc.*;
import android.os.Bundle;
import android.os.Parcelable;
import android.app.Activity;
import android.content.ContentResolver;
import android.content.Context;
import android.content.Intent;
import android.telephony.SmsManager;
import android.telephony.SmsMessage;
import android.view.Menu;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

public class MainActivity extends Activity {
	
	SmsManager textManager;
	NfcAdapter mNfcAdapter;
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
    	textManager = SmsManager.getDefault();
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        mNfcAdapter = NfcAdapter.getDefaultAdapter(this);
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
    public void doEncrypt(View view)
    {
    	EditText msgContent = (EditText) findViewById(R.id.editMessegeContents);
    	String encrypted = OneTimePadEngine.EncryptString(msgContent.getText().toString(),OneTimePadEngine.testPad);
    	
    	
		
    	msgContent.setText(encrypted);

    	
    }
    public void doDecrypt(View view)
    {
    	EditText msgContent = (EditText) findViewById(R.id.editMessegeContents);
    	String decrypted = OneTimePadEngine.DecryptString(msgContent.getText().toString(),OneTimePadEngine.testPad);
    	
    	
		
    	msgContent.setText(decrypted);

    	
    }
    public void doRegenerate(View view)
    {
    	EditText msgContent = (EditText) findViewById(R.id.editMessegeContents);
    	String newpad = OneTimePadEngine.generatePad(msgContent.getText().toString());
    	
    	
		
    	msgContent.setText(newpad);
    }
    public void doNFCsend(View view)
    {
    	EditText msgContent = (EditText) findViewById(R.id.editMessegeContents);
    	NdefMessage msg = new NdefMessage(
    			
    			new NdefRecord[]
    					{
    					NdefRecord.createMime("application/com.padtext",msgContent.getText().toString().getBytes()),
    					//NdefRecord.createMime("text/plain", "this is a demo pad".getBytes()),
    					NdefRecord.createApplicationRecord("com.padtext")
    					});
    	mNfcAdapter.setNdefPushMessage(msg, this);
    	
    	
    }
    @Override
    public void onResume() {
        super.onResume();
        // Check to see that the Activity started due to an Android Beam
        String temp = getIntent().getAction();
        
        if (NfcAdapter.ACTION_NDEF_DISCOVERED.equals(getIntent().getAction())) {
            processIntent(getIntent());
        }
        
    }

    @Override
    public void onNewIntent(Intent intent) {
        // onResume gets called after this to handle the intent
        setIntent(intent);
    }

    /**
     * Parses the NDEF Message from the intent and prints to the TextView
     */
    void processIntent(Intent intent) {
    	EditText msgContent = (EditText) findViewById(R.id.editMessegeContents);
        Parcelable[] rawMsgs = intent.getParcelableArrayExtra(
                NfcAdapter.EXTRA_NDEF_MESSAGES);
        // only one message sent during the beam
        NdefMessage msg = (NdefMessage) rawMsgs[0];
        // record 0 contains the MIME type, record 1 is the AAR, if present
        msgContent.setText(new String(msg.getRecords()[0].getPayload()));
    }
   
    	
    
    
}
