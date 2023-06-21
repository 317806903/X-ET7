using System.Collections.Generic;
using System.ComponentModel;
using System.Net;

namespace ET
{
    public partial class StartSceneConfigCategory
    {
        public MultiMap<int, StartSceneConfig> Gates = new ();
        
        public MultiDictionary<int, int, StartSceneConfig> ProcessScenes = new ();
        
        public Dictionary<long, Dictionary<string, StartSceneConfig>> ClientScenesByName = new ();

        public StartSceneConfig LocationConfig;

        public List<StartSceneConfig> Realms = new ();
        
        public List<StartSceneConfig> Routers = new ();
        public StartSceneConfig RouterManager;
        
        public List<StartSceneConfig> Robots = new ();

        public StartSceneConfig BenchmarkServer;
        public StartSceneConfig RoomManager;
        public MultiMap<int, StartSceneConfig> DynamicMaps = new ();
        
        public Dictionary<int, StartSceneConfig> GetByProcess(int process)
        {
            return this.ProcessScenes[process];
        }
        
        public StartSceneConfig GetRoomManager(int zone)
        {
            return this.RoomManager;
        }

        public StartSceneConfig GetDynamicMap(int zone)
        {
            var dynamicMapList = this.DynamicMaps[zone];
            int n = RandomGenerator.RandomNumber(0, dynamicMapList.Count);
            return dynamicMapList[n];
        }

        public StartSceneConfig GetBySceneName(int zone, string name)
        {
            return this.ClientScenesByName[zone][name];
        }

        partial void PostResolve()
        {
            foreach (StartSceneConfig startSceneConfig in this.GetAll().Values)
            {
                this.ProcessScenes.Add(startSceneConfig.Process, startSceneConfig.Id, startSceneConfig);
                
                if (!this.ClientScenesByName.ContainsKey(startSceneConfig.Zone))
                {
                    this.ClientScenesByName.Add(startSceneConfig.Zone, new Dictionary<string, StartSceneConfig>());
                }
                if(startSceneConfig.Type == SceneType.Map && startSceneConfig.Name == "Dynamic")
                {
                    this.DynamicMaps.Add(startSceneConfig.Zone, startSceneConfig);
                }
                else
                {
                    this.ClientScenesByName[startSceneConfig.Zone].Add(startSceneConfig.Name, startSceneConfig);
                }
                
                switch (startSceneConfig.Type)
                {
                    case SceneType.Realm:
                        this.Realms.Add(startSceneConfig);
                        break;
                    case SceneType.Gate:
                        this.Gates.Add(startSceneConfig.Zone, startSceneConfig);
                        break;
                    case SceneType.Location:
                        this.LocationConfig = startSceneConfig;
                        break;
                    case SceneType.Robot:
                        this.Robots.Add(startSceneConfig);
                        break;
                    case SceneType.Router:
                        this.Routers.Add(startSceneConfig);
                        break;
                    case SceneType.RouterManager:
                        this.RouterManager = startSceneConfig;
                        break;
                    case SceneType.BenchmarkServer:
                        this.BenchmarkServer = startSceneConfig;
                        break;
                    case SceneType.Room:
                        this.RoomManager = startSceneConfig;
                        break;
                    case SceneType.Match:
                        break;
                    case SceneType.Map:
                        break;
                }
            }
        }
    }
    
    public partial class StartSceneConfig
    {
        public long InstanceId;
        
        public SceneType Type;

        public StartProcessConfig StartProcessConfig
        {
            get
            {
                return StartProcessConfigCategory.Instance.Get(this.Process);
            }
        }
        
        public StartZoneConfig StartZoneConfig
        {
            get
            {
                return StartZoneConfigCategory.Instance.Get(this.Zone);
            }
        }

        // 内网地址外网端口，通过防火墙映射端口过来
        private IPEndPoint innerIPOutPort;

        public IPEndPoint InnerIPOutPort
        {
            get
            {
                if (innerIPOutPort == null)
                {
                    this.innerIPOutPort = NetworkHelper.ToIPEndPoint($"{this.StartProcessConfig.InnerIP}:{this.OuterPort}");
                }

                return this.innerIPOutPort;
            }
        }

        private IPEndPoint outerIPPort;

        // 外网地址外网端口
        public IPEndPoint OuterIPPort
        {
            get
            {
                if (this.outerIPPort == null)
                {
                    this.outerIPPort = NetworkHelper.ToIPEndPoint($"{this.StartProcessConfig.OuterIP}:{this.OuterPort}");
                }

                return this.outerIPPort;
            }
        }

        partial void PostResolve()
        {
            this.Type = EnumHelper.FromString<SceneType>(this.SceneType);
            InstanceIdStruct instanceIdStruct = new InstanceIdStruct(this.Process, (uint) this.Id);
            this.InstanceId = instanceIdStruct.ToLong();
        }
    }
}