# **OrderUP**

This project will be implemented for a business and therefore may **CHANGE AT ANYTIME**, based upon evolving requirements.

A C# based system for communication of custom order details between a sales floor and production floor. CRUD operations are implemented upon a list of items to be displayed above a fabrication station. A console program will serve as proof of concept on the basic function of the program and save the current working list of items to a JSON file upon exiting. This list will be loaded into the program upon launch. 

Later development will seek to implement a touchscreen interface for optimal user experience, and utilize an API and Database to handle data persistance, upon approval by the client.

## Features 
---
- Master Loop used for Main Menu
- A list of custom class LineItem is used to hold current orders
- Data is Read/Written to an external JSON file for storage