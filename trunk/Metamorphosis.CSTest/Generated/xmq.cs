 
using System;
using System.Collections.Generic;
using Helpers;



namespace xmq
{

public enum TE : int
{
    AA ,
    BB = 3 ,
    CC ,
    
}


public class SM 
{
    public Dictionary<string, string> mss ;
    
     
    public SM()
    {
        
    }
    
     
    public void read(byte[] bytes, ByteReader byteReader = null)
    {
        
        ByteReader reader = byteReader == null ? new ByteReader(bytes) : byteReader;
        
         
        // read array mss
        int s_mss;
        reader.Read(out s_mss);
        mss = new Dictionary<string, string>();
        
        for (int i_1 = 0; i_1 < s_mss; i_1++)
        {
            string e_key_2;        
            string e_value_3;       
             
            e_key_2 = reader.ReadString();
            e_value_3 = reader.ReadString();
        
            mss[e_key_2] = e_value_3;
        }
        
        
        
        
    }
    
     
    public byte[] write(ByteWriter byteWriter = null)
    {
        
        ByteWriter writer = byteWriter == null ? new ByteWriter() : byteWriter;
        
         
        // write array mss
        writer.Write(mss.Count);
        
        foreach (var p in mss)
        {
            string e_key_2 = p.Key;
            string e_value_3 = p.Value;
                
            writer.WriteString(e_key_2);
            writer.WriteString(e_value_3);
        }
        
         
        
        return writer.GetBuffer();
        
    }
    
     
    public int size()
    {
        
        
        int s_mss = sizeof(int);
        {
            foreach (var p in mss)
            {
                string e_key_2 = p.Key;
                string e_value_3 = p.Value;
                
                
                
                
                s_mss += 0  + (2 + e_key_2.Length)
                                + (2 + e_value_3.Length);
            }
        }
        
        
        
        return 0  + s_mss
        ;
        
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
        
        for (int i_4 = 0; i_4 < s_ai; i_4++)
        {
            int e_name_5;        
            reader.Read(out e_name_5);
            ai[i_4] = e_name_5;
        }
        
        s = reader.ReadString();
         
        // read array ass
        int s_ass;
        reader.Read(out s_ass);
        ass = new List<List<string>>(s_ass);
        
        for (int i_6 = 0; i_6 < s_ass; i_6++)
        {
            List<string> e_name_7;        
             
            // read array e_name_7
            int s_e_name_7;
            reader.Read(out s_e_name_7);
            e_name_7 = new List<string>(s_e_name_7);
            
            for (int i_8 = 0; i_8 < s_e_name_7; i_8++)
            {
                string e_name_9;        
                e_name_9 = reader.ReadString();
                e_name_7[i_8] = e_name_9;
            }
            
            ass[i_6] = e_name_7;
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
        
        for (int i_4 = 0; i_4 < ai.Count; i_4++)
        {
            writer.Write(ai[i_4]);
        }
        
        writer.WriteString(s);
         
        // write array ass
        writer.Write(ass.Count);
        
        for (int i_6 = 0; i_6 < ass.Count; i_6++)
        {
             
            // write array ass[i_6]
            writer.Write(ass[i_6].Count);
            
            for (int i_10 = 0; i_10 < ass[i_6].Count; i_10++)
            {
                writer.WriteString(ass[i_6][i_10]);
            }
            
        }
        
         
        
        return writer.GetBuffer();
        
    }
    
     
    public int size()
    {
        
         
        
        int s_ass = sizeof(int);
        {
            for (int i_6 = 0; i_6 < ass.Count; i_6++)
            {
                List<string> e_name_7 = ass[i_6];
                
                
                int s_e_name_7 = sizeof(int);
                {
                    for (int i_11 = 0; i_11 < e_name_7.Count; i_11++)
                    {
                        string e_name_12 = e_name_7[i_11];
                        
                        
                        
                        s_e_name_7 += 0  + (2 + e_name_12.Length);
                    }
                }
                
                
                s_ass += 0  + s_e_name_7;
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
