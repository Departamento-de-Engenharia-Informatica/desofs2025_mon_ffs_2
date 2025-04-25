| ![Logo ISEP](figs/logoisep.png) | ![Logo DEI](figs/logo_DEI_big_transparente.png) |
| :-----------------------------: | :---------------------------------------------: |
|:------------------------------:|:----------------------------------------------:|


# Phase 1: Threat Modeling

**_Group desofs2025_mon_ffs_2_**
*DESOFS*

**Master in Informatics Engineering - 2024/2025**

**Students:**
Ilídio Magalhães - 1191577 <br>
Hugo Coelho - 1162086 <br>
Pedro Oliveira - 1240482 <br>
Paulo Abreu - 1240481 <br>
...
**Location:** Porto, April 27, 2025

---

## Table of Contents

- [Phase 1: Threat Modeling](#phase-1-threat-modeling)
  - [Table of Contents](#table-of-contents)
  - [Introduction](#introduction)
  - [Project Analysis](#project-analysis)
    - [Project description](#project-description)
    - [Domain Model](#domain-model)
    - [Component Diagram](#component-diagram)
    - [Threat Model Information](#threat-model-information)
    - [Application Users](#application-users)
      - [Producer](#producer)
      - [Co-Producer (Consumer)](#co-producer-consumer)
      - [AMAP Administrators](#amap-administrators)
      - [System Admin](#system-admin)
      - [Non-Authenticated User](#non-authenticated-user)
      - [System](#system)
    - [Use Cases](#use-cases)
    - [Functional Requirements](#functional-requirements)
    - [Non-Functional Requirements](#non-functional-requirements)
    - [Security Requirements](#security-requirements)
    - [External Dependencies](#external-dependencies)
    - [Entry Points](#entry-points)
    - [Exit Points](#exit-points)
    - [Assets](#assets)
    - [Trust Levels](#trust-levels)
  - [Data Flow Diagrams](#data-flow-diagrams)
    - [Authentication](#authentication)
      - [Level 0](#level-0)
      - [External Actor:](#external-actor)
      - [Main Process:](#main-process)
      - [Data Flows:](#data-flows)
      - [Level 1](#level-1)
      - [**External Actor:**](#external-actor-1)
      - [**Subprocesses:**](#subprocesses)
      - [**Data Storage:**](#data-storage)
      - [**Data Flows:**](#data-flows-1)
      - [**Trust Boundaries:**](#trust-boundaries)
    - [Create Product](#create-product)
      - [Level 0](#level-0-1)
      - [Level 1](#level-1-1)
    - [Generic Representation](#generic-representation)
      - [Level 0](#level-0-2)
      - [Level 1](#level-1-2)
    - [Order Payments Deliveries Reports](#order-payments-deliveries-reports)
      - [Level 0](#level-0-3)
      - [Level 1](#level-1-3)
    - [Stride](#stride)
    - [Product Reservation](#product-reservation)
      - [Level 0](#level-0-4)
      - [Level 1](#level-1-4)
    - [Registration](#registration)
      - [Level 0](#level-0-5)
      - [**External Actors:**](#external-actors)
      - [**Main Process:**](#main-process-1)
      - [**Data Flows:**](#data-flows-2)
      - [**Trust Boundaries:**](#trust-boundaries-1)
      - [Level 1](#level-1-5)
      - [**External Actors:**](#external-actors-1)
      - [**Internal Components:**](#internal-components)
      - [**Data Flows:**](#data-flows-3)
      - [**Data Objects:**](#data-objects)
      - [**Trust Boundaries:**](#trust-boundaries-2)
    - [User Management](#user-management)
      - [Level 0](#level-0-6)
      - [**External Actor:**](#external-actor-2)
      - [**Main Process:**](#main-process-2)
      - [**Data Flows:**](#data-flows-4)
      - [Level 1](#level-1-6)
      - [**External Actor:**](#external-actor-3)
      - [**Internal Components:**](#internal-components-1)
      - [**Data Flows:**](#data-flows-5)
      - [**Data Objects:**](#data-objects-1)
      - [**Trust Boundaries:**](#trust-boundaries-3)
  - [Stride](#stride-1)
    - [Authentication](#authentication-1)
    - [Create Product](#create-product-1)
    - [Generic Representation](#generic-representation-1)
    - [Payments](#payments)
    - [Product Reservation](#product-reservation-1)
  - [| INP32 - XML Injection | Reservation Processing | Tampering, Information Disclosure | Attackers inject malicious XML code to manipulate application logic, potentially allowing authentication bypass, data exposure, or system compromise. | • Implement strong input validation for XML content• Filter illegal characters and XML structures• Use custom error pages to prevent information leakage• Implement proper XML parsing with schema validation |](#-inp32---xml-injection--reservation-processing--tampering-information-disclosure--attackers-inject-malicious-xml-code-to-manipulate-application-logic-potentially-allowing-authentication-bypass-data-exposure-or-system-compromise---implement-strong-input-validation-for-xml-content-filter-illegal-characters-and-xml-structures-use-custom-error-pages-to-prevent-information-leakage-implement-proper-xml-parsing-with-schema-validation-)
    - [Registration](#registration-1)
    - [User Management](#user-management-1)
  - [Use Cases and Abuse Cases](#use-cases-and-abuse-cases)
    - [Authentication](#authentication-2)
    - [Create Product](#create-product-2)
      - [**Use Cases**](#use-cases-1)
      - [**Abuse Cases**](#abuse-cases)
      - [**Countermeasures**](#countermeasures)
    - [Payments](#payments-1)
    - [Product Reservation](#product-reservation-2)
    - [Registration](#registration-2)
    - [User Management](#user-management-2)
  - [Threat Classification](#threat-classification)
  - [Mitigations and Countermeasures](#mitigations-and-countermeasures)
  - [Threat Profile](#threat-profile)
  - [Conclusion](#conclusion)
  - [References](#references)

---

## Introduction

*_[Blablabla]_*

---

## Project Analysis

### Project description

*_[Blablabla]_*

---

### Domain Model

![Domain Model](diagrams/Domain%20Model/domain_model_diagram.png)

*_[Blablabla]_*

---

### Component Diagram

![Component Diagram](diagrams/Component%20Diagram/Amap-component-diagram.png)

*_[Blablabla]_*

---

### Threat Model Information

*_[Blablabla]_*

---

### Application Users

#### Producer

Responsible for the production and management of products available within the AMAP
system, the producer is the main supplier in the community. Producers update the platform
with information about product availability, production cycles, and inventory, which allows
consumers to know exactly what is available each quarter. In addition, they ensure that
production aligns with the orders placed beforehand, minimizing waste and maximizing
sustainability. This user class has permissions to manage and adjust production data, keeping
operations synchronized with consumer orders.

#### Co-Producer (Consumer)

Also known as co-producers, consumers play an active role in AMAP’s sustainable model by
ordering products directly from producers. These users engage in a long-term commitment,
supporting local consumption and securing regular orders, typically on a quarterly basis.
They have access to detailed information about the products, origin, and production
practices, and can track order status up to delivery. Although they do not have permissions
to modify production data, this user class can access product inquiry and ordering functions,
along with resources that support sustainable consumption.

#### AMAP Administrators

These users oversee the operational management of the system within AMAP. Acting as
intermediaries between producers and consumers, they ensure data accuracy on the
platform, address user issues or questions, and uphold AMAP’s values of sustainability and
transparency. AMAP administrators have the authority to edit and review system content,
facilitate updates or changes in practices, and ensure that digital operations align with
organizational objectives. They also handle user support issues and facilitate communication
among the different stakeholders.

#### System Admin

With high-level permissions, the technical administrator is responsible for the overall
configuration and maintenance of the system. They ensure the security, functionality, and
stability of the platform, managing user permissions, updates, backups, and routine
maintenance. This role is accountable for resolving complex issues and advanced settings,
ensuring that the system runs efficiently, data is secure, and compliance and data protection
practices are met.

#### Non-Authenticated User

Representing new visitors or those interested in AMAP, these users can browse the
system without needing to register. Access is limited to general information about AMAP, its
mission, values, and available products. However, they cannot place orders or access data
exclusive to authenticated users. This class enables visitors to learn more about AMAP’s
purpose, encouraging engagement and fostering a path to becoming co-producers.

#### System

The System itself is responsible for sending automating notifications between users,
such as notifying a Co-Producer that new product is available for delivery or is available or
a payment date is due.

---

### Use Cases

![Use Cases Diagram]()

*_[Blablabla]_*

---

### Functional Requirements

*_[Blablabla]_*

---

### Non-Functional Requirements

*_[Blablabla]_*

---

### Security Requirements

*_[Blablabla]_*

...

### External Dependencies

*_[Blablabla]_*

...

### Entry Points

*_[Blablabla]_*

...

### Exit Points

*_[Blablabla]_*

### Assets

*_[Blablabla]_*

### Trust Levels

*_[Blablabla]_*

---

## Data Flow Diagrams

Data Flow Diagrams (DFDs) are used to graphically represent the flow of data within a business information system. In the context of the AMAPP project, which aims to manage users, products, orders, subscriptions, and deliveries in an AMAP environment, DFDs serve as a valuable tool to understand how the application handles and processes data across its various components.

The use of DFDs in this project allows us to gain a clearer understanding of the application by providing a visual representation of how data moves through the system and what transformations or processes occur along the way. Rather than focusing on implementation details, DFDs emphasize the flow of information — such as user authentication, order creation, or payment processing — between different parts of the system.

DFDs are hierarchical in structure, which makes them particularly useful for decomposing the application into subsystems and lower-level components. A high-level DFD (Level 0) will help us define the scope of the system by showing the application as a single process interacting with external entities such as producers, co-producers (clients), and administrators. Subsequent lower-level diagrams (Level 1 and beyond) will then break down the main process into more specific internal operations, such as managing subscriptions, scheduling deliveries, or handling payment validation.

By using DFDs at different levels of abstraction, we ensure a structured approach to analyzing the system’s requirements and logic, making it easier to communicate the system's behavior to all stakeholders involved in the development process.

### Authentication

#### Level 0

![DFD Authentication Level 0](diagrams/DFD/Authentication/amapp_dfd_auth_0.png)

The Level 0 Data Flow Diagram (DFD) provides a high-level overview of the **user authentication process** within the AMAPP application. This diagram illustrates the basic interaction between an external actor (the user) and the internal AMAPP authentication system.

#### External Actor:

- `User`: Any actor (e.g., co-producer, producer, or AMAPP admin) attempting to log in to the system.
- **External Actor:**
  - `User`: Any actor (e.g., co-producer, producer, or AMAPP admin) attempting to log in to the system.

#### Main Process:

- `AMAPP System`: The internal authentication service responsible for validating login credentials and issuing authentication tokens.
- **Main Process:**
  - `AMAPP System`: The internal authentication service responsible for validating login credentials and issuing authentication tokens.

#### Data Flows:

- `Submit login credentials`: The user submits their login details (e.g., email and password) to the AMAPP system via a secure HTTPS connection.
- `Authentication JWT Token`: Upon successful verification, the system responds with a JSON Web Token (JWT) which allows the user to access protected endpoints in future requests.
- **Data Flows:**
  - `Submit login credentials`: The user submits their login details (e.g., email and password) to the AMAPP system via a secure HTTPS connection.
  - `Authentication JWT Token`: Upon successful verification, the system responds with a JSON Web Token (JWT) which allows the user to access protected endpoints in future requests.

This context-level diagram defines the **boundary between the user and the system**, emphasizing what data is exchanged during the authentication process without yet detailing how the credentials are validated internally or how the JWT is generated and stored.

#### Level 1

![DFD Authentication Level 1](diagrams/DFD/Authentication/amapp_dfd_auth_1.png)

The Level 1 Data Flow Diagram (DFD) refines the context-level view of the user authentication process by decomposing the **AMAPP API** into internal subprocesses and detailing how data flows through the system. It also introduces data storage components and defines clear **trust boundaries**.

#### **External Actor:**

- `User`: An individual (e.g., co-producer, producer, or administrator) attempting to authenticate and gain access to the AMAPP platform.
- **External Actor:**
  - `User`: An individual (e.g., co-producer, producer, or administrator) attempting to authenticate and gain access to the AMAPP platform.


#### **Subprocesses:**

- `Receive Credentials`: Handles the initial reception of login credentials (username/email and password) from the user.
- `Fetch User Record`: Queries the database to retrieve the stored user record corresponding to the submitted credentials.
- `Validate Credentials`: Compares the submitted credentials with the stored hash (e.g., using password hashing functions).
- `Generate Token`: If validation is successful, creates a signed JWT (JSON Web Token) to be used in subsequent authenticated requests.
- `Return Token`: Sends the authentication token back to the user.
- **Subprocesses:**
  - `Receive Credentials`: Handles the initial reception of login credentials (username/email and password) from the user.
  - `Fetch User Record`: Queries the database to retrieve the stored user record corresponding to the submitted credentials.
  - `Validate Credentials`: Compares the submitted credentials with the stored hash (e.g., using password hashing functions).
  - `Generate Token`: If validation is successful, creates a signed JWT (JSON Web Token) to be used in subsequent authenticated requests.
  - `Return Token`: Sends the authentication token back to the user.

#### **Data Storage:**

- `AMAPP DB`: The internal database where user records are securely stored, including hashed passwords and roles.
- **Data Storage:**
  - `AMAPP DB`: The internal database where user records are securely stored, including hashed passwords and roles.


#### **Data Flows:**

- `Submit login credentials`: The user submits their authentication details to the API.
- `Request user record`: The API requests the corresponding user data from the database.
- `Return user record`: The database sends the user’s stored information (e.g., hashed password) back to the API.
- `Validated result`: The outcome of the credential validation is passed to the token generator.
- `Generated JWT`: A secure token is created for the session.
- `Authentication JWT Token`: The token is returned to the user as proof of successful authentication.
- **Data Flows:**
  - `Submit login credentials`: The user submits their authentication details to the API.
  - `Request user record`: The API requests the corresponding user data from the database.
  - `Return user record`: The database sends the user’s stored information (e.g., hashed password) back to the API.
  - `Validated result`: The outcome of the credential validation is passed to the token generator.
  - `Generated JWT`: A secure token is created for the session.
  - `Authentication JWT Token`: The token is returned to the user as proof of successful authentication.


#### **Trust Boundaries:**

- `Internet Zone`: External environment where the user resides.
- `AMAPP System Zone`: The internal API and authentication logic, trusted but must validate all inputs.
- `Database Zone`: A protected area where sensitive user data is stored, with stricter access controls and security policies.
- **Trust Boundaries:**
  - `Internet Zone`: External environment where the user resides.
  - `AMAPP System Zone`: The internal API and authentication logic, trusted but must validate all inputs.
  - `Database Zone`: A protected area where sensitive user data is stored, with stricter access controls and security policies.


This detailed diagram provides a more granular view of how the authentication workflow operates, highlighting not only the logical flow of data but also the interaction between components across different **trust zones**, which is crucial for identifying and mitigating potential security risks.

---

### Create Product

#### Level 0

![DFD Create Product Level 0](diagrams/DFD/Create%20Product/amapp_dfd_create_product_0.png)

The Level 0 DFD represents a high-level view of the product creation system, focusing on the interaction between the external actor (producer) and the AMAP API system.

- **External Actor:**

  - `Producer`: The user who intends to create a product.
- **Main Process:**

  - `AMAP API`: Interface responsible for receiving product creation requests, processing them, and returning feedback to the producer.
- **Data Flows:**

  - `Send Product Info`: The producer sends product data (name, description, price, etc.) to the API via HTTPS.
  - `Send Feedback`: After processing, the API returns a response (success, error, or validation messages) to the producer.

This diagram simply shows who interacts with the system and what data is exchanged, without yet detailing the internal processes.

#### Level 1

![DFD Create Product Level 1](diagrams/DFD/Create%20Product/amapp_dfd_create_product_1.png)

The Level 1 DFD deepens the details of the product creation process by breaking down the API into internal subprocesses and introducing data storage and trust boundaries.

- **External Actors:**

  - `Producer`: Remains the user who initiates the process.
- **Subprocesses:**

  - `Validate Input`: Validates the data received from the producer (checks required fields, formats, etc.).
  - `Store Product`: Stores the validated product in the database.
  - `Send Response`: Generates and sends a response with the operation result.
- **Data Storage:**

  - `Product DB`: The database where validated products are stored.
- **Data Flows:**

  - `Submit Product`: The producer sends product data for validation.
  - `Validated Data`: Verified data is passed to the storage process.
  - `Save to DB`: The product is saved into the database.
  - `Operation Outcome`: The result of the storage operation is passed to the response process.
  - `Return Result`: The response is sent back to the producer.
- **Trust Boundaries:**

  - `User Zone`: Where the producer resides (external environment).
  - `AMAP API Zone`: Where internal API processes occur.
  - `Database Zone`: Where the database resides, typically with stricter access controls.

This level shows in greater detail how the system processes and stores data, helping to identify potential security threats and ensure proper handling of information across different trust zones.

---

### Generic Representation

#### Level 0

![DFD Generic Representation Level 0](diagrams/DFD/Generic%20Representation/amapp_dfd_generic_0.png)

The Level 0 Data Flow Diagram (DFD) provides a high-level overview of the AMAP/CSA agricultural system. This context diagram illustrates the core interactions between the system's main components and external entities.

At the center of the diagram is the AMAP API, which serves as the core processing unit handling all business logic and operations. The system interacts with three primary user types:

- Consumers (Co-Producers) who browse products, place orders, and manage their subscriptions. The system responds by providing product information, order confirmations, and various notifications.
- Producers who manage their product listings, update inventory, and process incoming orders. The system provides them with order notifications and delivery schedules.
- AMAP Administrators who manage users, organize deliveries, and configure system settings. They receive system status updates, user data, and various reports.

All data persistence is handled through the external AMAP Database, where the API performs read and write operations for user data, orders, products, and inventory information. The database returns the requested data records to the API.

This Level 0 DFD effectively captures the fundamental data exchanges within the sustainable agriculture platform, showing how information flows between the system and its stakeholders without delving into the internal processing details.

#### Level 1

![DFD Generic Representation Level 1](diagrams/DFD/Generic%20Representation/amapp_dfd_generic_1.png)

The Level 1 Data Flow Diagram (DFD) provides a more detailed view of the AMAP/CSA agricultural system architecture, expanding on the context diagram by revealing the internal components and their interactions.

The diagram is structured with nested boundaries:

Localhost serves as the outer boundary
AMAP System operates within the Localhost boundary
Database Server represents a separate boundary for data storage
Within the AMAP System boundary, two main components are identified:

- AMAP API - The core processing component handling business logic, user authentication, and orchestrating the system's operations. It directly interfaces with all external actors and coordinates data operations.
- AmapDB_API - A dedicated server component that serves as an intermediary layer between the main API and the database, providing abstraction and security for database operations.

Outside the system boundary, the AMAP Database exists as an external datastore where all system information is persistently stored.

The diagram illustrates several key data flows:

- External Actor Communications: The three user types (Consumers, Producers, and Administrators) send API requests to and receive responses from the AMAP API.
- Internal Data Processing: The AMAP API sends database requests to the AmapDB_API, which translates these into structured database queries.
- Data Exchange: The database communication flow shows how CRUD operations are transformed into SQL queries, with result sets being returned and processed back into application-level data.

This Level 1 DFD demonstrates the system's layered architecture approach, with clear separation between the user interface logic, business processing, and data persistence layers. This architecture enhances security by ensuring database operations are properly abstracted and controlled through dedicated interfaces.

---

### Order Payments Deliveries Reports

#### Level 0

![DFD Payments Level 0](diagrams\DFD\Order%20Payments%20Deliveries%20Reports\amapp_dfd_pay_del_rep_0.png)
![DFD Payments Level 0](diagrams\DFD\Order%20Payments%20Deliveries%20Reports\amapp_dfd_pay_del_rep_0.png)

*_[Blablabla]_*

#### Level 1

![DFD Payments Level 1](diagrams\DFD\Order%20Payments%20Deliveries%20Reports\amapp_dfd_pay_del_rep_1.png)
![DFD Payments Level 1](diagrams\DFD\Order%20Payments%20Deliveries%20Reports\amapp_dfd_pay_del_rep_1.png)

*_[Blablabla]_*

### Stride

| **Threat**                                 | **Targeted Element** | **STRIDE Category**      | **Description**                                                                                                                                                                                      | **Mitigation**                                                                                                                          |
|--------------------------------------------|----------------|--------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------|
| INP02 – Overflow Buffers                   | Request Own Report | Tampering                | Buffer overflows in the request handler could allow an attacker to crash or take over the report-generation endpoint.                                                                               | Use languages/compilers with automatic bounds checking; prefer safe APIs; run static analysis to catch overflow risks.                 |
| AA01 – Authentication Abuse/ByPass         | View Own Report | Spoofing                 | An attacker who bypasses or steals credentials could view another producer’s report, compromising confidentiality.                                                                                  | Enforce strong authentication (e.g. OAuth 2.0), session timeouts, and multi-factor authentication.                                    |
| INP07 – Buffer Manipulation                | Request Report for Specific CoProducer | Tampering                | Maliciously crafted request parameters could manipulate internal buffers, leading to malformed queries or code execution in the report engine.                                                    | Validate and bound-check all inputs; use compiler-based canaries (StackGuard/ProPolice); adopt secure coding guidelines.             |
| AA02 – Principal Spoof                     | View Selected CoProducer Report | Spoofing                 | An attacker may spoof another user’s identity or stolen token to retrieve reports they’re not authorized to see.                                                                                    | Enforce strict authorization checks per request; implement token binding and rotate credentials regularly.                             |
| CR06 – Communication Channel Manipulation  | Generate Report | Information Disclosure   | A man-in-the-middle on the API↔engine channel could intercept the raw report stream and extract sensitive data.                                                                                     | Encrypt all in-transit data (TLS with strong ciphers); mutually authenticate endpoints; pin certificates.                              |
| DE03 – Sniffing Attacks                    | Query Database | Information Disclosure   | If the database link isn’t encrypted, an attacker sniffing the network can capture query results containing privileged report data.                                                                  | Use encrypted database connections (e.g. TLS); isolate the database network; enforce least-privilege network policies.                |
| AC21 – Cross Site Request Forgery (CSRF)   | Request Report | Spoofing                 | A forged request (e.g. via hidden form or link) could trick a logged-in user into submitting a report action they didn’t intend, exposing or altering data.                                         | Implement anti-CSRF tokens for each form/action; validate Referer/Origin headers; require re-authentication for sensitive operations. |
| INP41 – Argument Injection                 | Request Report | Tampering                | Injection of unexpected arguments into the report-request parameters could cause unintended behavior, data leakage or code execution in the report engine.                                         | Whitelist and sanitize all parameter values; enforce strict length/type checks; use parameterized APIs rather than string concatenation. |

*_[Blablabla]

---

### Product Reservation

#### Level 0

![DFD Product Reservation Level 0](diagrams/DFD/Product%20Reservation/amapp_dfd_productReservation_0.png)

The Level 0 Data Flow Diagram (DFD) for the AMAP/CSA product purchase flow provides a high-level overview of how consumers interact with the system to browse and reserve agricultural products.

This simplified context diagram focuses specifically on the purchase/reservation process from the consumer perspective. At its core is the AMAP API, which serves as the central processing component that handles all product purchase operations. Unlike the general system overview, this diagram isolates the specific functionality related to product transactions.

The diagram shows a single external actor, the Consumer (Co-Producer), who interacts with the system to browse available products and place orders. This focused view highlights the consumer's journey through the purchase process without the complexity of other system interactions.

The data flows are presented in a logical sequence that follows the typical purchase process:

The consumer initiates the process by sending a Purchase Request to the system, which includes product selections, order details, and payment information.

The AMAP API communicates with the external AMAP Database through Data Operations to store and retrieve purchase-related data, including product queries, order storage, and inventory updates.

The database responds with Data Results containing product data, order confirmations, and current inventory status.

Finally, the AMAP API sends a Purchase Response back to the consumer with product listings, order confirmations, and payment receipts.

This Level 0 DFD effectively captures the fundamental flow of the purchase process, showing how information moves between the consumer, the system, and the database in a complete transaction cycle. This representation allows stakeholders to understand the high-level purchase flow without being overwhelmed by implementation details.

#### Level 1

![DFD Product Reservation Level 1](diagrams/DFD/Product%20Reservation/amapp_dfd_productReservation_1.png)

The Level 1 Data Flow Diagram (DFD) for the AMAP/CSA product reservation process provides a detailed view of the system components involved in the consumer product reservation journey. Unlike the Level 0 diagram which presented a high-level overview, this diagram breaks down the internal processes and data flows that facilitate the reservation experience.

The diagram defines two main boundaries:

AMAP System containing the application processes
Database Server containing the data persistence layer
Within the AMAP System boundary, four distinct processes work together to handle the product reservation workflow:

- Product Catalog - Manages product listings and inventory information, serving as the entry point for consumers browsing available products.
- Order Management - Handles order creation and processing, coordinating the overall reservation workflow.
- Reservation Processing - Specifically manages product reservation requests, ensuring products are properly reserved for consumers.
- Delivery Management - Handles product delivery coordination after reservation.

The diagram shows a single external actor, the Co-Producer (consumer), who interacts with the system to browse products and make reservations.

The data flows illustrate a comprehensive reservation process:

The consumer begins by browsing products, with product information flowing from the catalog.
Once products are selected, the consumer places an order that is processed by Order Management.
Order Management checks product availability through the Product Catalog.
The reservation is processed by the Reservation Processing component.
Delivery details are managed by the Delivery Management component.
Throughout the process, data is stored and retrieved from the external AMAP Database.
This Level 1 DFD reveals how the system's modular architecture separates concerns into distinct processing components, each handling a specific part of the reservation workflow. The diagram shows not only the consumer-facing interactions but also the important internal communications between components that ensure the reservation process functions correctly.

By breaking down the process into these components, the system achieves better maintainability and security through separation of responsibilities while providing a seamless experience for consumers reserving agricultural products.

---

### Registration

#### Level 0

![DFD Registration Level 0](diagrams/DFD/Registration/amapp_dfd_user_reg_0.png)

The Level 0 Data Flow Diagram (DFD) provides a high-level view of the **user registration process** within the AMAPP platform. It outlines the main interactions between the external actors (`User` and `AMAPP Admin`) and the internal `AMAPP System`, focusing on the exchange of registration and approval data.

#### **External Actors:**

- `User`: An individual who wishes to register in the AMAPP platform (e.g., co-producer or producer).
- `AMAPP Admin`: The administrator responsible for approving or rejecting registration requests.
- **External Actors:**
  - `User`: An individual who wishes to register in the AMAPP platform (e.g., co-producer or producer).
  - `AMAPP Admin`: The administrator responsible for approving or rejecting registration requests.


#### **Main Process:**

- `AMAPP System`: The central component that receives registration requests, communicates with the administrator, and notifies the user of the final decision.
- **Main Process:**
  - `AMAPP System`: The central component that receives registration requests, communicates with the administrator, and notifies the user of the final decision.


#### **Data Flows:**

- `Submit registration request`: The user submits a request to register on the platform.
- `Send approval request`: The system forwards the registration data to the administrator for review.
- `Approval decision`: The administrator sends their decision (approve or reject) back to the system.
- `Notify decision`: The system communicates the result of the registration process to the user.
- **Data Flows:**
  - `Submit registration request`: The user submits a request to register on the platform.
  - `Send approval request`: The system forwards the registration data to the administrator for review.
  - `Approval decision`: The administrator sends their decision (approve or reject) back to the system.
  - `Notify decision`: The system communicates the result of the registration process to the user.


#### **Trust Boundaries:**

- `Internet Zone`: The untrusted external zone where users reside and submit their requests.
- `AMAPP System Zone`: The internal, trusted environment where the API and backend logic are executed.
- **Trust Boundaries:**
  - `Internet Zone`: The untrusted external zone where users reside and submit their requests.
  - `AMAPP System Zone`: The internal, trusted environment where the API and backend logic are executed.


This context-level DFD clearly defines the boundaries of the user registration process, focusing on who is involved, what data is exchanged, and how the approval workflow functions. It sets the stage for more detailed diagrams that may further decompose the internal decision logic or validation mechanisms.

#### Level 1

![DFD Registration Level 1](diagrams/DFD/Registration/amapp_dfd_user_reg_1.png)

The Level 1 Data Flow Diagram (DFD) expands the context-level view of the user registration process in the AMAPP platform. It decomposes the internal system into subprocesses, introduces data storage, and clearly defines trust boundaries and specific data flows.

#### **External Actors:**

- `User`: A new user (e.g., co-producer or producer) attempting to register on the platform.
- `AMAPP Admin`: The administrator responsible for reviewing and approving or rejecting registration requests.
- **External Actors:**
  - `User`: A new user (e.g., co-producer or producer) attempting to register on the platform.
  - `AMAPP Admin`: The administrator responsible for reviewing and approving or rejecting registration requests.


#### **Internal Components:**

- `AMAPP API`: The backend process that handles user registration, stores user data, communicates with the admin, and notifies users of the result.
- `AMAPP DB`: The database that stores user information, including credentials and approval status.
- **Internal Components:**
  - `AMAPP API`: The backend process that handles user registration, stores user data, communicates with the admin, and notifies users of the result.
  - `AMAPP DB`: The database that stores user information, including credentials and approval status.


#### **Data Flows:**

- `Submit registration data`: The `User` submits personal details (`Registration Info`) to the `AMAPP API` over HTTPS.
- `Store user data`: The `AMAPP API` stores the user's account info (`User Data`) in the `AMAPP DB` via secure SQL.
- `Review registration requests`: The `AMAPP Admin` sends their review action (`Registration Review Action`) to the `AMAPP API`.
- `Update approval status`: The system updates the approval decision (`Approval Status`) in the `AMAPP DB`.
- `Notify approval decision`: The `User` is notified of the final result (`Approval Notification`) via HTTPS.
- **Data Flows:**
  - `Submit registration data`: The `User` submits personal details (`Registration Info`) to the `AMAPP API` over HTTPS.
  - `Store user data`: The `AMAPP API` stores the user's account info (`User Data`) in the `AMAPP DB` via secure SQL.
  - `Review registration requests`: The `AMAPP Admin` sends their review action (`Registration Review Action`) to the `AMAPP API`.
  - `Update approval status`: The system updates the approval decision (`Approval Status`) in the `AMAPP DB`.
  - `Notify approval decision`: The `User` is notified of the final result (`Approval Notification`) via HTTPS.


#### **Data Objects:**

- `Registration Info`: User's submitted data (e.g., name, email, password).
- `User Data`: Stored account information (e.g., hashed password, email).
- `Registration Review Action`: Admin’s decision regarding pending registration.
- `Approval Status`: Approval or rejection flag stored in the database.
- `Approval Notification`: Message sent to the user with the outcome.
- **Data Objects:**
  - `Registration Info`: User's submitted data (e.g., name, email, password).
  - `User Data`: Stored account information (e.g., hashed password, email).
  - `Registration Review Action`: Admin’s decision regarding pending registration.
  - `Approval Status`: Approval or rejection flag stored in the database.
  - `Approval Notification`: Message sent to the user with the outcome.


#### **Trust Boundaries:**

- `Internet`: Where external actors (`User`, `AMAPP Admin`) reside.
- `AMAPP System`: Internal zone that runs the application logic and processes data.
- `DB Server`: A protected database zone with stricter access control where sensitive information is stored.
- **Trust Boundaries:**
  - `Internet`: Where external actors (`User`, `AMAPP Admin`) reside.
  - `AMAPP System`: Internal zone that runs the application logic and processes data.
  - `DB Server`: A protected database zone with stricter access control where sensitive information is stored.


This level of detail helps to understand how registration data is validated, stored, and processed, while also supporting security analysis by clarifying which zones handle sensitive operations and which protocols are used in data transmission.

---

### User Management

#### Level 0

![DFD User Management Level 0](diagrams/DFD/User%20Management/amapp_dfd_user_management_0.png)

The Level 0 Data Flow Diagram (DFD) provides a high-level overview of how the AMAPP system handles user account and permission management. It captures the interaction between the administrative actor and the internal system responsible for executing user and role operations.

#### **External Actor:**

- `Administrator`: A privileged user (typically part of the AMAPP team) responsible for managing user accounts and defining roles and permissions.
- **External Actor:**
  - `Administrator`: A privileged user (typically part of the AMAPP team) responsible for managing user accounts and defining roles and permissions.


#### **Main Process:**

- `AMAPP System`: The backend module that processes requests to create, update, or delete users, and manage their associated roles and permissions.
- **Main Process:**
  - `AMAPP System`: The backend module that processes requests to create, update, or delete users, and manage their associated roles and permissions.


#### **Data Flows:**

- `Send request to manage users or roles`: The `Administrator` sends commands to the `AMAPP System` to perform management actions.
- `Send operation result or data`: The `AMAPP System` returns feedback to the `Administrator`, such as confirmation of changes or relevant user/role data.
- **Data Flows:**
  - `Send request to manage users or roles`: The `Administrator` sends commands to the `AMAPP System` to perform management actions.
  - `Send operation result or data`: The `AMAPP System` returns feedback to the `Administrator`, such as confirmation of changes or relevant user/role data.


This context-level diagram outlines the scope of the user and permission management functionality, focusing on who interacts with the system and what data is exchanged. It lays the groundwork for future refinements, where the internal logic (e.g., validation, auditing, access control checks) may be explored in more detail through Level 1 diagrams.

#### Level 1

![DFD User Management Level 1](diagrams/DFD/User%20Management/amapp_dfd_user_management_1.png)

The Level 1 Data Flow Diagram (DFD) provides a more detailed view of how the AMAPP system handles the management of user accounts and their associated roles and permissions. This diagram decomposes the main system into internal components and shows how data flows between the administrator, the system, and the database.

#### **External Actor:**

- `Administrator`: A privileged user who initiates user management operations (e.g., create/update/delete users, assign roles).
- **External Actor:**
  - `Administrator`: A privileged user who initiates user management operations (e.g., create/update/delete users, assign roles).


#### **Internal Components:**

- `AMAPP API`: The internal component responsible for processing requests related to user and permission management.
- `AMAPP DB`: The database where user accounts and role/permission data are stored.
- **Internal Components:**
  - `AMAPP API`: The internal component responsible for processing requests related to user and permission management.
  - `AMAPP DB`: The database where user accounts and role/permission data are stored.

#### **Data Flows:**

- `Submit user or permission management request`: The `Administrator` sends a management request (`User Management Request` or `Role/Permission Management Request`) to the `AMAPP API` via HTTPS.
- `Create/update/delete user account`: The `AMAPP API` performs operations on the user account in the `AMAPP DB` using secure SQL.
- `Assign/update/retrieve roles and permissions`: The `AMAPP API` handles role and permission data in the `AMAPP DB`.
- `Return user data`: The `AMAPP DB` returns relevant user information (`User Data Response`) to the `AMAPP API`.
- `Return permission/role data`: The `AMAPP DB` returns role and permission information (`Role/Permission Data Response`) to the `AMAPP API`.
- `Send operation confirmation or results`: The `AMAPP API` returns the result (`Operation Confirmation or Result`) to the `Administrator`.
- **Data Flows:**
  - `Submit user or permission management request`: The `Administrator` sends a management request (`User Management Request` or `Role/Permission Management Request`) to the `AMAPP API` via HTTPS.
  - `Create/update/delete user account`: The `AMAPP API` performs operations on the user account in the `AMAPP DB` using secure SQL.
  - `Assign/update/retrieve roles and permissions`: The `AMAPP API` handles role and permission data in the `AMAPP DB`.
  - `Return user data`: The `AMAPP DB` returns relevant user information (`User Data Response`) to the `AMAPP API`.
  - `Return permission/role data`: The `AMAPP DB` returns role and permission information (`Role/Permission Data Response`) to the `AMAPP API`.
  - `Send operation confirmation or results`: The `AMAPP API` returns the result (`Operation Confirmation or Result`) to the `Administrator`.


#### **Data Objects:**

- `User Management Request`: Instructions for creating, updating, or deleting a user account.
- `Role/Permission Management Request`: Instructions to assign or modify a user’s roles and permissions.
- `User Data Response`: Information about user accounts.
- `Role/Permission Data Response`: Information about user roles and assigned permissions.
- `Operation Confirmation or Result`: Feedback on the success or failure of the requested operation.
- **Data Objects:**
  - `User Management Request`: Instructions for creating, updating, or deleting a user account.
  - `Role/Permission Management Request`: Instructions to assign or modify a user’s roles and permissions.
  - `User Data Response`: Information about user accounts.
  - `Role/Permission Data Response`: Information about user roles and assigned permissions.
  - `Operation Confirmation or Result`: Feedback on the success or failure of the requested operation.

#### **Trust Boundaries:**

- `Internet`: Where the `Administrator` submits requests.
- `AMAPP System`: The internal environment where requests are processed and business logic is applied.
- `DB Server`: The secure database zone responsible for storing and retrieving sensitive account and permission data.
- **Trust Boundaries:**
  - `Internet`: Where the `Administrator` submits requests.
  - `AMAPP System`: The internal environment where requests are processed and business logic is applied.
  - `DB Server`: The secure database zone responsible for storing and retrieving sensitive account and permission data.

This diagram exposes the internal flow of user and permission management in the AMAPP system, supporting both system design and security analysis by detailing protocol use, data handling, and boundary enforcement.

---

## Stride

STRIDE is a threat modeling methodology used to categorize and analyze security threats in software systems. It helps identify potential vulnerabilities by classifying them into six main categories, each representing a specific type of threat.

The STRIDE acronym stands for:

- **S – Spoofing**: When an attacker pretends to be another entity (e.g., credential theft or impersonation).
- **T – Tampering**: Unauthorized modification of data or system components.
- **R – Repudiation**: Performing actions that cannot be traced or proven, allowing users to deny their actions without accountability.
- **I – Information Disclosure**: Exposure of sensitive information to unauthorized entities.
- **D – Denial of Service (DoS)**: Making a system or service unavailable to legitimate users by overwhelming or disrupting it.
- **E – Elevation of Privilege**: Gaining higher access rights than initially authorized, often used to escalate attacks.

### Authentication

*_[Blablabla]_*

---

### Create Product

The most relevant potential threats identified in the document [amapp_dfd_create_product_report.md](diagrams/DFD/Create%20Product/amapp_dfd_create_product_report.md), generated from the Level 1 DFD of the product creation process, were selected based on their significance and potential impact. The STRIDE methodology was then applied to these threats to support the analysis and categorization of risks associated with the system.

To select the most important threats listed in the report, the following criteria were used:

1. **High or Critical Severity**: Threats with the greatest potential impact on the system.
2. **Critical Target**: Threats affecting essential components such as input validation, data storage, or communication.
3. **Likelihood of Exploitation**: Threats that are more common or easier to exploit.
4. **Business Impact**: Threats that could compromise sensitive data, cause service disruption, or damage the organization's reputation.


| **Threat**                                    | **Targeted Element**                                      | **STRIDE Category**    | **Description**                                                                                                                                                                                                        | **Mitigation**                                                                                                                                                             |
| --------------------------------------------- | --------------------------------------------------------- | ---------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **INP02 - Overflow Buffers**                  | Validate Input, Store Product, Send Response              | Tampering              | Buffer overflow é uma vulnerabilidade crítica que pode levar à execução de código arbitrário, comprometendo todo o sistema. Como afeta a validação de entrada e o armazenamento, é uma ameaça prioritária. | Use languages or compilers that perform automatic bounds checking. Utilize secure functions and static analysis tools to identify vulnerabilities.                         |
| **INP07 - Buffer Manipulation**               | Validate Input, Store Product, Send Response              | Tampering              | Manipulação de buffer pode ser explorada para corromper dados ou executar código malicioso. É uma ameaça comum em sistemas que lidam com buffers de dados.                                                        | Use secure coding practices to prevent buffer manipulation. Validate input sizes and use tools to detect vulnerabilities.                                                  |
| **AC21 - Cross-Site Request Forgery (CSRF)**  | Send Response                                             | Spoofing               | CSRF pode permitir que um atacante realize ações maliciosas em nome de um usuário autenticado, comprometendo a integridade do sistema e a confiança do usuário.                                                   | Use cryptographic tokens to associate requests with specific actions. Validate HTTP Referrer headers and implement multi-factor authentication for sensitive actions.      |
| **INP23 - File Content Injection**            | Store Product, Send Response                              | Tampering              | A injeção de conteúdo em arquivos pode levar à execução de código remoto, comprometendo o servidor e os dados armazenados.                                                                                      | Validate all input, including files. Place accepted files in a sandbox environment. Use host integrity monitoring and antivirus scanning.                                  |
| **CR06 - Communication Channel Manipulation** | Submit Product, Validated Data, Save to DB, Return Result | Information Disclosure | Manipulação de canais de comunicação pode expor dados sensíveis, como credenciais e informações confidenciais, além de permitir ataques como MITM (Man-in-the-Middle).                                         | Encrypt all sensitive communications using properly configured cryptography. Associate authentication/authorization with each channel/message.                             |
| **AC12 - Privilege Escalation**               | Store Product, Send Response                              | Elevation of Privilege | Escalação de privilégios pode permitir que um atacante obtenha controle total do sistema, comprometendo todos os dados e operações.                                                                               | Carefully manage privileges and follow the principle of least privilege. Implement privilege separation and require multiple conditions for accessing sensitive resources. |
| **INP08 - Format String Injection**           | Store Product, Send Response                              | Tampering              | Injeção de strings de formato pode ser usada para acessar ou modificar dados sensíveis, além de causar falhas no sistema.                                                                                          | Limit the use of string formatting functions. Validate and filter user input for illegal formatting characters.                                                            |
| **DE04 - Audit Log Manipulation**             | Product DB                                                | Repudiation            | Manipulação de logs pode ocultar atividades maliciosas, dificultando a detecção de ataques e comprometendo a integridade do sistema.                                                                               | Follow the principle of least privilege to prevent unauthorized access to logs. Validate input before writing to logs and avoid tools that interpret control characters.   |

---

### Generic Representation

*_[Blablabla]_*

---

### Payments

*_[Blablabla]_*

---

### Product Reservation

The most relevant potential threats identified for the AMAPP Product Reservation System were selected based on their significance and potential impact. The STRIDE methodology was applied to these threats to support the analysis and categorization of risks associated with the system.

The following table outlines the most significant security vulnerabilities requiring immediate attention, along with detailed mitigation strategies for each threat. These recommendations should form the foundation of our security hardening plan to protect customer data, maintain system integrity, and ensure business continuity.

| **Threat** | **Targeted Element** | **STRIDE Category** | **Description** | **Mitigation** |
|------------|---------------------|---------------------|-----------------|----------------|
| INP23 - File Content Injection | Product Catalog | Tampering | Allows attackers to upload malicious files that can be executed through a browser, potentially enabling remote code execution and system compromise. PHP applications with global variables are particularly vulnerable. | • Enforce principle of least privilege<br>• Validate all file content and metadata<br>• Place uploaded files in sandboxed locations<br>• Execute programs with constrained privileges<br>• Use proxy communication to sanitize requests<br>• Implement virus scanning and host integrity monitoring |
| INP07 - Buffer Manipulation | Order Management | Spoofing, Tampering, Elevation of Privilege | Attackers exploit vulnerable code (especially in C/C++) to manipulate buffer contents, potentially allowing arbitrary code execution with the application's privileges. | • Use memory-safe languages (Java, etc.)<br>• Implement secure functions resistant to buffer manipulation<br>• Perform proper boundary checking<br>• Use compiler protections like StackGuard<br>• Apply OS-level preventative functionality |
| AC18 - Session Hijacking | Order Management | Spoofing, Information Disclosure | Attackers capture user session IDs (often via XSS) to impersonate legitimate users and gain unauthorized access to accounts and sensitive data. | • Encrypt and sign identity tokens in transit<br>• Use industry standard session key generation with high entropy<br>• Implement session timeouts<br>• Generate new session keys after login<br>• Use HTTPS for all communications |
| AC21 - Cross Site Request Forgery | Reservation Processing | Spoofing, Tampering | Tricks authenticated users into executing unwanted actions on the application, potentially allowing attackers to modify data or perform unauthorized operations using the victim's identity. | • Implement anti-CSRF tokens for all state-changing operations<br>• Regenerate tokens with each request<br>• Validate Referrer headers<br>• Require confirmation for sensitive actions<br>• Implement proper session handling |
| AC14 - Catching Exception from Privileged Block | Reservation Processing | Elevation of Privilege | Exploits poorly designed error handling to retain elevated privileges when exceptions occur, allowing attackers to perform unauthorized privileged operations. | • Design callback/signal handlers to shed excess privilege before calling untrusted code<br>• Ensure privileged code blocks properly drop privileges on any return path (success, failure, or exception)<br>• Implement proper privilege boundary enforcement |
| CR06 - Communication Channel Manipulation | Browse Products, Place Order | Information Disclosure, Tampering | Attackers perform man-in-the-middle attacks to intercept communications, potentially allowing them to steal sensitive information or inject malicious data into the communication stream. | • Encrypt all sensitive communications with properly-configured cryptography<br>• Implement proper authentication for all communication channels<br>• Use secure protocols and cipher suites<br>• Verify certificate validity |
| INP13 - Command Delimiters | Order Management | Tampering, Elevation of Privilege | Attackers inject special characters into inputs to execute unauthorized commands, potentially allowing SQL injection, LDAP injection, or shell command execution. | • Implement whitelist validation for command parameters<br>• Limit program privileges<br>• Perform thorough input validation<br>• Use parameterized queries (e.g., JDBC prepared statements)<br>• Encode user input properly |
| INP32 - XML Injection | Reservation Processing | Tampering, Information Disclosure | Attackers inject malicious XML code to manipulate application logic, potentially allowing authentication bypass, data exposure, or system compromise. | • Implement strong input validation for XML content<br>• Filter illegal characters and XML structures<br>• Use custom error pages to prevent information leakage<br>• Implement proper XML parsing with schema validation |
---

---

### Registration

*_[Blablabla]_*

---

### User Management

*_[Blablabla]_*

---

## Use Cases and Abuse Cases

*_[Blablabla]_*

### Authentication

![Use and Abuse Cases - Authentication](diagrams/Abuse%20Cases/auth-abuse-case.png)

*_[Blablabla]_*

---

### Create Product

![Use and Abuse Cases - Create Product](diagrams/Abuse%20Cases/createProduct-abuse-cases.png)

This diagram represents a security-focused approach using both **Use Cases** and **Abuse Cases** within the product creation process. The main goal is to identify potential threats to the system and link them with appropriate countermeasures.

#### **Use Cases**

- **Submit Product**
  The producer submits new product data through the API. This is the starting point of the product creation process.
- **Validate Input**
  The submitted data is validated for structure, format, and required fields. This ensures data quality and integrity before persistence.
- **Store Product**
  After successful validation, the product data is saved to the database.
- **Send Response**
  The system sends a response to the producer, indicating success or failure, including validation or error messages.

#### **Abuse Cases**

- **Submit Malicious Product Data**
  An attacker attempts to send malicious content (e.g., scripts or SQL commands) disguised as product data.
- **Exploit Validation Loopholes**
  The attacker exploits weaknesses or omissions in the validation logic to inject invalid or harmful data.
- **Inject Malicious Code into Product Data**
  Product fields are manipulated with malicious code (e.g., XSS or SQL injection), taking advantage of weak validation.
- **Tamper with Stored Product Data**
  The attacker tries to directly alter stored data, compromising the integrity of the database.
- **Flood API with Product Submissions**
  A denial-of-service (DoS) attack where the attacker continuously submits product creation requests to overload the system.

#### **Countermeasures**

- **Input Validation and Sanitization**
  Protects against malicious input by validating and sanitizing all fields. Mitigates abuse cases AC1 and AC2.
- **Rate Limiting and Throttling**
  Limits the number of requests allowed per user over time, preventing system overload. Mitigates AC5.
- **Secure Coding Practices**
  Involves practices like avoiding `eval`, using parameterized queries, and applying strict input validation. Mitigates AC3.
- **Database Access Controls**
  Restricts direct access and enforces permission controls on the database, preventing unauthorized modifications. Mitigates AC4.
- **Monitoring and Alerts**
  Continuous monitoring to detect abnormal behavior and trigger automated alerts. Acts as a monitoring measure for AC5.

This model provides a clear foundation for threat analysis, illustrating how the system could be exploited and what preventive measures are in place.

---

### Payments

![Use and Abuse Cases - Payments](diagrams/Abuse%20Cases/pay-del-rep-abuse-case.png)

*_[Blablabla]_*

---

### Product Reservation

![Use and Abuse Cases - Product Reservation](diagrams/Abuse%20Cases/product-reservation-abuse-cases.png)

The diagram illustrates the **Use Cases**, **Abuse Cases**, and **Countermeasures** for the AMAP System. 

Legitimate actors, like the Co-Producer, interact with the system to:
- **Browse product catalogs**
- **Select products**
- **Place orders**

However, potential threats arise from malicious users attempting abuse cases such as:
- **Submitting fraudulent orders**
- **Performing SQL injections**
- **Intercepting user credentials**
- **Manipulating product prices**

To mitigate these threats, the system implements strong countermeasures:
- **Using secure connections** prevents interception of sensitive data.
- **Two-factor authentication (2FA)** protects user accounts against unauthorized access.
- The server **verifies order details**, validating prices and quantities against the database to prevent price manipulation.

This model ensures a balanced view of normal functionality and security needs, aligning protective mechanisms with potential vulnerabilities to maintain the system’s integrity.

---

### Registration

![Use and Abuse Cases - Registration]()

*_[Blablabla]_*

---

### User Management

![Use and Abuse Cases - User Management](diagrams/Abuse%20Cases/user-management-abuse-case.png)

*_[Blablabla]_*

---

## Threat Classification

*_[Blablabla]_*

---

## Mitigations and Countermeasures

*_[Blablabla]_*

---

## Threat Profile

*_[Blablabla]_*

---

## Conclusion

*_[Blablabla]_*

---

## References

*[Bibliographic references go here, in ACM-Reference-Format]*
