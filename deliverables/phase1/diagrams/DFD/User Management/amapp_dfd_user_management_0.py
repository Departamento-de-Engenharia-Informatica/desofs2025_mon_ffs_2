from pytm import TM, Actor, Server, Dataflow, Boundary

tm = TM("DFD Level 0 - User and Permission Management")
tm.description = "Context-level DFD for managing user accounts and their roles/permissions."

# External Actor
admin = Actor("Administrator")

# Internal System
user_permission_module = Server("AMAPP System")

# Data Flows
Dataflow(admin, user_permission_module, "Send request to manage users or roles")
Dataflow(user_permission_module, admin, "Send operation result or data")

tm.process()