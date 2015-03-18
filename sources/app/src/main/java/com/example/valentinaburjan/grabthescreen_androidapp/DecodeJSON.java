package com.example.valentinaburjan.grabthescreen_androidapp;


public class DecodeJSON {

    String json;

    public DecodeJSON(String json) {
        this.json = json;
    }

    public DecodeJSON() {

    }

    public String getJson() {
        return json;
    }

    public void setJson(String json) {
        this.json = json;
    }

    //Decode the Base64-encoded data in input and return the data in a new byte array.

    //The padding '=' characters at the end are considered optional, but if any are present, there must be the correct number of them.

    // str	the input String to decode, which is converted to bytes using the default charset
    // flags	controls certain features of the decoded output. Pass DEFAULT to decode standard Base64.
    //Throws
    // IllegalArgumentException	if the input contains incorrect padding

    public static byte[] decode(String str, int flags) {

        // String inputString;
        //String decoded = new String(Base64.decode(inputString));
        return null;
    }
    // Converting the JSON string to a JSON object.
    // Extract the String under data key.
    // Make sure that starts with image/png so you know is a png image.
    // Make sure that contains base64 string, so you know that data must be decoded.
    // Decode the data after base64 string to get the image.
}
