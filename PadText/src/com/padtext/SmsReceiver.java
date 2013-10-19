package com.padtext;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.telephony.SmsMessage;
import android.widget.Toast;

public class SmsReceiver extends BroadcastReceiver {
	 public void onReceive(Context context, Intent intent)
	    {
	    	Bundle extras = intent.getExtras();
	    	
	    	//String message = "";
	    	
	    	
	    	if (extras != null)
	    	{
	    		Object[] attachments = (Object[]) extras.get("pdus");
	    		
	        	//ContentResolver contentResolver = context.getContentResolver();
	        	
	        	for (int i = 0; i < attachments.length;i++)
	        	{
	        		SmsMessage sms = SmsMessage.createFromPdu((byte[])attachments[i]);
	        		
	        		Toast.makeText(context, sms.getDisplayMessageBody(), Toast.LENGTH_LONG).show();
	        		
	        	}

	    		
	    		
	    		
	    		
	    	}
	    }

}
