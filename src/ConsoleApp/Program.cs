using System;
using System.Collections.Immutable;
using Akka.Actor;
using Akka.Actor.Setup;
using Akka.Configuration;
using Akka.Serialization;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var Bootstrap = BootstrapSetup.Create().WithConfig(ConfigurationFactory.ParseString(@"
akka
{
    actor
    {
        serialize-messages = off
    }
}"));

            var ActorSystemSettings = ActorSystemSetup.Create(SerializationSettings, Bootstrap);


        }

        public static SerializationSetup SerializationSettings =>
            SerializationSetup.Create(actorSystem =>
                ImmutableHashSet<SerializerDetails>.Empty.Add(
                    SerializerDetails.Create("app-protocol",
                        new AppProtocolSerializer(actorSystem),
                        ImmutableHashSet<Type>.Empty.Add(typeof(IAppProtocol)))));
    }
}
