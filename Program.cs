using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Channels;
using System.Security.AccessControl;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using ConvertXmlToCSharpClasses;
using File = ConvertXmlToCSharpClasses.File;
using ExtensionMethods;

/** Program Summary ***********************************************************
 *	Neil Silver www.lightingcontrol.co.uk
 *	07767888871
 *	
 *  25.04.17
 *	
 * SUMMARY:
 * Class Crestor for Simpl# tounchpanels
 *  
 * Converts panel.xml to Class files for use in a Simpl# Pro Project
 * 
 * NOTES:
 *		See "program_readme.txt
 *
 *
 *
 *****************************************************************************/
namespace TP_XML_CSharp
{
    internal class Program
    {
        #region constants and variables
        public const byte indentation = 4;

        public static IDictionary<String, String> typeXMLtoSharp = new Dictionary<string, string>()
        {
            {"addsliders","Gauges"},
            {"addsubpages","Pages"},
            {"addbuttons","Buttons"},
            {"addtextentry","Text"},
            {"addformattedtext","Text"}
            
            

        };
        public static readonly IList<String> unsupportedSmartObjects = new ReadOnlyCollection<string>
    (new List<String> { 
          "Background Selector Horizontal",
            "Spinner List",
            "Checkbox List Vertical" });




        // NAMESPACE
        public const string system_namespace = "myNamespace";

        //XML PANEL FILE
        // Template Filenames
        public const string panel_xml = @"Panel_xml\panel.xml";

        // Template Filenames
        public const string template_pages = @"Templates\Panel_vtpro.template";
        public const string template_objects = @"Templates\Object_vtpro.template";

        //Method templates
        public const string template_button_methods = @"Templates\button_method_vtpro.template";
        public const string template_smartobject_methods = @"Templates\smartobject_methods_vtpro.template";
        public const string template_text_methods = @"Templates\text_methods_vtpro.template";
        public const string template_slider_methods = @"Templates\slider_methods_vtpro.template";

        // Output Filenames
        public const string output_pages = @"\OUTPUT\panel.cs";
        public const string output_objects = @"\OUTPUT\Constants\VTSObjects.cs";
        // Output Filenames Methods
        public const string output_buttons = @"\OUTPUT\Callbacks\{0}\VTButtons.cs";
        public const string output_methods = @"\OUTPUT\Callbacks\{0}\VTSMethods.cs";
        public const string output_texts    = @"\OUTPUT\Callbacks\{0}\VTTextEntryBoxes.cs";
        public const string output_sliders = @"\OUTPUT\Callbacks\{0}\VTSliders.cs";

        public const string output_errors = @"\OUTPUT\Errorlog\Log.txt";
        
        public const string program_readme = @"\\readme.txt";

        #endregion

        private static void Main(string[] args)
        {


            ConsoleApplicationHeader("Crestron TP 2 Class Convertor v1.0");

            Console.WriteLine("Type Y key to read the readme in the consle");
            Console.WriteLine(".");
            Console.WriteLine("Alternatively open the program.readme.txt in a text editor");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {

                ConsoleReadMe(@"\program_readme_brief.txt");
            }

            Console.WriteLine("When you are ready ...");

            WaitForKey();

            //Delete the output folder if it exsists!
            if (Directory.Exists("Output"))
            {
                try
                {
                    //DeleteDirectoriesRecursive("Output");
                    SafeDeleteTestDirectory("Output");
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine(
                        "Error Deleting Output Directory Please ensure no files in the BIN/Output Directory are open.");
                    Console.WriteLine("Application will now exit please close all explorer windows and restart.");
                    Thread.Sleep(1000);

                    System.Environment.Exit(1);

                }

            }
            File TPFILESTRUCTURE = new File();

            Console.Clear();

            // Import XML and Create Object
            TPFILESTRUCTURE = TpImport(panel_xml);

            checkforUnsupportedItems(TPFILESTRUCTURE,unsupportedSmartObjects);

            Console.Clear();

            Console.WriteLine("Type your Namespace Name - Invalid characters will be removed!");

            var system_namespace = checkText(Console.ReadLine());
            // if blank then set to mynamespace
            if (system_namespace == "")
            {
                   Console.WriteLine("Setting Default Namespace as blank entered.");
                   
                   
                   system_namespace = "myNamespace";
            }
         
        

            CreateConstants(TPFILESTRUCTURE, system_namespace);

            

            Console.Clear();
            addLine(8);
            Console.WriteLine("########################");
            Console.WriteLine("Class Creation Completed");
            Console.WriteLine("########################");

            ConsoleReadMe(output_errors);

            #region TESTING
            // Used for local testing copy to Local Folder.

            //Console.WriteLine("Type CONFIRM key to copy to specified Local folder files... NB: This will delete exsisting files in that location");
            // Console.WriteLine("Press any other key to exit program");
            //if (Console.ReadLine() == "CONFIRM")
            //{
            //    CopyOutputToSimplSharp();

            //}
            #endregion

            Console.WriteLine("Press enter to exit the application...");
            Console.ReadLine();
        }

        private static void checkforUnsupportedItems(File tpfilestructure,IList<String> unsupportedcontrols )
        {
            var unupportedfound = false;
            StringBuilder unsupportedControlList = new StringBuilder();
            // Build List of all controls
            var query = from page in tpfilestructure.Page
                        from control in page.Control
                        select control;

            foreach (var control in query)
            {
                if (unsupportedcontrols.Contains(control.Name))
                {
                    unupportedfound = true;
                    unsupportedControlList.Append(string.Format("Unsupported Control Found:{0}",control.Name));
                    unsupportedControlList.AppendLine();

                }
                if (unupportedfound)
                {
                    Console.Clear();
                    Console.Write(unsupportedControlList);
                    Console.WriteLine("Application will now exit  .. remove unsupported items and re-run.");
                    WaitForKey();
                    System.Environment.Exit(1);

                }
                   



               
            }
            
        }

