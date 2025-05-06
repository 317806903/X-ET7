using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using Unity.Mathematics;

namespace ET
{
    public static class MongoHelper
    {
        private class StructBsonSerialize<TValue>: StructSerializerBase<TValue> where TValue : struct
        {
            public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TValue value)
            {
                Type nominalType = args.NominalType;

                IBsonWriter bsonWriter = context.Writer;

                bsonWriter.WriteStartDocument();

                FieldInfo[] fields = nominalType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (FieldInfo field in fields)
                {
                    bsonWriter.WriteName(field.Name);
                    BsonSerializer.Serialize(bsonWriter, field.FieldType, field.GetValue(value));
                }

                bsonWriter.WriteEndDocument();
            }

            public override TValue Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            {
                //boxing is required for SetValue to work
                object obj = new TValue();
                Type actualType = args.NominalType;
                IBsonReader bsonReader = context.Reader;

                bsonReader.ReadStartDocument();

                while (bsonReader.State != BsonReaderState.EndOfDocument)
                {
                    switch (bsonReader.State)
                    {
                        case BsonReaderState.Name:
                        {
                            string name = bsonReader.ReadName(Utf8NameDecoder.Instance);
                            FieldInfo field = actualType.GetField(name);
                            if (field != null)
                            {
                                object value = BsonSerializer.Deserialize(bsonReader, field.FieldType);
                                field.SetValue(obj, value);
                            }

                            break;
                        }
                        case BsonReaderState.Type:
                        {
                            bsonReader.ReadBsonType();
                            break;
                        }
                        case BsonReaderState.Value:
                        {
                            bsonReader.SkipValue();
                            break;
                        }
                    }
                }

                bsonReader.ReadEndDocument();

                return (TValue)obj;
            }
        }

        class DictionaryRepresentationConvention : ConventionBase, IMemberMapConvention
        {
            private readonly DictionaryRepresentation _dictionaryRepresentation;
            public DictionaryRepresentationConvention(DictionaryRepresentation dictionaryRepresentation)
            {
                _dictionaryRepresentation = dictionaryRepresentation;
            }
            public void Apply(BsonMemberMap memberMap)
            {
                memberMap.SetSerializer(ConfigureSerializer(memberMap.GetSerializer()));
            }
            private IBsonSerializer ConfigureSerializer(IBsonSerializer serializer)
            {
                var dictionaryRepresentationConfigurable = serializer as IDictionaryRepresentationConfigurable;
                if (dictionaryRepresentationConfigurable != null)
                {
                    serializer = dictionaryRepresentationConfigurable.WithDictionaryRepresentation(_dictionaryRepresentation);
                }

                var childSerializerConfigurable = serializer as IChildSerializerConfigurable;
                return childSerializerConfigurable == null
                    ? serializer
                    : childSerializerConfigurable.WithChildSerializer(ConfigureSerializer(childSerializerConfigurable.ChildSerializer));
            }
        }

        public class DictionaryRepresentationConvention2 : ConventionBase, IMemberMapConvention
        {
            private readonly DictionaryRepresentation _dictionaryRepresentation;

            public DictionaryRepresentationConvention2(DictionaryRepresentation dictionaryRepresentation = DictionaryRepresentation.ArrayOfDocuments)
            {
                // see http://mongodb.github.io/mongo-csharp-driver/2.2/reference/bson/mapping/#dictionary-serialization-options

                _dictionaryRepresentation = dictionaryRepresentation;
            }

            public void Apply(BsonMemberMap memberMap)
            {
                memberMap.SetSerializer(ConfigureSerializer(memberMap.GetSerializer(),Array.Empty<IBsonSerializer>()));
            }

            private IBsonSerializer ConfigureSerializer(IBsonSerializer serializer, IBsonSerializer[] stack)
            {
                if (serializer is IDictionaryRepresentationConfigurable dictionaryRepresentationConfigurable)
                {
                    serializer = dictionaryRepresentationConfigurable.WithDictionaryRepresentation(_dictionaryRepresentation);
                }

                if (serializer is IChildSerializerConfigurable childSerializerConfigurable)
                {
                    if (!stack.Contains(childSerializerConfigurable.ChildSerializer))
                    {
                        var newStack = stack.Union(new[] { serializer }).ToArray();
                        var childConfigured = ConfigureSerializer(childSerializerConfigurable.ChildSerializer, newStack);
                        return childSerializerConfigurable.WithChildSerializer(childConfigured);
                    }
                }

                return serializer;
            }
        }

        [StaticField]
        private static readonly JsonWriterSettings defaultSettings = new() { OutputMode = JsonOutputMode.RelaxedExtendedJson };

        static MongoHelper()
        {
            // 自动注册IgnoreExtraElements

            ConventionPack conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };

            ConventionRegistry.Register("IgnoreExtraElements", conventionPack, type => true);
            //ConventionRegistry.Register("DictionaryRepresentationConvention", new ConventionPack {new DictionaryRepresentationConvention(DictionaryRepresentation.ArrayOfArrays)}, _ => true);

