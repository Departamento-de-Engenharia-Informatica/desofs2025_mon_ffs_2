from pytm import TM, Actor, Process, Dataflow, Data, Datastore, Boundary

# Initialize the Threat Model
tm = TM("AMAP Product Creation - Level 1")
tm.description = "Level 1 DFD for product creation in AMAP"

# External Entities (Actors)
producer = Actor("Producer")

# Main Processes
validate = Process("Validate Input")
store = Process("Store Product")
respond = Process("Send Response")

# Data Stores
db = Datastore("Product DB")

# Data Objects
raw_data = Data("Raw Product Info")
validated_data = Data("Validated Product")
result = Data("Operation Result")

# Trust Boundaries
producer_zone = Boundary("User Zone")
app_zone = Boundary("System Zone")
db_zone = Boundary("Database Zone")

# Assigning elements to boundaries
producer.inBoundary = producer_zone
validate.inBoundary = app_zone
store.inBoundary = app_zone
respond.inBoundary = app_zone
db.inBoundary = db_zone

# Simple Product Creation Flows
flow1 = Dataflow(producer, validate, "Submit Product")
flow1.protocol = "HTTPS"
flow1.dstPort = 443
flow1.data = raw_data

flow2 = Dataflow(validate, store, "Validated Data")
flow2.data = validated_data

flow3 = Dataflow(store, db, "Save to DB")
flow3.data = validated_data

flow4 = Dataflow(store, respond, "Operation Outcome")
flow4.data = result

flow5 = Dataflow(respond, producer, "Return Result")
flow5.protocol = "HTTPS"
flow5.dstPort = 443
flow5.data = result

# Process the model
tm.process()