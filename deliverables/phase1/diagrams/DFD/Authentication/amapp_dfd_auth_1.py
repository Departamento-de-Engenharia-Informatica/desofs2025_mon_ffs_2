from pytm import TM, Actor, Server, Datastore, Dataflow, Boundary, Data

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

# Data objects
login_credentials = Data("Login Credentials")
login_credentials.description = "User login data: username and password."

validated_credentials = Data("Validated Credentials")
validated_credentials.description = "Validated user data after login check."

user_record = Data("User Record")
user_record.description = "User's profile and account status information."

jwt_token = Data("JWT Authentication Token")
jwt_token.description = "JWT token issued upon successful authentication."

# Data Flows
Dataflow(user, auth_server, "Submit login credentials", data=login_credentials, protocol="HTTPS", port=443)
Dataflow(auth_server, user_db, "Validate credentials", data=login_credentials, protocol="SQL/TLS", port=1433)
Dataflow(user_db, auth_server, "Return user record", data=user_record, protocol="SQL/TLS", port=1433)
Dataflow(auth_server, user, "Authentication JWT Token", data=jwt_token, protocol="HTTPS", port=443)


tm.process()
