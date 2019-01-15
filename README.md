--------------------------------------DNLiCore框架-----------------------------------------  
愿景:打造一款即装即用的快速开发框架，更少的耦合更多的功能更高效的开发效率  
--------------------------------------介绍说明---------------------------------------------  
DNLiCore_Cache是基于.NetCore下的一个缓存帮助工具,对应的缓存包括Cache,Redis 
  
--------------------------------------Redis使用说明---------------------------------------------
1.通过Nuget安装DNLiCore_Cache工具类
2.在appsettings.json配置redis字符串
  "Redis": {
    "InstanceName": "Redis",
    "ConnectionString": "*****:6379,defaultDatabase=4" //redis连接地址
  }
3.在Startup.cs里面注入
    services.AddSingleton(typeof(DNLiCore_Cache_Redis.IRedisHelper), new DNLiCore_Cache_Redis.RedisHelper(new RedisCacheOptions()
            {
                Configuration = Configuration.GetSection("Redis:ConnectionString").Value,
                InstanceName = Configuration.GetSection("Redis:Redis").Value
            }));
4.在需要引用的地方进行构造注入引用，或者安装DNLiCore_DI框架可以在任意地方调用，例如:
DNLiCore_DI.ServiceContext.GetService<DNLiCore_Cache_Redis.IRedisHelper>().Set();//增加缓存


--------------------------------------MemoryCache使用说明---------------------------------------------
1.在Startup.cs里面注入
  services.AddMemoryCache();
2.在需要引用的地方进行构造注入引用，或者安装DNLiCore_DI框架可以在任意地方调用，例如:
DNLiCore_DI.ServiceContext.GetService<IMemoryCache>().Set();//增加缓存
