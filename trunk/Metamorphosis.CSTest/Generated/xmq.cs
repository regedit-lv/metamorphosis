 
using System;
using System.Collections.Generic;
using Helpers;
using System.Xml;
using System.IO;
using System.Xml.Linq;


namespace xmq.settings
{

public enum Error : int
{
     Ok , 
     Failed , 
    
}


public enum MessageType : int
{
     GetAll , 
     GetAllReply , 
     Set , 
     SetReply , 
    
}


public class Property 
{
     public string key; 
     public string value; 
    
     
    public Property()
    {
        
    }
    
     
    public void read(byte[] bytes, ByteReader byteReader = null)
    {
        
        ByteReader reader = byteReader == null ? new ByteReader(bytes) : byteReader;
        
        key = reader.ReadString();
        value = reader.ReadString();
        
        
    }
    
     
    public byte[] write(ByteWriter byteWriter = null)
    {
        
        ByteWriter writer = byteWriter == null ? new ByteWriter() : byteWriter;
        
        writer.WriteString(key);
        writer.WriteString(value);
         
        
        return writer.GetBuffer();
        
    }
    
     
    public int size()
    {
        
        
        
        return 0  + (2 + key.Length)
         + (2 + value.Length)
        ;
        
    }
     
}


public class BaseMessage 
{
     public MessageType messageType; 
    
     
    public BaseMessage()
    {
        
    }
    
    virtual  
    public void read(byte[] bytes, ByteReader byteReader = null)
    {
        
        ByteReader reader = byteReader == null ? new ByteReader(bytes) : byteReader;
        
        reader.Read(out messageType);
        
        
    }
    
    virtual  
    public byte[] write(ByteWriter byteWriter = null)
    {
        
        ByteWriter writer = byteWriter == null ? new ByteWriter() : byteWriter;
        
        writer.Write(messageType);
         
        
        return writer.GetBuffer();
        
    }
    
    virtual  
    public int size()
    {
        
        
        
        return 0  + sizeof(MessageType)
        ;
        
    }
     
}


public class GetAllMessage : BaseMessage
{
    
     
    public GetAllMessage() :base()
    {
         messageType = MessageType.GetAll; 
        
    }
    
    override  
    public void read(byte[] bytes, ByteReader byteReader = null)
    {
        
        ByteReader reader = byteReader == null ? new ByteReader(bytes) : byteReader;
        
        reader.Read(out messageType);
        
        
    }
    
    override  
    public byte[] write(ByteWriter byteWriter = null)
    {
        
        ByteWriter writer = byteWriter == null ? new ByteWriter() : byteWriter;
        
        writer.Write(messageType);
         
        
        return writer.GetBuffer();
        
    }
    
    override  
    public int size()
    {
        
        
        
        return 0  + sizeof(MessageType)
        ;
        
    }
     
};


public class GetAllReplyMessage : BaseMessage
{
     public List<xmq.settings.Property> settings; 
    
     
    public GetAllReplyMessage() :base()
    {
         messageType = MessageType.GetAllReply; 
        
    }
    
    override  
    public void read(byte[] bytes, ByteReader byteReader = null)
    {
        
        ByteReader reader = byteReader == null ? new ByteReader(bytes) : byteReader;
        
        reader.Read(out messageType);
         
        // read array settings
        int s_settings;
        reader.Read(out s_settings);
        settings = new List<xmq.settings.Property>(s_settings);
        
        for (int i_1 = 0; i_1 < s_settings; i_1++)
        {
            xmq.settings.Property e_name_2;        
             
            e_name_2 = new xmq.settings.Property();
            e_name_2.read(null, reader);
            
            settings.Add(e_name_2);
        }
        
        
        
    }
    
    override  
    public byte[] write(ByteWriter byteWriter = null)
    {
        
        ByteWriter writer = byteWriter == null ? new ByteWriter() : byteWriter;
        
        writer.Write(messageType);
         
        // write array settings
        writer.Write(settings.Count);
        
        for (int i_1 = 0; i_1 < settings.Count; i_1++)
        {
             
            settings[i_1].write(writer);
            
        }
        
         
        
        return writer.GetBuffer();
        
    }
    
    override  
    public int size()
    {
        
        
        int s_settings = sizeof(int);
        {
            for (int i_1 = 0; i_1 < settings.Count; i_1++)
            {
                xmq.settings.Property e_name_2 = settings[i_1];
                
                
                
                s_settings += 0  + e_name_2.size();
            }
        }
        
        
        
        return 0  + sizeof(MessageType)
         + s_settings
        ;
        
    }
     
};


public class SetMessage : BaseMessage
{
     public string key; 
     public string value; 
    
     
    public SetMessage() :base()
    {
         messageType = MessageType.Set; 
        
    }
    
    override  
    public void read(byte[] bytes, ByteReader byteReader = null)
    {
        
        ByteReader reader = byteReader == null ? new ByteReader(bytes) : byteReader;
        
        reader.Read(out messageType);
        key = reader.ReadString();
        value = reader.ReadString();
        
        
    }
    
    override  
    public byte[] write(ByteWriter byteWriter = null)
    {
        
        ByteWriter writer = byteWriter == null ? new ByteWriter() : byteWriter;
        
        writer.Write(messageType);
        writer.WriteString(key);
        writer.WriteString(value);
         
        
        return writer.GetBuffer();
        
    }
    
    override  
    public int size()
    {
        
        
        
        return 0  + sizeof(MessageType)
         + (2 + key.Length)
         + (2 + value.Length)
        ;
        
    }
     
};


public class SetReplyMessage : BaseMessage
{
     public Error error; 
    
     
    public SetReplyMessage() :base()
    {
         messageType = MessageType.SetReply; 
        
    }
    
    override  
    public void read(byte[] bytes, ByteReader byteReader = null)
    {
        
        ByteReader reader = byteReader == null ? new ByteReader(bytes) : byteReader;
        
        reader.Read(out messageType);
        reader.Read(out error);
        
        
    }
    
    override  
    public byte[] write(ByteWriter byteWriter = null)
    {
        
        ByteWriter writer = byteWriter == null ? new ByteWriter() : byteWriter;
        
        writer.Write(messageType);
        writer.Write(error);
         
        
        return writer.GetBuffer();
        
    }
    
    override  
    public int size()
    {
        
        
        
        return 0  + sizeof(MessageType)
         + sizeof(Error)
        ;
        
    }
     
};


}
