// Setup
void setup(){
     Serial.begin(9600);

     pinMode(2,INPUT); // for water
     pinMode(11,OUTPUT);
     digitalWrite(11,HIGH);

     pinMode(5,INPUT); // for shocking
     pinMode(10,OUTPUT);
     digitalWrite(10,LOW);
     
     pinMode(4,INPUT); // for odor1
     pinMode(8,OUTPUT);
     digitalWrite(8,LOW);
     
     pinMode(6,INPUT); // for odor2
     pinMode(12,OUTPUT);
     digitalWrite(12,LOW);
}

// Main loop
void loop(){

    // open water solenoid for 1 s
    if(digitalRead(2) == 1) {
       digitalWrite(11, LOW);
       delay(1000);
       digitalWrite(11, HIGH);
    }

    // turn shock on for 1 s
     if(digitalRead(5) == 1){
       digitalWrite(10, HIGH);
       delay(1000);
       digitalWrite(10, LOW);
     }
     
     // turn odor1 on for 1 s
     if(digitalRead(4) == 1){
        digitalWrite(8, HIGH);
        delay(1);
        digitalWrite(8, LOW);
        delay(1000);
        digitalWrite(8,HIGH);
        delay(1);
        digitalWrite(8,LOW);
     }
     
     // turn odor2 on for 1 s
     if(digitalRead(6) == 1){
        digitalWrite(12, HIGH);
        delay(1);
        digitalWrite(12, LOW);
        delay(1000);
        digitalWrite(12,HIGH);
        delay(1);
        digitalWrite(12,LOW);
     }
}
