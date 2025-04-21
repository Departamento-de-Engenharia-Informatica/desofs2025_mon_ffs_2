from pytm import TM, Actor, Server, Process, Datastore, Dataflow, Boundary, Data

tm = TM("User Registration Level 0")
tm.description = "User Registration Level 0"
tm.isOrdered = True

# Fronteiras
internet = Boundary("Internet")
server = Boundary("AMAPP System")
dbserver = Boundary("DB Server")

# Entidades externas
user = Actor("User")
user.inBoundary = internet

admin = Actor("AMAPP Admin")
admin.inBoundary = internet

# Componentes internos
app_server = Process("AMAPP API")
app_server.inBoundary = server

database = Datastore("AMAPP DB")
database.inBoundary = dbserver
database.isSql = True

# Data objects
registration_info = Data("Registration Info")
registration_info.description = "User's name, email, password."

user_data = Data("User Data")
user_data.description = "Stored user account info (hashed password, email, etc.)"

review_action = Data("Registration Review Action")
review_action.description = "Administrator's decision on pending registration."

approval_status = Data("Approval Status")
approval_status.description = "Approval or rejection of a user registration."

approval_notification = Data("Approval Notification")
approval_notification.description = "Notification message sent to user."

# Dataflows
Dataflow(user, app_server, "Submit registration data", data=registration_info, protocol="HTTPS", port=443)
Dataflow(app_server, database, "Store user data", data=user_data, protocol="SQL/TLS", port=1433)
Dataflow(admin, app_server, "Review registration requests", data=review_action, protocol="HTTPS", port=443)
Dataflow(app_server, database, "Update approval status", data=approval_status, protocol="SQL/TLS", port=1433)
Dataflow(app_server, user, "Notify approval decision", data=approval_notification, protocol="HTTPS", port=443)


tm.process()
