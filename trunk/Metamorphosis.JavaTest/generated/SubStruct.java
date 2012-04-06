package generated;
 
import java.util.List;
 
import java.io.StringReader;
import java.util.ArrayList;
import java.util.Iterator;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.dom.DOMSource;

import org.w3c.dom.Attr;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.InputSource;

import metamorphosis.helpers.*;



public class SubStruct 
{
     public String ss; 
     public int si; 
    
     
    public SubStruct()
    {
         ss = "ssValue"; 
         si = 123; 
        
    }
    
     
    public String toXml(Element parentRoot, Document parentDoc)
    {
        
        
        Element parent = null;
        Document doc = null;
        try {
            if (parentRoot == null)
            {  
                DocumentBuilderFactory docFactory = DocumentBuilderFactory.newInstance();
                DocumentBuilder docBuilder = docFactory.newDocumentBuilder();
                doc = docBuilder.newDocument();;
          	    // root elements
          	    parent = doc.createElement("SubStruct");
          	    doc.appendChild(parent);
            }
            else
            {
                doc = parentDoc;
                parent = parentRoot;
            }
        
            // write primitive types
            
            
            // int si
            XmlHelper.writeAttribute(parent, doc, "si", si);
            
            
              
            
            // String ss
            XmlHelper.writeAttribute(parent, doc, "ss", ss);
            
             
            
        
            // write complex types
             
             
        
            if (parentRoot == null)
            {
          	    DOMSource source = new DOMSource(doc);
          	    String xml = XmlHelper.getStringFromDoc(doc);
                return xml;
            }
            else
            {
                return "";
            }
        } catch (ParserConfigurationException pce) {
            pce.printStackTrace();
        }
        
        return "";
        
    }
    
     
    public void fromXml(String xml, Element parentRoot)
    {
        
        try {
        
            Element parent = null;
        
            if (parentRoot == null)
            {
                DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
                DocumentBuilder builder = factory.newDocumentBuilder();
                InputSource is = new InputSource(new StringReader(xml));
                Document doc = builder.parse(is);
                parent = doc.getDocumentElement();    
            }
            else
            {
                parent = parentRoot;
            }
        
            
            
            // int si
            si = XmlHelper.readAttribute(parent, "si", si);
            
            
              
            
            // String ss
            ss = XmlHelper.readAttribute(parent, "ss", ss);
            
             
            
        
             
             
        
        } catch (Exception e) {
            e.printStackTrace();
        }  
        
        
    }
     
}

