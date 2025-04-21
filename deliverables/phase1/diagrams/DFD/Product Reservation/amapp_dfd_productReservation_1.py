#!/usr/bin/env python3

from pytm import TM, Actor, Boundary, Dataflow, Process, Datastore

# Initialize the threat model
tm = TM("Consumer Purchase Products - Level 1")
tm.description = "Overview of consumer reserving agricultural products in AMAP/CSA system (consumer perspective only)"
tm.isOrdered = True

# Boundaries
internal_boundary = Boundary("AMAP System")
db_boundary = Boundary("Database Server")

# Actor
consumer = Actor("Co-Producer")
consumer.description = "End user reserving agricultural products"

# Processes
product_catalog = Process("1.0 Product Catalog")
product_catalog.inBoundary = internal_boundary
product_catalog.description = "Manages product listings and inventory"

order_management = Process("2.0 Order Management")
order_management.inBoundary = internal_boundary
order_management.description = "Handles order creation and processing"

reservation_processing = Process("3.0 Reservation Processing")
reservation_processing.inBoundary = internal_boundary
reservation_processing.description = "Manages product reservation requests"

delivery_management = Process("4.0 Delivery Management")
delivery_management.inBoundary = internal_boundary
delivery_management.description = "Handles product delivery coordination"

# Datastore
amap_database = Datastore("AMAP Database")
amap_database.inBoundary = db_boundary
amap_database.isExternal = True
amap_database.description = "Central database at vsgate-s1.dei.isep.ipp.pt:10279"

# --- Dataflows ---

# Product Catalog (Consumer)
Dataflow(consumer, product_catalog, "Browse Products", description="Co-Producer views available products")
Dataflow(product_catalog, consumer, "Product Information", description="Product listings and details")

# Product Catalog (DB)
Dataflow(amap_database, product_catalog, "Product Data", description="Retrieve product information")

# Order Management (Consumer)
Dataflow(consumer, order_management, "Place Order", description="Co-Producer submits order for products")
Dataflow(order_management, consumer, "Order Confirmation", description="Order details and confirmation")

# Order Management (Internal)
Dataflow(order_management, product_catalog, "Check Availability", description="Verify product availability")
Dataflow(product_catalog, order_management, "Availability Status", description="Product availability information")
Dataflow(order_management, product_catalog, "Update Inventory", description="Request inventory update after order")

# Order Management (DB)
Dataflow(order_management, amap_database, "Store Order", description="Save order details to database")
Dataflow(amap_database, order_management, "Order Details", description="Retrieve order information")

# Reservation Processing (Consumer)
Dataflow(consumer, reservation_processing, "Submit Reservation", description="Co-Producer submits product reservation request")
Dataflow(reservation_processing, consumer, "Reservation Confirmation", description="Confirmation of successful reservation")

# Reservation Processing (Internal)
Dataflow(order_management, reservation_processing, "Process Reservation", description="Request product reservation for order")
Dataflow(reservation_processing, order_management, "Reservation Status", description="Reservation outcome information")

# Reservation Processing (DB)
Dataflow(reservation_processing, amap_database, "Store Reservation", description="Record reservation details")
Dataflow(amap_database, reservation_processing, "Reservation Details", description="Retrieve reservation information")

# Delivery Management (Consumer)
Dataflow(delivery_management, consumer, "Delivery Notification", description="Notify co-producer about delivery details")

# Delivery Management (Internal)
Dataflow(order_management, delivery_management, "Delivery Request", description="Request delivery for reserved products")

# Delivery Management (DB)
Dataflow(delivery_management, amap_database, "Store Delivery Info", description="Record delivery details")
Dataflow(amap_database, delivery_management, "Delivery Data", description="Retrieve delivery information")

# Generate DFD
tm.process()