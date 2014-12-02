#include <TinkerKit.h>
TKTouchSensor lickport1(I0);
TKTouchSensor lickport2(I1);

int flag1=0;
int flag2=0;
//int counter=0;

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
     //Reset flag
     pinMode(3,INPUT);
     //Solenoid output
     pinMode(13,OUTPUT);
     digitalWrite(13,LOW);
}

// Main loop
void loop(){
  
  //Reset flags
  if (digitalRead(3) ==1) {
    flag1=0;
    flag2=0;
  }
  
  //Set context1 flag on
  if(digitalRead(2) == 1) {
    flag1=1;
  }
  
  //Set context2 flag on
  if (digitalRead(5) == 1) {
    flag2=1;
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
    
  //Trigger solenoid 1
  if(flag1==1 & lickport1.read()==HIGH) {
    flag1=0;
    digitalWrite(10, HIGH);
    digitalWrite(13, HIGH);
    //counter=counter+1;
    delay(250);
    digitalWrite(10, LOW);
    digitalWrite(13, LOW);
    Serial.println("correct");
  } else if(flag1==1 & lickport2.read()==HIGH){
    flag1=0; 
    Serial.println("incorrect");
  }

  //Trigger solenoid 2
  if(flag2==1 & lickport2.read()==HIGH) {
    flag2=0;
    digitalWrite(12, HIGH);
    digitalWrite(13, HIGH);
    //counter=counter+1;
    delay(250);
    digitalWrite(12, LOW);
    digitalWrite(13, LOW);
    Serial.println("correct");
  } else if (flag2==1 & lickport1.read()==HIGH) {
    flag2=0;
    Serial.println("incorrect");
  }
  //Serial.println(counter);
}
