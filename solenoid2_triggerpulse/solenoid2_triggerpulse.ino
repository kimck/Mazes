#include <TinkerKit.h>

#include <TinkerKit.h>
TKTouchSensor lickport1(I0);
TKTouchSensor lickport2(I1);

// Setup
void setup(){
     Serial.begin(9600);
     //Solenoid 1
     pinMode(2,INPUT);
     pinMode(10,OUTPUT);
     digitalWrite(10, LOW);
     pinMode(7,OUTPUT);
     digitalWrite(7,LOW);
     //Solenoid 2
     pinMode(5,INPUT);
     pinMode(12, OUTPUT);
     digitalWrite(12,LOW);
     pinMode(8,OUTPUT);
     digitalWrite(8,LOW);
}

// Main loop
void loop(){
  //Solenoid 1
    if(digitalRead(2) == 1) {
       digitalWrite(10, HIGH);
       delay(250);
       digitalWrite(10, LOW);
    }
  //Solenoid 2
    if(digitalRead(5) == 1) {
       digitalWrite(12, HIGH);
       delay(250);
       digitalWrite(12, LOW);
    }
    
    //Log lickport 1    
  if (lickport1.read()==HIGH) {
    digitalWrite(7,HIGH);  
  } else{
    digitalWrite(7,LOW);
  }
  
  //Log lickport 2
  if (lickport2.read()==HIGH) {
    digitalWrite(8,HIGH);
  } else{
    digitalWrite(8,LOW);
  }
    
}
