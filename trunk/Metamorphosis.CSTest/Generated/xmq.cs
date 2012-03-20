 
using System;
using System.Collections.Generic;
using Helpers;


namespace xmq.administration
{

enum TE : int
{
    AA ,
    BB = 3 ,
    CC ,
    
}


public class SubStruct 
{
    public int subI ;
    public string subString ;
    
     
    public SubStruct()
    {
        
    }
    
     
    public void read(byte[] bytes, ByteReader byteReader = null)
    {
        
        ByteReader reader = byteReader == null ? new ByteReader(bytes) : byteReader;
        
        reader.Read(out subI);
        subString = reader.ReadString();
        
        
    }
    
     
    public byte[] write(ByteWriter byteWriter = null)
    {
        
        ByteWriter writer = byteWriter == null ? new ByteWriter() : byteWriter;
        
        writer.Write(subI);
        writer.WriteString(subString);
         
        
        return writer.GetBuffer();
        
    }
    
     
    public int size()
    {
        
        
        
        return 0  + sizeof(int)
         + (2 + subString.Length)
        ;
        
    }
     
}


public class BaseStruct 
{
    public string bs ;
    public int bi ;
    
     
    public BaseStruct()
    {
        
    }
     
}


public class TestStruct : BaseStruct
{
    public SubStruct sub ;
    public uint ui ;
    public List<int> ai ;
    public string s ;
    public List<List<string>> ass ;
    
     
    public TestStruct() :base()
    {
        bs = "bs_value" ;
        bi = 3 ;
        
    }
    
     
    public void read(byte[] bytes, ByteReader byteReader = null)
    {
        
        ByteReader reader = byteReader == null ? new ByteReader(bytes) : byteReader;
        
        bs = reader.ReadString();
        reader.Read(out bi);
         
        sub = new SubStruct();
        sub.read(null, reader);
        
        reader.Read(out ui);
         
        // read array ai
        int s_ai;
        reader.Read(out s_ai);
        ai = new List<int>(s_ai);
        
        for (int i_1 = 0; i_1 < s_ai; i_1++)
        {
            int e_name_2;        
            reader.Read(out e_name_2);
            ai[i_1] = e_name_2;
        }
        
        s = reader.ReadString();
         
        // read array ass
        int s_ass;
        reader.Read(out s_ass);
        ass = new List<List<string>>(s_ass);
        
        for (int i_3 = 0; i_3 < s_ass; i_3++)
        {
            List<string> e_name_4;        
             
            // read array e_name_4
            int s_e_name_4;
            reader.Read(out s_e_name_4);
            e_name_4 = new List<string>(s_e_name_4);
            
            for (int i_5 = 0; i_5 < s_e_name_4; i_5++)
            {
                string e_name_6;        
                e_name_6 = reader.ReadString();
                e_name_4[i_5] = e_name_6;
            }
            
            ass[i_3] = e_name_4;
        }
        
        
        
    }
    
     
    public byte[] write(ByteWriter byteWriter = null)
    {
        
        ByteWriter writer = byteWriter == null ? new ByteWriter() : byteWriter;
        
        writer.WriteString(bs);
        writer.Write(bi);
         
        sub.write(writer);
        
        writer.Write(ui);
         
        // write array ai
        writer.Write(ai.Count);
        
        for (int i_1 = 0; i_1 < ai.Count; i_1++)
        {
            writer.Write(ai[i_1]);
        }
        
        writer.WriteString(s);
         
        // write array ass
        writer.Write(ass.Count);
        
        for (int i_3 = 0; i_3 < ass.Count; i_3++)
        {
             
            // write array ass[i_3]
            writer.Write(ass[i_3].Count);
            
            for (int i_7 = 0; i_7 < ass[i_3].Count; i_7++)
            {
                writer.WriteString(ass[i_3][i_7]);
            }
            
        }
        
         
        
        return writer.GetBuffer();
        
    }
    
     
    public int size()
    {
        
         
        
        int s_ass = sizeof(int);
        {
            for (int i_3 = 0; i_3 < ass.Count; i_3++)
            {
                List<string> e_name_4 = ass[i_3];
                
                
                int s_e_name_4 = sizeof(int);
                {
                    for (int i_8 = 0; i_8 < e_name_4.Count; i_8++)
                    {
                        string e_name_9 = e_name_4[i_8];
                        
                        
                        
                        s_e_name_4 += 0  + (2 + e_name_9.Length);
                    }
                }
                
                
                s_ass += 0  + s_e_name_4;
            }
        }
        
        
        
        return 0  + (2 + bs.Length)
         + sizeof(int)
         + sub.size()
         + sizeof(uint)
         + (sizeof(int) + sizeof(int) * ai.Count)
         + (2 + s.Length)
         + s_ass
        ;
        
    }
     
};


}
