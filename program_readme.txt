This small Application creates C# / Simpl # classes for use in a Simpl# Pro Project

This is a programmers tool and created a starting point to work from rather than completed code!

ARCHITECTURE:

The Program reads from an XML file located in the application folder and named "panel.xml"

The Program Creates a folder named "OUPUT" in the application folder and creates all other 
class files within this folder.

For now the only user-settable variable is the namespace that you want the files to reference.

This is entered in runtime at the console.

The class structure created relies on the VTPro.dll Library which was released on CrestronLabs 
as part of the Dynamic Drivers SDK 1.8.1

Once created you can copy the contents of the "OUTPUT" folder into a folder in your simpl# 
project. 

The project also needs the panel sgd file from the same build.

XML FILE CREATION / TP PREP:

The xml file is created by running "File->Generate Project Document..." Save and Compile first 
	nb: The option only shows if you have a page or subpage selected in the Project view

The program creates Methods and classes for use in the program using Object names from VT Pro

Spending some time editing these names from the defaults will make your code hugely more readable. 

NB: ***Smart Object Names should always include the root name created by VTPro*** 

for e.g. "Dynamic Button List" could be changed to "Dynamic Button List_Sources"

TESTING:

I've included a test setup which allows you to test the created classes. dll's are included in a reference folder.

You need to copy the output panel files into this project and compile and check for errors.

You will need to place the correct SGD file from your touchpanel in the NVRAM folder.

And use then use the xpanel to test the registration of the panel.

If the panel does not register then take a look in the error log to see what is causing the problem.

The most likely causes are un-supported smart objects. Which can be re-referenced to a base object in the Simpl# code for. 

POST CREATION:

The method for controls are created empty although some have some commented hints.  

The only functions that will do anything "out of the box" are pageflips and subpage flips.

Buttons are created without any interlocks but a comment section explains the process of adding interlocks.

Smartobject Lists have interlocks enabled as default and all reference an empty List.

Unsupported smartobjects can be used but need to be manually implemented using the generic

VTSmartObject 

SUPPORTED:

Buttons
Sliders (Gauges)
Formatted Texts
Text Entry


KPad
Dpad
Button Lists
Dynamic Button Lists
Dynamic Icon Lists

NOT SUPPORTED:
Checkbox List 
Spinner List
Any Widgets
Subpage Reference 
Draggable List


THINGS TO DO / IDEAS:

Add other smart object support will need to add classes, initialisation methods and update methods.

remove reliance on sgd file?

Port in it's entirity to Simpl# ? To allow Creation on the fly using common Function libraries?

Change the Object List to make it ENUMS rather than Constants.


TESTING:

Carried out testing using Crestron Market place vtp files Included in Testing ZIP.

commercial_basic_rev2.08  and residential_sample_rev2.08