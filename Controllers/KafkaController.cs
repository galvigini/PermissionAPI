﻿using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using PermissionsAPI.Models;

namespace ApacheKafkaProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly string
        bootstrapServers = "localhost:9092";
        private readonly string topic = "test";

        [HttpPost]
        public async Task<IActionResult>
        Post([FromBody] Permisos PermissionReuqest)
        {
            string message = JsonSerializer.Serialize(PermissionReuqest);
            return Ok(await SendPermissionRequest(topic, message));
        }
        private async Task<bool> SendPermissionRequest
        (string topic, string message)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            };

            try
            {
                using (var producer = new ProducerBuilder
                <Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync
                    (topic, new Message<Null, string>
                    {
                        Value = message
                    });

                return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            return await Task.FromResult(false);
        }
    }
}
