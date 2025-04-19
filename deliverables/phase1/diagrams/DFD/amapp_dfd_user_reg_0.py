from pytm import TM, Actor, Server, Dataflow, Boundary

tm = TM("User Registration Level 0")
tm.description = "User Registration Level 0"
tm.isOrdered = True

# Fronteiras
internet = Boundary("Internet")
server = Boundary("Server")

# Entidades externas
user = Actor("User")
admin = Actor("AMAPP Admin")

# Sistema principal
server = Server("AMAPP System")
server.OS = "Windows"
server.protocol = "HTTPS"

Dataflow(user, server, "Submit registration request")
Dataflow(server, admin, "Send approval request")
Dataflow(admin, server, "Approval decision")
Dataflow(server, user, "Notify decision")

tm.process()
