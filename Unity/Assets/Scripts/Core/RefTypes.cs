using System;
using System.IO;
using ET;
using MongoDB.Bson.Serialization;
using System.Runtime.CompilerServices;

class RefTypes
{
    public void MyAOTRefs()
    {
        // var bytes = new byte[1];
        // using (MemoryStream memoryStream = new MemoryStream(bytes))
        // {
        //     var tmp1 = (Entity)BsonSerializer.Deserialize(memoryStream, typeof (Entity));
        //     object o = null;
        //     Unsafe.As<object>(bytes);
        //     System.SByte tmp2 = SByte.MinValue;
        //     Unsafe.As<System.SByte, object>(ref tmp2);
        // }
    }
}