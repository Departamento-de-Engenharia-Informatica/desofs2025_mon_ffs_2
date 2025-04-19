from pytm import TM, Actor, Process, Datastore, Dataflow, Boundary

tm = TM("Order Payments & Deliveries Report API Level 1")
tm.description = "Order Payments & Deliveries Report API Detailed Diagram Level 1"
tm.isOrdered = True

# Fronteiras
internet = Boundary("Internet")
server = Boundary("AMAPP System")
dbserver = Boundary("DB Server")

# Entidades externas
co_producer = Actor("CoProducer")
co_producer.inBoundary = internet

admin = Actor("AMAPP Administrator")
admin.inBoundary = internet

# Componentes internos
api_endpoint = Process("AMAPP API Endpoint")
api_endpoint.inBoundary = server

report_engine = Process("Report Generation Engine")
report_engine.inBoundary = server

database = Datastore("Report Data DB")
database.inBoundary = dbserver
database.isSql = True

# Fluxos de dados
Dataflow(co_producer, api_endpoint, "Request own report")
Dataflow(admin, api_endpoint, "Request report for specific CoProducer")
Dataflow(api_endpoint, report_engine, "Forward authorized request")
Dataflow(report_engine, database, "Query data (filtered by role)")
Dataflow(database, report_engine, "Return report data")
Dataflow(report_engine, api_endpoint, "Generated PDF (binary stream)")
Dataflow(api_endpoint, co_producer, "PDF Report (own data only)")
Dataflow(api_endpoint, admin, "PDF Report (selected CoProducer data)")

tm.process()