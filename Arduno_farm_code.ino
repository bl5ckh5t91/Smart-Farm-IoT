#include <Adafruit_Sensor.h>   // DHT22
#include <DHT.h>               // DHT11 
#define DHTPIN 2
#define DHTTYPE DHT11          // DHT 11 
int val = 0;
DHT dht = DHT(DHTPIN, DHTTYPE);//normal 16mhz Arduino:

void setup() {
  dht.begin();
    //fan 
  pinMode(8,OUTPUT);
  //Alarm Voice 
  pinMode(9,OUTPUT);

   //PomP Pin 
  pinMode(10,OUTPUT);
  Serial.begin(9600);
}

void loop() {
  
//---------------------Water level
  val = analogRead(A0);
  val=map(val,0,345,0,100);
  int valW=val; //save Water value
  Serial.println(val);
  if(val<20)
   digitalWrite(9,HIGH);
  else
   digitalWrite(9,LOW);
  delay(1000);
//--------------------------------------
  //---------------------TRBA
val = analogRead(A1);
val=map(val,0,1024,0,100);
  Serial.println(val*100);
if(val<96 && valW>20)
digitalWrite(10,HIGH); // run pomp
else
digitalWrite(10,LOW);
delay(1000);
 //---------------------DTH

  float h = dht.readHumidity();
  float t = dht.readTemperature();
  float f = dht.readTemperature(true);
  float hif = dht.computeHeatIndex(f, h);
  float hic = dht.computeHeatIndex(t, h, false);
  Serial.println(h*10000);
  Serial.println(t*1000000);
  if(h>50 && t>35)
  digitalWrite(8,HIGH); // run fan
  else
  digitalWrite(8,LOW);
  delay(1000);
}    
