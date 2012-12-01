using System.EnterpriseServices;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("COMTask")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("CodePlex Community")]
[assembly: AssemblyProduct("COMTask")]
[assembly: AssemblyCopyright("Copyright © 2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(true)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("02921866-6572-4ef5-b358-2d793ab150af")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.9.2.0")]
[assembly: AssemblyFileVersion("1.9.2.0")]

// The ActivationOption attribute indicates whether the component will be 
// activated within the caller's process. You can set Activation.Option to 
// Library or to Server.
[assembly: ApplicationActivation(ActivationOption.Server)]

// The ApplicationName attribute is the name that appears for the COM+  
// application in the COM+ Catalog and the Component Services Administration 
// console.
[assembly: ApplicationName("My COM Task")]

// Specify the application ID.
//[assembly: ApplicationID("11F3EE74-29A6-4773-82C6-274A67961FB4")]

// The Description attribute provides a description for the COM+ application 
// in the COM+ Catalog and Component Services Administration console.
[assembly: Description("COM Task for Sample ITaskHandler implementation.")]

// COM+ security setting
[assembly: ApplicationAccessControl(true,   //Authentication is on
	AccessChecksLevel = AccessChecksLevelOption.ApplicationComponent,
	Authentication = AuthenticationOption.Packet,
	ImpersonationLevel = ImpersonationLevelOption.Identify)]

[assembly: SecurityRole("Tester", SetEveryoneAccess = true)]