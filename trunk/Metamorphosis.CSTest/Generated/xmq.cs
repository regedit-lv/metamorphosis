 
using System;
using System.Collections.Generic;
using Helpers;


namespace xmq
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
        
        for (int i_2 = 0; i_2 < ass.Count; i_2++)
        {
             
            // write array ass[i_2]
            writer.Write(ass[i_2].Count);
            
            for (int i_3 = 0; i_3 < ass[i_2].Count; i_3++)
            {
                writer.WriteString(ass[i_2][i_3]);
            }
            
        }
        
         
        
        return writer.GetBuffer();
        
    }
    
     
    public int size()
    {
        
         
        
        int s_ass = sizeof(int);
        {
            for (int i_2 = 0; i_2 < ass.Count; i_2++)
            {
                List<string> e_name_4 = ass[i_2];
                
                
                int s_e_name_4 = sizeof(int);
                {
                    for (int i_5 = 0; i_5 < e_name_4.Count; i_5++)
                    {
                        string e_name_6 = e_name_4[i_5];
                        
                        
                        
                        s_e_name_4 += 0  + (2 + e_name_6.Length);
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