        #region MAIN Process Methods
        // XML File Import
        public static File TpImport(string filename)
        {
            var serializer = new XmlSerializer(typeof(File));

            FileStream fs = new FileStream(filename, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            var PanelFile = (File)serializer.Deserialize(reader);
            
            fs.Close();

                // Initial verification to console
                foreach(var page in PanelFile.Page)
                {
                    Console.WriteLine("PageName:{0}", checkText(page.Name));
                    if (page.DigitalJoin.IsNotNullOrWhiteSpace())
                    {
                        Console.WriteLine("Join:{0}", page.DigitalJoin.Visibility_Digital_Join.EmptyIfNull());
                    }
                    foreach (var controls in page.Control)
                    {
                        Console.WriteLine("Controls:{0}", checkText(controls.Name));
                        
                       
                    }
                    foreach (var so in page.Smart_Object_ID)
                    {
                        Console.WriteLine("Smart Object:{0}", so);
                    }

                    Console.WriteLine("/////////////////////////");
                    
                }


            return PanelFile;




        }
      
        // MAIN Process

        private static void CreateConstants(File panel,string system_namespace)
        {
            var sep = "_";

            StringBuilder addnamespace = new StringBuilder();

            addnamespace.Append(system_namespace);

            StringBuilder addpages = new StringBuilder();
            StringBuilder addsubpages = new StringBuilder();
            StringBuilder addbuttons = new StringBuilder();
            StringBuilder addsliders = new StringBuilder();
            StringBuilder addtextentry = new StringBuilder();
            StringBuilder addformattedtext = new StringBuilder();
            StringBuilder addsmartobjects = new StringBuilder();

            //  String to build the VTS Objects file that holds the SmartObject Join Numbers.
            StringBuilder addsmartobjectconstants = new StringBuilder();
            StringBuilder addsmartobjectcallbacks = new StringBuilder();

            StringBuilder addclassdeclarations = new StringBuilder();
            StringBuilder addclassinitializations = new StringBuilder();

            // Add any Build errors to a log file - not implemented
            StringBuilder buildFile = new StringBuilder();

            buildFile.AppendLine();
            buildFile.AppendLine(string.Format("System Built:{0},Namespace = {1}",DateTime.Now.ToShortDateString(),system_namespace));
            buildFile.AppendLine();

            buildFile.AppendLine("Build File Error List:");
            buildFile.AppendLine();
            buildFile.AppendLine();

            // Dictionary of files that will be created dynamically depending on the page contents.
            Dictionary<String,String> filesDictionary = new Dictionary<string, string>();
            Dictionary<String,String> filesOutputDictionary = new Dictionary<string, string>();
            Dictionary<String,String> filesTemplateDictionary = new Dictionary<string, string>();
            Dictionary<String,StringBuilder>  MethodsStringBuilderDictionary = new Dictionary<string, StringBuilder>();
            Dictionary<String, StringBuilder> MethodNameStringBuilderDictionary = new Dictionary<string, StringBuilder>();
            Dictionary<String, String> MethodClassDeclarationStringBuilderDictionary = new Dictionary<string, String>();



            // Dictionary of Stringbulders used for filtering and then for file creation
            Dictionary<String, StringBuilder> stringBuilders = new Dictionary<string, StringBuilder>();

            stringBuilders.Add("addsubpages", addsubpages);
            stringBuilders.Add("addbuttons", addbuttons);
            stringBuilders.Add("addsliders", addsliders);
            stringBuilders.Add("addtextentry", addtextentry);
            stringBuilders.Add("addformattedtext", addformattedtext);

            addpages.AppendLine();

            // counters
            int countpages = 0; // count pages
            int countsmartobjects = 0; // count smart
           
            foreach (var page in panel.Page)
            {
                // Reset the SmartObject counter for the start of each page
                countsmartobjects = 0;
                
                
                // Create Element for the Page if it has an associated join number
                
                if (page.DigitalJoin.IsNotNullOrWhiteSpace()) // Only create a page element if there is a join!
                {
                    if (page.DigitalJoin.Visibility_Digital_Join.IsNotNullOrWhiteSpace())
                    {
                        addpages.Append(String.Format("Pages.AddElement({0},\"{1}\");",
                            page.DigitalJoin.Visibility_Digital_Join, page.Name));
                        addpages.AppendLineAndIndent(indentation);
                    }

                }

                //  Build File Section
                // for each page we need a folder and three files /Output/Template/Callbacks/Classname/
                foreach (var variable in stringBuilders)
                {

                    // Queries if we have any of the type of controls 
                    var query = from control in page.Control
                        where control.Type == typeXMLtoSharp[variable.Key]
                              &&
                              (control.DigitalJoin.IsNotNullOrWhiteSpace() || control.SerialJoin.IsNotNullOrWhiteSpace() ||
                               control.AnalogJoin.IsNotNullOrWhiteSpace())
                        select control;
                   
                    // If we have any of this type then
                    if (query.Any())
                    {

                       
                        // Add a file build to the dictionary
                        switch (variable.Key)
                        {
                            case "addbuttons":
                            {
                                
                                //filesDictionary.Add(string.Format(output_buttons, checkText(page.Name),template_button_methods);
                                filesOutputDictionary.Add(checkText(page.Name) + sep + typeXMLtoSharp[variable.Key],
                                    string.Format(output_buttons, checkText(page.Name)));
                                filesTemplateDictionary.Add(checkText(page.Name) + sep + typeXMLtoSharp[variable.Key], template_button_methods);
                                MethodNameStringBuilderDictionary.Add(checkText(page.Name) + sep + typeXMLtoSharp[variable.Key], new StringBuilder(checkText(page.Name)));
                                MethodsStringBuilderDictionary.Add(checkText(page.Name) + sep + typeXMLtoSharp[variable.Key], new StringBuilder());
                                
                                break;
                            }
                            case "addtextentry":
                            {
                                filesOutputDictionary.Add(checkText(page.Name) + sep + typeXMLtoSharp[variable.Key],
                                    string.Format(output_texts, checkText(page.Name)));
                                filesTemplateDictionary.Add(checkText(page.Name) + sep + typeXMLtoSharp[variable.Key], template_text_methods);
                                MethodNameStringBuilderDictionary.Add(checkText(page.Name) + sep + typeXMLtoSharp[variable.Key],
                                    new StringBuilder(checkText(page.Name)));
                                MethodsStringBuilderDictionary.Add(checkText(page.Name) + sep + typeXMLtoSharp[variable.Key], new StringBuilder());

                                break;
                            }
                            case "addsliders":
                            {
                                filesOutputDictionary.Add(checkText(page.Name) + sep + typeXMLtoSharp[variable.Key],string.Format(output_sliders, checkText(page.Name)));
                                filesTemplateDictionary.Add(checkText(page.Name) + sep + typeXMLtoSharp[variable.Key], template_slider_methods);
                                MethodNameStringBuilderDictionary.Add(checkText(page.Name) + sep + typeXMLtoSharp[variable.Key], new StringBuilder(checkText(page.Name)));
                                MethodsStringBuilderDictionary.Add(checkText(page.Name) + sep + typeXMLtoSharp[variable.Key], new StringBuilder());

                                break;
                            }
                        }
                    }



                }

                if (page.Smart_Object_ID.Count > 0) // Create a Methods Callback File
                {
                     filesOutputDictionary.Add(checkText(page.Name) + sep + "Lists",string.Format(output_methods, checkText(page.Name)));
                     filesTemplateDictionary.Add(checkText(page.Name) + sep + "Lists", template_smartobject_methods);
                     MethodNameStringBuilderDictionary.Add(checkText(page.Name) + sep + "Lists", new StringBuilder(checkText(page.Name)));
                     MethodsStringBuilderDictionary.Add(checkText(page.Name) + sep + "Lists", new StringBuilder( ));

                }
            

            // Add a start Region to the relevant section if there are more than one controls in the section.


                foreach (var variable in stringBuilders)
                {
                   
                    var query = from control in page.Control
                                where control.Type == typeXMLtoSharp[variable.Key]
                                   && (control.DigitalJoin.IsNotNullOrWhiteSpace() || control.SerialJoin.IsNotNullOrWhiteSpace() || control.AnalogJoin.IsNotNullOrWhiteSpace())
                                select control;


                    
                    if(query.Any())
                        startPageSection(variable.Value,checkText(page.Name));
                }

                  
                /*
                private void AddPages()
        {
            Pages.AddElement(1, "Display Page");
            Pages.AddElement(2, "Source Page");
            Pages.AddElement(3, "VideoServer Page");
            Pages.AddElement(12, "BlurayPlayer Page");
        }
                */
                // Buttons and SmartObjects
                foreach (var controls in page.Control)
                {
                    if(controls.Type != null)
                    {
                        //  Work out what joins are available


                        List<String> joinsList = new List<String>();

                       
                        if (controls.DigitalJoin.IsNotNullOrWhiteSpace())
                        {
                            if (controls.DigitalJoin.Press_Digital_Join.IsNotNullOrWhiteSpace())
                                joinsList.Add("{ VTBJoin.Press," + controls.DigitalJoin.Press_Digital_Join + "}");

                            if (controls.DigitalJoin.Visibility_Digital_Join.IsNotNullOrWhiteSpace())
                            {
                                //joinsList.Add("{ VTBJoin.Visibility,"+controls.DigitalJoin.Visibility_Digital_Join+"}");
                                //There is a typo in VTPro.dll VTB Join ENUM should be as the line above!!
                                joinsList.Add("{ VTBJoin.Visbility," + controls.DigitalJoin.Visibility_Digital_Join + "}");
                            }
                            
                            if (controls.DigitalJoin.Enable_Digital_Join.IsNotNullOrWhiteSpace())
                                joinsList.Add("{ VTBJoin.Enable,"+controls.DigitalJoin.Enable_Digital_Join+"}");
                            
                        }
                        if (controls.SerialJoin.IsNotNullOrWhiteSpace())
                        {
                            if (controls.SerialJoin.Indirect_Text_Serial_Join.IsNotNullOrWhiteSpace())
                                joinsList.Add("{ VTBJoin.IndirectText,"+ controls.SerialJoin.Indirect_Text_Serial_Join+"}");
                            
                            if (controls.SerialJoin.Output_Text_Serial_Join.IsNotNullOrWhiteSpace())
                                joinsList.Add("{ VTBJoin.OutputText,"+ controls.SerialJoin.Output_Text_Serial_Join+"}");
                            
                            

                        }
                        if (controls.AnalogJoin.IsNotNullOrWhiteSpace())
                        {
                            if (controls.AnalogJoin.Touch_Feedback_Analog_Join.IsNotNullOrWhiteSpace())
                                joinsList.Add("{ VTBJoin.Analog,"+ controls.AnalogJoin.Touch_Feedback_Analog_Join+"}");
                           
                        }
                        
                        
                        
                        var joins = String.Join(",", joinsList.ToArray());
                        
                        switch (controls.Type)
                        {
                            



                            case "Buttons":
                            {
                                if (controls.DigitalJoin.IsNotNullOrWhiteSpace())
                                {
                                    var callback = checkText(page.Name) + "_Buttons." + checkText(controls.Name);

                                    // Dont add Interlocks at all!!
                                    
                                        addbuttons.Append(
                                           "ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {" +
                                           joins + "}, \"" + checkText(controls.Name) + "\", " + callback + "));");
                                        addbuttons.AppendLineAndIndent(indentation);
                                    
                                    //Add method to the Methods file
                                    MethodsStringBuilderDictionary[checkText(page.Name) + sep + controls.Type].Append(
                                        buttonmethod(checkText(controls.Name)));


                                }
                                break;
                            }
                            case "Text": // Formatted Text or a simple label need to test for the Name 
                            {
                                if(controls.SerialJoin.IsNotNullOrWhiteSpace())
                                {
                                    if (controls.SerialJoin.Output_Text_Serial_Join.IsNotNullOrWhiteSpace())
                                    {
                                        var callback = checkText(page.Name) + "_TextEntryBoxes." +
                                                       checkText(controls.Name);

                                        addtextentry.Append("TextEntryCollection.AddElement( new VTTextEntry("+callback+", this, new Dictionary<VTBJoin, uint>  { " + joins + " }, \"" + checkText(controls.Name) + "\"));");
                                        addtextentry.AppendLineAndIndent(indentation);

                                        //Add method to the Methods file
                                        MethodsStringBuilderDictionary[checkText(page.Name) + sep + controls.Type].Append(
                                            textentrymethod(checkText(controls.Name)));


                                    }
                                    else
                                    {
                                        addformattedtext.Append("FormattedTextCollection.AddElement( new VTFormattedText(this, new Dictionary<VTBJoin, uint>  { " + joins + " }, \"" + checkText(controls.Name) + "\"));");
                                        addformattedtext.AppendLineAndIndent(indentation);

                                    }
                                }
                                break;

                            }
                            case "Page": // Subpage Reference
                            {
                                // If the page has a visibility join then add as a subpage element 
                                if (controls.DigitalJoin.IsNotNullOrWhiteSpace())
                                {
                                    // Note the name is not very friendly!
                                    if (controls.DigitalJoin.Visibility_Digital_Join.IsNotNullOrWhiteSpace())
                                    {
                                        addsubpages.Append(String.Format("Subpages.AddElement({0},\"{1}\");",
                                            controls.DigitalJoin.Visibility_Digital_Join,
                                            checkText(page.Name) + sep + checkText(controls.Name)));
                                        addsubpages.AppendLineAndIndent(indentation);
                                    }
                                }
                               
                                break;
                            }
                            case "Gauges": // Sliders
                            {
                                if (controls.AnalogJoin.IsNotNullOrWhiteSpace())
                                {
                                    var callback = checkText(page.Name) + "_Sliders." + checkText(controls.Name);
                                                                     
                                    addsliders.Append("SliderCollection.AddElement(new VTSlider(this, new Dictionary<VTBJoin, uint> { "+joins+" }, \""+checkText(controls.Name)+"\", "+callback+"));");
                                    addsliders.AppendLineAndIndent(indentation);

                                    //Add method to the Methods file
                                    MethodsStringBuilderDictionary[checkText(page.Name) + sep + controls.Type].Append(
                                        sliderobjectmethod(checkText(controls.Name)));


                                }
                                break;
                            }
                            case "Images": // Images
                            {
                                break;
                            }
                            case "Keypad": // Note Dpads and Keypads are Type Keypad 
                            case "Lists":  // Lists of all sorts are ty[e Lists
                            // Smart Objects
                            {

                                var smartobjectcallback = "VTS" + checkText(page.Name) + "_Methods." + checkText(controls.Name);

                                var smartobjectcallbackroot = "VTS" + checkText(page.Name) + "_Methods";

                                var smartobjectconstanttext = checkText(page.Name) + sep + checkText(controls.Name);


                                // no firm way of tying the smat object to the ID!
                                // Except the order that they appear in the XML
                                // The SmartObject ID appears just after the Control but not in the same element
                                //if (page.Smart_Object_ID.Count == 0)
                                
                                // Error handling Skip Control if there is no Smartobject ID Set!

                                    if (panel.Page[countpages].Smart_Object_ID.ElementAtOrDefault(countsmartobjects) !=
                                        null)
                                    {
                                        var ID = panel.Page[countpages].Smart_Object_ID[countsmartobjects];

                                        // Add the smartobject Constant
                                        addsmartobjectconstants.AppendLine();
                                        addsmartobjectconstants.AppendLineWithIndent(
                                            "public const uint " + smartobjectconstanttext + "= " + ID + ";", 4);
                                        // Add the smartobject Element callback
                                        addsmartobjectcallbacks.Append("public " + smartobjectcallbackroot + " " +
                                                                       smartobjectcallback + " { get; private set; }");

                                    }
                                    else
                                    {
                                        buildFile = addtobuildfile(string.Format("Error No smartObject ID found Page:{0} Control:{1} ",page.Name,controls.Name), buildFile);
                                        break;
                                    }
                                
                                


                                //if(panel.Page.)
                                //increment smartobject counter
                                countsmartobjects++;
                                //VTSVideoServerMethods.SubpageSelection checkText(controls.Name);

                                switch (controls.Type)
                                {
                                    case "Keypad":
                                    {
                                        if (controls.Name.Contains("DPad"))
                                        {
                                            addsmartobjects.AppendLine();

                                            addsmartobjects.AppendLineWithIndent(
                                                "SmartObjectCollection.AddElement(new VTDPad(this, null, ExtendedInterface.SmartObjects[VTSObjects." +
                                                smartobjectconstanttext + "],\"" + checkText(controls.Name) + "\", " +
                                                smartobjectcallback + "));");

                                            //Add method to the Methods file
                                            MethodsStringBuilderDictionary[checkText(page.Name) + sep + "Lists"].Append(
                                                keypadobjectmethod(checkText(controls.Name)));
                                            break;
                                        }
                                        if (controls.Name.Contains("Keypad"))
                                        {

                                            addsmartobjects.AppendLine();
                                            addsmartobjects.AppendLineWithIndent(
                                                "SmartObjectCollection.AddElement(new VTKeypad(this, null, ExtendedInterface.SmartObjects[VTSObjects." +
                                                smartobjectconstanttext + "],\"" + checkText(controls.Name) + "\", " +
                                                smartobjectcallback + "));");

                                            //Add method to the Methods file
                                            MethodsStringBuilderDictionary[checkText(page.Name) + sep + "Lists"].Append(
                                                keypadobjectmethod(checkText(controls.Name)));
                                            
                                        }
                                        break;
                                    }

                                    case "Lists": // No way to ascertain if it is a Button List or a Dynamic Button List  
                                    {
                                        
                                        if (controls.Name.Contains("Dynamic Button List"))    
                                        {
                                            // VTDynamicButton
                                            addsmartobjects.AppendLine();
                                            addsmartobjects.AppendLineWithIndent(
                                                "SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects." +
                                                smartobjectconstanttext + "],\"" + checkText(controls.Name) +
                                                "\", TempList, " + smartobjectcallback + ", true));");


                                            //Add method to the Methods file
                                            MethodsStringBuilderDictionary[checkText(page.Name) + sep + controls.Type]
                                                .Append(
                                                    smartobjectmethod(checkText(controls.Name)));
                                            break;
                                        } 
                                        if (controls.Name.Contains("Button List") && !controls.Name.Contains("Dynamic"))    
                                        {
                                                // VTButtonList  ( Not Dynamic!)
                                                addsmartobjects.AppendLine();
                                                addsmartobjects.AppendLineWithIndent("// Set Number of Items in ButtonList");
                                                addsmartobjects.AppendLine();
                                                addsmartobjects.AppendLineWithIndent(
                                                    "SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects." +
                                                    smartobjectconstanttext + "],\"" + checkText(controls.Name) +
                                                    "\", 10, " + smartobjectcallback + ", true));");


                                                //Add method to the Methods file
                                                MethodsStringBuilderDictionary[checkText(page.Name) + sep + controls.Type]
                                                    .Append(
                                                        smartobjectmethod(checkText(controls.Name)));
                                            break;
                                        }
                                        
                                        
                                        if (controls.Name.Contains("Dynamic Icon List"))
                                        {
                                            // VTDynamicButton
                                            addsmartobjects.AppendLine();
                                            // Icon Lists need a List of type Icon 
                                            addsmartobjects.AppendLineWithIndent(
                                                "List<DynamicIconButton> IconList_" + checkText(controls.Name) +
                                                " = new List<DynamicIconButton>();");
                                            addsmartobjects.AppendLine();
                                            addsmartobjects.AppendLineWithIndent(
                                                "SmartObjectCollection.AddElement(new VTDynamicIconList(this, null, ExtendedInterface.SmartObjects[VTSObjects." +
                                                smartobjectconstanttext + "], IconList_"+checkText(controls.Name)+", " + smartobjectcallback + ",\"" + checkText(controls.Name) + "\", true));");


                                            //Add method to the Methods file
                                            MethodsStringBuilderDictionary[checkText(page.Name) + sep + controls.Type]
                                                .Append(
                                                    smartobjectmethod(checkText(controls.Name)));
                                        }


                                            break;
                                    }
                                        
                                }
                                    
                               
                                
                                
                                break;
                            }



                        } // switch
                    }
                    
                }
               

                
                // Add a end Region to the section 

               
                foreach (var variable in stringBuilders)
                {

                    // Queries if we have any of the type of controls 
                    var query = from control in page.Control
                                where control.Type == typeXMLtoSharp[variable.Key]
                                   && ( control.DigitalJoin.IsNotNullOrWhiteSpace() ||  control.SerialJoin.IsNotNullOrWhiteSpace() || control.AnalogJoin.IsNotNullOrWhiteSpace())
                                select control;
                    // If we have any of this type then
                    if (query.Any())
                    {
                        // Add an #endregion complier directive
                        completePageSection(variable.Value);
                       
                    }



                }
               
                
                // increment page counter
                countpages++;
            }
            // End Pages


            //Create the Class Declarations for the panels file and initialisations for the objects
            addclassinitializations.AppendLine();   // Add a newline to straighten things up!
            foreach (KeyValuePair<string, StringBuilder> entry in MethodNameStringBuilderDictionary)
            {

                if (entry.Key.Contains("_Lists"))
                {

                    addclassdeclarations.AppendLine();
                    addclassdeclarations.AppendLineWithIndent("//Smart Object",2);
                    addclassdeclarations.AppendLineWithIndent(String.Format("public VTS{0}Methods VTS{0}_Methods {{ get; private set; }}",entry.Value),2);
                    
                    addclassinitializations.AppendLineWithIndent(String.Format("VTS{0}_Methods = new VTS{0}Methods(ControlSystem, this);",entry.Value),2);

                }
                if ( entry.Key.Contains("_Buttons") || entry.Key.Contains("_Gauges")|| entry.Key.Contains("_Text"))
                {
                    addclassdeclarations.AppendLineWithIndent("//Element callbacks",2);
                }
                if (entry.Key.Contains("_Buttons"))
                {
                    addclassdeclarations.AppendLineWithIndent(
                           String.Format("public {0}VTButtons {0}_Buttons {{ get; private set; }}", entry.Value), 2);
                    addclassinitializations.AppendLineWithIndent(
                            String.Format("{0}_Buttons = new {0}VTButtons(this);", entry.Value), 2);
                
                }
                if (entry.Key.Contains("_Gauges"))
                {
                     addclassdeclarations.AppendLineWithIndent(String.Format("public {0}VTSliders {0}_Sliders {{ get; private set; }}",entry.Value),2);
                     addclassinitializations.AppendLineWithIndent(String.Format("{0}_Sliders = new {0}VTSliders(this);",entry.Value),2);
                }
                if (entry.Key.Contains("_Text"))
                {
                    addclassdeclarations.AppendLineWithIndent(String.Format( "public {0}VTTextEntryBoxes {0}_TextEntryBoxes {{ get; private set; }}",entry.Value),2);
                    addclassinitializations.AppendLineWithIndent(String.Format( "{0}_TextEntryBoxes = new {0}VTTextEntryBoxes(this);",entry.Value),2);
                }
               
            }


            // Add the remaining elements  to the Dict which is used in the Main files string replace
            
            stringBuilders.Add("addnamespace", addnamespace);
            
            stringBuilders.Add("addsmartobjects",addsmartobjects);
            stringBuilders.Add("addsmartobjectconstants",addsmartobjectconstants);
            stringBuilders.Add("addsmartobjectcallbacks",addsmartobjectcallbacks);
            stringBuilders.Add("addclassdeclarations",addclassdeclarations);
            stringBuilders.Add("addclassinitializations",addclassinitializations);
           
            stringBuilders.Add("addpages", addpages);


            // build the main two files

            ProcessTemplate(template_pages, stringBuilders, output_pages);
            ProcessTemplate(template_objects, stringBuilders, output_objects);

            //  Now Build the remaining files based on the Dictionaries
            

            foreach (KeyValuePair<string, string> entry in filesOutputDictionary)
            {
                stringBuilders.Clear();
                // Add the namespace every time
                stringBuilders.Add("addnamespace", addnamespace);
                
                // add the relevant strings and search terms
                stringBuilders.Add("addmethodname",MethodNameStringBuilderDictionary[entry.Key]);
                stringBuilders.Add("addmethods", MethodsStringBuilderDictionary[entry.Key]);
                
                // do something with entry.Value template or entry.Key filename
                ProcessTemplate(filesTemplateDictionary[entry.Key],stringBuilders,entry.Value);


                
            }

            // Save the Buildfile
            savefile(buildFile.ToString(),output_errors);
            
        }
      
        //  Templating and File Saving

        private static void ProcessTemplate(string templatefilename,Dictionary<String,StringBuilder> args,string savefilename)
        {
           
            // Progress
            //drawTextProgressBarHeader("Processing Templates");
            FileStream fs = new FileStream(templatefilename, FileMode.Open);

            StreamReader myReader = new StreamReader(fs);
            while (!myReader.EndOfStream)
            {
                Thread.Sleep(100);
                drawTextProgressBar((int)myReader.BaseStream.Position, (int)myReader.BaseStream.Length, templatefilename);

                string contents = myReader.ReadToEnd();

                
                // Build file from Templates 
                var i = 0;
                foreach (var arg in args)
                {
                    //contents = stringtemplate("$$" + MemberInfoGetting.GetMemberName(() => arg) + "$$", arg.ToString(), contents);
                    contents = stringtemplate("$$"+arg.Key+"$$", arg.Value.ToString(), contents);
                    
                    i++;
                }

                // Save Files

                savefile(contents,savefilename);
               
                

            }

           
            fs.Close();

            
        }

        // replaces each instance of serch in the template with the replce string!
        private static string stringtemplate(string search, string replace, string template)
        {
            string result = template;

            while (result.Contains(search))
            {
                int Place = result.IndexOf(search);
            
                if (Place > 1)
                {
                result = result.Remove(Place, search.Length).Insert(Place, replace);
                
                }


            }
            return result;

        }

#endregion

        #region string_methods

        private static string checkText(string str)
        {
            string name = str;
            return Regex.Replace(name, @"^[^A-Za-z_]+|\W+", "_");
            //return Regex.Replace(temp, @"/-/","_");

        }

        private static StringBuilder addtobuildfile(string note, StringBuilder buildFile)
        {

            buildFile.AppendLine("Build Note:" + note);
             

            return buildFile;
        }

        public static string keypadobjectmethod(string name)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLineAndIndent();
            sb.Append(@"public void ");
            sb.Append(name);
            sb.Append(@"(VTSmartObject SmartObject, KeypadButton Button)
        {
            

            switch (Button)
            {
                case KeypadButton.Misc_1:
                    throw new NotImplementedException();
                    break;

                case KeypadButton.Num_0:
                    //ControlSystem._videoServer.KeypadNumber(0);
                    break;

                case KeypadButton.Num_1:
                    //ControlSystem._videoServer.KeypadNumber(1);
                    break;

                case KeypadButton.Num_2:
                    //ControlSystem._videoServer.KeypadNumber(2);
                    break;

                case KeypadButton.Num_3:
                    //ControlSystem._videoServer.KeypadNumber(3);
                    break;

                case KeypadButton.Num_4:
                    //ControlSystem._videoServer.KeypadNumber(4);
                    break;

                case KeypadButton.Num_5:
                    //ControlSystem._videoServer.KeypadNumber(5);
                    break;

                case KeypadButton.Num_6:
                    //ControlSystem._videoServer.KeypadNumber(6);
                    break;

                case KeypadButton.Num_7:
                    //ControlSystem._videoServer.KeypadNumber(7);
                    break;

                case KeypadButton.Num_8:
                    //ControlSystem._videoServer.KeypadNumber(8);
                    break;

                case KeypadButton.Num_9:
                    //ControlSystem._videoServer.KeypadNumber(9);
                    break;

                case KeypadButton.Misc_2:
                    break;

                case KeypadButton.Null:
                    break;
            }
        }");


            return sb.ToString();

        }
        public static string dpadobjectmethod(string name)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLineAndIndent();
            sb.Append(@"public void ");
            sb.Append(name);
            sb.Append(@"DPad(VTSmartObject SmartObject, DPadButton Button, bool IsPressed)
        {
            if (ControlSystem._videoServer == null)
            {
                return;
            }

            switch (Button)
            {
                case DPadButton.Center:
                    if (IsPressed)
                    {
                        ControlSystem._videoServer.Enter();
                    }
                    break;

                case DPadButton.Down:
                    if (IsPressed)
                    {
                        ControlSystem._videoServer.ArrowKey(ArrowDirections.Down, CommandAction.Hold);
                    }
                    else
                    {
                        ControlSystem._videoServer.ArrowKey(ArrowDirections.Down, CommandAction.Release);
                    }                    
                    break;

                case DPadButton.Left:
                    if (IsPressed)
                    {
                        ControlSystem._videoServer.ArrowKey(ArrowDirections.Left, CommandAction.Hold);
                    }
                    else
                    {
                        ControlSystem._videoServer.ArrowKey(ArrowDirections.Left, CommandAction.Release);
                    } 
                    break;

                case DPadButton.Right:
                    if (IsPressed)
                    {
                        ControlSystem._videoServer.ArrowKey(ArrowDirections.Right, CommandAction.Hold);
                    }
                    else
                    {
                        ControlSystem._videoServer.ArrowKey(ArrowDirections.Right, CommandAction.Release);
                    } 
                    break;

                case DPadButton.Up:
                    if (IsPressed)
                    {
                        ControlSystem._videoServer.ArrowKey(ArrowDirections.Up, CommandAction.Hold);
                    }
                    else
                    {
                        ControlSystem._videoServer.ArrowKey(ArrowDirections.Up, CommandAction.Release);
                    } 
                    break;
            }
        }");


            return sb.ToString();

        }
        public static string smartobjectmethod(string name)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AddIndent(2);
            sb.Append("public void ");
            sb.Append(name);
            sb.AppendLine("(VTSmartObject SmartObject, SmartObjectEventArgs Args)");

            sb.AppendLineWithIndent("{", 2);
            sb.AppendLineWithIndent("throw new NotImplementedException();", 3);
            sb.AppendLine();
            sb.AppendLineWithIndent("//if (Args.Sig.Name.Contains(\"Pressed\"))", 3);
            sb.AppendLineWithIndent("//{", 3);
            sb.AppendLineWithIndent("//    int joinValue = 1;", 3);
            sb.AppendLineWithIndent("//", 3);
            sb.AppendLineWithIndent("// joinValue = Int32.Parse(Regex.Match(Args.Sig.Name, @\"\\d+\").Value);", 3);
            sb.AppendLineWithIndent("//}", 3);
            sb.AppendLine();
            sb.AppendLineWithIndent("}", 2);

            return sb.ToString();

        }
        public static string sliderobjectmethod(string name)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AddIndent(2);
            sb.Append("public void ");
            sb.Append(name);
            sb.AppendLine("(VTSlider Slider)");

