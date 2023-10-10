---
title: 轻量通讯协议 --- MQTT
category: 通讯
tags: MQTT
updatedAt: 2023-10-10T01:40:08.791Z
createdAt: 2023-10-09T15:34:58.363Z
---

##  介绍

**MQTT（Message Queuing Telemetry Transport）** 是一种轻量级的消息传输协议，通常用于在物联网（IoT）和传感器网络中进行通信。它设计用于在低带宽、不稳定或高延迟的网络环境下传输数据，因此非常适用于连接设备之间的通信，尤其是在资源有限的环境中。

<!-- more -->

### 一、MQTT简介

**MQTT（Message Queuing Telemetry Transport）** 是一种轻量级的消息传输协议，通常用于在物联网（IoT）和传感器网络中进行通信。它设计用于在低带宽、不稳定或高延迟的网络环境下传输数据，因此非常适用于连接设备之间的通信，尤其是在资源有限的环境中。

MQTT 的主要特点包括以下几点：

1. 轻量级：MQTT 协议本身非常简洁，消息头部占用较少的带宽，使其在低带宽网络中运行效率高。

2. 发布/订阅模型：MQTT 使用发布/订阅模型，其中客户端可以订阅特定的主题（Topic），并接收与该主题相关的消息。发布者发布消息到特定主题，然后所有订阅了该主题的客户端都将收到该消息。

3. 可靠性：MQTT 支持三种不同级别的消息传输质量，包括最多一次、至少一次和仅一次传输，可根据应用需求选择合适的级别。

4. 持久会话：MQTT 允许客户端建立持久会话，以便在连接丢失后重新连接时能够恢复之前的订阅和消息传递状态。

5. QoS（Quality of Service）：MQTT 提供不同的 QoS 级别，以确保消息的可靠传递。这包括 QoS 0（最多一次传输）、QoS 1（至少一次传输）和 QoS 2（仅一次传输）。

6. 适应性：MQTT 可以在多种网络协议上运行，包括 TCP/IP、WebSocket 和其他协议。

总之，MQTT 是一种非常适合物联网和传感器网络的通信协议，因其轻量级和高效的特性而受到广泛应用。它允许设备之间实时地交换信息，从而支持各种应用，包括智能家居、工业自动化、农业监测等。

### 二、MQTT 的 QoS 机制

什么是 QoS 机制？（https://www.emqx.com/zh/blog/introduction-to-mqtt-qos）

很多时候，使用 MQTT 协议的设备都运行在网络受限的环境下，而只依靠底层的 TCP 传输协议，并不能完全保证消息的可靠到达。因此，MQTT 提供了 QoS 机制，其核心是设计了多种消息交互机制来提供不同的服务质量，来满足用户在各种场景下对消息可靠性的要求。

MQTT 定义了三个 QoS 等级，分别为：

- QoS 0，最多交付一次。
- QoS 1，至少交付一次。
- QoS 2，只交付一次。

其中，使用 QoS 0 可能丢失消息，使用 QoS 1 可以保证收到消息，但消息可能重复，使用 QoS 2 可以保证消息既不丢失也不重复。QoS 等级从低到高，不仅意味着消息可靠性的提升，也意味着传输复杂程度的提升。

## MQTT 的.Net 库 --- MQTTnet

MQTTnet是一个开源的用于基于MQTT的通信的高性能.NET库。它提供了一个MQTT客户端和一个MQTT服务器（代理），并支持MQTT协议，直到版本5。它与大多数受支持的.NET兼容框架版本和CPU体系结构。

Guthub地址： https://github.com/dotnet/MQTTnet

MQTTnet通过NuGet软件包管理器交付。可以在这里找到软件包：https://www.nuget.org/packages/MQTTnet/

在Visual Studio中，在Package Manager控制台中使用以下命令手动安装MQTTnet:

```shell
Install-Package MQTTnet
```

可以通过GitHub上直接查看Demo源码，或者下载源码后使用Visual Studio打开，它提供了多个`Samples` ，每个`Samples`下有不同的相关方法，有以下几类：

- Client_Connection_Samples ---
- Client_Publish_Samples
- Client_Subscribe_Samples
- Logger_Samples
- Managed_Client_Simple_Samples
- PackageInspection_Samples
- RpcClient_Samples
- Server_ASP_NET_Samples
- Server_Diagnostics_Samples
- Server_Intercepting_Samples
- Server_Retained_Messages_Samples
- Server_Simple_Samples
- Server_TLS_Samples

