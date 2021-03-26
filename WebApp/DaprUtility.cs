using Dapr.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp
{
    public class DaprUtility
    {

        public static async Task Save(DaprClient _dapr, string stateStore, User _user)
        {
            //writing the value in redis
            var state = await _dapr.GetStateEntryAsync<string>(stateStore, _user.Key);
            state.Value = _user.Value;
            await state.SaveAsync();

            //reading the value from redis
            var data = await _dapr.GetStateAsync<string>("Azurestatestore", _user.Key);
            Console.WriteLine(data);
        }
    }
}
