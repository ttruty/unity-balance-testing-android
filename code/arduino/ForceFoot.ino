// include the bluefruitlib
#include <bluefruit.h>

#define forceToe  A0
#define forceHeel A1
#define PIN_VBAT A7

BLEDis bledis;
BLEUart bleuart;
BLEBas  blebas;  // battery

//Battery 
uint32_t vbat_pin = PIN_VBAT;   

#define VBAT_MV_PER_LSB   (0.73242188F)   // 3.0V ADC range and 12-bit ADC resolution = 3000mV/4096

#ifdef NRF52840_XXAA    // if this is for nrf52840
#define VBAT_DIVIDER      (0.5F)               // 150K + 150K voltage divider on VBAT
#define VBAT_DIVIDER_COMP (2.0F)          // Compensation factor for the VBAT divider
#else
#define VBAT_DIVIDER      (0.71275837F)   // 2M + 0.806M voltage divider on VBAT = (2M / (0.806M + 2M))
#define VBAT_DIVIDER_COMP (1.403F)        // Compensation factor for the VBAT divider
#endif
 
#define REAL_VBAT_MV_PER_LSB (VBAT_DIVIDER_COMP * VBAT_MV_PER_LSB)

#define LED_PIN 13
bool blinkState = false;

float readVBAT(void) {
  float raw;
 
  // Set the analog reference to 3.0V (default = 3.6V)
  analogReference(AR_INTERNAL_3_0);
 
  // Set the resolution to 12-bit (0..4095)
  analogReadResolution(12); // Can be 8, 10, 12 or 14
 
  // Let the ADC settle
  delay(1);
 
  // Get the raw 12-bit, 0..3000mV ADC value
  raw = analogRead(vbat_pin);
 
  // Set the ADC back to the default settings
  analogReference(AR_DEFAULT);
  analogReadResolution(10);
 
  // Convert the raw value to compensated mv, taking the resistor-
  // divider into account (providing the actual LIPO voltage)
  // ADC range is 0..3000mV and resolution is 12-bit (0..4095)
  return raw * REAL_VBAT_MV_PER_LSB;
}
 
uint8_t mvToPercent(float mvolts) {
  if(mvolts<3300)
    return 0;
 
  if(mvolts <3600) {
    mvolts -= 3300;
    return mvolts/30;
  }
 
  mvolts -= 3600;
  return 10 + (mvolts * 0.15F );  // thats mvolts /6.66666666
}



void setup() {

    // initialize serial communication
    // (38400 chosen because it works as well at 8MHz as it does at 16MHz, but
    // it's really up to you depending on your project)
    Serial.begin(115200);
 
    while ( !Serial ) delay(10);   // for nrf52840 with native usb

    Bluefruit.autoConnLed(true);

    float batt = readVBAT(); // battery
    uint8_t battPer = mvToPercent(batt);
    Serial.print("Battery = ");
    Serial.println(String(battPer));

    Bluefruit.begin();
    Bluefruit.setTxPower(4);    // Check bluefruit.h for supported values
    Bluefruit.setName("OculusLeft"); //RENAME TO TARGET
    Bluefruit.Periph.setConnectCallback(connect_callback);
    Bluefruit.Periph.setDisconnectCallback(disconnect_callback);
  
    // Configure and Start Device Information Service
    bledis.setManufacturer("Adafruit Industries");
    bledis.setModel("Bluefruit Feather52");
    bledis.begin();
    // Start BLE Battery Service
    blebas.begin();

    // Configure and Start BLE Uart Service
    bleuart.begin();
  
    // Set up and start advertising
    startAdv();

    // configure Arduino LED pin for output
    pinMode(LED_PIN, OUTPUT);
}

void loop() {   
   
    if (Bluefruit.connected() && bleuart.notifyEnabled())
    {
        float batt = readVBAT(); // battery
        blebas.write(batt);//    bleuart.print(delimit);
    }
    
    
    
    while (Bluefruit.connected())
    {     
      Serial.println("Stepping");
      int forceToeRead = analogRead(forceToe);    
       int forceHeelRead = analogRead(forceHeel);    
        
      // blink LED to indicate activity
      blinkState = !blinkState;
      digitalWrite(LED_PIN, blinkState);    
      int forceToeInt  = forceToeRead;
      int forceHeelInt  = forceHeelRead;
        
      //bleuart.print(forceToeInt);  
      //bleuart.print(":");
      //bleuart.println(forceHeelInt); 

      uint8_t buf[] = {(uint8_t)(forceToeInt >> 8), (uint8_t)(forceToeInt & 0xFF),
                           (uint8_t)(forceHeelInt >> 8), (uint8_t)(forceHeelInt & 0xFF)};
       // int count = Serial.readBytes(buf, sizeof(buf));
       bleuart.write( buf, sizeof(buf));
      
    }
}

void startAdv(void)
{
  Bluefruit.Advertising.addFlags(BLE_GAP_ADV_FLAGS_LE_ONLY_GENERAL_DISC_MODE);
  Bluefruit.Advertising.addTxPower();

  // Include bleuart 128-bit uuid
  Bluefruit.Advertising.addService(bleuart);

  // There is no room for Name in Advertising packet
  // Use Scan response for Name
  Bluefruit.ScanResponse.addName();
  
  /* Start Advertising
   * - Enable auto advertising if disconnected
   * - Interval:  fast mode = 20 ms, slow mode = 152.5 ms
   * - Timeout for fast mode is 30 seconds
   * - Start(timeout) with timeout = 0 will advertise forever (until connected)
   * 
   * For recommended advertising interval
   * https://developer.apple.com/library/content/qa/qa1931/_index.html   
   */
  Bluefruit.Advertising.restartOnDisconnect(true);
  Bluefruit.Advertising.setInterval(32, 244);    // in unit of 0.625 ms
  Bluefruit.Advertising.setFastTimeout(30);      // number of seconds in fast mode
  Bluefruit.Advertising.start(0);                // 0 = Don't stop advertising after n seconds  
}

void connect_callback(uint16_t conn_handle)
{
  (void) conn_handle;
  Serial.println("Connected");
}

/**
 * Callback invoked when a connection is dropped
 * @param conn_handle connection where this event happens
 * @param reason is a BLE_HCI_STATUS_CODE which can be found in ble_hci.h
 */
void disconnect_callback(uint16_t conn_handle, uint8_t reason)
{
  (void) conn_handle;
  (void) reason;

  Serial.println();
  Serial.println("Disconnected");
}

/**************************************************************************/
/*!
    @brief  Get user input from Serial
*/
/**************************************************************************/
char* getUserInput(void)
{
  static char inputs[64+1];
  memset(inputs, 0, sizeof(inputs));

  // wait until data is available
  while( Serial.available() == 0 ) { delay(1); }

  uint8_t count=0;
  do
  {
    count += Serial.readBytes(inputs+count, 64);
  } while( (count < 64) && Serial.available() );

  return inputs;
}
