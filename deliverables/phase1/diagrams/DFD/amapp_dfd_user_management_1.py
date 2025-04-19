from pytm import TM, Actor, Server, Datastore, Dataflow, Boundary

tm = TM("DFD Level 1 - User and Permission Management")
tm.description = "Detailed DFD for managing users and assigning roles/permissions."

# Boundaries
internet = Boundary("Internet")
server = Boundary("AMAPP System")
dbserver = Boundary("DB Server")

# Actors
admin = Actor("Administrator")
admin.inBoundary = internet

# Internal Components
user_permission_server = Server("AMAPP API")
user_permission_server.inBoundary = server

database = Datastore("AMAPP DB")
database.inBoundary = dbserver
database.isSql = True

# Data Flows
Dataflow(admin, user_permission_server, "Submit user or permission management request")
Dataflow(user_permission_server, database, "Create/update/delete user account")
Dataflow(user_permission_server, database, "Assign/update/retrieve roles and permissions")
Dataflow(database, user_permission_server, "Return user data")
Dataflow(database, user_permission_server, "Return permission/role data")
Dataflow(user_permission_server, admin, "Send operation confirmation or results")

tm.process()
