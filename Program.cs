using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Json.Schema;

namespace JsonEverythingLoop
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var metaSchemaId = new Uri("https://myserver.net/meta-schema");

            var vocabId = "https://myserver.net/my-vocab";

            var metaSchema = await JsonSchema.FromStream(GetResource("MetaSchema"));

            var schema = await JsonSchema.FromStream(GetResource("Schema"));

            SchemaKeywordRegistry.Register<MinDateKeyword>();

            VocabularyRegistry.Global.Register(new Vocabulary(vocabId, typeof(MinDateKeyword)));
            SchemaRegistry.Global.Register(metaSchemaId, metaSchema);

            var result = await JsonSerializer.DeserializeAsync<JsonElement>(GetResource("Data"));

            schema.Validate(result);
        }

        private static Stream GetResource(string name) => typeof(Program).Assembly.GetManifestResourceStream($"JsonEverythingLoop.{name}.json");

        [SchemaKeyword(Name)]
        private class MinDateKeyword : IJsonSchemaKeyword, IEquatable<MinDateKeyword>
        {
            private const string Name = "minDate";

            public bool Equals(MinDateKeyword other)
            {
                throw new NotImplementedException();
            }

            public void Validate(ValidationContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}
