// Setup
void setup(){
     Serial.begin(9600);
     //Solenoid 1
     pinMode(2,INPUT);
     pinMode(10,OUTPUT);
     digitalWrite(10, HIGH);
     //Solenoid 2
     pinMode(5,INPUT);
     pinMode(12, OUTPUT);
     digitalWrite(12,HIGH);
}

// Main loop
void loop(){
  //Solenoid 1
    if(digitalRead(2) == 1) {
       digitalWrite(10, LOW);
       delay(1000);
       digitalWrite(10, HIGH);
    }
  //Solenoid 2
    if(digitalRead(5) == 1) {
       digitalWrite(12, LOW);
       delay(1000);
       digitalWrite(12, HIGH);
    }
    
}
