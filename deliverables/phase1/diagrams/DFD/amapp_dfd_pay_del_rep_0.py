from pytm import TM, Actor, Server, Dataflow, Boundary

tm = TM("Order Payments & Deliveries Report API Level 0")
tm.description = "Order Payments & Deliveries Report API Context Diagram Level 0"
tm.isOrdered = True

# Fronteiras
internet = Boundary("Internet")
server_boundary = Boundary("AMAPP System")

# Entidades externas
co_producer = Actor("CoProducer")
co_producer.inBoundary = internet

admin = Actor("AMAPP Administrator")
admin.inBoundary = internet

# Sistema principal
report_api = Server("AMAPP.API")
report_api.OS = "Windows"
report_api.protocol = "HTTPS"
report_api.inBoundary = server_boundary

# Define fluxos de dados
Dataflow(co_producer, report_api, "Own Report Request (PDF history of Payments & Deliveries)")
Dataflow(admin, report_api, "Report Request for Any CoProducer")
Dataflow(report_api, co_producer, "PDF Report (Binary Stream)")
Dataflow(report_api, admin, "PDF Report (Binary Stream)")

tm.process()