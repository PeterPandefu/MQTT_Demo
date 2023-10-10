using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MQTT_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IMqttClient c1;
        MqttClientOptions o1;
        IMqttClient c2;
        MqttClientOptions o2;
        MqttFactory mqttFactory = new MqttFactory();
        public MainWindow()
        {
            InitializeComponent();
        }

        private Tuple<IMqttClient, MqttClientOptions> CreateMQTTClient(string host, int prot, string clientId)
        {
            var mqttClient = mqttFactory.CreateMqttClient();

            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(host, prot)
                .WithClientId(clientId)
                .Build();

            return new Tuple<IMqttClient, MqttClientOptions>(mqttClient, mqttClientOptions);
        }

        private void Init_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                initBtn.IsEnabled = false;

                var tuple1 = CreateMQTTClient(hostTbx.Text, int.Parse(protTbx.Text), "client1");
                c1 = tuple1.Item1;
                o1 = tuple1.Item2;

                var tuple2 = CreateMQTTClient(hostTbx.Text, int.Parse(protTbx.Text), "client2");
                c2 = tuple2.Item1;
                o2 = tuple2.Item2;
            }
            catch (Exception ex)
            {
                RefresTipText(ex.Message);
            }

        }



        private async void Button_Create(object sender, RoutedEventArgs e)
        {
            try
            {
                createPublishBtn.IsEnabled = false;
                sendPublishBtn.IsEnabled = true;
                c1.DisconnectedAsync += (e) =>
                {

                    Task task = Task.Factory.StartNew(() =>
                    {

                        Dispatcher.Invoke(() =>
                        {
                            var connectResult = e.ConnectResult.ResultCode.ToString();
                            RefresTipText($"c1 connectResult: {connectResult}");
                        });
                    });

                    return task;
                };

                var connectResult = await c1.ConnectAsync(o1, CancellationToken.None);
                RefresTipText("c1 connectResult:" + connectResult.ResultCode.ToString());
            }
            catch (Exception ex)
            {
                RefresTipText(ex.Message);
            }
        }

        private async void Button_Send(object sender, RoutedEventArgs e)
        {
            try
            {

                string topic = "testtopic/a";
                string payload = $"{msgTbx.Text} {DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}"; // 消息内容

                var message = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(payload)
                    .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce) // 设置消息质量
                    .WithRetainFlag(false) // 是否保留消息
                    .Build();

                await c1.PublishAsync(message, CancellationToken.None);

            }
            catch (Exception ex)
            {
                RefresTipText(ex.Message);
            }

        }

        private async void Button_Subscribe_Create(object sender, RoutedEventArgs e)
        {
            try
            {
                subscribeCreateBtn.IsEnabled = false;
                c2.ApplicationMessageReceivedAsync += (e) =>
                {

                    Task task = Task.Factory.StartNew(() =>
                    {

                        Dispatcher.Invoke(() =>
                        {
                            var msgArray = e.ApplicationMessage.Payload;
                            string result = Encoding.UTF8.GetString(msgArray);
                            msgTbx2.AppendText(result + "\r\n");
                        });
                    });

                    return task;
                };

                var connectResult = await c2.ConnectAsync(o2, CancellationToken.None);

                RefresTipText("c2 connectResult:" + connectResult.ResultCode.ToString());

                string topic = "testtopic/a";

                var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter(topic)
                    .Build();

                await c2.SubscribeAsync(subscribeOptions);
            }
            catch (Exception ex)
            {
                RefresTipText(ex.Message);
            }

        }

        void RefresTipText(string tip)
        {
            Dispatcher.Invoke(() =>
            {
                TipText.Text = $"提示：{tip} {DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}";
            });
        }



        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            msgTbx.Clear();
        }

        private void Clear_Right_Click(object sender, RoutedEventArgs e)
        {
            msgTbx2.Clear();
        }


    }
}