// Declare variables
int led_flag = 0;
int zap_flag = 0;

// Setup
void setup(){
     Serial.begin(9600);

     pinMode(2,INPUT); // for water
     pinMode(11,OUTPUT);
     digitalWrite(11,HIGH);

     pinMode(5,INPUT); // for shocking
     pinMode(10,OUTPUT);
     digitalWrite(10,LOW);
     
     pinMode(4,INPUT); // for LED
     pinMode(8,OUTPUT);
     digitalWrite(8,LOW);
}

// Main loop
void loop(){

    if(digitalRead(2) == 1) {
       digitalWrite(11, LOW);
       delay(1000);
       digitalWrite(11, HIGH);
    }

    // ZAP on if pin  5 high and zap_flag off
     if(digitalRead(5) == 1 & zap_flag == 0){
       zap_flag = 1;
       digitalWrite(10, HIGH);
     }
     // ZAP off if pin 5 low and zap_flag on
     else if(digitalRead(5) == 0 & zap_flag == 1) {
       zap_flag = 0;
       digitalWrite(10, LOW);
     }
     
     // LED on if pin 4 is high and led_flag off
     if(digitalRead(4) == 1 & led_flag ==0){
        led_flag = 1;
        digitalWrite(8, HIGH);
     }
     
     // LED off if pin 4 is low and led_flag on
     else if (digitalRead(4) == 0 & led_flag ==1) {
         led_flag = 0;
         digitalWrite(8, LOW);
     }
}
