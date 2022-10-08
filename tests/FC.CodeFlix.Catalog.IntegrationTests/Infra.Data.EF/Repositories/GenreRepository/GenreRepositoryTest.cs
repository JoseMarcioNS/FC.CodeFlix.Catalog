using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.GenreRepository
{
    [CollectionDefinition(nameof(GenreRepositoryTestFixture))]
    public class GenreRepositoryTest
    {
        private readonly GenreRepositoryTestFixture _fixture;

        public GenreRepositoryTest(GenreRepositoryTestFixture fixture) 
            => _fixture = fixture;

        //[Fact(DisplayName = nameof(Insert))]
        //[Trait("Integration/Infra.Data", "GenreRepositoryRepository - Repositories")]
        //public async Task Insert()
        //{
        //   // var genre = _fixture.Genre();
        //}
    }
}
