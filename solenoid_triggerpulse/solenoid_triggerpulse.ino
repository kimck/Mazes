// Setup
void setup(){
     Serial.begin(9600);
     pinMode(2,INPUT);
     pinMode(10,OUTPUT);
     digitalWrite(10, HIGH);
}

// Main loop
void loop(){
    if(digitalRead(2) == 1) {
       digitalWrite(10, LOW);
       delay(1000);
       digitalWrite(10, HIGH);
    }
}
