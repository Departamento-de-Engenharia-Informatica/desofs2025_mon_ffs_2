from pytm import TM, Actor, Server, Process, Datastore, Dataflow, Boundary

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

admin = Actor("Administrator")
admin.inBoundary = internet

# Componentes internos
app_server = Process("AMAPP API")
app_server.inBoundary = server

database = Datastore("AMAPP DB")
database.inBoundary = dbserver
database.isSql = True

# Fluxos de dados
Dataflow(user, app_server, "Submit registration data")
Dataflow(app_server, database, "Store user data")
Dataflow(admin, app_server, "Review registration requests")
Dataflow(app_server, database, "Update approval status")
Dataflow(app_server, user, "Notify approval decision")

tm.process()
