//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;


namespace ET.AbilityConfig
{

/// <summary>
/// 目标对象类型
/// </summary>
public abstract partial class SelectObjectUnitTypeBase: Bright.Config.BeanBase
{
    public SelectObjectUnitTypeBase(ByteBuf _buf) 
    {
        PostInit();
    }

    public static SelectObjectUnitTypeBase DeserializeSelectObjectUnitTypeBase(ByteBuf _buf)
    {
        switch (_buf.ReadInt())
        {
            case SelectObjectUnitTypeNone.__ID__: return new SelectObjectUnitTypeNone(_buf);
            case SelectObjectUnitTypeSelf.__ID__: return new SelectObjectUnitTypeSelf(_buf);
            case SelectObjectUnitTypeSelfAndFriend.__ID__: return new SelectObjectUnitTypeSelfAndFriend(_buf);
            case SelectObjectUnitTypeFriend.__ID__: return new SelectObjectUnitTypeFriend(_buf);
            case SelectObjectUnitTypeHostile.__ID__: return new SelectObjectUnitTypeHostile(_buf);
            case SelectObjectUnitTypeSelfHome.__ID__: return new SelectObjectUnitTypeSelfHome(_buf);
            case SelectObjectUnitTypeSelfAndFriendHome.__ID__: return new SelectObjectUnitTypeSelfAndFriendHome(_buf);
            case SelectObjectUnitTypeFriendHome.__ID__: return new SelectObjectUnitTypeFriendHome(_buf);
            case SelectObjectUnitTypeHostileHome.__ID__: return new SelectObjectUnitTypeHostileHome(_buf);
            default: throw new SerializationException();
        }
    }



    public virtual void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        PostResolve();
    }

    public virtual void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}