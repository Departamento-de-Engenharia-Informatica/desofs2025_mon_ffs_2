#!/usr/bin/env python3

from pytm import TM, Actor, Boundary, Dataflow, Datastore, Process, Server

# Initialize the threat model for Level 0 DFD
tm = TM("AMAP/CSA System - Level 0 DFD")
tm.description = "High-level overview of the AMAP/CSA system for sustainable agriculture"
tm.isOrdered = True  

# Define the actors
consumer = Actor("Consumer (Co-Producer)")
producer = Actor("Producer")
admin = Actor("AMAP Administrator")

# Define the system as a single process at Level 0
amap_system = Process("AMAP API") 
amap_system.description = "Core AMAP API that handles all business logic and operations"

# Define the external datastore
amap_db = Datastore("AMAP Database")
amap_db.description = "External database at vsgate-s1.dei.isep.ipp.pt:10279"
amap_db.isExternal = True

# Define the dataflows for Level 0
# Consumer interactions
consumer_to_system = Dataflow(consumer, amap_system, "Consumer Requests")
consumer_to_system.description = "Browse products, place orders, manage subscriptions"

system_to_consumer = Dataflow(amap_system, consumer, "System Responses")
system_to_consumer.description = "Product info, order confirmations, notifications"

# Producer interactions
producer_to_system = Dataflow(producer, amap_system, "Producer Requests")
producer_to_system.description = "Manage products, update inventory, process orders"

system_to_producer = Dataflow(amap_system, producer, "System Responses")
system_to_producer.description = "Order notifications, delivery schedules"

# Admin interactions
admin_to_system = Dataflow(admin, amap_system, "Admin Requests")
admin_to_system.description = "Manage users, organize deliveries, system config"

system_to_admin = Dataflow(amap_system, admin, "System Responses")
system_to_admin.description = "System status, user data, reports"

# Database interactions
system_to_db = Dataflow(amap_system, amap_db, "Data Queries")
system_to_db.description = "Read and write operations to database"
system_to_db.data = "User data, orders, products, inventory"

db_to_system = Dataflow(amap_db, amap_system, "Query Results")
db_to_system.description = "Results of database operations"
db_to_system.data = "Requested data records"

# Generate the diagram
tm.process()