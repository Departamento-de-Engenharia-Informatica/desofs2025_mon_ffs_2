from pytm import TM, Actor, Process, Dataflow, Data

tm = TM("AMAP Product Creation - Level 0")
tm.description = "Context DFD (Level 0) for product creation in the AMAP API"

# Atores e sistema
producer = Actor("Producer")
producer.description = "User that creates products"

amap_api = Process("AMAP Product API")
amap_api.description = "API that receives and processes product creation requests"

# Dados
product_data = Data("Product Info")
feedback_data = Data("Feedback")

# Fluxos
flow1 = Dataflow(producer, amap_api, "Send Product Info")
flow1.protocol = "HTTPS"
flow1.dstPort = 443
flow1.data = product_data

flow2 = Dataflow(amap_api, producer, "Send Feedback")
flow2.protocol = "HTTPS"
flow2.dstPort = 443
flow2.data = feedback_data

# Processar modelo
tm.process()