            sb.AppendLineWithIndent("{", 2);
            sb.AppendLineWithIndent("throw new NotImplementedException();", 3);
            sb.AppendLine();
            sb.AppendLineWithIndent("//", 3);
            sb.AppendLineWithIndent("//{", 3);
            sb.AppendLineWithIndent("//   ", 3);
            sb.AppendLineWithIndent("//", 3);
            sb.AppendLineWithIndent("//", 3);
            sb.AppendLineWithIndent("//}", 3);
            sb.AppendLine();
            sb.AppendLineWithIndent("}", 2);

            return sb.ToString();

        }

        public static string buttonmethod(string name)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine();
            sb.AddIndent(2);
            sb.Append("public void ");
            sb.Append(name);
            sb.AppendLine("(VTButton Button, bool IsPressed)");

            sb.AppendLineWithIndent("{", 2);
            sb.AppendLineWithIndent("if (IsPressed)", 3);
            sb.AppendLineWithIndent("{", 3);
            sb.AppendLineWithIndent("//press function here", 4);
            sb.AppendLineWithIndent("throw new NotImplementedException();", 4);
            sb.AppendLine();
            sb.AppendLineWithIndent("}", 3);
            sb.AppendLineWithIndent("}", 2);

            return sb.ToString();


        }
        public static string textentrymethod(string name)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine();
            sb.AddIndent(2);
            sb.Append("public void ");
            sb.Append(name);
            sb.AppendLine("(VTTextEntry VTTextEntry, string Text)");

            sb.AppendLineWithIndent("{", 2);
            sb.AppendLineWithIndent("if (Text != String.Empty)", 3);
            sb.AppendLineWithIndent("{", 3);
            sb.AppendLineWithIndent("// e.g. ControlSystem.GetSystem.TCPIPDriverMapping.SelectedEndpoint = Text;", 4);
            sb.AppendLineWithIndent("throw new NotImplementedException();", 4);
            sb.AppendLine();
            sb.AppendLineWithIndent("}", 3);
            sb.AppendLineWithIndent("}", 2);

            return sb.ToString();


        }
        #endregion

        #region Add Regions
        private static void startPageSection(StringBuilder stringB, string pagename)
        {

            stringB.Append("#region " + pagename);
            stringB.AppendLineAndIndent(indentation);

        }
        private static void completePageSection(StringBuilder stringB)
        {
            stringB.Append("#endregion ");
            stringB.AppendLineAndIndent(indentation);

        }
        #endregion

        #region file Operations

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                    
                }
            }
        }

        private static void savefile(string content,string filename)
        {
            
            // Write the directory 
            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(Directory.GetCurrentDirectory() + filename));
            // Write the file
            System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "/" + filename, content);
            

        }
        static void SafeDeleteTestDirectory(string dir)
        {
            try
            {
                
                Directory.Delete(dir, true);
            }
            catch (Exception ex)
            {
                DisplayError(ex);
                Thread.Sleep(1000);
                Console.WriteLine("Failed Delete! Trying again...");
                try
                {
                    Directory.Delete(dir, true);
                }
                catch (Exception ex2)
                {
                    DisplayError(ex2);
                    Thread.Sleep(1000);
                    Console.WriteLine("Failed Delete on Second attempt!");
                    WaitForKeyWithMessage("Please Close any Windows explorers windows and then press any key to retry");
                    try
                    {
                        Directory.Delete(dir, true);
                    }
                    catch (Exception ex3)
                    {
                        DisplayError(ex3);
                        WaitForKey();
                        return;
                    }
                }
            }
        }
        public static void DeleteDirectoriesRecursive(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {

                System.IO.File.SetAttributes(file, FileAttributes.Normal);
                System.IO.File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectoriesRecursive(dir);
            }

            Directory.Delete(target_dir, false);
        }
        // Used for testing
        public static void CopyOutputToSimplSharp()
        {
            if (Directory.Exists("Output"))
                Console.WriteLine("Deleting exsisting Files");
                DeleteDirectoriesRecursive(@"C:\Users\Neil Silver\Dropbox\Crestron\Crestron_Panel_Based_Folder\myBasePanel1\Panel");
                Console.WriteLine("Copying Output files");
                DirectoryCopy("Output",
                    @"C:\Users\Neil Silver\Dropbox\Crestron\Crestron_Panel_Based_Folder\myBasePanel1\Panel", true);
            
        }

        #endregion

        #region console helpers
        public static void WaitForKeyWithMessage(string str)
        {
            Console.WriteLine(str);
            WaitForKey();
        }
        public static void WaitForKey()
        {
            
            Console.WriteLine("Press any Key to proceed ....");
            Console.ReadKey();
        }
        public static void addLine(int j)
        {
            for (int i = 0; i < j; i++)
            {
                Console.WriteLine("....");
            }
            
        }
        public static void DisplayError(Exception e)
        {
            Console.WriteLine("Error:{0}",e);

        }
        public static void ConsoleApplicationHeader(string ProgramName)
        {
            Console.Title = ProgramName;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            builder.Append(@"
 _     _       _     _   _               _____             _             _ 
| |   (_)     | |   | | (_)             /  __ \           | |           | |
| |    _  __ _| |__ | |_ _ _ __   __ _  | /  \/ ___  _ __ | |_ _ __ ___ | |
| |   | |/ _` | '_ \| __| | '_ \ / _` | | |    / _ \| '_ \| __| '__/ _ \| |
| |___| | (_| | | | | |_| | | | | (_| | | \__/\ (_) | | | | |_| | | (_) | |
\_____/_|\__, |_| |_|\__|_|_| |_|\__, |  \____/\___/|_| |_|\__|_|  \___/|_|
          __/ |                   __/ |                                    
         |___/                   |___/                                     
    
#
#  | . _ |_ _|_. _  _   /~` _  _ _|_ _ _ | 
#  |_|(_|| | | || |(_|  \_,(_)| | | | (_)| 
#      _|           _|                     
#                                          
#        Copyright© 2017                   
#                                          
#       www.lightingcontrol.co.uk          
#
#
#
#
#                                                                      
     ");
            Console.Write(builder.ToString());





        }
        public static void ConsoleReadMe(string readmefilename)
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + readmefilename))
                {
                    string line;

                    // Read and display lines from the file until 
                    // the end of the file is reached. 
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {

                // Let the user know what went wrong.
                Console.WriteLine("The readme file could not be read:");
                Console.WriteLine(e.Message);
            }
            
        }
        private static void drawTextProgressBar(int progress, int total, string str)
        {
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 40;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(str); //blanks at the end remove any excess
        }

        private static void drawTextProgressBarHeader(string str)
        {
            Console.Clear();
            addLine(8);
            Console.WriteLine("Processing {0}...",str);
        }

        #endregion


    }
    #region expression class
    // Expression class used to get variable names.
    public static class MemberInfoGetting
    {
        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
    }
    #endregion
}