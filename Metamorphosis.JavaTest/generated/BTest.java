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



public class BTest 
{
     public String bs; 
     public int bi; 
     public List<String> bas; 
    
     
    public BTest()
    {
         bs = "bsValue"; 
         bi = 3; 
        
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
          	    parent = doc.createElement("BTest");
          	    doc.appendChild(parent);
            }
            else
            {
                doc = parentDoc;
                parent = parentRoot;
            }
        
            // write primitive types
            
            
            // int bi
            XmlHelper.writeAttribute(parent, doc, "bi", bi);
            
            
              
            
            // String bs
            XmlHelper.writeAttribute(parent, doc, "bs", bs);
            
             
            
        
            // write complex types
             
            
            // bas
            if (bas != null)
            {
                Element arrayElement = doc.createElement("bas");
                Element parentOriginal = parent; // save parent
            
                for(Iterator<String> i_1 = bas.iterator(); i_1.hasNext(); ) {
                    String value = i_1.next();
            
                    Element subElement = doc.createElement("String");
                    parent = subElement;
            
                    
                    // String value
                    XmlHelper.writeAttribute(parent, doc, "value", value);
                    
                
                    arrayElement.appendChild(parent);
                }
            
                parent = parentOriginal; // restore parent
                parent.appendChild(arrayElement);
            }
            
             
        
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
        
            
            
            // int bi
            bi = XmlHelper.readAttribute(parent, "bi", bi);
            
            
              
            
            // String bs
            bs = XmlHelper.readAttribute(parent, "bs", bs);
            
             
            
        
             
             
            { // array bas
                Element originalParent = parent;
                NodeList pList_2 = parent.getElementsByTagName("bas");
                for (int i_1 = 0; i_1 < pList_2.getLength(); i_1++) {
                    Node pNode_3 = pList_2.item(i_1);
                    if (pNode_3.getNodeType() == pNode_3.ELEMENT_NODE) {
                        Element pElement_4 = (Element) pNode_3;
            
                        // create new instance if needed               
                         
                        bas = new ArrayList<String>();
                        
            
                        NodeList sList_5 = pElement_4.getElementsByTagName("String");
            
                        for (int i2_6 = 0; i2_6 < sList_5.getLength(); i2_6++) {
                            Node sNode_7 = sList_5.item(i2_6);
                            if (sNode_7.getNodeType() == sNode_7.ELEMENT_NODE) {
                                Element sElement_8 = (Element) sNode_7;
                                String value;
            
                                // create new instance if needed               
                                 
                                value = new String();
                                
                                
                                parent = sElement_8;
            
                                
                                // String value
                                value = XmlHelper.readAttribute(parent, "value", value);
                                
                                
                                bas.add(value);
                            }                               
                        }   
                    }
                }
                parent = originalParent;
            }
            
            
             
        
        } catch (Exception e) {
            e.printStackTrace();
        }  
        
        
    }
     
}