            RegisterStruct<float2>();
            RegisterStruct<float3>();
            RegisterStruct<float4>();
            RegisterStruct<quaternion>();
            RegisterStruct<(string, int)>();
            RegisterStruct<(string, float)>();

        }

        public static void Init()
        {
            Type typeBsonClassMap = typeof(MongoDB.Bson.Serialization.BsonClassMap);

            var __knownTypesQueue = typeBsonClassMap.GetField("__knownTypesQueue", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | BindingFlags.NonPublic);
            var bsonClassMap_dic1 = ((Queue<Type>)__knownTypesQueue.GetValue(null));
            bsonClassMap_dic1.Clear();

            var __classMaps = typeBsonClassMap.GetField("__classMaps", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | BindingFlags.NonPublic);
            var bsonClassMap_dic2 = ((Dictionary<Type, BsonClassMap>)__classMaps.GetValue(null));
            bsonClassMap_dic2.Clear();

            var __freezeNestingLevel = typeBsonClassMap.GetField("__freezeNestingLevel", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | BindingFlags.NonPublic);
            __freezeNestingLevel.SetValue(null, 0);


            ////=====================================================
            Type typeBson = typeof(MongoDB.Bson.Serialization.BsonSerializer);

            var _idGenerators = typeBson.GetField("__idGenerators", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | BindingFlags.NonPublic);
            var dic1 = ((Dictionary<Type, IIdGenerator>)_idGenerators.GetValue(null));
            dic1.Clear();

            var _discriminatorConventions = typeBson.GetField("__discriminatorConventions", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | BindingFlags.NonPublic);
            var dic2 = ((Dictionary<Type, IDiscriminatorConvention>)_discriminatorConventions.GetValue(null));
            dic2.Clear();

            var _discriminators = typeBson.GetField("__discriminators", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | BindingFlags.NonPublic);
            var dic3 = ((Dictionary<BsonValue, HashSet<Type>>)_discriminators.GetValue(null));
            dic3.Clear();

            var _discriminatedTypes = typeBson.GetField("__discriminatedTypes", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | BindingFlags.NonPublic);
            var dic4 = ((HashSet<Type>)_discriminatedTypes.GetValue(null));
            dic4.Clear();

            var _typesWithRegisteredKnownTypes = typeBson.GetField("__typesWithRegisteredKnownTypes", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | BindingFlags.NonPublic);
            var dic5 = ((System.Collections.Concurrent.ConcurrentDictionary<Type, object>)_typesWithRegisteredKnownTypes.GetValue(null));
            dic5.Clear();
            //
            // var staticMethod = typeBson.GetMethod("CreateSerializerRegistry", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | BindingFlags.NonPublic);
            // staticMethod.Invoke(typeBson, null);
            var staticMethod2 = typeBson.GetMethod("RegisterIdGenerators", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | BindingFlags.NonPublic);
            staticMethod2.Invoke(typeBson, null);

            Dictionary<string, Type> types = EventSystem.Instance.GetTypes();
            foreach (Type type in types.Values)
            {
                if (!type.IsSubclassOf(typeof (Object)))
                {
                    continue;
                }

                if (type.IsGenericType)
                {
                    continue;
                }

                BsonClassMap.LookupClassMap(type);
            }
        }

        public static void RegisterStruct<T>() where T : struct
        {
            BsonSerializer.RegisterSerializer(typeof (T), new StructBsonSerialize<T>());
        }

        public static string ToJson(object obj)
        {
            if (obj is ISupportInitialize supportInitialize)
            {
                supportInitialize.BeginInit();
            }
            return obj.ToJson(defaultSettings);
        }

        public static string ToJson(object obj, JsonWriterSettings settings)
        {
            if (obj is ISupportInitialize supportInitialize)
            {
                supportInitialize.BeginInit();
            }
            return obj.ToJson(settings);
        }

        public static T FromJson<T>(string str)
        {
            try
            {
                return BsonSerializer.Deserialize<T>(str);
            }
            catch (Exception e)
            {
                throw new Exception($"{str}\n{e}");
            }
        }

        public static object FromJson(Type type, string str)
        {
            return BsonSerializer.Deserialize(str, type);
        }

        public static byte[] Serialize(object obj)
        {
            if (obj is ISupportInitialize supportInitialize)
            {
                supportInitialize.BeginInit();
            }
            return obj.ToBson();
        }

        public static void Serialize(object message, MemoryStream stream)
        {
            if (message is ISupportInitialize supportInitialize)
            {
                supportInitialize.BeginInit();
            }
            using (BsonBinaryWriter bsonWriter = new BsonBinaryWriter(stream, BsonBinaryWriterSettings.Defaults))
            {
                BsonSerializationContext context = BsonSerializationContext.CreateRoot(bsonWriter);
                BsonSerializationArgs args = default;
                args.NominalType = typeof (object);
                IBsonSerializer serializer = BsonSerializer.LookupSerializer(args.NominalType);
                serializer.Serialize(context, args, message);
            }
        }

        public static object Deserialize(Type type, byte[] bytes)
        {
            try
            {
                return BsonSerializer.Deserialize(bytes, type);
            }
            catch (Exception e)
            {
                throw new Exception($"from bson error: {type.Name}", e);
            }
        }

        public static object Deserialize(Type type, byte[] bytes, int index, int count)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(bytes, index, count))
                {
                    return BsonSerializer.Deserialize(memoryStream, type);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"from bson error: {type.Name}", e);
            }
        }

        public static object Deserialize(Type type, Stream stream)
        {
            try
            {
                return BsonSerializer.Deserialize(stream, type);
            }
            catch (Exception e)
            {
                throw new Exception($"from bson error: {type.Name}", e);
            }
        }

        public static T Deserialize<T>(byte[] bytes)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    return (T)BsonSerializer.Deserialize(memoryStream, typeof (T));
                }
            }
            catch (Exception e)
            {
                throw new Exception($"from bson error: {typeof (T).Name}", e);
            }
        }

        public static T Deserialize<T>(byte[] bytes, int index, int count)
        {
            return (T)Deserialize(typeof (T), bytes, index, count);
        }

        public static T Clone<T>(T t)
        {
            return Deserialize<T>(Serialize(t));
        }
    }
}