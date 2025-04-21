from pytm import TM, Actor, Server, Datastore, Dataflow, Boundary, Data

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

# Data objects
user_mgmt_request = Data("User Management Request")
user_mgmt_request.description = "Request for creating, updating, or deleting a user."

role_permission_request = Data("Role/Permission Management Request")
role_permission_request.description = "Request to assign or update user roles and permissions."

user_data_response = Data("User Data Response")
user_data_response.description = "Response containing user account information."

role_permission_data_response = Data("Role/Permission Data Response")
role_permission_data_response.description = "Response containing assigned roles and permissions."

operation_result = Data("Operation Confirmation or Result")
operation_result.description = "Success or error message after operation."

# Data Flows
Dataflow(admin, user_permission_server, "Submit user or permission management request", data=user_mgmt_request, protocol="HTTPS", port=443)
Dataflow(user_permission_server, database, "Create/update/delete user account", data=user_mgmt_request, protocol="SQL/TLS", port=1433)
Dataflow(user_permission_server, database, "Assign/update/retrieve roles and permissions", data=role_permission_request, protocol="SQL/TLS", port=1433)
Dataflow(database, user_permission_server, "Return user data", data=user_data_response, protocol="SQL/TLS", port=1433)
Dataflow(database, user_permission_server, "Return permission/role data", data=role_permission_data_response, protocol="SQL/TLS", port=1433)
Dataflow(user_permission_server, admin, "Send operation confirmation or results", data=operation_result, protocol="HTTPS", port=443)

tm.process()
