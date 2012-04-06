package metamorphosis.helpers;

import java.io.StringReader;
import java.io.StringWriter;

import javax.xml.transform.OutputKeys;
import javax.xml.transform.Source;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import javax.xml.transform.stream.StreamSource;

import org.w3c.dom.Attr;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.ls.DOMImplementationLS;
import org.w3c.dom.ls.LSSerializer;


public class XmlHelper {

	public static String prettyFormat(String input, int indent) {
	    try {
	        Source xmlInput = new StreamSource(new StringReader(input));
	        StringWriter stringWriter = new StringWriter();
	        StreamResult xmlOutput = new StreamResult(stringWriter);
	        TransformerFactory transformerFactory = TransformerFactory.newInstance();
	        transformerFactory.setAttribute("indent-number", indent);
	        Transformer transformer = transformerFactory.newTransformer(); 
	        transformer.setOutputProperty(OutputKeys.INDENT, "yes");
	        transformer.transform(xmlInput, xmlOutput);
	        return xmlOutput.getWriter().toString();
	    } catch (Exception e) {
	        throw new RuntimeException(e); // simple exception handling, please review it
	    }
	}

	public static String prettyFormat(String input) {
	    return prettyFormat(input, 4);
	}	
	
	public static String getStringFromDoc(org.w3c.dom.Document doc)    {
	    DOMImplementationLS domImplementation = (DOMImplementationLS) doc.getImplementation();
	    LSSerializer lsSerializer = domImplementation.createLSSerializer();
	    String xml = lsSerializer.writeToString(doc);
	    xml = prettyFormat(xml);
	    return xml;    
	}

	public static void writeAttribute(Element element, Document doc, String name, int value)
	{
		Attr attr = doc.createAttribute(name);		
  		attr.setValue(Integer.toString(value));
  		element.setAttributeNode(attr);	
	}
	
	public static void writeAttribute(Element element, Document doc, String name, String value)
	{
		Attr attr = doc.createAttribute(name);		
  		attr.setValue(value);
  		element.setAttributeNode(attr);	
	}
	
	public static void writeAttribute(Element element, Document doc, String name, boolean value)
	{
		Attr attr = doc.createAttribute(name);		
  		attr.setValue(value ? "true" : "false");
  		element.setAttributeNode(attr);	  		
	}
	
	public static int readAttribute(Element element, String name, int defaultValue)
	{
		if (element.hasAttribute(name))
		{
			String value = element.getAttribute(name);
			int i = Integer.parseInt(value);
			return i;
		}
		else
		{
			return defaultValue;
		}
	}
	
	public static boolean readAttribute(Element element, String name, boolean defaultValue)
	{
		if (element.hasAttribute(name))
		{
			String value = element.getAttribute(name);
			return value.equalsIgnoreCase("true");
		}
		else
		{
			return defaultValue;
		}
	}
	
	public static String readAttribute(Element element, String name, String defaultValue)
	{
		if (element.hasAttribute(name))
		{
			String value = element.getAttribute(name);
			return value;
		}
		else
		{
			return defaultValue;
		}
		
	}	
}
