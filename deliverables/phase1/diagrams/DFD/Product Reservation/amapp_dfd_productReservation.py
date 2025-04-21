#!/usr/bin/env python3

from pytm import TM, Actor, Boundary, Dataflow, Process, Datastore

# Initialize the threat model for Level 0 DFD - Purchase/Subscribe Flow
tm = TM("Consumer Purchase Products - Level 0")
tm.description = "High-level view of consumer purchasing products in AMAP/CSA system"
tm.isOrdered = True  

# Define the actors
consumer = Actor("Consumer (Co-Producer)")
consumer.description = "End user purchasing agricultural products"

# Define the main system process (Level 0 - single process)
amap_api = Process("AMAP API")
amap_api.description = "Core API that handles product purchases"

# Define the external database
amap_database = Datastore("AMAP Database")
amap_database.description = "Central database at vsgate-s1.dei.isep.ipp.pt:10279"
amap_database.isExternal = True

# 1. Consumer to System
consumer_to_api = Dataflow(consumer, amap_api, "Purchase Request")
consumer_to_api.description = "Consumer browses products and places orders"
consumer_to_api.data = "Product selections, order details, payment info"

# 2. System to Database
api_to_db = Dataflow(amap_api, amap_database, "Data Operations")
api_to_db.description = "System stores and retrieves purchase data"
api_to_db.data = "Product queries, order storage, inventory updates"

# 3. Database to System
db_to_api = Dataflow(amap_database, amap_api, "Data Results")
db_to_api.description = "Database returns requested purchase data"
db_to_api.data = "Product data, order confirmations, inventory status"

# 4. System to Consumer
api_to_consumer = Dataflow(amap_api, consumer, "Purchase Response")
api_to_consumer.description = "System confirms orders and payments"
api_to_consumer.data = "Product listings, order confirmations, payment receipts"

# Generate the diagram
tm.process()