可以下载源码编译，运行起来后如下：


![image.png](https://niuery.com/static/img/9470f3089ef3578d205a0b82a0ba7ea7.image.png)

## Windows下MQTT消息服务器的安装使用

一般，常见的MQTT服务器软件有：

- **Mosquitto** - 流行的开源MQTT服务器，但是没有可视化界面，需要借助其他工具才可以可视化。

- **EMQX** - 强大的开源MQTT服务器，有可视化界面。

- **HiveMQ**  - HiveMQ 是一个商业的MQTT服务器，提供免费的开发者版。

这里推荐使用EMQX ，它提供了可视化界面，以便更容易地配置、管理和监控MQTT服务器。

### 一、下载EMQX

EMQX 官网提供了丰富的文档，Quick Start 地址：https://www.emqx.io/docs/zh/v5.2/

这里不建议安装最新版本，建议降低版本，若安装最新版本 emqx-5.3.0-windows-amdx64，则会启动异常，如下所示：


![image.png](https://niuery.com/static/img/942aabe7d0f36e4de5fdc40513b5283d.image.png)
本次测试使用 [emqx-4.4.19-otp24.3.4.6-windows-amd64](https://www.emqx.com/en/downloads/broker/4.4.19/emqx-4.4.19-otp24.3.4.6-windows-amd64.zip) 版本，如下：


![image.png](https://niuery.com/static/img/c3e927d346b91526e900a15fdee6f230.image.png)

按照官网教程，进入到安装目录/emqx/bin 下，使用以下指令启动EMQX ：

```shell
emqx start
```

### 二、启动EMQX 服务

EMQX 常用启动命令：

| **命令**   | **描述**                                                     |
| ---------- | ------------------------------------------------------------ |
| start      | 以守护进程模式启动 EMQX，运行期间不需要交互式 shell。        |
| console    | 在 Erlang 或 Elixir 交互式 shell 中启动 EMQX。用于在开发环境中调试 EMQX，需要与 EMQX 进行交互。 |
| foreground | 在前台模式下启动 EMQX，不使用交互式 shell。用于在开发环境中启动 EMQX，但不需要后台运行。 |
| stop       | 停止运行中的 EMQX 节点。                                     |
| ctl        | 管理和监控 EMQX，执行 `emqx ctl help` 可以获取更多详细信息。 |

EMQX 常用ctl命令：

| **命令**      | **描述**                                                     |
| ------------- | ------------------------------------------------------------ |
| status        | 快速查看当前运行的节点是否运行。                             |
| broker        | 查看当前节点的运行的版本状态以及运行时长。                   |
| observer      | 可以用于查看运行时状态。展示一个类似于 linux 的 `top` 命令的界面。 |
| admins        | 用于创建、修改、删除管理员账户。                             |
| clients       | 查看和管理客户端。                                           |
| topics        | 查看当前系统中所有订阅的主题。                               |
| subscriptions | 查看、增加或者删除某个客户端的订阅。                         |

### 三、EMQX Dashboard

EMQX Dashboard 是EMQX内置的Web 应用程序，它支持查看运行中的 EMQX 集群的整体连接数，订阅主题数，消息收发数量和流入流出速率，还包括节点列表和节点信息和一些系统指标信息，同时也可以对一些客户端连接与订阅数据进行查看与管理`。`

如果 EMQ 安装在本机，则使用浏览器打开地址 [http://127.0.0.1:18083](http://127.0.0.1:18083/) ，输入默认用户名 `admin` 与默认密码 `public` ，登录进入 Dashboard，如下图：


![image.png](https://niuery.com/static/img/161d2c912bf44eca16619dbea6c0405f.image.png)


如果忘记了 Dashboard 登录密码，可以通过 cli 的 `admins` 命令进行重置，详情请参考 [命令行 - admins](https://www.emqx.io/docs/zh/v5.2/admin/cli.html#admins)：

```sh
./bin/emqx ctl admins passwd <Username> <Password>
```

###  四、MQTTX Desktop

[MQTTX 客户端](https://www.emqx.io/docs/zh/v5.2/messaging/publish-and-subscribe.html#mqttx)是一款跨平台的 MQTT 桌面客户端工具。它提供用户友好的图形界面，让用户可以快速创建、测试 MQTT 连接，并进行MQTT 消息的发布和订阅。下载地址：https://mqttx.app/zh/downloads 界面如下图：


![image.png](https://niuery.com/static/img/dca88db90c49aa3861f011c8873f9f43.image.png)


## 客户端代码编写

### 一、准备工作

接下来 我们使用MQTTnet，编写服务端和客户端测试一下：

1. 新建控制台项目，添加MQTTnet库。

2. 按照上文中命令启动EMQX服务

3. 使用MQTTX Desktop，设置 **host** 为`localhost` ，**prot** 为 `1883` ，连接服务，如下图：


![image.png](https://niuery.com/static/img/7900ba06c1349217b26ce475ff4e8588.image.png)

### 二、代码编写

这样准备工作就做好了，编写创建发布客户端代码，如下：

```c#
public static async Task CreatePublishMQTTClient()
{
    try
    {
        MqttFactory mqttFactory = new MqttFactory();

        var mqttClient = mqttFactory.CreateMqttClient();

        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("localhost", 1883)
            .WithClientId("Client1")
            .Build();
        var connectResult = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        Console.WriteLine("mqttClient connectResult: " + connectResult.ResultCode.ToString());

        while (true)
        {
            var msg = Console.ReadLine();

            string topic = "testtopic/publish";
            string payload = $"{msg} {DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}"; // 消息内容

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce) // 设置消息质量
                .WithRetainFlag(false) // 是否保留消息
                .Build();

            await mqttClient.PublishAsync(message, CancellationToken.None);
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}

```

接下来再编写一个订阅客户端代码：

```c#
public static async Task CreateSubscribeMQTTClient()
{
    try
    {
        MqttFactory mqttFactory = new MqttFactory();

        var mqttClient = mqttFactory.CreateMqttClient();

        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("localhost", 1883)
            .WithClientId("Client1")
            .Build();


        mqttClient.ApplicationMessageReceivedAsync += (e) =>
        {
            Task task = Task.Factory.StartNew(() =>
            {
                var msgArray = e.ApplicationMessage.Payload;
                string result = Encoding.UTF8.GetString(msgArray);
                Console.WriteLine("Received: " + result);
            });

            return task;
        };

        var connectResult = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        Console.WriteLine("mqttClient connectResult: " + connectResult.ResultCode.ToString());

        string topic = "testtopic/subscribe";

        var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
            .WithTopicFilter(topic)
            .Build();

        await mqttClient.SubscribeAsync(subscribeOptions);

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}

```

接下来编写控制台Main方法，由于MQTT Client方法是异步的，所以为了避免控制台退出，在调用方法后，增加了一个`While` 死循环保证控制台程序是激活状态，代码如下：

``` c#
static void Main(string[] args)
{
    Console.WriteLine("Choose a creation type: \r\n 1: PublishClient\r\n 2: SubscribeClient");
    var type = Console.ReadLine();
    switch (type)
    {
        case "1":
            _ = CreatePublishMQTTClient();
            break;
        case "2":
            _ = CreateSubscribeMQTTClient();
            break;

    }
    while (true) Thread.Sleep(1000);
}

```

### 三、测试

先测试发布客户端，在控制台选择PublishClient，然后等待连接，可以看到连接结果为Success，发送两条测试消息，可以看到MQTTX Desktop 均收到。


![image.png](https://niuery.com/static/img/7c3a7884b0732e2aae5e871c2ae6f480.image.png)

接下来测试订阅客户端，在控制台选择SubscribeClient，然后等待连接，可以看到连接结果为Success，在MQTTX Desktop 发布一条消息给订阅客户端，可以看到控制台程序中，接收到了测试消息。


![image.png](https://niuery.com/static/img/4536c15b406035365da3c60e88cff917.image.png)
## 总结

总的来说， 使用C#编写 MQTT相关代码的资料还是比较少的，但好在官方文档足够详细，今天试玩一下还是花费不少功夫的。本篇文章作抛砖引玉，浅浅了解MQTT这个轻量级的通讯协议，在辅以Demo加深理解，熟悉如何使用，文章末尾也提供诸多参考文章，方便大家借鉴学习。



> 参考链接
>
> MQTT 入门指南：https://www.emqx.com/zh/mqtt-guide
>
> EMQX 官方文档：https://www.emqx.io/docs/zh/v5.2/
>
> EMQX 命令行文档：https://www.emqx.io/docs/zh/v5.2/admin/cli.html
>
> EMQX 配置手册：https://www.emqx.io/docs/zh/v5.2/configuration/configuration-manual.html
>
> EMQX基础功能： https://juejin.cn/post/7081629128650129416
> 
> MQTTX 客户端下载：https://mqttx.app/zh/downloads

