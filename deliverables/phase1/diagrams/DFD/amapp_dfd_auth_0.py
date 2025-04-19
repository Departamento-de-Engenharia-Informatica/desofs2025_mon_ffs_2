from pytm import TM, Actor, Server, Dataflow, Boundary

tm = TM("DFD Level 0 - User Authentication")
tm.description = "Context-level DFD for user authentication."

# External Actor
user = Actor("User")

# Internal System
auth_server = Server("AMAPP System")

# Data Flows
Dataflow(user, auth_server, "Submit login credentials")
Dataflow(auth_server, user, "Authentication JWT Token")

tm.process()
