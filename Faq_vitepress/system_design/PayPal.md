# PayPal 


# Architecture
- Actor Model (Java)
	Actor - Thread (Process message and once free assign to any actor)
	Actor - Thread
	Note: The number of threads is proportional to the number of CPU cores. 
### Actor
	- Thread 
	- Mailbox (Process message in FIFO order)
	- They store the local state in the application server.
	
	
- consistent hashing[https://newsletter.systemdesign.one/p/what-is-consistent-hashing]