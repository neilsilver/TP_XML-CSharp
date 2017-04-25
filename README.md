# TP_XML-CSharp


This small Console Application creates C# / Simpl # classes for use in a Simpl# Pro Project



ARCHITECTURE:
-----------

The Program reads from an XML file located in the application folder and named "panel.xml"

The Program Creates a folder named "OUPUT" in the application folder and creates all other 
class files within this folder.

For now the only user-settable is the namespace that you want the files to reference.

This is entered in runtime at the console.

The class structure created relies on the VTPro.dll Library which was released on CrestronLabs 
as part of the Dynamic Drivers SDK 1.8.1

Once created you can copy the contents of the "OUTPUT" folder into a folder in your simpl# 
project. 

The project also needs the panel sgd file from the same build.

XML FILE CREATION / TP PREP:
-----------

The xml file is created by running "File->Generate Project Document..." Save and Compile first 
	nb: The option only shows if you have a page or subpage selected in the Project view

The program creates Methods and classes for use in the program using Object names from VT Pro
Spending some time editing these names from the defaults will make your code hugely more readable. 

POST CREATION TASKS:
-----------
The method for controls are created empty although some have some commented hints.  

The only functions that will do anything "out of the box" are pageflips and subpage flips.

Buttons are created without any interlocks but a comment section explains the process of adding interlocks.

Smartobject Lists have interlocks enabled as default and all reference an empty List.

Unsupported smartobjects can be used but need to be manually implemented using the generic

VTSmartObject 

SUPPORT:
-----------
Control Types

Buttons
Sliders
Formatted Texts
Text Entry

Smart Objects:


KPad
Dpad
Dynamic Button Lists


Things to do:
-----------
Add other smart object support ( Dynamic Icon list exsists in the VTPro.dll)

Others do not so implement with VTSmartObject

remove reliance on sgd file.
