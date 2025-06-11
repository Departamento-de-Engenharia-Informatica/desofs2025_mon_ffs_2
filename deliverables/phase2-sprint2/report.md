| ![Logo ISEP](figs/logoisep.png) | ![Logo DEI](figs/logo_DEI_big_transparente.png) |
| :-----------------------------: | :---------------------------------------------: |

# Phase 2: Sprint 2

**_Group desofs2025_mon_ffs_2_**
*DESOFS*

**Master in Informatics Engineering - 2024/2025**

**Students:**

Ilídio Magalhães - 1191577 <br>
Hugo Coelho - 1162086 <br>
Paulo Abreu - 1240481 <br>
Pedro Oliveira - 1240482 <br>

**Location:** Porto, May 26, 2025

---

## Table of Contents

- [Phase 2: Sprint 2](#phase-2-sprint-1)
  - [Table of Contents](#table-of-contents)
  - [Introduction](#introduction)
    - [Objectives](#objectives)
    - [Scope](#scope)
  - [Project Analysis](#project-analysis)
    - [Project description](#project-description)
    - [Domain Model](#domain-model)
    - [Component Diagram](#component-diagram)
    - [Application Users](#application-users)
      - [Producer](#producer)
      - [Co-Producer](#co-producer)
      - [AMAP Administrators](#amap-administrators)
    - [Use Cases](#use-cases)
    - [Functional Requirements](#functional-requirements)
      - [UC01 - Manage Users/Roles](#uc01---manage-usersroles)
      - [UC02 - Manage Delivery Settings](#uc02---manage-delivery-settings)
      - [UC03 - Manage Products](#uc03---manage-products)
      - [UC04 - Manage AMAP Details](#uc04---manage-amap-details)
      - [UC05 - Login](#uc05---login)
      - [UC06 - Register](#uc06---register)
      - [UC07 - View/Search Products](#uc07---viewsearch-products)
      - [UC08 - View Order History](#uc08---view-order-history)
      - [UC09 - View Delivery](#uc09---view-delivery)
      - [UC10 - View Orders](#uc10---view-orders)
      - [UC11 - Manage Orders](#uc11---manage-orders)
      - [UC12 - Manage Profile](#uc12---manage-profile)
    - [Non-Functional Requirements](#non-functional-requirements)
      - [1. Performance](#1-performance)
      - [2. Availability](#2-availability)
      - [3. Scalability](#3-scalability)
      - [4. Security](#4-security)
      - [5. Reliability and Integrity](#5-reliability-and-integrity)
      - [6. Maintainability](#6-maintainability)
      - [7. Portability](#7-portability)
      - [8. Monitoring and Alerts](#8-monitoring-and-alerts)
    - [Security Requirements](#security-requirements)
      - [Functional Security Requirements (CIA-Based)](#functional-security-requirements-cia-based)
        - [Confidentiality](#confidentiality)
        - [Integrity](#integrity)
        - [Availability](#availability)
      - [Non-Functional Security Requirements](#non-functional-security-requirements)
  - [Risk Assessment](#risk-assessment)
    - [Risk Register](#risk-register)
  - [Development](#development)
    - [Technology Used](#technology-used)
    - [Structure](#structure)
  - [Authentication](#authentication)
  - [User Management](#user-management)
  - [Input Validation](#input-validation)
    - [Create Product](#create-product)
  - [Other Relevant Practices Adopted](#other-relevant-practices-adopted)
    - [Rate limiting - DDOS](#rate-limiting---ddos)
    - [CSRG](#csrg)
    - [TLS/SSL Encryption](#tlsssl-encryption)
    - [CORS](#cors)
    - [Dto's](#dtos)
    - [Logout com revoke do token](#logout-com-revoke-do-token)
    - [Branch Management](#branch-management)
      - [Peer Review Process](#peer-review-process)
      - [Branch Protection Rules](#branch-protection-rules)
  - [Pipeline](#pipeline)
    - [Job 1: Code Analysis (SAST with CodeQL)](#job-1-code-analysis-sast-with-codeql)
    - [Job 2: Build and Test](#job-2-build-and-test)
    - [Job 3: Dependency Security Scan (SCA)](#job-3-dependency-security-scan-sca)
    - [Job 4: Code Quality Analysis](#job-4-code-quality-analysis)
    - [Job 5: OWASP ZAP Baseline Scan (DAST)](#job-5-owasp-zap-baseline-scan-dast)
    - [Job 6: Deployment](#job-6-deployment)
  - [ASVS](#asvs)
  - [Conclusion](#conclusion)
    - [Sprint Achievements](#sprint-achievements)
    - [Key Accomplishments](#key-accomplishments)
    - [Risk Mitigation Progress](#risk-mitigation-progress)
    - [Areas for Future Enhancement](#areas-for-future-enhancement)
    - [Final Assessment](#final-assessment)

---

## Introduction

Phase 2 Sprint 2 represents the culmination of the DESOFS project, delivering the final set of features and security enhancements for the AMAP platform. Over three weeks, our team embedded robust security controls, fully integrated the DevSecOps pipeline with comprehensive SAST, DAST, and SCA testing, and completed all user management and workflow capabilities—bringing this initiative to a successful close.

### Objectives

The primary objectives include:

- **Development**: Building core functionality following secure coding practices
- **DevSecOps Pipeline**: Implementing CI/CD with integrated security testing (SAST, DAST, SCA)
- **Quality Assurance**: Code reviews, automated testing, and security validation
- **ASVS Compliance**: Demonstrating adherence to security standards through practical implementation

### Scope

This three-week sprint delivers sufficient functionality to showcase security automation capabilities, including automated security scanning, comprehensive testing strategies, and secure development practices with full documentation and traceability between security requirements and implemented controls.

---

## Project Analysis

### Project description

This product is designed to enhance the AMAP (Associação para a Mobilização de Alimentos e Produtos) initiative, a Portuguese organization that connects local producers directly with consumers. AMAP's core model is based on pre-ordering, where consumers place orders before the production cycle begins, ensuring that only the requested products are produced. This reduces food waste, supports local agriculture, and promotes sustainability. AMAP’s principles include sustainability, transparency, local consumption, and community engagement.

The primary objectives of AMAP are to promote sustainable agricultural practices, provide consumers with transparency about the food they purchase, support local economies, and foster community ties between consumers and producers. The system being specified will automate processes like order management, production planning, inventory tracking, and delivery logistics, improving efficiency while maintaining AMAP's core values.

This system will ensure a seamless flow from order to delivery, improving overall operational efficiency. This will help the AMAP to better manage its processes and provide a more transparent and sustainable service to its consumers.

---

### Domain Model

![Domain Model](./diagrams/Domain%20Model/domain_model_diagram.png)

This class diagram represents the **core data structure** of the AMAPP platform, outlining the main entities involved in the management of orders between co-producers and producers.

- The base class `User` is extended by three user types: `Producer`, `AMAPAdministrator`, and `CoProducer`, each with specific roles.
- Producers can **create products**, which are tracked in an associated `Inventory` component.
- Co-producers can **place orders** (`Order`), which consist of multiple `OrderItem` elements, each linked to a specific product.
- Orders are associated with **delivery logistics**, managed through the `Delivery` class, under the supervision of the `AMAPAdministrator`.
- Inventory updates are reflected based on `OrderItem` activity.

---

### Component Diagram

![Component Diagram](./diagrams/Component%20Diagram/ComponentDiagram.jpg)

The component diagram illustrates the architecture of the system developed to support the AMAP initiative. The system is composed of two main subsystems: the AMAP System and the AMAP Database Server.

- The AMAP System includes the AMAP BackEnd component, which is responsible for handling core business logic, managing interactions with users through the AMAP API, and orchestrating internal processes such as order management, production planning, and delivery scheduling.
- The AMAP Database Server hosts the AMAP Database component, which stores all critical data related to users, orders, producers, and inventory. Communication between the backend and the database is handled via the AmapDB_API, ensuring a secure and structured data flow.

This architecture ensures modularity and separation of concerns, supporting maintainability and scalability. It enables efficient management of AMAP's core operations while aligning with its principles of transparency, sustainability, and local community support.

---

### Application Users

#### Producer

Responsible for the production and management of products available within the AMAP system, the producer is the main supplier in the community. Producers update the platform with information about product availability, production cycles, and inventory, which allows consumers to know exactly what is available each quarter. In addition, they ensure that production aligns with the orders placed beforehand, minimizing waste and maximizing sustainability. This user class has permissions to manage and adjust production data, keeping operations synchronized with consumer orders.

#### Co-Producer

Also known as co-producers, consumers play an active role in AMAP's sustainable model by ordering products directly from producers. These users engage in a long-term commitment, supporting local consumption and securing regular orders, typically on a quarterly basis. They have access to detailed information about the products, origin, and production practices, and can track order status up to delivery. Although they do not have permissions to modify production data, this user class can access product inquiry and ordering functions, along with resources that support sustainable consumption.

#### AMAP Administrators

These users oversee the operational management of the system within AMAP. Acting as intermediaries between producers and consumers, they ensure data accuracy on the platform, address user issues or questions, and uphold AMAP’s values of sustainability and transparency. AMAP administrators have the authority to edit and review system content, facilitate updates or changes in practices, and ensure that digital operations align with organizational objectives. They also handle user support issues and facilitate communication among the different stakeholders.

---

### Use Cases

![Use Cases Diagram](./diagrams/UseCases/UseCases.png)

The use case diagram above illustrates the interactions between the main actors of the AMAP system and its functionalities. The identified actors are: Unauthenticated User, Co-Producer, Producer, and AMAP Administrator. Each actor interacts with the system through specific use cases that reflect their roles and responsibilities within the platform. This diagram provides a high-level view of the system's functional scope and user access paths.

---

### Functional Requirements

The functional requirements listed below are organized by use case and describe the essential system functionalities expected to be implemented. Each requirement corresponds to an action or capability that the system must support to fulfill user needs, aligned with their roles and privileges. These requirements serve as the foundation for the system’s behavior and guide the development and testing phases.

#### UC01 - Manage Users/Roles

- **REQ-01**: Add a role to a user.
- **REQ-02**: Change user role.
- **REQ-03**: View current role configurations.
- **REQ-04**: Modify criteria and role permissions.

#### UC02 - Manage Delivery Settings

- **REQ-05**: Create the delivery settings.
- **REQ-06**: Update the delivery settings.

#### UC03 - Manage Products

- **REQ-07**: Add new products to the catalog.
- **REQ-08**: Update existing product information.
- **REQ-09**: Deactivate products from the catalog.

#### UC04 - Manage AMAP Details

- **REQ-10**: View current AMAP information.
- **REQ-11**: Update location details and contact information.

#### UC05 - Login

- **REQ-12**: Authenticate users based on entered credentials and role privileges.
- **REQ-13**: Recover user account password via a recovery link or code sent to a registered email or phone number.

#### UC06 - Register

- **REQ-14**: Create a new user account.

#### UC07 - View/Search Products

- **REQ-15**: View list of products from the AMAP, with filtering options for product type, producer, and availability.
- **REQ-16**: View detailed information about a product, including information about the producer, type, and availability.

#### UC08 - View Order History

- **REQ-17**: Provides access to a comprehensive history of all past orders within the AMAP, including key details such as requested products and order status. It also allows the actor to filter the history by parameters like product, date, or status to easily locate specific orders.

#### UC09 - View Delivery

- **REQ-18**: Access detailed information about scheduled deliveries, including date, address, and delivered products, and can filter deliveries by criteria such as date or delivery status.
- **REQ-19**: View detailed information about the status of each delivery, such as "pending", "in progress", or "completed".

#### UC10 - View Orders

- **REQ-20**: Display all active and completed orders.
- **REQ-21**: Provide detailed information for each order.
- **REQ-22**: Allow filtering and sorting orders by date, status, product, co-producer or producer.
- **REQ-23**: Provide detailed information for each order.

#### UC11 - Manage Orders

- **REQ-24**: Co-producer can make new order.
- **REQ-25**: Co-producer can see the details of their orders.
- **REQ-26**: Co-producer can update their orders.
- **REQ-27**: Producer can see the list of orders for their products.
- **REQ-28**: Producer can update orders that contain their products.

#### UC12 - Manage Profile

- **REQ-29**: The actor is capable of viewing and editing profile details, including address, contact information, and other personal data.
- **REQ-30**: The actor can upload important documents, such as organic certification or other credentials, to verify their qualifications.

---

### Non-Functional Requirements

The non-functional requirements define quality attributes and technical constraints that the AMAPP system must meet, ensuring robustness, performance, and ease of use and maintenance.

#### 1. Performance

- The system must respond to user requests in under 1 second under normal operating conditions.
- The API must be capable of processing at least 100 requests per second in a production environment, ensuring low latency and high throughput.
- Page load times in the frontend should not exceed 2 seconds in 95% of accesses.

#### 2. Availability

- The application must be available to users 24/7, except during scheduled maintenance periods.
- The infrastructure must ensure **fault tolerance**, maintaining functionality of at least one critical component (API, database, frontend) in case of partial failure.
- The system must achieve a **minimum availability of 99%** during regular operating hours (08:00–22:00).

#### 3. Scalability

- The system architecture must support **horizontal scalability**, allowing backend services to run in multiple instances.
- The system must handle up to **3 times the average number of active users** without noticeable performance degradation.
- Storage and processing capacity should be adjustable dynamically according to demand.

#### 4. Security

- All communication between client and server must be encrypted via **HTTPS/TLS**.
- Authentication must be implemented using **secure, expiring tokens (e.g., JWT)**, with support for refresh tokens.
- The system must enforce **role-based access control** (e.g., AMAP manager, producer, co-producer).
- Measures must be in place to protect against common attacks such as **SQL Injection, XSS, CSRF, and brute-force login attempts**.
- Passwords must be securely stored using strong hashing algorithms (e.g., bcrypt or Argon2).

#### 5. Reliability and Integrity

- Critical operations (e.g., orders and payments) must guarantee **persistence and atomicity**, even in the event of partial failures.
- There must be **error and event logging** for **auditability** and recovery.
- The system must follow the **ACID principle** for relational database operations, and eventual consistency for MongoDB operations.

#### 6. Maintainability

- The code must follow **software engineering best practices**, such as separation of concerns, design patterns, and documentation.
- The system must allow for **modular updates**, enabling new features to be added without requiring full downtime.
- There should be **test coverage above 80%** for critical features, including unit, integration, and acceptance tests.

#### 7. Portability

- The application must be **compatible with Linux environments**, preferably via **Docker containers** orchestrated with **Kubernetes**.
- The API must follow **RESTful standards**, enabling future integration with other systems and external services.
- The system should be easily deployable in both local and cloud environments.

#### 8. Monitoring and Alerts

- The system must expose **technical metrics** (CPU, memory, latency, throughput, etc.) compatible with tools like **Prometheus and Grafana**.
- There must be **automatic alerts** for critical errors, service failures, or performance degradation, ensuring rapid response to incidents.
- Logs must be centralized and analyzable with tools such as the **ELK Stack** or **Grafana Loki**.

---

### Security Requirements

This report pulls together AMAPP’s security requirements by CIA (confidentiality, integrity, availability), includes a checklist mapped to functional/non-functional specs, and uses PyTM-generated DFDs, abuse cases, and CAPEC/CWE threat reports to cover every risk.

#### Functional Security Requirements (CIA-Based)

##### Confidentiality

- FS01: The system must authenticate all users (OAuth 2.0, JWT).
- FS02: The system must enforce role-based access control (RBAC).
- FS03: All communication must use HTTPS/TLS.
- FS04: Sensitive data must be encrypted in transit and at rest.
- FS05: Authentication tokens must be unique per session and revocable.

##### Integrity

- FS06: All input must be validated and sanitized (whitelisting, type, size).
- FS07: Protection against injection attacks (SQL, XML, LDAP, etc.).
- FS08: Audit logs must be tamper-proof and protected from modification.
- FS09: Error messages must not expose internal system details.

##### Availability

- FS10: The system must implement rate limiting and DoS protections (flooding, resource exhaustion).
- FS11: Regular, automated backups must be supported and tested.
- FS12: The system must support high availability (clustering, replication).

#### Non-Functional Security Requirements

- NFS01: System uptime must be ≥ 99.5% under load.
- NFS02: The system must scale horizontally without compromising security.
- NFS03: Audit logs must ensure integrity via hashing or signing.
- NFS04: Real-time anomaly detection and alerting must be in place.
- NFS05: Incident response time must be ≤ 2 hours.
- NFS06: MFA must not degrade system performance by more than 15%.
- NFS07: Backup recovery must take no longer than 30 minutes.
- NFS08: Incident response procedures must be documented.
- NFS09: Logs must be protected and retained for at least one year.

---

## Risk Assessment

1. **Threat Identification**
   We begin with the STRIDE-based threat model, which enumerates potential attacks across Spoofing, Tampering, Repudiation, Information Disclosure, Denial of Service, and Elevation of Privilege.
2. **Scoring Criteria**Each threat is evaluated on four dimensions:

- `Severity`: potential damage if exploited (1–5)
- `Asset Criticality`: importance of the targeted component (1–5)
- `Likelihood`: probability of successful exploitation given existing controls (1–5)
- `Business Impact`: financial, operational, reputational, or regulatory consequences (1–5)

3. **Risk Calculation**
   We compute the **Risk Score** as:  Risk Score = Likelihood × ((Severity + Asset Criticality) ÷ 2)
4. **Risk Prioritization**

- `High`: Risk Score ≥ 15
- `Medium`: 8 ≤ Risk Score < 15
- `Low`: Risk Score < 8

### Risk Register


| Threat                      | Category           | Likelihood | Severity | Asset Criticality | Impact (avg) | Risk Score | Priority |
| --------------------------- | ------------------ | ---------- | -------- | ----------------- | ------------ | ---------- | -------- |
| Authentication Bypass       | Spoofing           | 4          | 5        | 5                 | 5.0          | 20.0       | High     |
| Password Brute Force        | Denial of Service  | 5          | 4        | 3                 | 3.5          | 17.5       | High     |
| Authentication Abuse/Bypass | Spoofing           | 3          | 4        | 4                 | 4.0          | 12.0       | Medium   |
| Buffer Overflow             | Tampering          | 3          | 4        | 4                 | 4.0          | 12.0       | Medium   |
| Session Hijacking           | Spoofing           | 3          | 4        | 4                 | 4.0          | 12.0       | Medium   |
| Cross-Site Request Forgery  | Spoofing/Tampering | 3          | 3        | 3                 | 3.0          | 9.0        | Medium   |
| Fake Registration           | Spoofing           | 3          | 3        | 3                 | 3.0          | 9.0        | Medium   |
| Admin Impersonation         | Spoofing           | 2          | 4        | 4                 | 4.0          | 8.0        | Medium   |

> **Comment:**We averaged **Severity** and **Asset Criticality** to derive **Impact**, then multiplied by **Likelihood**.
>
> - Two **High**-priority threats (Authentication Bypass and Password Brute Force) exceed 15, indicating immediate focus on strong MFA, rate-limiting, and token validation.
> - The remaining **Medium**-priority threats (scores 8–12) cover authorization weaknesses, buffer issues, and CSRF
> - No threats currently fall into **Low**, but periodic review may reclassify them as controls mature.

---

## Development

During the first sprint, the development team laid the system’s foundation by implementing core domain aggregates such as Product, Order, Delivery, and Users. These aggregates encapsulate essential business logic and are key to the platform’s operations.

The project follows Onion Architecture, promoting separation of concerns, maintainability, testability, and security. The structure includes:

- Domain models
- DTOs with input validation
- Mapping profiles between DTOs and domain models
- Repositories for data persistence
- Services with business logic
- Controllers for REST API interactions

From a security perspective, several measures have been applied:

- Input Validation: DTOs enforce strict validation rules to block malformed or malicious input.
- JWT Authentication: Stateless token-based authentication ensures secure identity and access control.
- Test Coverage: Unit and integration tests validate component behavior and interactions.
- Secure CI/CD Pipeline: Includes code reviews, SAST, DAST, SCA, and automated security testing.
- Secret Management: GitLeaks is integrated to detect hardcoded secrets in source control.
- Dependency Scanning: Regular analysis for vulnerabilities in third-party libraries.
- Monitoring & Alerts: Early detection of runtime issues in production environments.

### Technology Used

The system is being developed using .NET 8, a modern and high-performance framework for building robust and scalable web APIs. This version of .NET provides improved minimal APIs, enhanced performance, and better integration with modern development tools.

For data persistence, the project uses a PostgreSQL relational database. PostgreSQL was chosen for its reliability, strong support for ACID transactions, advanced querying capabilities, and scalability, making it well-suited for managing complex order and production data in the AMAP context.

To test and validate the API endpoints, two tools are being used:

- **Postman**: to manually test requests and automate testing collections.
- **Swagger**: to provide interactive API documentation and facilitate testing during development. Swagger also serves as a reference for developers and stakeholders to understand the available endpoints and expected inputs/outputs.

This technology stack ensures the system is maintainable, testable, and performant, supporting the goals of automation, transparency, and operational efficiency within the AMAP initiative.

### Structure

![Structure Representation](./figs/Structure.jpg)

The project follows a modular and maintainable Onion Architecture, which emphasizes the separation of concerns and dependency inversion. This architecture places the domain and core logic at the center, with infrastructure and external dependencies in the outer layers.

The solution is structured into clear and well-defined folders:

- **Controllers**: Contain the API endpoints responsible for handling HTTP requests and returning appropriate responses.
- **Services**: Contain the business logic of the application, implementing the use cases and interacting with repositories.
- **Repository**: Encapsulates data access logic and abstracts interactions with the PostgreSQL database.
- **Data / Migrations**: Responsible for managing Entity Framework configurations and database migrations.
- **Models / DTOs / Profiles**: Define the domain entities, Data Transfer Objects (DTOs), and AutoMapper profiles used to map between them.
- **Middlewares**: Custom middleware for handling exceptions, and request validation.
- **Configurations & Utils**: Store system-wide configuration logic and utility classes.

Additional files include:

`appsettings.json` & `appsettings_docker_db.json`: Application configuration files for different environments.

`docker-compose.yaml`: Used to orchestrate and run the PostgreSQL database in a containerized environment.

`Program.cs`: The entry point of the application, where services are configured and the web host is built.

`CIpipeline.yaml`:
The project also includes a GitHub Actions pipeline, which supports continuous integration and delivery workflows. This ensures that the project remains consistent, maintainable, and ready for deployment as it evolves.
This architecture allows for scalability, testability, and easier maintenance, making it ideal for the long-term sustainability goals of the AMAP system.

---

## Authentication

The authentication system implements comprehensive security measures aligned with modern security standards and best practices.

**Strong Password Requirements**: The system enforces robust password policies requiring a minimum of 12 characters with mandatory inclusion of uppercase letters, lowercase letters, numbers, and special characters. Additionally, passwords must contain at least 3 unique characters and include non-alphanumeric characters, following current security guidelines that prioritize password strength through length and complexity.

**Account Lockout Protection**: The system implements automatic account lockout after exactly 5 failed authentication attempts. When an account is locked, it remains inaccessible for 30 minutes before automatically unlocking. This mechanism applies to all users, including newly created accounts, providing robust protection against brute force attacks while balancing security with user accessibility.

**Two-Factor Authentication (2FA)**: Multi-factor authentication is implemented using email-based token verification. When 2FA is enabled for a user account, the system generates a time-limited authentication token that is sent via email. Users must provide both their password and the received token to complete the authentication process, significantly enhancing account security beyond traditional password-only authentication.

**Secure JWT Token Management**: The authentication system uses JWT tokens with comprehensive validation including issuer verification, audience validation, and signature verification. Tokens have configurable expiration times and implement proper blacklist management through a dedicated TokenBlacklistService. The logout functionality ensures complete token revocation by adding JWT IDs to a blacklist, preventing token reuse even if intercepted.

**Rate Limiting Protection**: The system implements rate limiting with a fixed window policy allowing 100 requests per minute per client, with a queue limit of 2 additional requests. This protects against automated attacks and ensures fair resource usage across all users.

**Role-Based Authorization**: Comprehensive role-based access control is implemented with specific policies for different user types (Administrator, Producer, CoProducer, Amap) and business-specific permissions for managing products, subscriptions, payments, and reports.

**HTTPS and Security Headers**: The system enforces HTTPS in production environments with proper security configurations including HSTS (HTTP Strict Transport Security) for enhanced connection security.

---

## Role Policies

The system implements a comprehensive role-based access control (RBAC) model that governs user access across all application functionality. Access control is enforced through role-based permissions combined with ownership verification and business logic constraints.

### User Roles and Capabilities

**Administrator**

- Complete system oversight with full access to all resources
- Can manage all products, orders, and deliveries regardless of ownership
- Access to comprehensive reporting across all users
- Exclusive control over delivery creation, updates, and deletion

**Producer**

- Product lifecycle management for their own products only
- View orders and deliveries related to their products
- Update order items when contextually appropriate
- Cannot create orders or manage deliveries

**CoProducer**

- Order management including creation, updates, and item modifications
- Access to their own orders and related deliveries
- Generate and download personal reports
- Cannot manage products or system-wide deliveries

### Access Control Matrix

**Product Management**
All product operations (create, update, delete) require the "CanManageProducts" policy, which grants access to Producer and Administrator roles. Product viewing is publicly available, with detailed product information accessible to anonymous users for transparency.

**Order Operations**
Order creation and management is primarily restricted to CoProducers, who can create orders and manage order items with strict ownership verification. Administrators have read-only access to all orders for oversight purposes. Producers can view orders related to their products and update order items within their product context.

**Delivery Management**
Delivery operations are heavily restricted, with only Administrators having full control over delivery creation, updates, and deletion. CoProducers and Producers can view deliveries related to their respective areas of responsibility, but with ownership verification to ensure users only access relevant delivery information.

**Reporting and Analytics**
Report access follows the "CanViewReports" policy, allowing CoProducers to download their own reports while Administrators can access reports for any valid CoProducer. This enables both self-service reporting and administrative oversight.

---

## Input Validation

The system implements comprehensive input validation across all user-facing functionality using FluentValidation framework with custom security extensions to prevent injection attacks and ensure data integrity.

**FluentValidation Framework Implementation**
The application uses FluentValidation as the primary validation mechanism, with dedicated validator classes for each DTO category. Every major functional area has its own validation folder structure (Auth/Validators, Order/Validators, Product/Validators, Delivery/Validators) ensuring organized and maintainable validation rules.

**Custom Security Extensions**
A centralized SecurityExtensions class provides reusable validation methods that are consistently applied across all DTOs:

- **NoUnsafeChars()**: Blocks dangerous characters (`< > " ' &`) that could enable XSS attacks
- **SafeName()**: Validates names with Portuguese character support including accents and special characters while preventing malicious input
- **SafeText()**: Provides general text validation that maintains security without breaking legitimate use cases
- **StrongPassword()**: Enforces robust password policies with 12-character minimum, requiring uppercase, lowercase, numbers, and special characters

**Comprehensive Validation Coverage**
Every DTO in the system has corresponding validators that implement multiple validation layers:

- **Data Type Validation**: Ensures correct data types and formats for all fields
- **Length Restrictions**: Prevents buffer overflow attacks through appropriate field length limits
- **Business Logic Validation**: Enforces domain-specific rules and constraints
- **Security Validation**: Blocks potential injection attempts and malicious input patterns
- **Format Validation**: Ensures proper email formats, numeric ranges, and acceptable character sets

**Localization and User Experience**
The validation system supports Portuguese language requirements while maintaining security standards. Name validation accommodates Portuguese characters, accents, and cultural naming conventions without compromising security integrity. Error messages are clear and helpful without exposing internal system details.

**Validation Pipeline Integration**
FluentValidation is integrated into the ASP.NET Core request pipeline, ensuring all incoming data is validated before reaching business logic. Invalid requests are immediately rejected with detailed error responses, preventing malformed or malicious data from entering the system. The validation occurs at the controller level through ModelState validation, providing early detection of security threats and data integrity issues.

**Consistent Security Standards**
All validators follow consistent security patterns including email format validation (max 254 characters), password strength requirements, proper handling of optional fields, and prevention of common attack vectors such as SQL injection and cross-site scripting through input sanitization.

### Create Product

To enhance security during product creation, we implemented comprehensive image validation using the ImageSharp library. This validation process includes multiple layers of security checks:

**File Format Validation**: The system validates image files by checking both file extensions (.jpg, .jpeg, .png, .webp) and MIME types (image/jpeg, image/png, image/webp) to ensure only legitimate image formats are accepted.

**Binary Signature Verification**: Each uploaded image undergoes binary signature validation to verify the file's actual format matches its extension, preventing file type spoofing attacks where malicious files are disguised with image extensions.

**Content Security Scanning**: The system scans image content for suspicious patterns such as embedded scripts (`<script`, `javascript:`, `<?php`, `<%`, `eval(`), preventing potential code injection attacks through image uploads.

**File Size Restrictions**: Images are limited to a maximum size of 5MB to prevent denial-of-service attacks and ensure reasonable storage usage.

**Image Processing and Sanitization**: Valid images are processed using ImageSharp to remove potentially dangerous metadata (EXIF data) and resize images that exceed 2048x2048 pixels, ensuring consistent and secure image handling.

This multi-layered approach significantly reduces the attack surface for image-based vulnerabilities while maintaining a smooth user experience for legitimate file uploads.

---

## Other Relevant Practices Adopted

To ensure a secure and robust development lifecycle, the team adopted a structured workflow with multiple security layers and quality controls.

### Rate Limiting - DDoS Mitigation

Rate limiting middleware is implemented to throttle incoming requests and prevent abuse. The API startup configuration includes policies that limit the number of requests a client can make within specific time windows (per IP or user). This application-level protection helps mitigate Denial of Service attacks by slowing down or blocking excessive requests, making it harder for attackers to flood the system. While not a complete defense against large-scale DDoS attacks, it provides an important security layer to absorb basic flooding and brute-force attempts.

### CSRF Protection

Cross-Site Request Forgery protection is addressed through the API's token-based authentication design. Since the system uses JWT tokens in Authorization headers rather than cookies, it's inherently less vulnerable to CSRF attacks - browsers don't automatically attach JWTs to cross-site requests. Combined with strict CORS configuration, this prevents unauthorized cross-site calls. The stateless token approach and origin restrictions effectively mitigate CSRF risks without requiring additional anti-forgery services.

### TLS/SSL Encryption

All client-server communications are secured via TLS/SSL encryption. The API is accessible only over HTTPS, ensuring data in transit is encrypted and protected from eavesdropping or man-in-the-middle attacks. The server is configured to redirect or refuse HTTP requests and only serve content over TLS, complying with industry best practices for transport-level encryption of sensitive information including credentials and personal data.

### CORS (Cross-Origin Resource Sharing)

CORS is deliberately restricted to allow only known, trusted origins to consume the API. The configuration uses allow-list rules specifying trusted domains while restricting methods and headers to only those required. This maintains browser same-origin protections while permitting legitimate cross-origin access for the intended frontend. The configuration avoids overly-broad settings that could introduce security risks.

### DTOs (Data Transfer Objects)

The application employs DTOs for input and output models rather than binding client requests directly to database entities. This prevents mass assignment and over-posting vulnerabilities by defining only the fields that clients are allowed to provide or receive. DTOs include data annotations and validation logic, acting as a security layer that whitelists acceptable data before mapping to domain models.

### Logout with Token Revocation

The system implements JWT token revocation on logout to enhance security. When users log out, the API adds the token identifier to a server-side blocklist, preventing further use even before natural expiration. Custom validation logic checks this blocklist on each request, rejecting revoked tokens. This approach ensures immediate session termination and protects against abuse of leaked or persisted tokens.

### Branch Management

The repository uses two main branches with different protection levels:

- **`development`** (default branch): Integration environment where new features are first tested. Requires approval from one team member for merge, enabling rapid development iteration.

- **`main`** (production branch): Represents production code with strict controls. Requires explicit approval from all three remaining team members before any merge, ensuring maximum stability and security.

#### Peer Review Process

All code changes are introduced through pull requests with mandatory review, ensuring security concerns, coding standards, and potential vulnerabilities are addressed early in the development cycle.

#### Branch Protection Rules

Key protection rules enforced at repository level:

- **Restrict Deletions**: Prevents protected branches from being deleted, avoiding accidental loss of critical code
- **Require Pull Request Before Merge**: All changes must go through pull requests, promoting code review and preventing direct pushes
- **Block Force Pushes**: Disabled to protect branch history integrity and prevent unwanted overwrites

These practices create a multi-layered approach that significantly reduces security flaws reaching production while maintaining clear separation between development and production environments.

---

## Pipeline

The CI/CD pipeline consists of six main jobs that execute automated security testing, quality assurance, and deployment processes. The pipeline runs on pushes to main/develop branches, pull requests, weekly schedules, and manual triggers.

### Job 1: CodeQL Security Analysis

Performs static application security testing (SAST) using GitHub's CodeQL engine to identify potential security vulnerabilities in the C# codebase. The analysis runs on every code change and generates SARIF reports that integrate with GitHub's Security tab for vulnerability tracking and remediation.

### Job 2: Build and Test

Executes comprehensive testing including unit tests with code coverage analysis, smoke tests to verify API health and Swagger endpoints, and mutation testing using Stryker to assess test quality. The job runs against a PostgreSQL test database and generates detailed coverage reports. Additionally, performs filesystem scanning with Trivy to detect vulnerabilities in dependencies and build artifacts.

### Job 3: Dependency Security Scan (SCA)

Conducts software composition analysis by checking for vulnerable and outdated NuGet packages, running OWASP Dependency Check to identify known vulnerabilities in dependencies, and generating a Software Bill of Materials (SBOM) using CycloneDX for supply chain transparency. Results include vulnerability reports and recommendations for package updates.

### Job 4: Code Quality Analysis

Performs code quality assessment by scanning for exposed secrets and credentials using Gitleaks, ensuring no sensitive information is accidentally committed to the repository. The analysis helps maintain security best practices and prevents credential leaks in the codebase.

### Job 5: OWASP ZAP Baseline Scan (DAST)

Executes dynamic application security testing by running the API in a test environment and performing automated security scans using OWASP ZAP. The scan identifies runtime vulnerabilities such as injection flaws, authentication issues, and configuration problems that may not be detectable through static analysis.

### Job 6: Deployment

Creates automated deployment packages for pull requests targeting the develop branch. Generates ready-to-run deployment artifacts with startup scripts for both Windows and Linux environments, enabling easy testing of feature branches. The deployment package includes the compiled application, configuration files, and helper scripts for local testing with a 3-day retention period.

---

## ASVS

In this chapter, we compare the ASVS results across both sprints to highlight the improvements we made.

### ASVS Sprint 1

![ASVS - Phase 2 - Sprint 1](./figs/ASVS-sprint1.PNG)

### ASVS Sprint 2

![ASVS - Phase 2 - Sprint 2](./figs/ASVS-sprint2.PNG)

### ASVS Sprint 1 → Sprint 2 Comparison

Over the course of Sprint 2, the security team tackled a large backlog of ASVS requirements, making considerable strides in core areas while a few lower-priority tasks remain pending.

**Significant Improvements**

- **Communication** soared from 0.0 % to 62.5 % after introducing structured messaging validations and sanitization checks.
- **Validation, Sanitization & Encoding** jumped from 29.2 % to 86.4 % as the team completed comprehensive input-sanitization rules and adopted a robust encoding library.
- **Session Management** climbed from 54.5 % to 100.0 % by rolling out secure cookie flags, idle timeouts and session-ID rotation.
- **Authentication** increased from 26.9 % to 68.9 % with the deployment of a multi-factor flow and hardened credential storage.

**Moderate Gains**

- **Access Control** improved from 66.7 % to 100.0 % by finalizing role-based checks across all endpoints.
- **Data Protection** rose from 13.3 % to 46.2 % after implementing encryption-at-rest considerations and enhancing access-logging.
- **Stored Cryptography** moved from 41.7 % to 73.3 % as key-management modules went live.
- **Error Handling & Logging** climbed from 58.3 % to 83.3 % upon introducing structured logs and expanding exception coverage.
- **API & Web Service** increased from 75.0 % to 100.0 % by completing endpoint-hardening tasks.

**Smaller Gains**

- **Files & Resources** rose from 63.6 % to 83.3 % with stricter resource-validation rules.
- **Configuration** ticked up from 38.1 % to 52.6 % as secure-by-default settings were partially enforced.
- **Business Logic** moved from 14.3 % to 28.6 % by adding domain-specific validation and control checks.

**Stagnations**

- **Malicious Code** remained flat at 85.7 %, pending advanced threat-detection implementations.

**Total Composite Change**

- Sprint 1: 44.9 % → Sprint 2: 75.3 %  **(Δ + 30.5 points)** – an exceptional uplift driven by communication, validation, session and authentication enhancements.

**Final Remarks**
Sprint 2 delivered a dramatic surge in ASVS compliance, fundamentally strengthening our security posture across nearly every domain. The remaining gaps in configuration, business logic and data protection will be addressed as priorities during the upcoming release and maintenance cycle, ensuring that these gains are solidified and sustained through to project handover.

---

## Conclusion

This conclusion synthesizes our Sprint 2 deliverables, security achievements, risk mitigation progress, and the path forward through the upcoming release and maintenance cycle.

### Sprint Achievements

* **Core Feature Delivery**: Completed UC01–UC12, enabling full user, product, order, and delivery workflows.
* **Security Pipeline**: Operated six CI/CD jobs covering SAST (CodeQL), DAST (ZAP), SCA, secret scanning, code quality analysis, and automated deployment.
* **ASVS Compliance**: Achieved a composite ASVS score increase from 44.9 % to 75.3 %, with perfect marks in Communication, Session Management, Access Control, and API & Web Service.
* **Risk Management**: Finalized STRIDE threat model and risk register, with high-priority mitigations for authentication bypass and brute-force attacks now in place.

### Key Accomplishments

* **Communication & Validation**: Established structured sanitization and encoding checks, lifting Communication from 0 % to 62.5 % and Validation from 29.2 % to 86.4 %.
* **Session & Authentication Hardening**: Rolled out secure cookie flags, session rotation, idle timeouts, and email-based MFA, boosting Session Management to 100 % and Authentication to 68.9 %.
* **Data & Cryptography**: Introduced encryption-at-rest considerations, enhanced access-logging, and live key-management modules, driving Data Protection to 46.2 % and Stored Cryptography to 73.3 %.
* **Error Handling & API Security**: Added structured logging and exception coverage, and completed endpoint hardening, raising Error Handling to 83.3 % and API & Web Service to 100 %.

### Risk Mitigation Progress

* **High-Priority Controls**: Authentication bypass and brute-force protections strengthened via rate limiting and token revocation.
* **Medium-Priority Enhancements**: Bolstered CSRF defenses, buffer-overflow checks, and session lifecycles to reduce threat likelihood.
* **Monitoring & Alerting**: Integrated Prometheus/Grafana for real-time metrics and anomaly alerts, closing the loop on continuous oversight.

### Areas for Future Enhancement

* **Business Logic** (28.6 %) and **Configuration** (52.6 %): Solidify domain-specific validation rules and full enforcement of secure-by-default settings through IaC and policy-as-code.
* **Data Protection** (46.2 %): Expand encryption coverage and refine key-rotation procedures to meet end-to-end confidentiality goals.
* **Malicious Code Review**: Although stable at 85.7 %, plan periodic reevaluation and advanced detection to guard against evolving threats.

### Final Assessment

Sprint 2 delivered a remarkable 30.5-point uplift in ASVS compliance, transforming our security posture across nearly every domain. With critical controls now at or near full coverage, the forthcoming release and maintenance cycle will focus on cementing these gains—especially in business logic, configuration, and data protection—to ensure a robust, production-ready platform.

---
