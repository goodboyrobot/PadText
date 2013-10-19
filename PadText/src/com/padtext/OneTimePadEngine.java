package com.padtext;

import java.util.Collections;
import java.util.HashMap;
import java.util.Map;
import java.util.Random;

import android.widget.Toast;

public class OneTimePadEngine {
	static char dictionaryToChar[] = " @£$¥0èéùìòÇØøÅåΔ_ΦΓΛΩΠΨΣΘΞÆæßÉ!\"#¤%&'()*+,-./0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà".toCharArray();
	static final Map<Character,Integer> dictionaryToInt;
	public static String testPad = "9buU94WTlsl0TJ6tT1LM84527EmOkKpb8l8Wc4dPIJ3YfpFV7qyeE2GkYtLVJYJh9XPHUovEbCkZpqLNC6WJRBjBAO6uMyl6qK7uE17hMFi8MilZqjaKpf0sp51iA8T45HAmm6lIOsIn";
	
	static
	{
		Map<Character,Integer> dict = new HashMap<Character,Integer>();
		
		//create a reverse look up table for the dictionary
		for (int i = 0;i<dictionaryToChar.length;i++)
		{
			dict.put(dictionaryToChar[i], i);
		}
		dictionaryToInt = Collections.unmodifiableMap(dict);
		
	}
	// Encrypts a string with a given pad and pads out its length to match the pad
	public static String EncryptString(String message, String pad)
	{
		Character c;
		String encrypted_string = "";
		for (int i = 0; i<message.length();i++)
		{
			//Get the char
			c = message.charAt(i);
			//preform the translation operation on the character
			int index;
			index =  ( dictionaryToInt.get(c) + dictionaryToInt.get(pad.charAt(i)) ) % dictionaryToChar.length;
			c = dictionaryToChar[index];
			//concatinate it back onto the encrypted string	
			encrypted_string += c;
		}
		//now we have to pad out the end of the string with the remainder of the pad, we dont want people to be able to
		//perform some kind of statistical analysis on message lengths
		//WARNING, EXPOSING BARE PORTIONS OF THE PADS PROBABLY OPENS US UP FOR AN ATTACK LATER, FIX THIS
		for (int i = message.length(); i<pad.length();i++)
		{
			encrypted_string += pad.charAt(i);
		}
		
		return encrypted_string;
		
	}
	public static String DecryptString (String encrypted_message,String pad)
	{
		Character c;
		String plain_text = "";
		for (int i = 0; i<encrypted_message.length();i++)
		{
			//Get the char
			c = encrypted_message.charAt(i);
			//preform the translation operation on the character
			int index;
			index =  ( dictionaryToInt.get(c) - dictionaryToInt.get(pad.charAt(i)) ) % dictionaryToChar.length;
			if (index<0)
				index+= dictionaryToChar.length;
			c = dictionaryToChar[index];
			//concatinate it back onto the encrypted string	
			plain_text += c;
		}
		return plain_text;
		
		
	}

}
