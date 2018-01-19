OneMan WebService Project
=========================

This is example of simple SOAP service application build using old **System.Web.Services** library.

Test
----

This interface contains several methods for testing purposes.

http://localhost:50050/Test.asmx

http://localhost:50050/Test.asmx?WSDL


* HelloWorld

Returns simply "hello, world" text.

```xml
<?xml version="1.0" encoding="UTF-8"?>
<string xmlns="http://tempuri.org/">hello, world</string>
``` 

* Dummy 

One way method that does nothing. Additional message will appear in debug output. 

* Echo

Returns back input string.

```xml
<?xml version="1.0" encoding="UTF-8"?>
<string xmlns="http://tempuri.org/">1</string>
```

* Sleep 

Sleeps for specified time in seconds and then returns nothing.

```xml
<?xml version="1.0" encoding="utf-8"?><soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><soap:Body><SleepResponse xmlns="http://tempuri.org/" /></soap:Body></soap:Envelope>
```

* Throw

Throws internal exception.
