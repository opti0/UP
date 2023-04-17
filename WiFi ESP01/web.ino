#include <ESP8266WiFi.h>

const char* ssid = "ssid";//Write your network ssid
const char* password = "password";//Network password

int ledPin = D7;
WiFiServer server(80);

void setup() {
  
  pinMode(ledPin,OUTPUT);
  digitalWrite(ledPin,LOW);//We start with the led turned off.

  Serial.begin(115200);
  Serial.println();
  Serial.print("Wifi connecting to ");
  Serial.println( ssid );

  WiFi.begin(ssid,password);//connecting to wifi

  Serial.println();
  Serial.print("Connecting");//connecting

  while( WiFi.status() != WL_CONNECTED ){
      delay(500);
      Serial.print(".");        
  }

  
  Serial.println();

  Serial.println("Wifi Connected Success!");//If network connection is success we will see these messages
  Serial.print("NodeMCU IP Address : ");
  Serial.println(WiFi.localIP() );

  server.begin();
  Serial.println("NodeMCU Server started");//server has started

  // Print the IP address
  Serial.print("Use this URL to connect: ");
  Serial.print("http://");
  Serial.print(WiFi.localIP());
  Serial.println("/");
  
}

void loop() {

    // Check if a client has connected
  WiFiClient client = server.available();
  if (!client) {
    return;
  }

  while(!client.available()){
    delay(1);
  }

   // Read the first line of the request
  String request = client.readStringUntil('\r');
  Serial.println(request);
  client.flush();

   // Match the request
 
  int value = LOW;
  if (request.indexOf("/LED=ON") != -1)  {
    digitalWrite(ledPin, HIGH);
    value = HIGH;
  }
  if (request.indexOf("/LED=OFF") != -1)  {
    digitalWrite(ledPin, LOW);
    value = LOW;
  }

  // Return the response
  client.println("HTTP/1.1 200 OK");
  client.println("Content-Type: text/html");
  client.println("");
  client.println("<!DOCTYPE HTML>");
  client.println("<html>");
  client.println("<body>");
  client.println("<h1>LED Control</h1>");
  client.println("<p>Made by Shaurya Pratap Singh</p>");
 
  client.print("Led pin is now: ");
 
  if(value == HIGH) {
    client.print("On");
  } else {
    client.print("Off");
  }
  client.println("<br><br>");
  client.println("<a href=\"/LED=ON\"\"><button>Turn On </button></a>");
  client.println("<a href=\"/LED=OFF\"\"><button>Turn Off </button></a><br />");  
  client.println("</html>");
 
  delay(1);
  Serial.println("Client disonnected");
  Serial.println("");
  
}
