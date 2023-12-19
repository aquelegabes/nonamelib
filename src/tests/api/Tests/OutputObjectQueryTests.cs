namespace NoNameLib.Application.Tests
{
    public class OutputObjectQueryTests
    {
        private readonly OutputObjectQueryHandler QueryHandler = new();

        [Fact]
        public void Query_Get_OK()
        {
            var handler = QueryHandler as IQuery<OutputObject>;

            Assert.NotNull(handler);

            var output = handler.Get();

            Assert.NotNull(output);
            Assert.NotEmpty(output);
        }

        [Fact]
        public async Task AsyncQuery_Get_OK()
        {
            var handler = QueryHandler as IAsyncQuery<OutputObject>;

            Assert.NotNull(handler);

            var output = await handler.Get();

            Assert.NotNull(output);
            Assert.NotEmpty(output);
        }

        [Fact]
        public void QueryFiltered_Get_OK()
        {
            var handler = QueryHandler as IQueryFiltered<OutputObject, OutputObjectQueryFilter>;
            var filters = new OutputObjectQueryFilter();

            Assert.NotNull(handler);

            var output = handler.Get(filters);

            Assert.NotNull(output);
            Assert.NotEmpty(output);
        }

        [Fact]
        public async Task AsyncQueryFiltered_Get_OK()
        {
            var handler = QueryHandler as IAsyncQueryFiltered<OutputObject, OutputObjectQueryFilter>;
            var filters = new OutputObjectQueryFilter();

            Assert.NotNull(handler);

            var output = await handler.Get(filters);

            Assert.NotNull(output);
            Assert.NotEmpty(output);
        }
    }
}
