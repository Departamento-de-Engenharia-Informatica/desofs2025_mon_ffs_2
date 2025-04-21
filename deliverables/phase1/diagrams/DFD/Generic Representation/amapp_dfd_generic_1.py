#!/usr/bin/env python3

from pytm import TM, Actor, Boundary, Dataflow, Datastore, Process, Server, Data

# Initialize the threat model for Level 1 DFD
tm = TM("AMAP/CSA System - Level 1 DFD")
tm.description = "Detailed view of the AMAP/CSA system for sustainable agriculture"
tm.isOrdered = True  

# Define the boundaries
local_boundary = Boundary("Localhost")
system_boundary = Boundary("AMAP System")
system_boundary.inBoundary = local_boundary

db_boundary = Boundary("Database Server")

# Define the actors
consumer = Actor("Consumer (Co-Producer)")
producer = Actor("Producer")
admin = Actor("AMAP Administrator")

# Define the system components in Level 1
amap_api = Process("AMAP API")
amap_api.inBoundary = system_boundary
amap_api.description = "Core API services for AMAP system"

# API interface
amap_db_api = Server("AmapDB_API")
amap_db_api.inBoundary = system_boundary
amap_db_api.description = "API interface for database operations"

# External database
amap_database = Datastore("AMAP Database")
amap_database.inBoundary = db_boundary
amap_database.isExternal = True
amap_database.description = "Central database for all AMAP data"

# Define Data objects
crud_data = Data("CRUD Operations")
crud_data.description = "CRUD operations for orders, products, users"

sql_queries = Data("SQL Queries")
sql_queries.description = "SQL queries, stored procedures"

result_sets = Data("Result Sets")
result_sets.description = "Result sets, status codes"

request_data = Data("Request Data")
request_data.description = "Requested data records"


# 1. Consumer request
consumer_to_api = Dataflow(consumer, amap_api, "API Requests")
consumer_to_api.description = "Consumer requests for products, orders, etc."

# 2. Producer request
producer_to_api = Dataflow(producer, amap_api, "API Requests")
producer_to_api.description = "Producer operations for product management"

# 3. Admin request
admin_to_api = Dataflow(admin, amap_api, "API Requests")
admin_to_api.description = "Administrative operations"

# 4. API to DB API
api_to_db_api = Dataflow(amap_api, amap_db_api, "Database Requests")
api_to_db_api.description = "Requests for data operations"
api_to_db_api.data = crud_data  

# 5. DB API to Database
db_api_to_db = Dataflow(amap_db_api, amap_database, "Database Queries")
db_api_to_db.description = "Structured queries to database"
db_api_to_db.data = sql_queries  

# 6. Database to DB API
db_to_db_api = Dataflow(amap_database, amap_db_api, "Query Results")
db_to_db_api.description = "Results of database operations"
db_to_db_api.data = result_sets  

# 7. DB API to API
db_api_to_api = Dataflow(amap_db_api, amap_api, "Data Results")
db_api_to_api.description = "Results from database operations"
db_api_to_api.data = request_data 

# 8. API to Admin response
api_to_admin = Dataflow(amap_api, admin, "API Responses")
api_to_admin.description = "Responses to admin requests"

# 9. API to Producer response
api_to_producer = Dataflow(amap_api, producer, "API Responses")
api_to_producer.description = "Responses to producer requests"

# 10. API to Consumer response
api_to_consumer = Dataflow(amap_api, consumer, "API Responses")
api_to_consumer.description = "Responses to consumer requests"

# Generate the diagram
tm.process()