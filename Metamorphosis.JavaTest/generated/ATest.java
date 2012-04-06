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



public class ATest extends BTest
{
     public generated.SubStruct ss; 
     public String s; 
     public int i; 
     public List<String> as; 
    
     
    public ATest()
    {
        super();
         s = "sValue"; 
         i = 2; 
        
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
          	    parent = doc.createElement("ATest");
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
            
            
            // int i
            XmlHelper.writeAttribute(parent, doc, "i", i);
            
            
              
            
            // String bs
            XmlHelper.writeAttribute(parent, doc, "bs", bs);
            
            
            // String s
            XmlHelper.writeAttribute(parent, doc, "s", s);
            
             
            
        
            // write complex types
            
            // ss
            if (ss != null)
            {
                Element subElement = doc.createElement("ss");
                ss.toXml(subElement, doc);
                parent.appendChild(subElement);
            }
            
             
            
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
            
            
            // as
            if (as != null)
            {
                Element arrayElement = doc.createElement("as");
                Element parentOriginal = parent; // save parent
            
                for(Iterator<String> i_9 = as.iterator(); i_9.hasNext(); ) {
                    String value = i_9.next();
            
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
            
            
            // int i
            i = XmlHelper.readAttribute(parent, "i", i);
            
            
              
            
            // String bs
            bs = XmlHelper.readAttribute(parent, "bs", bs);
            
            
            // String s
            s = XmlHelper.readAttribute(parent, "s", s);
            
             
            
        
             
            { // struct ss
                NodeList nList = parent.getElementsByTagName("ss");
                for (int temp = 0; temp < nList.getLength(); temp++) {
                    Node nNode = nList.item(temp);
                    if (nNode.getNodeType() == Node.ELEMENT_NODE) {
                        Element eElement = (Element) nNode;
                        ss = new generated.SubStruct();
                        ss.fromXml("", eElement);
                   }
                }
            }
            
             
             
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
            
            
             
            { // array as
                Element originalParent = parent;
                NodeList pList_10 = parent.getElementsByTagName("as");
                for (int i_9 = 0; i_9 < pList_10.getLength(); i_9++) {
                    Node pNode_11 = pList_10.item(i_9);
                    if (pNode_11.getNodeType() == pNode_11.ELEMENT_NODE) {
                        Element pElement_12 = (Element) pNode_11;
            
                        // create new instance if needed               
                         
                        as = new ArrayList<String>();
                        
            
                        NodeList sList_13 = pElement_12.getElementsByTagName("String");
            
                        for (int i2_14 = 0; i2_14 < sList_13.getLength(); i2_14++) {
                            Node sNode_15 = sList_13.item(i2_14);
                            if (sNode_15.getNodeType() == sNode_15.ELEMENT_NODE) {
                                Element sElement_16 = (Element) sNode_15;
                                String value;
            
                                // create new instance if needed               
                                 
                                value = new String();
                                
                                
                                parent = sElement_16;
            
                                
                                // String value
                                value = XmlHelper.readAttribute(parent, "value", value);
                                
                                
                                as.add(value);
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
     
};

