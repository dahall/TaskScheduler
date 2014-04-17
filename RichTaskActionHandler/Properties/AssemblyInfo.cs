using System.EnterpriseServices;
using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("RichTaskActionHandler")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(true)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("618F8076-BE87-4EAB-A362-BCD3A9198D47")]

// The ActivationOption attribute indicates whether the component will be 
// activated within the caller's process. You can set Activation.Option to 
// Library or to Server.
[assembly: ApplicationActivation(ActivationOption.Server)]

// The ApplicationName attribute is the name that appears for the COM+  
// application in the COM+ Catalog and the Component Services Administration 
// console.
[assembly: ApplicationName("RichTaskActionHandler")]

// The Description attribute provides a description for the COM+ application 
// in the COM+ Catalog and Component Services Administration console.
[assembly: Description("COM task action for rich Task Scheduler features.")]

// COM+ security setting
[assembly: ApplicationAccessControl(true,   //Authentication is on
	AccessChecksLevel = AccessChecksLevelOption.ApplicationComponent,
	Authentication = AuthenticationOption.Packet,
	ImpersonationLevel = ImpersonationLevelOption.Identify)]

[assembly: SecurityRole("Tester", SetEveryoneAccess = true)]