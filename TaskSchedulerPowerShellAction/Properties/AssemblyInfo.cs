using System.EnterpriseServices;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("TaskSchedulerPowerShellAction")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(true)]

// The following GUID is for the ID of the typelib
[assembly: Guid("0c45799e-c867-4fbf-be00-a20feb3ad7bf")]

// The ActivationOption attribute indicates whether the component will be 
// activated within the caller's process. You can set Activation.Option to 
// Library or to Server.
[assembly: ApplicationActivation(ActivationOption.Server)]

// The ApplicationName attribute is the name that appears for the COM+  
// application in the COM+ Catalog and the Component Services Administration 
// console.
[assembly: ApplicationName("TaskSchedulerPowerShellAction")]

// The Description attribute provides a description for the COM+ application 
// in the COM+ Catalog and Component Services Administration console.
[assembly: Description("")]

// COM+ security setting
[assembly: ApplicationAccessControl(true,   //Authentication is on
	AccessChecksLevel = AccessChecksLevelOption.ApplicationComponent,
	Authentication = AuthenticationOption.Packet,
	ImpersonationLevel = ImpersonationLevelOption.Identify)]

[assembly: SecurityRole("Tester", SetEveryoneAccess = true)]
