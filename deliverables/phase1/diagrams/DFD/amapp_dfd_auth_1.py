from pytm import TM, Actor, Server, Datastore, Dataflow, Boundary

tm = TM("DFD Level 1 - User Authentication")
tm.description = "Detailed DFD for user login/authentication process."

# Boundaries
internet = Boundary("Internet")
server = Boundary("AMAPP System")
dbserver = Boundary("DB Server")

# Actors
user = Actor("User")
user.inBoundary = internet

# Internal Components
auth_server = Server("AMAPP API")
auth_server.inBoundary = server

user_db = Datastore("AMAPP DB")
user_db.inBoundary = dbserver
user_db.isSql = True

# Data Flows
Dataflow(user, auth_server, "Submit login credentials")
Dataflow(auth_server, user_db, "Validate credentials")
Dataflow(user_db, auth_server, "Return user record")
Dataflow(auth_server, user, "Authentication JWT Token")

tm.process()
